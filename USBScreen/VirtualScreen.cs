using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBScreen
{
    public class VirtualScreen : IUSBScreen
    {
        public int ScreenWidth => 480;

        public int ScreenHeight => 320;

        public Bitmap ResultImage { get { return this.canvas; } }

        public eScreenStatus Status => throw new NotImplementedException();

        private Bitmap canvas;
        private Graphics graphics;
        public VirtualScreen()
        {
            this.canvas = new Bitmap(this.ScreenWidth, this.ScreenHeight);
            this.graphics = Graphics.FromImage(this.canvas);
        }


        public void Connect()
        {
            //
        }

        public void Dispose()
        {
           //
        }

        public void RenderBitmap(Bitmap img, int posX, int posY)
        {
            this.graphics.DrawImage(img, posX, posY);
            this.graphics.Save();
        }

        public void RenderPixels(int offsetX, int offsetY, Color pixelColor, byte[] coordinates)
        {

        }

        public void RenderPixels(Color pixelColor, IEnumerable<Point> points)
        {
            foreach(var p in points)
            {
                this.canvas.SetPixel(p.X, p.Y, pixelColor);
            }
        }

        public void SaveImage()
        {
            this.canvas.Save($"virtual_imgs\\{new Random().Next(100000)}.png");
        }

        public void Shutdown()
        {
            
        }

        public void Startup()
        {
            
        }

        public void Restart()
        {
            
        }

        public void AjustScreen(bool isMirror, bool isLandscape, bool isInvert)
        {

        }

        public void SetBrightness(int brightness)
        {
            throw new NotImplementedException();
        }

        public void SetMirror(bool isMirror)
        {
            throw new NotImplementedException();
        }

        public void SetLandscapeDisplay(bool isInvert)
        {
            throw new NotImplementedException();
        }

        public void SetVerticalDisplay(bool isInvert)
        {
            throw new NotImplementedException();
        }
    }
}
