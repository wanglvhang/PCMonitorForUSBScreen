using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor
{
    public class DataForRender
    {
        public DataForRender(float? num, string str)
        {
            this.Num = num;
            this.Str = str;
        }

        public float? Num { get; set; }

        public string Str { get; set; }
    }

}
