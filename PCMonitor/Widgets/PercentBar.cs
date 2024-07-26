using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor.Widgets
{
    public class PercentBar : WidgetBase
    {

        private int preDataDimensionValue;

        //
        public PercentBar(eMonitorDataType dataType, Rectangle area, Color frontColor, Color? bgColor)
        {
            //宽高取长的作为数据显示
            this.DataType = dataType;
            this.Area = area;
            this.FrontColor = frontColor;
            this.BackgroundColor = bgColor;
        }


        //所需的数据为0~100的浮点数
        public override void Render(IScreen screen, Bitmap widget_canvas, DataForRender data)
        {
            if (!data.Num.HasValue) return;

            bool isHorizontal = true;
            if (this.Area.Height > this.Area.Width) isHorizontal = false;

            //用户呈现数据方向的长度
            var bar_length = isHorizontal ? this.Area.Width : this.Area.Height;

            //设置默认背景颜色
            this.BackgroundColor = this.BackgroundColor.HasValue ? this.BackgroundColor : Color.Black;

            using (Graphics graphics = Graphics.FromImage(widget_canvas))
            {
                //
                if (this.PrevData != null) //非首次绘制
                {
                    int changed_length = Convert.ToInt32(Math.Abs((data.Num - this.PrevData.Num).Value) * bar_length / 100f);

                    //若无变化则不渲染
                    if (changed_length == 0)
                    {
                        this.PrevData = data;//保留数据微小的变化
                        return;
                    }
                }

                var data_lenth = Convert.ToInt32(bar_length * data.Num / 100f);

                var front_rec = new Rectangle();

                if (isHorizontal)
                {
                    front_rec.X = this.Area.X;
                    front_rec.Y = this.Area.Y;
                    front_rec.Height = this.Area.Height;
                    front_rec.Width = data_lenth;

                    this.preDataDimensionValue = front_rec.Right;
                }
                else
                {
                    front_rec.X = this.Area.X;
                    front_rec.Y = this.Area.Y + (this.Area.Height - bar_length);
                    front_rec.Height = data_lenth;
                    front_rec.Width = this.Area.Width;

                    this.preDataDimensionValue = front_rec.Top;
                }

                //背景
                graphics.FillRectangle(new SolidBrush(this.BackgroundColor.Value), 0, 0, this.Area.Width, this.Area.Height);

                //前景
                graphics.FillRectangle(new SolidBrush(this.FrontColor), front_rec.X - this.Area.X, front_rec.Y - this.Area.Y, front_rec.Width, front_rec.Height);

                graphics.Save();

                screen.RenderBitmap(widget_canvas, this.Area.X, this.Area.Y);

                this.PrevData = data;

            }





        }


        public override void Reset()
        {
            PrevData = null;
        }

    }
}
