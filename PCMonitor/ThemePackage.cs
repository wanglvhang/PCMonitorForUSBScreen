using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace PCMonitor
{
    public class ThemePackage
    {
        //保存为一个文件


        //读取.theme
        //public static ThemePackage FromFile(string packagePath)
        //{
        //    var tpakcage = new ThemePackage();
        //}

        public static ThemePackage FromFolder(string theme_folder)
        {
            if (Directory.Exists(theme_folder))
            {
                var tpackage = new ThemePackage();

                var folder_name = Path.GetDirectoryName(theme_folder);

                var theme_yaml_path = $"{theme_folder}\\config.yaml";
                var bg_path = $"{theme_folder}\\bg.png";

                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)// see height_in_inches in sample yml 
                    .Build();

                var theme_config = deserializer.Deserialize<ThemeConfig>(File.ReadAllText(theme_yaml_path));

                tpackage.ThemeName = folder_name;
                tpackage.Config = theme_config;

                if (File.Exists(bg_path))
                {
                    tpackage.Background = new Bitmap(bg_path);
                }

                return tpackage;
            }
            else
            {
                throw new Exception("theme folder not exist");
            }

        }



        private ThemePackage()
        {
            this.Config = new ThemeConfig();


        }


        public string ThemeName { get; private set; }

        
        //all files


        //themeconfig
        public ThemeConfig Config { get; private set; }


        //background
        public Bitmap Background { get; private set; }


        //gifs
    }


    public class ThemeConfig
    {
        public string device { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public bool isDataOnly { get; set; } = false; //是否值输出数据到串口，对于这种设备则不进行屏保调用，无论设置如何

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


        //波形图支持参数============arc也可以=========percentbar也可以=================
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

        //arc所需参数============================================
        //开始角度

        //结束角度

        //内径

        //外径


        //专门为tgus 添加的配置========================================
        public string Address { get; set; } //16进制变量地址
        public string valueType { get; set; } //用于输出数值到数值类型控件
        public int StrLength { get; set; } //字符串长度



    }


}
