using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor
{
    public enum eMonitorDataType
    {
        CPU_Load, 
        CPU_Temp, 
        CPU_Hz, 
        CPU_Fan_Speed, 

        GPU_Load,
        GPU_Temp,
        GPU_Hz, 

        GPU_RAM_Total,
        GPU_RAM_Used,
        GPU_RAM_Load,

        GPU_Fan_Speed,

        RAM_Used,
        RAM_Free,
        RAM_Load,


        Network_Upload,
        Network_Download,

        Total_Days,

    }

}
