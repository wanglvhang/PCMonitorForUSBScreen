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
using Microsoft.Win32.TaskScheduler;
using Task = System.Threading.Tasks.Task;
using System.Diagnostics;

namespace PCMonitor
{
    class Program
    {
        static bool Stop;

        static IUSBScreen Screen;

        static int Main(string[] args)
        {
            DisbleQuickEditMode();
            SetConsoleCtrlHandler(cancelHandler, true);
            Console.Title = "PCMonitor";
            Console.CursorVisible = false;

            Console.WriteLine("PCMonitor start!");

            //read configuration
            var theme = ConfigurationManager.AppSettings["theme"];
            var cpu_index_str = ConfigurationManager.AppSettings["cpu_fan_index"];
            var ni_name = ConfigurationManager.AppSettings["network_interface_name"];
            var render_interval_str = ConfigurationManager.AppSettings["render_interval"];
            var start_date_str = ConfigurationManager.AppSettings["start_date"];
            var task_schedule_str = ConfigurationManager.AppSettings["task_schedule"];

            //theme
            if (string.IsNullOrWhiteSpace(theme))
            {
                theme = "default";
                Console.WriteLine($"config default [theme]:{theme}");
            }
            else
            {
                Console.WriteLine($"config read [theme]:{theme}");
            }

            //cpu_index
            int cpu_index;
            if (int.TryParse(cpu_index_str, out cpu_index))
            {
                Console.WriteLine($"config read [cpu_fan_index]:{cpu_index}");
            }
            else
            {
                cpu_index = 0;
                Console.WriteLine($"config default [cpu_fan_index]:{cpu_index}");
            }

            //network_interface_name
            Console.WriteLine($"config read [network_interface_name]:{ni_name}");

            //render_interval
            int render_interval;
            if (int.TryParse(render_interval_str, out render_interval))
            {
                Console.WriteLine($"config read [cpu_fan_index]:{render_interval}");
            }
            else
            {
                render_interval = 1000;
                Console.WriteLine($"config default [cpu_fan_index]:{render_interval}");
            }

            //start_date
            DateTime start_date;
            if (DateTime.TryParse(start_date_str, out start_date))
            {
                Console.WriteLine($"config read [start_date]:{start_date.ToShortDateString()}");
            }
            else
            {
                start_date = DateTime.Parse("1949/10/1");
                Console.WriteLine($"config default [start_date]:{start_date.ToShortDateString()}");
            }


            //task_schedule
            bool task_schedule;
            if (bool.TryParse(task_schedule_str, out task_schedule))
            {
                Console.WriteLine($"config read [task_schedule]:{task_schedule}");
            }
            else
            {
                task_schedule = false;
                Console.WriteLine($"config default [task_schedule]:{task_schedule}");
            }

            if (task_schedule)
            {
                //set task schedule
                setupTaskScheduleOnLogon();
            }

            var theme_config_path = $"{Environment.CurrentDirectory}\\themes\\{theme}\\config.json";
            var bg_path = $"{Environment.CurrentDirectory}\\themes\\{theme}\\bg.png";

            var theme_config = JsonConvert.DeserializeObject<ThemeConfig>(File.ReadAllText(theme_config_path));
            var bg_img = new Bitmap(bg_path);
            var mdp = new MonitorDataProvider(start_date, cpu_index, ni_name);


            Screen = Device3_5.Instance;

            //var virtuualScreen = new VirtualScreen();
            //build render
            var sr = new ScreenRender(bg_img, Screen, mdp, theme_config);

            sr.Setup();


            Console.WriteLine("start render...");


            var current_cusor_top = Console.CursorTop;

            var count = 0;
            Stop = false;
            while (true && !Stop)
            {
                Console.CursorTop = current_cusor_top;
                Console.WriteLine($"render frame count，{count}...");
                var now = DateTime.Now;
                sr.Refresh();
                var span = DateTime.Now - now;
                Console.WriteLine($"render cost：{span.TotalMilliseconds} ms.");

                span = DateTime.Now - now;

                count++;
                //if render time is lower then interval, sleep
                if (span.TotalMilliseconds < render_interval)
                {
                    Thread.Sleep(render_interval - (int)span.TotalMilliseconds);
                }

            }

            return 1;

        }

        public static void writeLog(string log)
        {
            var path = "log_.txt";
            File.AppendAllText(path, $"{log}\r\n");
        }



        #region add task scheduler

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


        #endregion


        #region console exit event

        public delegate bool ControlCtrlDelegate(int CtrlType);
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ControlCtrlDelegate handler, bool Add);

        private static ControlCtrlDelegate cancelHandler = new ControlCtrlDelegate(HandlerRoutine);

        public static bool HandlerRoutine(int CtrlType)
        {
            Console.CursorTop += 1;
            switch (CtrlType)
            {
                case 0:
                    Console.WriteLine("task cancel"); //Ctrl+C关闭  
                    Stop = true;
                    Screen.Shutdown();
                    break;
                case 2:
                    Console.WriteLine("console closed");//按控制台关闭按钮关闭  
                    Stop = true;
                    Screen.Shutdown();
                    break;
            }
            return true;
        }

        #endregion


        #region 关闭控制台 快速编辑模式、插入模式
        const int STD_INPUT_HANDLE = -10;
        const uint ENABLE_QUICK_EDIT_MODE = 0x0040;
        const uint ENABLE_INSERT_MODE = 0x0020;
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetStdHandle(int hConsoleHandle);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint mode);

        public static void DisbleQuickEditMode()
        {
            IntPtr hStdin = GetStdHandle(STD_INPUT_HANDLE);
            uint mode;
            GetConsoleMode(hStdin, out mode);
            mode &= ~ENABLE_QUICK_EDIT_MODE;//移除快速编辑模式
            mode &= ~ENABLE_INSERT_MODE;//移除插入模式
            SetConsoleMode(hStdin, mode);
        }
        #endregion

    }


}
