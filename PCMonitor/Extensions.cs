using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USBScreen;

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


        public static Bitmap CreateCopy(this Bitmap source)
        {
           return source.Clone(new Rectangle(0, 0, source.Width, source.Height), System.Drawing.Imaging.PixelFormat.Format16bppRgb565);
        }

        //与目标对比，并以source为准（获取的像素为对于source上的） 获取所有不同的像素
        public static IList<Pixel> DiffFrom(this Bitmap source, Bitmap target)
        {
            return null;
        }

    }
}
