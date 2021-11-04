using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBScreen;

namespace PCMonitor.Widgets
{
    public class PercentBar : WidgetBase
    {
        public override eWidgetType WidgetType =>  eWidgetType.PercentBar;


        //private Rectangle prevFrontRec;

        private int preDataDimensionValue;

        //
        public PercentBar(eMonitorDataType dataType, Rectangle area, Color frontColor, Color bgColor)
        {
            //宽高取长的作为数据显示
            this.DataType = dataType;
            this.Area = area;
            this.FrontColor = frontColor;
            this.BackgroundColor = bgColor;
        }


        //所需的数据为0~100的浮点数
        public override void Render(IUSBScreen screen, Bitmap widget_canvas, DataForRender data)
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

                //}
                //else
                //{

                //    //检查数值是变大了还是变小了，为了节省性能，只绘制变化区域，若变小则绘制成背景色，若变大则设置为前景色
                //    //根据之前的的变化区域获取本次的变化区域
                //    var changed_rec = new Rectangle();
                //    var isIncrease = data.Num > prevData.Num ? true : false;


                //    int changed_length = Convert.ToInt32(Math.Abs((data.Num - prevData.Num).Value) * bar_length / 100f);

                //    //若无变化则不选软
                //    if (changed_length == 0)
                //    {
                //        this.prevData = data;//保留数据微小的变化
                //        return;
                //    }

                //    if (isHorizontal && isIncrease)
                //    {
                //        changed_rec.X = preDataDimensionValue + changed_length;
                //        changed_rec.Y = this.Area.Y;
                //        changed_rec.Width = changed_length;
                //        changed_rec.Height = this.Area.Height;

                //        this.preDataDimensionValue += changed_length;

                //        graphics.FillRectangle(new SolidBrush(this.FrontColor), changed_rec);
                //    }
                //    else if (isHorizontal && !isIncrease)
                //    {
                //        changed_rec.X = preDataDimensionValue - changed_length;
                //        changed_rec.Y = this.Area.Y;
                //        changed_rec.Width = changed_length;
                //        changed_rec.Height = this.Area.Height;

                //        this.preDataDimensionValue -= changed_length;

                //        graphics.FillRectangle(new SolidBrush(this.BackgroundColor.Value), changed_rec);
                //    }
                //    else if (!isHorizontal && isIncrease)
                //    {
                //        changed_rec.X = this.Area.X;
                //        changed_rec.Y = preDataDimensionValue + changed_length;
                //        changed_rec.Width = this.Area.Width;
                //        changed_rec.Height = changed_length;

                //        this.preDataDimensionValue += changed_length;

                //        graphics.FillRectangle(new SolidBrush(this.FrontColor), changed_rec);
                //    }
                //    else if (!isHorizontal && !isIncrease)
                //    {
                //        changed_rec.X = this.Area.X;
                //        changed_rec.Y = preDataDimensionValue - changed_length;
                //        changed_rec.Width = this.Area.Width;
                //        changed_rec.Height = changed_length;

                //        this.preDataDimensionValue -= changed_length;

                //        graphics.FillRectangle(new SolidBrush(this.BackgroundColor.Value), changed_rec);
                //    }

                //    graphics.Save();

                //    var changed_bitmap = widget_canvas.Clone(changed_rec, PixelFormat.Format16bppRgb565);

                //    screen.RenderBitmap(changed_bitmap, changed_rec.X, changed_rec.Y);


                //}

                //prevData = data;

            }





        }


        public override void Reset()
        {
            PrevData = null;
        }

    }
}
