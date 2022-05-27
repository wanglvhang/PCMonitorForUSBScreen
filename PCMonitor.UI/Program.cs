using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCMonitor.UI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool isAuto = false;
            if(args.Length > 0 && args[0] == "-auto")
            {
                isAuto = true;
            }


            if (RunningInstance() == null)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main(isAuto));
            }
            else
            {
                MessageBox.Show("PCMonitor.UI 已运行 / already running.");
            }

        }


        public static System.Diagnostics.Process RunningInstance()
        {

            var current = System.Diagnostics.Process.GetCurrentProcess();

            var processes = System.Diagnostics.Process.GetProcesses();

            foreach (var process in processes) 
            {
                if (process.Id != current.Id)
                {
                    //check process name
                    if (process.ProcessName == current.ProcessName)
                    {
                        return process;
                    }

                    //check process location
                    //if (System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", @"/") == current.MainModule.FileName)
                    //{
                    //    return process;
                    //}
                }

            } 

            return null;

        }



    }
}
