using USBScreen;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor
{
    public abstract class WidgetBase
    {

        public eMonitorDataType DataType { get; protected set; }

        public abstract eWidgetType WidgetType { get; }

        public DataForRender PrevData { get; protected set; }

        public bool IsOneTimeOnly { get; protected set; }

        public bool IsRendered { get; protected set; }

        public Rectangle Area { get; protected set; }

        public Color FrontColor { get; protected set; }

        public Color? BackgroundColor { get; protected set; }

        public int UpdateInterval { get; set; }

        public DateTime LastUpdatedTime { get; protected set; }

        public abstract void Render(IUSBScreen screen, Bitmap widget_canvas, DataForRender data);

    }

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
