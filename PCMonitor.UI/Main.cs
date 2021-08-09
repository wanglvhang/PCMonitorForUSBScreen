using Microsoft.Win32.TaskScheduler;
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
        public Main(bool autoStart = false)
        {
            InitializeComponent();
            synchronizationContext = SynchronizationContext.Current;
            this.btnStop.Enabled = false;
            this.isAutoStart = autoStart;
        }



        private RenderLauncher renderLauncher;
        private SynchronizationContext synchronizationContext;
        private RenderStopSignal signal = new RenderStopSignal() { Stop = false };
        private string appConfig_path;
        private AppConfig appConfig;
        private ThemeConfig themeConfig;
        private string themeFolder_path;
        private Thread workThread;
        private string work_dir;
        private bool isAutoStart;
        private readonly string task_name = "PCMonitor_Schedule";




        private void Main_Load(object sender, EventArgs e)
        {
            //初始化UI内容以及读取配置
            var exe_path = typeof(Main).Assembly.Location;
            this.work_dir = Path.GetDirectoryName(exe_path);
            var themes_path = Path.Combine(this.work_dir, "themes");
            this.appConfig_path = $"{ this.work_dir}\\app.json";
            this.appConfig = JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText(this.appConfig_path));

            var theme_json_path = $"{this.work_dir}\\themes\\{this.appConfig.Theme}\\config.json";
            this.themeConfig = JsonConvert.DeserializeObject<ThemeConfig>(File.ReadAllText(theme_json_path));


            //网卡
            var network_interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var ni in network_interfaces)
            {
                this.cmbNetInterfaces.Items.Add(ni.Name);
                if (ni.Name == this.appConfig.NetworkInterface)
                {
                    this.cmbNetInterfaces.SelectedItem = ni.Name;
                }
            }

            //cpu风扇
            var fans = this.GetSuperIOFanSensors();

            foreach (var f in fans)
            {
                this.cmbCPUFans.Items.Add(f.Name);
            }

            this.cmbCPUFans.SelectedIndex = 0;

            //start date
            var startData = Convert.ToDateTime(this.appConfig.StartDate);
            this.dtpStartDate.Value = startData;

            //frame time
            foreach (var i in this.cmbFrameTime.Items)
            {
                if (i.ToString() == this.appConfig.FrameTime.ToString())
                {
                    this.cmbFrameTime.SelectedItem = i;
                }
            }


            //themes,get all folder under theme folder
            //var folders = Directory.GetDirectories(themes_path);
            var directory = new DirectoryInfo(themes_path);
            var theme_folders = directory.GetDirectories();
            foreach (var di in theme_folders)
            {
                this.cmbThemes.Items.Add(di.Name);
                if (di.Name.ToLower() == this.appConfig.Theme)
                {
                    this.cmbThemes.SelectedItem = di.Name;
                    this.themeFolder_path = $"{this.work_dir}\\themes\\{this.appConfig.Theme}";
                    this.labDevice.Text = this.themeConfig.device;
                    this.labWidgetCount.Text = this.themeConfig.Widgets.Count.ToString();
                }
            }

            //isAutoStart
            using (TaskService ts = new TaskService())
            {
                var task = ts.FindTask(this.task_name);

                if (task != null)
                {
                    this.ckbAutoStart.Checked = true;
                }
                else
                {
                    this.ckbAutoStart.Checked = false;
                }
            }

            //screenprotect
            this.ckbScreenProtect.Checked = this.appConfig.ScreenProtect;
            this.ckbScreenProtect.Enabled = false;

            //初始化完成后挂载时间
            this.cmbNetInterfaces.SelectedIndexChanged += CmbNetInterfaces_SelectedIndexChanged;
            this.cmbCPUFans.SelectedIndexChanged += CmbFans_SelectedIndexChanged;
            this.cmbFrameTime.SelectedIndexChanged += CmbFrameTime_SelectedIndexChanged;
            this.dtpStartDate.ValueChanged += DtpStartDate_ValueChanged;
            this.cmbThemes.SelectedIndexChanged += CmbThemes_SelectedIndexChanged;

            this.ckbAutoStart.CheckedChanged += CkbAutoStart_CheckedChanged;
            this.ckbScreenProtect.CheckedChanged += CkbScreenProtect_CheckedChanged;


            //自动运行
            if (this.isAutoStart)
            {
                this.WindowState = FormWindowState.Minimized;
                //启动
                btnStart_Click(this, new EventArgs());
            }

        }

        private void CkbScreenProtect_CheckedChanged(object sender, EventArgs e)
        {
            this.appConfig.ScreenProtect = this.ckbScreenProtect.Checked;
            saveAppConfig();
        }

        private void CkbAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            setupTaskScheduleOnLogon(this.ckbAutoStart.Checked);

        }

        private void CmbThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.appConfig.Theme = this.cmbThemes.SelectedItem.ToString();
            this.themeFolder_path = $"{this.work_dir}\\themes\\{this.appConfig.Theme}\\config.json";
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
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            disableUI();

            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;

            this.renderLauncher = new RenderLauncher(this.appConfig, this.themeFolder_path,this.themeConfig);
            this.renderLauncher.Initial();

            this.signal.Stop = false;

            this.workThread = new Thread(ThreadProcSafePost);

            this.workThread.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.signal.Stop = true;
            //等待让thread中的任务完成一次循环
            Thread.Sleep(50);

            this.btnStop.Enabled = false;
            this.btnStart.Enabled = true;

            this.enableUI();

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.WindowsShutDown:
                case CloseReason.MdiFormClosing:
                case CloseReason.UserClosing:
                case CloseReason.TaskManagerClosing:
                case CloseReason.FormOwnerClosing:
                case CloseReason.ApplicationExitCall:
                    this.Hide();
                    if (this.renderLauncher != null)
                    {
                        this.signal.Stop = true;
                        Thread.Sleep(50);
                        this.renderLauncher.Dispose();
                    }
                    e.Cancel = false;
                    this.Dispose();
                    this.Close();
                    break;
            }
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
            this.labRenderTime.Text = $"{data.MS}ms";
        }

        private void saveAppConfig()
        {
            File.WriteAllText(this.appConfig_path, JsonConvert.SerializeObject(this.appConfig,
                Formatting.Indented,
                new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() })
                );
        }

        private void disableUI()
        {
            this.cmbFrameTime.Enabled = false;
            this.cmbCPUFans.Enabled = false;
            this.cmbNetInterfaces.Enabled = false;
            this.cmbThemes.Enabled = false;
            this.dtpStartDate.Enabled = false;
            this.ckbAutoStart.Enabled = false;
            this.ckbScreenProtect.Enabled = false;
        }

        private void enableUI()
        {
            this.cmbFrameTime.Enabled = true;
            this.cmbCPUFans.Enabled = true;
            this.cmbNetInterfaces.Enabled = true;
            this.cmbThemes.Enabled = true;
            this.dtpStartDate.Enabled = true;
            this.ckbAutoStart.Enabled = true;
            //this.ckbScreenProtect.Enabled = true;
        }

        public void setupTaskScheduleOnLogon(bool enable)
        {
            var exe_path = typeof(Program).Assembly.Location;
            var working_dir = Path.GetDirectoryName(exe_path);

            using (TaskService ts = new TaskService())
            {
                if (enable)
                {
                    //check if schedule alreay exist
                    var task = ts.FindTask(this.task_name);

                    if (task != null)
                    {
                        ts.RootFolder.DeleteTask(this.task_name);
                    }

                    var definition = ts.NewTask();
                    definition.RegistrationInfo.Description = "PCMonitor Auto-Start";
                    definition.Triggers.Add<LogonTrigger>(new LogonTrigger());
                    definition.Principal.RunLevel = TaskRunLevel.Highest;
                    definition.Actions.Add<ExecAction>(new ExecAction(exe_path, "-auto", working_dir));

                    var new_task = ts.RootFolder.RegisterTaskDefinition(this.task_name, definition);
                    new_task.Enabled = enable;

                    MessageBox.Show("自动启动计划任务设置成功");


                }
                else
                {
                    var task = ts.FindTask(this.task_name);
                    if (task != null)
                    {
                        ts.RootFolder.DeleteTask(this.task_name);
                        MessageBox.Show("自动启动计划任务删除成功");
                    }
                }


            }
        }

        private void lnkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/wanglvhang/PCMonitorForUSBScreen");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


    public class UIUpdateData
    {
        public int Count { get; set; }

        public double MS { get; set; }
    }


}
