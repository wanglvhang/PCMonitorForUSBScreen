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

        //屏幕的属性
        //屏幕需要在startup后设置好屏幕的默认的宽高像素
        int ScreenWidth { get; }
        int ScreenHeight { get; }

        eScreenStatus Status { get; }

        string COMName { get; }

        //屏幕常规操作

        //连接, 该方法需要在生成serialport实例后 调用open方法来测试连接是否成功
        void Connect();

        //启动/打开
        void Startup();

        //重启
        void Restart();

        //关闭
        void Shutdown();


        //void AjustScreen(bool isMirror, bool isLandscape, bool isInvert);

        //镜像设置
        void SetMirror(bool isMirror);

        //设置为横屏
        void SetLandscapeDisplay(bool isInvert);

        //设置为竖屏
        void SetVerticalDisplay(bool isInvert);

        //设置亮度
        void SetBrightness(int brightness);



        //渲染与绘制相关方法

        void RenderPixels(int offsetX, int offsetY, Color pixelColor, byte[] coordinates);

        void RenderPixels(Color pixelColor, IEnumerable<Point> points);

        void RenderBitmap(Bitmap img, int posX, int posY);

        void SendCMD(byte[] data);


        //绘制圆弧


        //绘制直线


        //绘制文字


        //绘制矩形


    }


    public enum eScreenStatus
    {
        UnKnown,
        Connected,
        NotFound,
        Error
    }


}
