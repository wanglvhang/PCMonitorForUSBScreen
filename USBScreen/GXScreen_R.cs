using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBScreen
{
    internal class GXScreen_R : IUSBScreen
    {
        public int ScreenWidth { get; private set; }

        public int ScreenHeight { get; private set; }

        public eScreenStatus Status => throw new NotImplementedException();

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void RenderBitmap(Bitmap img, int posX, int posY)
        {
            throw new NotImplementedException();
        }

        public void RenderPixels(int offsetX, int offsetY, Color pixelColor, byte[] coordinates)
        {
            throw new NotImplementedException();
        }

        public void RenderPixels(Color pixelColor, IEnumerable<Point> points)
        {
            throw new NotImplementedException();
        }

        public void Restart()
        {
            throw new NotImplementedException();
        }

        public void SetBrightness(int brightness)
        {
            throw new NotImplementedException();
        }

        public void SetLandscapeDisplay(bool isInvert)
        {
            throw new NotImplementedException();
        }

        public void SetMirror(bool isMirror)
        {
            throw new NotImplementedException();
        }

        public void SetVerticalDisplay(bool isInvert)
        {
            throw new NotImplementedException();
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        public void Startup()
        {
            throw new NotImplementedException();
        }
    }
}
