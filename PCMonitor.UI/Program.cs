using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            try
            {


                bool isAuto = false;
                if (args.Length > 0 && args[0] == "-auto")
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
            catch (Exception ex)
            {

                MessageBox.Show("程序出现异常，请检查程序log目录中的日志信息");

                var log_foler_path = $"{Environment.CurrentDirectory}\\log";
                var log_file_path = $"{log_foler_path}\\{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.log";

                if (!Directory.Exists(log_foler_path)) Directory.CreateDirectory(log_foler_path);
                if (!File.Exists(log_file_path)) File.Create(log_file_path).Dispose();

                var sb = new StringBuilder();

                sb.AppendLine("ex ==============================");
                sb.AppendLine("ex message:");
                sb.AppendLine(ex.Message);
                sb.AppendLine("ex stack trace");
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine("inner ex=================");
                if(ex.InnerException != null)
                {
                    sb.AppendLine("inner ex message");
                    sb.AppendLine(ex.InnerException.Message);
                    sb.AppendLine("inner ex stack trace");
                    sb.AppendLine(ex.InnerException.StackTrace);
                }


                File.AppendAllText(log_file_path, sb.ToString());

            }

        }


        public static System.Diagnostics.Process RunningInstance()
        {
            //return null;

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
