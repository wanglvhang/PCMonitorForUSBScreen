using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBScreen;

namespace PCMonitor.Widgets
{
    internal class TGUSControl : WidgetBase
    {
        public override eWidgetType WidgetType => eWidgetType.TGUSControl;

        public override void Render(IUSBScreen screen, Bitmap widget_canvas, DataForRender data)
        {
            //直接调用screen的send cmd 发送指令更新空间数据


        }
    }
}
