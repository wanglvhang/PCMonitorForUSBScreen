using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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


        private void Main_Load(object sender, EventArgs e)
        {
            //初始化UI内容以及读取配置

            //网卡
            var network_interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach(var ni in network_interfaces)
            {
                this.cmbNetInterfaces.Items.Add(ni.Name);
            }
            this.cmbNetInterfaces.SelectedIndex = 0;

            //cpu风扇
            var fans = this.GetSuperIOFanSensors();

            foreach(var f in fans)
            {
                this.cmbFans.Items.Add(f.Name);
            }

            this.cmbFans.SelectedIndex = 0;

            //start date

            //frame time
            this.cmbFrameTime.SelectedIndex = 2;


            //themes

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
            this.CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.renderLauncher = new RenderLauncher();

            this.signal.Stop = false;

            var thread = new Thread(ThreadProcSafePost);

            thread.Start();

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
            this.label7.Text = $"{data.Count}";
            this.label8.Text = $"{data.MS}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.signal.Stop = true;
        }
    }

    public class UIUpdateData
    {
        public int Count { get; set; }

        public float MS { get; set; }
    }


}
