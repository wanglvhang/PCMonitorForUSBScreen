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

        public DataForRender PrevData { get; protected set; }

        public Bitmap PrevFrame { get; protected set; }

        //public bool IsOneTimeOnly { get; protected set; } 可以用 IsStatic 来替换

        public bool IsRendered { get; protected set; }

        public Rectangle Area { get; protected set; }

        //前景色
        public Color FrontColor { get; protected set; }

        //背景色
        public Color? BackgroundColor { get; protected set; }

        //public int UpdateInterval { get; set; }

        public DateTime LastUpdatedTime { get; protected set; }

        public abstract void Render(IScreen screen, Bitmap widget_canvas, DataForRender data);

        public abstract void Reset();

    }



}
