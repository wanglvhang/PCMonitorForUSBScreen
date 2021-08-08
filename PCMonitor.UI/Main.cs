using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCMonitor.UI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            synchronizationContext = SynchronizationContext.Current;
        }



        private RenderLauncher renderLauncher;
        private SynchronizationContext synchronizationContext;
        private RenderStopSignal signal = new RenderStopSignal() { Stop = false };
        private string appConfig_path;
        private AppConfig appConfig;
        private string themeConfig_path;
        private ThemeConfig themeConfig;




        private void Main_Load(object sender, EventArgs e)
        {
            //初始化UI内容以及读取配置
            var exe_path = typeof(Main).Assembly.Location;
            var working_dir = Path.GetDirectoryName(exe_path);
            this.appConfig_path = $"{working_dir}\\app.json";

            this.appConfig = JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText(this.appConfig_path));

            this.themeConfig_path = $"{working_dir}\\themes\\{this.appConfig.Theme}\\config.json";
            //var bg_path = $"{working_dir}\\themes\\{this.appConfig.Theme}\\bg.png";

            //this.themeConfig = JsonConvert.DeserializeObject<ThemeConfig>(File.ReadAllText(this.themeConfig_path));

            //网卡
            var network_interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach(var ni in network_interfaces)
            {
                this.cmbNetInterfaces.Items.Add(ni.Name);
                if(ni.Name == this.appConfig.NetworkInterface)
                {
                    this.cmbNetInterfaces.SelectedItem = ni.Name;
                }
            }

            //cpu风扇
            var fans = this.GetSuperIOFanSensors();

            foreach(var f in fans)
            {
                this.cmbCPUFans.Items.Add(f.Name);
            }

            this.cmbCPUFans.SelectedIndex = 0;

            //start date
            var startData = Convert.ToDateTime(this.appConfig.StartDate);
            this.dtpStartDate.Value = startData;

            //frame time
            this.cmbFrameTime.SelectedIndex = 2;


            //themes


            //初始化完成后挂载时间
            this.cmbNetInterfaces.SelectedIndexChanged += CmbNetInterfaces_SelectedIndexChanged;
            this.cmbCPUFans.SelectedIndexChanged += CmbFans_SelectedIndexChanged;
            this.cmbFrameTime.SelectedIndexChanged += CmbFrameTime_SelectedIndexChanged;
            this.dtpStartDate.ValueChanged += DtpStartDate_ValueChanged;
            this.cmbTheme.SelectedIndexChanged += CmbTheme_SelectedIndexChanged;

        }




        private void CmbTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.appConfig.Theme = this.cmbTheme.SelectedItem.ToString();
            saveAppConfig();
        }

        private void DtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            this.appConfig.StartDate = this.dtpStartDate.Value.ToShortDateString();
            saveAppConfig();
        }

        private void CmbFrameTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.appConfig.FrameTime = Convert.ToInt32(this.cmbFrameTime.SelectedItem.ToString());
            saveAppConfig();
        }

        private void CmbFans_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.appConfig.CPUFanIndex = this.cmbCPUFans.SelectedIndex;
            saveAppConfig();
        }

        private void CmbNetInterfaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.appConfig.NetworkInterface = this.cmbNetInterfaces.SelectedItem.ToString();
            saveAppConfig();
        }

        private void ckbScreenProtect_CheckedChanged(object sender, EventArgs e)
        {
            this.appConfig.ScreenProtect = this.ckbScreenProtect.Checked;
            saveAppConfig();
        }

        private void ckbAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            this.appConfig.IsAutoStart = this.ckbAutoStart.Checked;
            saveAppConfig();
        }






        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
                return;
            this.ShowInTaskbar = false;
            this.NotifyIcon.Visible = true;
            this.Visible = false;
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.ShowInTaskbar = true;
            }
            //this.CenterToScreen();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.renderLauncher = new RenderLauncher();

            this.signal.Stop = false;

            var thread = new Thread(ThreadProcSafePost);

            thread.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.signal.Stop = true;
        }








        private IEnumerable<ISensor> GetSuperIOFanSensors()
        {
            IEnumerable<ISensor> result = null;

            var comp = new Computer();
            comp.MainboardEnabled = true;
            var hv = new UpdateVisitor();
            comp.Accept(hv);
            comp.Open();
            var mainboard = comp.Hardware.Where(x => x.HardwareType == HardwareType.Mainboard).First();
            mainboard.Update();
            var superIO = mainboard.SubHardware.Where(x => x.HardwareType == HardwareType.SuperIO).First();
            superIO.Update();
            result = superIO.Sensors.Where(s => s.SensorType == SensorType.Fan).ToList();
            comp.Close();

            return result;

        }

        public void ThreadProcSafePost()
        {
            this.renderLauncher.Run((count, ms) =>
            {

                synchronizationContext.Post(updateUI, new UIUpdateData() { Count = count, MS = ms });

            }, signal);

        }

        private void updateUI(object updateData)
        {
            var data = updateData as UIUpdateData;
            this.labFrameCount.Text = $"{data.Count}";
            this.labRenderTime.Text = $"{data.MS}";
        }

        private void saveAppConfig()
        {
            File.WriteAllText(this.appConfig_path, JsonConvert.SerializeObject(this.appConfig,
                Formatting.Indented,
                new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() })
                );
        }

        private void saveThemeConfig()
        {
            File.WriteAllText(this.themeConfig_path, JsonConvert.SerializeObject(this.themeConfig));
        }

        private void disableUI()
        {
            this.cmbFrameTime.Enabled = false;
            this.cmbCPUFans.Enabled = false;
            this.cmbNetInterfaces.Enabled = false;
            this.cmbTheme.Enabled = false;
            this.dtpStartDate.Enabled = false;
            this.ckbAutoStart.Enabled = false;
            this.ckbScreenProtect.Enabled = false;
        }

        private void enableUI()
        {
            this.cmbFrameTime.Enabled = true;
            this.cmbCPUFans.Enabled = true;
            this.cmbNetInterfaces.Enabled = true;
            this.cmbTheme.Enabled = true;
            this.dtpStartDate.Enabled = true;
            this.ckbAutoStart.Enabled = true;
            this.ckbScreenProtect.Enabled = true;
        }

    }


    public class UIUpdateData
    {
        public int Count { get; set; }

        public double MS { get; set; }
    }


}
