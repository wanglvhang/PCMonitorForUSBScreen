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
        int ScreenWidth { get; }
        int ScreenHeight { get; }
        void Connect();

        //屏幕需要在startup后设置好屏幕的默认的宽高像素
        void Startup();

        void Shutdown();

        void RenderPixels(int offsetX, int offsetY, Color pixelColor, byte[] coordinates);

        void RenderPixels(Color pixelColor, IEnumerable<Point> points);

        void RenderBitmap(Bitmap img, int posX, int posY);
    }
}
