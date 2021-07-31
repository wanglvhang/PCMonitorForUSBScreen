using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //foreach (T item in Enum.GetValues(typeof(T)))
            //{
            //    if (item.ToString().ToLower().Equals(value.Trim().ToLower())) return item;
            //}
            //return defaultValue;
        }
    }
}
