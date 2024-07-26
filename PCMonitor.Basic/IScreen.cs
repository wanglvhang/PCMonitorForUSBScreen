using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor
{
    public interface IScreen:IDisposable
    {

        //屏幕实例信息
        int RenderWidth { get; }
        int RenderHeight { get; }

        eScreenConnectionStatus Status { get; } //连接状态

        string ConnectionInfo { get; } //连接信息


        //该方法只设置屏幕的显示方式，具体设置屏幕的过程应放在Startup中
        void SetDisplay(int width, int height, bool isInvert);

        //屏幕基本操作方法===============================================
        //连接
        void Connect();

        //启动/打开
        void Startup();

        //重启
        void Restart();

        //关闭
        void Shutdown();


        //屏幕显示设置相关方法=============================================
        //镜像设置
        void SetMirror(bool isMirror);

        //设置为横屏
        void SetLandscapeDisplay(bool isInvert);

        //设置为竖屏
        void SetVerticalDisplay(bool isInvert);

        //设置亮度
        void SetBrightness(int brightness);




        //渲染与绘制相关方法==============================================

        //渲染矩形色块
        void RenderColor(Rectangle rec, Color color);

        //渲染同色像素
        void RenderPixels(Color pixelColor, IEnumerable<Point> points);

        //渲染像素集合
        void RenderPixels(IEnumerable<Pixel> Pixels);

        //渲染图片
        void RenderBitmap(Bitmap img, int posX, int posY);

        void SendRaw(byte[] bytes);

        //该方法会在每一帧（即每次循环所有widges渲染完毕后）渲染结束时会调用
        void OnFrameEnd();

    }


    public enum eScreenConnectionStatus
    {
        UnKnown,
        Connected,
        NotFound,
        Error
    }


    public class Pixel
    {
        public Pixel(Point point, Color color) {
            this.X = point.X;
            this.Y = point.Y;
            Point = point;
            Color = color;
        }

        public Pixel(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Point = new Point(X, Y);
        }

        public int X;

        public int Y;
        public Point Point { get; private set; }
        public Color Color { get; set; }
    }


}
