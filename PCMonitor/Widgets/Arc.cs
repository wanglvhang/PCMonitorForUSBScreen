using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor.Widgets
{
    internal class Arc : WidgetBase
    {

        //arc所需参数
        //已存在
        //area 根据 x y w h 确定圆心
        //前景，背景

        //内径  //外径
        public int ArcWidth { get; set; }

        //开始角度    结束角度   x+ 轴为0度
        public float StartAngle { get; set; } //单位为度 0~360；

        //扫过角度
        public float SweepAngle { get; set; } 


        //边框颜色
        public Color? BorderColor { get; set; }

        //边框宽度
        public int BorderWidth { get; set; }

        public Arc(WidgetConfig wc, Rectangle area)
        {
            this.StartAngle = wc.StartAngle;
            this.SweepAngle = wc.SweepAngle;
            this.ArcWidth = wc.ArcWidth;

            var fc = wc.FrontColor.ToColor();
            this.FrontColor = fc.HasValue ? fc.Value : Color.Black;

            //var bc = wc.BackgroundColor.ToColor();
            this.BackgroundColor = wc.BackgroundColor.ToColor();


            this.Area = area;
        }


        public override void Render(IScreen screen, Bitmap widget_canvas, DataForRender data)
        {
            //此处的widget_canvas 每次都是拷贝新实例

            //使用 Graphics.DrawArc 绘制图形

            //接受 0~100的浮点数

            using (var graph = Graphics.FromImage(widget_canvas))
            {

                //draw arc 的宽高需要结合 widget范围 与 圆弧宽度重新计算 以宽度为例： arc_w = w - pen with 
                //由于已经是在对应位置截取出来背景上绘制，所以不需要再添加坐标， 添加坐标会导致图形偏移
                //var arc_rec = new RectangleF(Area.X + ArcWidth/2 , Area.Y + ArcWidth /2, Area.Width - ArcWidth, Area.Height - ArcWidth);
                var arc_rec = new RectangleF(ArcWidth / 2, ArcWidth / 2, Area.Width - ArcWidth, Area.Height - ArcWidth);


                if (this.BackgroundColor.HasValue)
                {
                    //graph.Clear(this.BackgroundColor.Value);

                    //arc中的背景色为arc百分之白时的弧形
                    var bg_pen = new Pen(this.BackgroundColor.Value, ArcWidth);
                    graph.DrawArc(bg_pen, arc_rec, StartAngle, SweepAngle);

                }


                var pen = new Pen(this.FrontColor,ArcWidth);

                var front_sweepangle = data.Num.Value * SweepAngle / 100;

                graph.DrawArc(pen, arc_rec, StartAngle, front_sweepangle);

            }

            //对比之前图片并获取不同像素
            if(this.PrevFrame == null)
            {
                //首次绘制输出对比背景图
                screen.RenderBitmap(widget_canvas, this.Area.X, this.Area.Y);
            }
            else
            {
                //对比prevframe并提取不同像素，使用renderpixels进行绘制
                var pixels = widget_canvas.DiffFrom(this.PrevFrame);
                screen.RenderPixels(pixels);
            }

            this.PrevFrame = widget_canvas;

        }

        public override void Reset()
        {
            this.PrevFrame = null;
        }
    }
}
