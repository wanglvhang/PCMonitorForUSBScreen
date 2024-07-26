using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBScreen
{
    public interface IUSBScreen:IDisposable
    {

        //屏幕实例信息
        int RenderWidth { get; }
        int RenderHeight { get; }

        eScreenConnectionStatus Status { get; } //连接状态

        string ConnectionInfo { get; } //连接信息

        void SetRenderResolution(int width, int height);

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

        void RenderPixels(Color pixelColor, IEnumerable<Point> points);

        //渲染像素集合？
        void RenderPixels(IEnumerable<Pixel> Pixels);

        //渲染图片
        void RenderBitmap(Bitmap img, int posX, int posY);

        void SendRaw(byte[] bytes);

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
            Point = point;
            Color = color;
        }

        public Point Point { get; private set; }
        public Color Color { get; private set; }
    }


}
