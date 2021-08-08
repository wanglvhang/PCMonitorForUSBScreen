using Microsoft.Win32.TaskScheduler;
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
    public class RenderLauncher
    {

        public ScreenRender ScreenRender { get; private set; }

        private AppConfig appConfig;
        private string themePath;
        private IUSBScreen Screen;


        public RenderLauncher(AppConfig appConfig,string theme_path)
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


        }



        public void Run(Action<int, double> uiCallback, RenderStopSignal signal)
        {

            //var idx = 0;
            //while (idx < 100 && !signal.Stop)
            //{
            //    uiCallback(idx, idx*10f);

            //    Thread.Sleep(500);

            //    idx++;
            //}

            var count = 1;
            while (true && !signal.Stop)
            {
                var now = DateTime.Now;
                this.ScreenRender.Refresh();
                var span = DateTime.Now - now;
                uiCallback(count,span.TotalMilliseconds);

                count++;
                span = DateTime.Now - now;
                //if render time is lower then interval, sleep
                if (span.TotalMilliseconds < this.appConfig.FrameTime)
                {
                    Thread.Sleep(this.appConfig.FrameTime - (int)span.TotalMilliseconds);
                }

            }

        }





        public static void setupTaskScheduleOnLogon()
        {
            var exe_path = typeof(Program).Assembly.Location;
            var working_dir = Path.GetDirectoryName(exe_path);
            var task_name = "PCMonitor_Schedule";

            using (TaskService ts = new TaskService())
            {
                //check if schedule alreay exist
                var task = ts.FindTask(task_name);
                if (task != null)
                {
                    Console.WriteLine($"task schedule [{task_name}] alreday exist");
                    return;
                }

                TaskDefinition definition = ts.NewTask();
                definition.RegistrationInfo.Description = "PCMonitor Auto-Start";
                definition.Triggers.Add<LogonTrigger>(new LogonTrigger());
                definition.Principal.RunLevel = TaskRunLevel.Highest;
                definition.Actions.Add<ExecAction>(new ExecAction(exe_path, null, working_dir));
                var new_task = ts.RootFolder.RegisterTaskDefinition(task_name, definition, TaskCreation.CreateOrUpdate, "SYSTEM");
                new_task.Enabled = true;

                Console.WriteLine($"task schedule [{task_name}] successfully setup");

            }
        }


    }


    public class RenderStopSignal
    {
        public bool Stop { get; set; }
    }

}
