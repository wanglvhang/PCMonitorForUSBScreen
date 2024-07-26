using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ScreenMetaAttribute : Attribute
    {
        public string DeviceName { get; set; }

        public int Width { get; set; }

        public int Height { get;set; }

        public bool IsQuarterRenderSupport { get; set; } = false;

        //是否可以低像素渲染?

        //是否可旋转屏幕?

        //是否可镜像?
    }
}
