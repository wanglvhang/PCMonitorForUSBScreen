using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Action = System.Action;
using Task = System.Threading.Tasks.Task;
using System.Diagnostics;

namespace PCMonitor
{
    /// <summary>
    /// 该类职责： 1.背景准备  2.构建screenRender 3.Run 回调
    /// </summary>
    public class RenderLauncher : IDisposable
    {
        public ScreenRenderer ScreenRenderer { get; private set; }
        public IScreen Screen { get; private set; }
        public ScreenMetaAttribute ScreenMeta { get; private set; }


        private AppConfig appConfig;
        private ThemePackage themePackage;
        private DateTime lastRunScreenProtectTime;


        public RenderLauncher(AppConfig appCon, ThemePackage themePackage)
        {
            this.appConfig = appCon;
            this.themePackage = themePackage;
            this.lastRunScreenProtectTime = DateTime.Now;//屏保运行时间记录

            //初始化usbscreen
            this.initialScreen();

            this.initialScreenRender();

        }


        private void initialScreen()
        {
            (this.Screen, this.ScreenMeta) = ScreenLoader.Load(this.themePackage.Config,appConfig);

        }
        //包括初始化 monitor
        private void initialScreenRender ()
        {

            var mdp = DataProviderLoader.Load(this.appConfig);

            this.ScreenRenderer = new ScreenRenderer(this.Screen,this.themePackage, mdp);

        }

        public void Run(Action<int, double> uiCallback, RenderStopSignal signal)
        {

            var count = 1;//绘制计数器

            var stopwatch = new Stopwatch();

            //检查屏保图片显示
            while (true && !signal.Stop)
            {
                stopwatch.Reset();
                stopwatch.Start();
                //判断是否需要执行屏保
                var time_since_last_screenprotect = DateTime.Now - lastRunScreenProtectTime;
                if(this.appConfig.ScreenProtect && !this.themePackage.Config.isDataOnly  
                    &&  time_since_last_screenprotect.TotalSeconds >= this.appConfig.ScreenProtectInterval * 60)
                {
                    //run screen protect
                    this.ScreenRenderer.ScreenProtect();
                    lastRunScreenProtectTime = DateTime.Now;
                }

                this.ScreenRenderer.RenderFrame();

                stopwatch.Stop();
                var frame_time = stopwatch.ElapsedMilliseconds;
                
                uiCallback(count, frame_time);

                count++;
                //if render time is lower then interval, sleep
                if (frame_time < this.appConfig.FrameTime)
                {
                    Thread.Sleep(this.appConfig.FrameTime - (int)frame_time);
                }

            }

        }

        public void Dispose()
        {
            this.Screen.Dispose();
        }

    }


    public class RenderStopSignal
    {
        public bool Stop { get; set; }
    }


}
