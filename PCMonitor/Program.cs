using System;
using System.Drawing;
using System.IO;
using System.Threading;
using USBScreen;
using System.Linq;
using System.Collections.Generic;
using OpenHardwareMonitor.Hardware;
using System.Threading.Tasks;
using PCMonitor.Widgets;
using Newtonsoft.Json;
using System.Configuration;
using System.Runtime.InteropServices;

namespace PCMonitor
{
    class Program
    {
        static bool Stop;
        static async Task Main(string[] args)
        {
            SetConsoleCtrlHandler(cancelHandler, true);
            Console.WriteLine("程序启动!");

            var theme = ConfigurationManager.AppSettings["theme"];
            var cpu_index = Convert.ToInt32(ConfigurationManager.AppSettings["cpu_fan_index"]);
            var ni_name = ConfigurationManager.AppSettings["network_interface_name"];
            var render_interval = Convert.ToInt32(ConfigurationManager.AppSettings["render_interval"]);
            var start_date = ConfigurationManager.AppSettings["start_date"];


            var config_path =$"themes/{theme}/config.json";
            var bg_path = $"themes/{theme}/bg.png";


            var theme_config = JsonConvert.DeserializeObject<ThemeConfig>(File.ReadAllText(config_path));
            var bg_img = new Bitmap(bg_path);
            var mdp = new MonitorDataProvider(cpu_index, ni_name, start_date);


            var virtuualScreen = new VirtualScreen();

            var sr = new ScreenRender(bg_img,virtuualScreen, mdp, theme_config);

            sr.Setup();

            Console.WriteLine("开始渲染...");

            var current_cusor_top = Console.CursorTop;

            var idx = 0;
            Stop = false;
            while(true && !Stop)
            {
                Console.CursorTop = current_cusor_top;
                Console.WriteLine($"渲染计数，{idx}次...");
                var now = DateTime.Now;
                await sr.Refresh();
                var span = DateTime.Now - now;
                //Console.WriteLine($"渲染花费：{span.TotalMilliseconds}毫秒");

                idx++;
                //补齐每次循环所需的 500ms
                if(span.TotalMilliseconds < render_interval)
                {
                    Thread.Sleep(render_interval - (int)span.TotalMilliseconds);
                }

            }


        }

        public delegate bool ControlCtrlDelegate(int CtrlType);
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ControlCtrlDelegate HandlerRoutine, bool Add);
        private static ControlCtrlDelegate cancelHandler = new ControlCtrlDelegate(HandlerRoutine);

        public static bool HandlerRoutine(int CtrlType)
        {
            switch (CtrlType)
            {
                case 0:
                    Console.WriteLine("任务取消"); //Ctrl+C关闭  
                    break;
                case 2:
                    Console.WriteLine("控制台关闭");//按控制台关闭按钮关闭  
                    break;
            }
            Stop = true;
            Device3_5.Instance.Shutdown();
            return true;
        }
    }


}
