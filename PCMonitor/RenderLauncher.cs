using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using USBScreen;
using Action = System.Action;
using Task = System.Threading.Tasks.Task;

namespace PCMonitor
{
    public class RenderLauncher : IDisposable
    {

        public ScreenRender ScreenRender { get; private set; }

        public IUSBScreen USBScreen
        {
            get
            {
                return GetUSBScreenByDevice(this.screenDevice);
            }
        }

        private AppConfig appConfig;
        private ThemeConfig themeConfig;
        private string themePath;
        private eScreenDevice screenDevice;


        private DateTime lastRunScreenProtectTime;


        private IUSBScreen GetUSBScreenByDevice(eScreenDevice device)
        {
            if (device == eScreenDevice.inch35)
            {
                return Device3_5.GetInstance(this.themeConfig.width, this.themeConfig.height);
            }
            else if (device == eScreenDevice.tgus)
            {
                return TGUScreen.GetInstance(this.themeConfig.width,this.themeConfig.height,
                    this.themeConfig.comName,this.themeConfig.baudRate);
            }
            else
            {
                return null;
            }
        }


        public RenderLauncher(AppConfig appCon, string theme_path, ThemeConfig themeCon)
        {
            this.appConfig = appCon;
            this.themePath = theme_path;
            this.themeConfig = themeCon;
            this.screenDevice = themeCon.device.toEnum<eScreenDevice>();
            this.lastRunScreenProtectTime = DateTime.Now;

            this.initialScreenRender();

        }

        //包括初始化 monitor
        private void initialScreenRender ()
        {
            //read theme config
            var bg_path = $"{themePath}\\bg.png";
            Bitmap bg_img = null; //若bg.png不存在则 bg_img为空
            if (File.Exists(bg_path))
            {
                bg_img = new Bitmap(bg_path);
            }


            var start_date = Convert.ToDateTime(this.appConfig.StartDate);

            var mdp = new MonitorDataProvider(start_date, this.appConfig.CPUFanIndex, this.appConfig.NetworkInterface);

            this.ScreenRender = new ScreenRender(bg_img, USBScreen, mdp, this.themeConfig);

        }


        public void Run(Action<int, double> uiCallback, RenderStopSignal signal)
        {

            var count = 1;//绘制计数器

            //检查屏保图片显示
            while (true && !signal.Stop)
            {
                //判断是否需要执行屏保
                var time_since_last_screenprotect = DateTime.Now - lastRunScreenProtectTime;
                if(this.appConfig.ScreenProtect && !this.themeConfig.isDataOnly  &&  time_since_last_screenprotect.TotalSeconds >= this.appConfig.ScreenProtectInterval * 60)
                {
                    //run screen protect
                    this.ScreenRender.ScreenProtect();
                    lastRunScreenProtectTime = DateTime.Now;
                }

                var now = DateTime.Now;
                this.ScreenRender.Refresh();
                var span = DateTime.Now - now;
                uiCallback(count, span.TotalMilliseconds);

                count++;
                span = DateTime.Now - now;
                //if render time is lower then interval, sleep
                if (span.TotalMilliseconds < this.appConfig.FrameTime)
                {
                    Thread.Sleep(this.appConfig.FrameTime - (int)span.TotalMilliseconds);
                }

            }

        }


        public void Dispose()
        {
            this.USBScreen.Dispose();
        }




    }


    public class RenderStopSignal
    {
        public bool Stop { get; set; }
    }

    public enum eScreenDevice
    {
        inch35,
        tgus, //冠显tgus屏幕，该适配只传输数据到串口屏幕
    }

}
