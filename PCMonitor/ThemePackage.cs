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
using Newtonsoft.Json;

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

                var di = new DirectoryInfo(theme_folder);
                tpackage.ThemeName = di.Name;

                var all_files = Directory.GetFiles(theme_folder);

                foreach (var file in all_files)
                {
                    var fi = new FileInfo(file);

                    if (fi.Name == "config.yaml")
                    {
                        var deserializer = new DeserializerBuilder()
                                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                .Build();

                        var theme_config = deserializer.Deserialize<ThemeConfig>(File.ReadAllText(fi.FullName));
                        tpackage.Config = theme_config;

                        //var theme_config = JsonConvert.DeserializeObject<ThemeConfig>(File.ReadAllText(theme_yaml_path));
                    }
                    else if (fi.Name == "bg.png")
                    {
                        //读取图片到内存，释放对文件资源的占用
                        //var bg_bytes = File.ReadAllBytes(fi.FullName);
                        var bg_stream = new MemoryStream(File.ReadAllBytes(fi.FullName));
                        tpackage.Background = new Bitmap(bg_stream,false);
                    }
                    else
                    {
                        tpackage.Resources.Add(fi.Name, File.ReadAllBytes(fi.FullName));
                    }

                }

                return tpackage;
            }
            else
            {
                throw new Exception("theme folder not exist");
            }

        }

        public static ThemePackage New()
        {
            var result = new ThemePackage();

            return result;
        }



        private ThemePackage()
        {
            this.Config = new ThemeConfig();
            this.Resources = new Dictionary<string, byte[]>();
        }


        public string ThemeName { get;  set; }

        //public string ThemeFolder { get; private set; }

        //themeconfig
        public ThemeConfig Config { get; private set; }

        //background
        public Bitmap Background { get; set; }

        //resouce, theme中 除 config.yaml 和 bg.png 之外的所有文件
        public Dictionary<string, byte[]> Resources { get; private set; }

    }


    public class ThemeConfig
    {

        public ThemeConfig()
        {
            this.Widgets = new List<WidgetConfig>();
        }

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
        public float StartAngle { get; set; }
        //扫过角度
        public float SweepAngle { get; set; }
        //内径
        public int ArcWidth { get; set; }


        //专门为tgus 添加的配置========================================
        public string Address { get; set; } //16进制变量地址
        public string valueType { get; set; } //用于输出数值到数值类型控件
        public int StrLength { get; set; } //字符串长度


        public AnimationStage[] Animation { get; set; }



    }

    public class AnimationStage
    {
        //public string Name { get; set; } //为添加过渡动画，所以需要name来确定 过渡动画

        public float MinVal { get; set; }

        public float MaxVal { get; set; }

        public string GifName { get; set; }

    }


    //Transition 动画widget stage 过渡动画配置  


}
