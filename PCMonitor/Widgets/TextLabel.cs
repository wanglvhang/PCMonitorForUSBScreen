using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBScreen;

namespace PCMonitor.Widgets
{
    public class TextLabel:WidgetBase
    {
        public Font TextFont { get; private set; }

        //public FontStyle TextStyle { get; private set; }

        public StringFormat TextFormat { get; private set; }

        //public string DisplayText { get; private set; }

        public override eWidgetType WidgetType => eWidgetType.TextLabel;



        public TextLabel(eMonitorDataType dataType, Rectangle rectangle, Color frontColor, Color? bgColor,Font textFont, StringAlignment textAlignment, StringAlignment lineAlignment)
        {
            this.DataType = dataType;
            this.Area = rectangle;
            this.FrontColor = frontColor;
            this.BackgroundColor = bgColor;
            this.TextFont = textFont;
            this.TextFormat = new StringFormat();
            this.TextFormat.Alignment = textAlignment;
            this.TextFormat.LineAlignment = lineAlignment;
            this.TextFormat.FormatFlags = StringFormatFlags.NoWrap; //禁止换行
        }



        public override void Render(IUSBScreen screen, Bitmap widget_canvas, DataForRender data)
        {
            //防止重复渲染，节省性能
            if (PrevData != null && PrevData.Str == data.Str) return;
            //在canvas上绘制字符，
            using (Graphics graphics = Graphics.FromImage(widget_canvas))
            {
                var rec = new Rectangle(0, 0, widget_canvas.Width, widget_canvas.Height);

                var sizeF = graphics.MeasureString(data.Str, this.TextFont);


                //Console.WriteLine($"width:{sizeF.Width}");
                //Console.WriteLine($"height:{sizeF.Height}");

                //测试用绘制背景色
                //graphics.FillRectangle(Brushes.Red, rec);


                graphics.DrawString(data.Str, this.TextFont, new SolidBrush(this.FrontColor), rec, this.TextFormat);


                graphics.Save();


                //输出到屏幕
                screen.RenderBitmap(widget_canvas, this.Area.X, this.Area.Y);
            }

            this.IsRendered = true;
            this.PrevData = data;

        }

    }

}
