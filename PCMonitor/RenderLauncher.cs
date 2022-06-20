﻿using Newtonsoft.Json;
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
            else if (device == eScreenDevice.sun35)
            {
                //return SunScreen3_5
                return null;
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
        }

        //包括初始化 monitor
        public void PrepareForRun()
        {
            //read theme config
            var bg_img = new Bitmap($"{themePath}\\bg.png");

            var start_date = Convert.ToDateTime(this.appConfig.StartDate);

            var mdp = new MonitorDataProvider(start_date, this.appConfig.CPUFanIndex, this.appConfig.NetworkInterface);

            this.ScreenRender = new ScreenRender(bg_img, USBScreen, mdp, this.themeConfig);

            this.ScreenRender.Prepare();

        }



        public void Run(Action<int, double> uiCallback, RenderStopSignal signal)
        {

            var count = 1;
            //检查屏保图片显示

            while (true && !signal.Stop)
            {
                //判断是否需要执行屏保
                var time_since_last_screenprotect = DateTime.Now - lastRunScreenProtectTime;
                if(this.appConfig.ScreenProtect && time_since_last_screenprotect.TotalSeconds >= this.appConfig.ScreenProtectInterval * 60)
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
            this.USBScreen.Shutdown();
        }




    }


    public class RenderStopSignal
    {
        public bool Stop { get; set; }
    }

    public enum eScreenDevice
    {
        inch35,
        sun35,
    }

}
