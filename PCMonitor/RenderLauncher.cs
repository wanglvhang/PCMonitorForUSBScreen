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
    /// <summary>
    /// 该类职责： 1.背景准备  2.构建screenRender 3.Run 回调
    /// </summary>
    public class RenderLauncher : IDisposable
    {

        public ScreenRenderer ScreenRenderer { get; private set; }

        public IUSBScreen Screen { get; private set; }
        public USBScreenMetaAttribute ScreenMeta { get; private set; }


        private AppConfig appConfig;
        private ThemePackage themePackage;
        private DateTime lastRunScreenProtectTime;



        public RenderLauncher(AppConfig appCon, ThemePackage themePackage)
        {
            this.appConfig = appCon;
            this.themePackage = themePackage;
            this.lastRunScreenProtectTime = DateTime.Now;//屏保运行时间记录

            //初始化usbscreen
            this.initialUSBScreen();

            this.initialScreenRender();

        }


        private void initialUSBScreen()
        {
            //使用反射
            var screen_assembly = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("USBScreen")).FirstOrDefault();

            var screen_types = screen_assembly.GetTypes().Where(t => typeof(IUSBScreen).IsAssignableFrom(t) && !t.IsInterface).ToList();

            foreach (var t in screen_types)
            {
                var attr = t.GetCustomAttributes(typeof(USBScreenMetaAttribute), false).FirstOrDefault();

                if (attr != null)
                {
                    var meta_attr = attr as USBScreenMetaAttribute;

                    if (meta_attr.DeviceName.ToLower() == this.themePackage.Config.device.ToLower())
                    {
                        //初始化类型
                        this.ScreenMeta = meta_attr;
                        this.Screen = Activator.CreateInstance(t) as IUSBScreen;

                        this.Screen.SetRenderResolution(themePackage.Config.width, themePackage.Config.height);
                        //TODO: 检查分辨率是否OK
                    }
                }
            }

            //建设themeconfig中device设置是否正确
            if (this.Screen == null || this.ScreenMeta == null)
            {
                throw new Exception("无法找到主题指定的设备，请检查主题配置中device设置是否正确。");
            }

        }
        //包括初始化 monitor
        private void initialScreenRender ()
        {

            var start_date = Convert.ToDateTime(this.appConfig.StartDate);

            var mdp = new MonitorDataProvider(start_date, this.appConfig.CPUFanIndex, this.appConfig.MainboardIndex, this.appConfig.NetworkInterface);

            this.ScreenRenderer = new ScreenRenderer(this.Screen,this.themePackage, mdp);

        }


        public void Run(Action<int, double> uiCallback, RenderStopSignal signal)
        {

            var count = 1;//绘制计数器

            //检查屏保图片显示
            while (true && !signal.Stop)
            {
                //判断是否需要执行屏保
                var time_since_last_screenprotect = DateTime.Now - lastRunScreenProtectTime;
                if(this.appConfig.ScreenProtect && !this.themePackage.Config.isDataOnly  &&  time_since_last_screenprotect.TotalSeconds >= this.appConfig.ScreenProtectInterval * 60)
                {
                    //run screen protect
                    this.ScreenRenderer.ScreenProtect();
                    lastRunScreenProtectTime = DateTime.Now;
                }

                var now = DateTime.Now;
                this.ScreenRenderer.RenderFrame();
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
            this.Screen.Dispose();
        }

    }


    public class RenderStopSignal
    {
        public bool Stop { get; set; }
    }


}
