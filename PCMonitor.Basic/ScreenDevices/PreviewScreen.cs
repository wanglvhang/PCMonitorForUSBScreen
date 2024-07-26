using PCMonitor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor.Basic.ScreenDevices
{
    [ScreenMeta(DeviceName = "preview", Width = 480, Height = 320)]
    public class PreviewScreen : IScreen
    {
        public int RenderWidth { get; private set; }

        public int RenderHeight { get; private set; }

        public eScreenConnectionStatus Status => eScreenConnectionStatus.Connected;

        public string ConnectionInfo => "N/A";

        public void Connect()
        {
            //检查previews 目录是否存在 不存在则新建
            var folder_path = Path.Combine(Environment.CurrentDirectory, "preview");

            if (!Directory.Exists(folder_path))
            {
                Directory.CreateDirectory(folder_path);
            }

            //初始化canvas
            this.canvas = new Bitmap(this.RenderWidth, this.RenderHeight);

        }

        public void Dispose()
        {

        }


        private Bitmap canvas;


        public void RenderBitmap(Bitmap img, int posX, int posY)
        {
            //将图像绘制到 canvas 
            using (var graph = Graphics.FromImage(this.canvas))
            {
                graph.DrawImage(img, posX, posY);
            }
        }

        public void RenderColor(Rectangle rec, Color color)
        {
            var color_img = new Bitmap(rec.Width, rec.Height);
            using (var g = Graphics.FromImage(this.canvas))
            {
                g.Clear(color);
            }

            using (var graph = Graphics.FromImage(this.canvas))
            {
                graph.DrawImage(color_img, rec.X, rec.Y);
            }
        }

        public void RenderPixels(Color pixelColor, IEnumerable<Point> points)
        {
            foreach(var p in points)
            {
                this.canvas.SetPixel(p.X, p.Y, pixelColor);
            }
        }

        public void RenderPixels(IEnumerable<Pixel> Pixels)
        {
            //using (var graph = Graphics.FromImage(this.canvas))
            //{
            foreach (var p in Pixels)
            {
                this.canvas.SetPixel(p.X, p.Y, p.Color);
            }
            //}

        }

        public void Restart()
        {
            throw new NotImplementedException();
        }

        public void SendRaw(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public void OnFrameEnd()
        {
            //在一帧结束是进行保存  preview.png

            //var file_path 
            //    = Path.Combine(Environment.CurrentDirectory, "preview", $"preview_{DateTime.Now.ToString("yyyyMMddHHmmss")}.png");

            //this.canvas.Save(file_path, ImageFormat.Png);

            var latest_path
                = Path.Combine(Environment.CurrentDirectory, "preview", "preview_latest.png");

            if (File.Exists(latest_path))
            {
                File.Delete(latest_path);
            }

            this.canvas.Save(latest_path, ImageFormat.Png);

        }




        public void SetBrightness(int brightness)
        {

        }

        public void SetLandscapeDisplay(bool isInvert)
        {

        }

        public void SetMirror(bool isMirror)
        {
 
        }

        public void SetDisplay(int width, int height,bool isInvert)
        {
            this.RenderWidth = width;
            this.RenderHeight = height;
        }

        public void SetVerticalDisplay(bool isInvert)
        {
            
        }

        public void Shutdown()
        {
            
        }

        public void Startup()
        {
           
        }
    }
}
