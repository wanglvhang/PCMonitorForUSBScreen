using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBScreen;

namespace PCMonitor.Widgets
{
    internal class Arc : WidgetBase
    {

        //arc所需参数
        //根据 x y w h 确定圆心

        //内径  //外径

        //开始角度    结束角度   x+ 轴为0度

        //前景色  背景色

        //边框宽度  边框颜色

        public Arc()
        {

        }


        public override void Render(IUSBScreen screen, Bitmap widget_canvas, DataForRender data)
        {
            //使用 Graphics.DrawArc 绘制图形

            using (var graph = Graphics.FromImage(widget_canvas))
            {
                //graph.DrawArc
            }
        }
    }
}
