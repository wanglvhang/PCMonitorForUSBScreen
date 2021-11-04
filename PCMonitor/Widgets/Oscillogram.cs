using USBScreen;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor.Widgets
{
    public class Oscillogram : WidgetBase
    {
        public override eWidgetType WidgetType => eWidgetType.Oscillogram;

        //当前渲染的所有数据
        public List<float> InputValues { get; private set; }

        //数据是否已经填充满了
        public bool IsFull { get { return this.InputValues.Count >= this.DataWidth; } }

        public int BorderWidth { get; set; }

        public Color BorderColor { get; set; }

        public int DataWidth { get; private set; }

        public int DataHeight { get; private set; }

        private Rectangle relative_contentRec { get; set; }


        public override void Reset()
        {
            //下次绘制时重置widget
            this.IsRendered = false;
        }



        //所需的数据为0~100的浮点数
        public Oscillogram(eMonitorDataType dataType, Rectangle rectangle, Color frontColor, Color? bgColor, int borderWidth, Color borderColor)
        {
            this.IsRendered = false;
            this.InputValues = new List<float>();

            //初始化渲染所需对象与配置
            this.Area = rectangle;
            this.BackgroundColor = bgColor;
            this.FrontColor = frontColor;
            this.BorderWidth = borderWidth;
            this.BorderColor = borderColor;
            this.DataType = dataType;

            //相对于 area 的的content
            this.relative_contentRec = new Rectangle(borderWidth, borderWidth, this.Area.Width - borderWidth * 2, this.Area.Height - borderWidth * 2);

            this.DataWidth = this.Area.Width - (borderWidth * 2);
            this.DataHeight = this.Area.Height - (borderWidth * 2);

        }


        //frameRender会将widget对应area的bitmap作为canvas传输给widget的render方法，防止原始的背景bitmap被修改
        public override void Render(IUSBScreen screen, Bitmap widget_canvas, DataForRender data)
        {

            //如果是首次绘制或数据已满时，刷新背景进行重置
            if (!this.IsRendered || this.IsFull)
            {
                this.InputValues.Clear();

                //绘制边框
                using (Graphics graphics = Graphics.FromImage(widget_canvas))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;

                    graphics.DrawRectangle(new Pen(new SolidBrush(this.BorderColor), this.BorderWidth), this.relative_contentRec);

                    graphics.Save();

                    //首次渲染或当页数据填充满时，重置
                    screen.RenderBitmap(widget_canvas, this.Area.X, this.Area.Y);

                    this.IsRendered = true;
                    //注意：若此次渲染为满屏后的重置则不再绘制数据像素
                }
            }
            else
            {
                //绘制数据 per_value 为 0~100的浮点数
                var per_value = data.Num.Value;

                var pixels_list = new List<Point>();

                //获取高度
                var col_height = Convert.ToInt32(this.DataHeight * per_value / 100f);

                var idx = 0;
                while(idx < col_height)
                {
                    pixels_list.Add(new Point(this.Area.X + this.relative_contentRec.X + this.BorderWidth + this.InputValues.Count, this.Area.Bottom - this.BorderWidth - idx));

                    idx++;
                }

                screen.RenderPixels( this.FrontColor, pixels_list);

                this.InputValues.Add(per_value);

            }


        }

    }
}
