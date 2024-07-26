using PCMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor.Basic.DataProviders
{
    [DataProviderMeta(ProviderName = "PreviewDataProvider")]
    internal class PreviewDataProvider : IMonitorDataProvider
    {
        public DataForRender GetData(eMonitorDataType dataType)
        {
            float? num = 0;
            string str = "";

            var rand = new Random();

            switch (dataType)
            {
                case eMonitorDataType.CPU_Fan_Speed: // RPM
                    num = rand.Next(900, 2300);
                    break;
                case eMonitorDataType.CPU_Hz: // MHz
                    num = rand.Next(100, 4000);
                    break;
                case eMonitorDataType.CPU_Temp: //
                    num = rand.Next(20, 100);
                    break;
                case eMonitorDataType.CPU_Load: //
                    num = rand.Next(0, 100);
                    break;

                case eMonitorDataType.GPU_Hz:
                    num = rand.Next(100, 4000);
                    break;
                case eMonitorDataType.GPU_Load:
                    num = rand.Next(0, 100);
                    break;
                case eMonitorDataType.GPU_RAM_Load:
                    num = rand.Next(0, 100);
                    break;
                case eMonitorDataType.GPU_Fan_Speed:
                    num = rand.Next(900, 2300);
                    break;
                case eMonitorDataType.GPU_RAM_Total: //MB
                    num = rand.Next(512, 20000);
                    break;

                case eMonitorDataType.RAM_Free: //MB
                    num = rand.Next(512, 20000);
                    break;
                case eMonitorDataType.RAM_Load: //
                    num = rand.Next(0, 100);
                    break;
                case eMonitorDataType.RAM_Used:
                    num = rand.Next(512, 20000);
                    break;

                case eMonitorDataType.Mainboard_Fan:
                    num = rand.Next(900, 2300);
                    break;

                case eMonitorDataType.Network_Download: //B/s
                    num = rand.Next(10240, 10240000);
                    break;
                case eMonitorDataType.Network_Upload:
                    num = rand.Next(10240, 10240000);
                    break;


                case eMonitorDataType.Total_Days:
                    num = rand.Next(100, 10000);
                    break;


                case eMonitorDataType.Customize:
                    break;
                case eMonitorDataType.Static:
                    break;


            }

            return new DataForRender(num, str);

        }

        public List<string> GetFans()
        {
            return new List<string>();
        }

        public void Initialize(AppConfig config)
        {
            
        }
    }
}
