using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor
{
    public class AppConfig
    {
        public string NetworkInterface { get; set; }

        public int CPUFanIndex { get; set; }

        public string StartDate { get; set; }

        public int FrameTime { get; set; }

        public string Theme { get; set; }

        public bool IsAutoStart { get; set; }

        public bool ScreenProtect { get; set; }

    }
}
