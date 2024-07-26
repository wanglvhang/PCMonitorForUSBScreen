using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBScreen
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class USBScreenMetaAttribute : Attribute
    {
        public string DeviceName { get; set; }

        public int Width { get; set; }

        public int Height { get;set; }

        //是否可以低像素渲染
        
        //是否可旋转屏幕

        //是否可镜像
    }
}
