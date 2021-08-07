using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Action = System.Action;
using Task = System.Threading.Tasks.Task;

namespace PCMonitor
{
    public class RenderLauncher
    {

        public ScreenRender ScreenRender { get; private set; }


        public RenderLauncher()
        {

        }


        public void Initial()
        {


            //read configuration

            //


        }



        public void Run(Action<int, float> uiCallback, RenderStopSignal signal)
        {

            //运行刷新
            var idx = 0;
            while (idx < 100 && !signal.Stop)
            {
                uiCallback(idx, idx*10f);

                Thread.Sleep(500);

                idx++;
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
