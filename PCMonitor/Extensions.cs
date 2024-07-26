using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PCMonitor;

namespace PCMonitor
{
    public static class Extensions
    {
        public static Color? ToColor(this string colorstr)
        {
            //检查字符串
            if(string.IsNullOrWhiteSpace(colorstr) 
                || colorstr.Length < 6 
                || colorstr.IsValidHex())
            {
                return null;
            }


            var r = Convert.ToInt32(colorstr.Substring(0, 2), 16);
            var g = Convert.ToInt32(colorstr.Substring(2, 2), 16);
            var b = Convert.ToInt32(colorstr.Substring(4, 2), 16);

            return Color.FromArgb(r, g, b);
        }

        public static bool IsValidHex(this string str)
        {
            var pattern = @"([^A-Fa-f0-9]|\s+?)+";

            return System.Text.RegularExpressions.Regex.IsMatch(str, pattern);
        }

        public static T toEnum<T>(this string str) where T : Enum
        {
            if (string.IsNullOrEmpty(str))
                return default(T);

            return (T)Enum.Parse(typeof(T), str);
        }


        public static Bitmap CreateCopyToRGB565(this Bitmap source)
        {
           //return source.Clone(new Rectangle(0, 0, source.Width, source.Height), System.Drawing.Imaging.PixelFormat.Format16bppRgb565);
            var newbitmap = new Bitmap(source.Width, source.Height, PixelFormat.Format16bppRgb565);

            using (var graph = Graphics.FromImage(newbitmap))
            {
                graph.DrawImage(source, 0, 0, source.Width, source.Height);
            }

            return newbitmap;
        }


        public static Bitmap[] GetGifFrames(this Image gif)
        {

            int numberOfFrames = gif.GetFrameCount(FrameDimension.Time);
            Bitmap[] frames = new Bitmap[numberOfFrames];

            for (int i = 0; i < numberOfFrames; i++)
            {
                gif.SelectActiveFrame(FrameDimension.Time, i);
                frames[i] = ((Bitmap)gif.Clone());
            }

            return frames;

        }

        //与目标对比，并以source为准（获取的像素为对于source上的） 获取所有不同的像素
        public static List<Pixel> DiffFrom(this Bitmap source, Bitmap target)
        {
            //var sw = new Stopwatch();
            //sw.Start();

            var changed_pixels = new ConcurrentBag<Pixel>();

            var width = source.Width;
            var height = source.Height;

            var source_reader = new BitmapBytesReader(source);
            var target_reader = new BitmapBytesReader(target);



            //按行并发
            Parallel.For(0, width, (x, state) =>
            {
                //经过测试 如果此处任然使用并发反而会导致性能下降 从 4ms 左右增加到 10ms 左右
                for (int y = 0; y < height; y++)
                {
                    var sc = source_reader.GetPixel(x, y);
                    var tc = target_reader.GetPixel(x, y);

                    if (tc != sc)
                    {
                        changed_pixels.Add(new Pixel(x, y) { Color = sc });
                    }
                }

            });


            //sw.Stop();

            //Debug.WriteLine($"changed_pixels:{sw.ElapsedMilliseconds}ms,{changed_pixels.Count()}pixels");

            return changed_pixels.ToList();
        }


        public static byte[] To565Bytes(this Color color)
        {
            var color_565 =  (int)color.R << 8 & 63488 | (int)color.G << 3 & 2016 | (int)color.B >> 3;

            var bytes = new byte[2];

            bytes[0] = (byte)(color_565 >> 8);//颜色的高八位数据
            bytes[1] = (byte)(color_565 & (int)byte.MaxValue);//颜色的低八位数据

            return bytes;
        }



        //将enum转化为list
        public static List<string> ToList(this Enum e)
        {
            var list = new List<string>();

            return list;
        }



        public static string GetThemeFolder(this ThemePackage tp)
        {
            if (string.IsNullOrWhiteSpace(tp.ThemeName)) throw new ArgumentException("tp.ThemeName 不能为空");

            var themeFolder = Path.Combine(Environment.CurrentDirectory, "themes", tp.ThemeName);

            return themeFolder;

        }


        public static string GetConfigFilePath(this ThemePackage tp)
        {
            if (string.IsNullOrWhiteSpace(tp.ThemeName)) throw new ArgumentException("tp.ThemeName 不能为空");

            var configPath = Path.Combine(Environment.CurrentDirectory, "themes", tp.ThemeName, "config.yaml");

            return configPath;
        }


        public static string GetBGFilePath(this ThemePackage tp)
        {
            if (string.IsNullOrWhiteSpace(tp.ThemeName)) throw new ArgumentException("tp.ThemeName 不能为空");

            var bgPath = Path.Combine(Environment.CurrentDirectory, "themes", tp.ThemeName, "bg.png");

            return bgPath;
        }



    }
}
