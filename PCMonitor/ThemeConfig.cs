﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor
{
    public class ThemeConfig
    {
        public string device { get; set; } 

        public int width { get; set; }

        public int height { get; set; }

        public List<WidgetConfig> Widgets { get; set; }

    }

    public class WidgetConfig
    {
        //共用配置=========================================
        public eWidgetType Type { get; set; }

        public eMonitorDataType Data { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        //FF_FF_FF, 具体为RGB
        public string FrontColor { get; set; }
        public string BackgroundColor { get; set; }


        //波形图支持参数======================================
        public int BorderWidth { get; set; }

        public string BorderColor { get; set; }

        //字符串所需参数=======================================
        public string TextFontFamily { get; set; }
        //
        public float TextSize { get; set; }

        //Regular Bold Italic Underline Strikeout
        public FontStyle TextStyle { get; set; }

        //水平位置 Near  Center Far
        public StringAlignment TextAlignment { get; set; }

        //垂直位置 Near  Center Far
        public StringAlignment TextLineAlignment { get; set; }



    }

}
