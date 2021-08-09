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

        private AppConfig appConfig;
        private string themePath;
        private IUSBScreen Screen;


        public RenderLauncher(AppConfig appConfig, string theme_path)
        {
            this.appConfig = appConfig;
            this.themePath = theme_path;
            this.Screen = Device3_5.Instance;
        }


        public void Initial()
        {
            //read theme config
            var bg_img = new Bitmap($"{themePath}\\bg.png");
            var theme_json_path = $"{themePath}\\config.json";
            var start_date = Convert.ToDateTime(this.appConfig.StartDate);

            var mdp = new MonitorDataProvider(start_date, this.appConfig.CPUFanIndex, this.appConfig.NetworkInterface);
            var theme_config = JsonConvert.DeserializeObject<ThemeConfig>(File.ReadAllText(theme_json_path));

            this.ScreenRender = new ScreenRender(bg_img, Screen, mdp, theme_config);

            this.ScreenRender.Setup();

        }



        public void Run(Action<int, double> uiCallback, RenderStopSignal signal)
        {

            var count = 1;
            while (true && !signal.Stop)
            {
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
            this.Screen.Shutdown();
        }



    }


    public class RenderStopSignal
    {
        public bool Stop { get; set; }
    }

}