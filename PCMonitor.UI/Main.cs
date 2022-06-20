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
            this.labBrightness.Text = this.tbarBrightness.Value.ToString();//初始化亮度值的显示
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

            //读取并显示应用配置==============================
            //初始化UI内容以及读取配置
            var exe_path = typeof(Main).Assembly.Location;
            this.work_dir = Path.GetDirectoryName(exe_path);
            this.appConfig_path = $"{ this.work_dir}\\app.json";
            this.appConfig = JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText(this.appConfig_path));


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
            this.numScreenprotectInterval.Value = this.appConfig.ScreenProtectInterval;
            this.tbarBrightness.Value = this.appConfig.ScreenBrightness;


            //读取并显示主题配置=============================
            //themes,get all folder under theme folder
            var themes_path = Path.Combine(this.work_dir, "themes");
            var directory = new DirectoryInfo(themes_path);
            var theme_folders = directory.GetDirectories();
            foreach (var di in theme_folders)
            {
                this.cmbThemes.Items.Add(di.Name);
            }

            //默认读取 app.json 中配置的 theme
            this.cmbThemes.SelectedItem = this.appConfig.Theme;


            this.initial_theme_and_renderlauncher(this.appConfig.Theme);



            //初始化完成后挂载时间
            this.cmbNetInterfaces.SelectedIndexChanged += CmbNetInterfaces_SelectedIndexChanged;
            this.cmbCPUFans.SelectedIndexChanged += CmbFans_SelectedIndexChanged;
            this.cmbFrameTime.SelectedIndexChanged += CmbFrameTime_SelectedIndexChanged;
            this.dtpStartDate.ValueChanged += DtpStartDate_ValueChanged;
            this.cmbThemes.SelectedIndexChanged += CmbThemes_SelectedIndexChanged;

            this.ckbAutoStart.CheckedChanged += CkbAutoStart_CheckedChanged;
            this.ckbScreenProtect.CheckedChanged += CkbScreenProtect_CheckedChanged;
            this.tbarBrightness.ValueChanged += tbarBrightness_ValueChanged;

            //this.txtScreenprotectInterval.TextChanged += TxtScreenprotectInterval_TextChanged;
            this.numScreenprotectInterval.ValueChanged += new System.EventHandler(this.numScreenprotectInterval_ValueChanged);


            //自动运行
            if (this.isAutoStart)
            {
                this.WindowState = FormWindowState.Minimized;
                //启动
                btnStart_Click(this, new EventArgs());
            }

        }


        private void initial_theme_and_renderlauncher(string theme_name)
        {

            if (this.renderLauncher != null)
            {
                this.renderLauncher.Dispose();
            }

            this.btnStart.Enabled = false;

            //读取并显示主题配置=============================

            this.themeFolder_path = $"{this.work_dir}\\themes\\{theme_name}";
            var theme_json_path = $"{this.work_dir}\\themes\\{theme_name}\\config.json";
            this.themeConfig = JsonConvert.DeserializeObject<ThemeConfig>(File.ReadAllText(theme_json_path));

            this.labDevice.Text = this.themeConfig.device;
            this.labWidgetCount.Text = this.themeConfig.Widgets.Count.ToString();


            //设置pnpdeviceid?
            this.labPNPDeviceId.Text = "???";

            //设备状态


            //宽度 高度
            this.labScreenWH.Text = $"W{this.themeConfig.width}xH{this.themeConfig.height}";

            this.renderLauncher = new RenderLauncher(this.appConfig, this.themeFolder_path, this.themeConfig);

            //连接设备
            try
            {
                this.renderLauncher.USBScreen.Connect();
                this.labDeviceStatus.Text = this.renderLauncher.USBScreen.Status.ToString();

                //根据状态 设置文字颜色
                if (this.renderLauncher.USBScreen.Status == USBScreen.eScreenStatus.Connected)
                {
                    this.labDeviceStatus.ForeColor = Color.Green;
                }
                else if (this.renderLauncher.USBScreen.Status == USBScreen.eScreenStatus.Error)
                {
                    this.labDeviceStatus.ForeColor = Color.Red;
                }
                else 
                {
                    this.labDeviceStatus.ForeColor = Color.Orange;
                }


                if (this.renderLauncher.USBScreen.Status == USBScreen.eScreenStatus.Connected)
                {
                    this.renderLauncher.USBScreen.Startup();
                    this.renderLauncher.PrepareForRun();
                    this.btnStart.Enabled = true;
                }
                else
                {
                    this.btnStart.Enabled = false;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void numScreenprotectInterval_ValueChanged(object sender, EventArgs e)
        {
            //this.appConfig.ScreenProtectInterval = Convert.ToInt32(this.numScreenprotectInterval.Value);
            var value = this.numScreenprotectInterval.Value;
            if(value < 60)
            {
                value = 60;
            }
            else if(value > 720)
            {
                value = 720;
            }
            this.appConfig.ScreenProtectInterval = Convert.ToInt32(value);
            saveAppConfig();
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

            this.initial_theme_and_renderlauncher(this.appConfig.Theme);

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
            this.updateScreenOperateBtns(true);

            this.btnStart.Enabled = false;
            this.btnStop.Enabled = true;

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
            this.updateScreenOperateBtns(false);

        }

        private void lnkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/wanglvhang/PCMonitorForUSBScreen");
        }

        private void linkAuthor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://lvhang.site");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbarBrightness_ValueChanged(object sender, EventArgs e)
        {
            this.labBrightness.Text = this.tbarBrightness.Value.ToString();

            var device = this.themeConfig.device.toEnum<eScreenDevice>();
            //var usbScreen = RenderLauncher.GetUSBScreenByDevice(device);
            var usbScreen = this.renderLauncher.USBScreen;

            usbScreen.SetBrightness(this.tbarBrightness.Value);

            Thread.Sleep(30);

            this.appConfig.ScreenBrightness = this.tbarBrightness.Value;
            saveAppConfig();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
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
            catch (Exception ex)
            {

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
            this.tbarBrightness.Enabled = false;
            this.numScreenprotectInterval.Enabled = false;

        }

        private void enableUI()
        {
            this.cmbFrameTime.Enabled = true;
            this.cmbCPUFans.Enabled = true;
            this.cmbNetInterfaces.Enabled = true;
            this.cmbThemes.Enabled = true;
            this.dtpStartDate.Enabled = true;
            this.ckbAutoStart.Enabled = true;
            this.ckbScreenProtect.Enabled = true;
            this.tbarBrightness.Enabled = true;
            this.numScreenprotectInterval.Enabled = true;


        }

        private void updateScreenOperateBtns(bool isRunning)
        {
            if (isRunning)
            {
                this.btnMirror.Enabled = false;
                this.btnNormal.Enabled = false;
                this.btnLandscape.Enabled = false;
                this.btnLandscapeInvert.Enabled = false;
                this.btnVertical.Enabled = false;
                this.btnVerticalInvert.Enabled = false;
            }
            else
            {
                if (this.renderLauncher.USBScreen.Status == USBScreen.eScreenStatus.Connected)
                {
                    this.btnMirror.Enabled = true;
                    this.btnNormal.Enabled = true;
                    this.btnLandscape.Enabled = true;
                    this.btnLandscapeInvert.Enabled = true;
                    this.btnVertical.Enabled = true;
                    this.btnVerticalInvert.Enabled = true;
                }
                else
                {
                    this.btnMirror.Enabled = false;
                    this.btnNormal.Enabled = false;
                    this.btnLandscape.Enabled = false;
                    this.btnLandscapeInvert.Enabled = false;
                    this.btnVertical.Enabled = false;
                    this.btnVerticalInvert.Enabled = false;
                }
            }
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

        private void btnNormal_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void btnMirror_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void btnLandscape_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void btnLandscapeInvert_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void btnVertical_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void btnVerticalInvert_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }


    public class UIUpdateData
    {
        public int Count { get; set; }

        public double MS { get; set; }
    }


}
