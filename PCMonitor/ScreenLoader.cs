using PCMonitor.Basic.ScreenDevices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor
{
    public class ScreenLoader
    {
        public static (IScreen, ScreenMetaAttribute) Load(ThemeConfig themeConfig, AppConfig appConfig, string overrideDevice = null)
        {
            IScreen screen = null;
            ScreenMetaAttribute screenMeta = null;

            if (overrideDevice is null)
            {
                overrideDevice = themeConfig.device;
            }


            switch (overrideDevice)
            {
                case "inch35":
                    screen = new Device3_5();
                    var attr = screen.GetType().GetCustomAttributes(typeof(ScreenMetaAttribute), false).FirstOrDefault();
                    var meta_attr = attr as ScreenMetaAttribute;
                    //初始化类型
                    screenMeta = meta_attr;

                    screen.SetDisplay(themeConfig.width, themeConfig.height, appConfig.ScreenInvert);

                    return (screen, screenMeta);
                default:
                    return (null, null);
            }


            ////使用反射获取目下 screendevice.*.dll的程序集并在其中找到对应devicename的 iscreen 实现
            //var files = Directory.GetFiles(Environment.CurrentDirectory);

            //foreach (var file in files)
            //{
            //    var fi = new FileInfo(file);

            //    if (fi.Name.ToLower().StartsWith($"screendevice.") && fi.Extension == ".dll")
            //    {
            //        var assembly = System.Reflection.Assembly.LoadFile(fi.FullName);

            //        var screen_types = assembly.GetTypes().Where(t => typeof(IScreen).IsAssignableFrom(t) && !t.IsInterface).ToList();

            //        foreach (var t in screen_types)
            //        {
            //            var attr = t.GetCustomAttributes(typeof(ScreenMetaAttribute), false).FirstOrDefault();

            //            if (attr != null)
            //            {
            //                var meta_attr = attr as ScreenMetaAttribute;

            //                if (meta_attr.DeviceName.ToLower() == overrideDevice)
            //                {
            //                    //初始化类型
            //                    screenMeta = meta_attr;
            //                    screen = Activator.CreateInstance(t) as IScreen;

            //                    screen.SetDisplay(themeConfig.width, themeConfig.height, appConfig.ScreenInvert);

            //                    return (screen, screenMeta);
            //                }
            //            }
            //        }

            //    }
            //}


            ////throw new Exception("无法成功找到主题指定的设备，请检查主题配置中device设置是否正确。");
            //return (null, null);


        }


        //public static ScreenMetaAttribute GetScreenMeta(string deviceName)
        //{
        //    //读取当前文件夹  中的device meta
        //    var files = Directory.GetFiles(Environment.CurrentDirectory);

        //    foreach (var file in files)
        //    {
        //        var fi = new FileInfo(file);

        //        if (fi.Name.ToLower().StartsWith($"screendevice.") && fi.Extension == ".dll")
        //        {
        //            var assembly = System.Reflection.Assembly.LoadFile(fi.FullName);

        //            var screen_types = assembly.GetTypes().Where(t => typeof(IScreen).IsAssignableFrom(t) && !t.IsInterface).ToList();

        //            foreach (var t in screen_types)
        //            {
        //                var attr = t.GetCustomAttributes(typeof(ScreenMetaAttribute), false).FirstOrDefault();

        //                if (attr != null)
        //                {
        //                    var meta_attr = attr as ScreenMetaAttribute;

        //                    if (meta_attr.DeviceName == deviceName)
        //                    {
        //                        return meta_attr;
        //                    }
        //                }
        //            }

        //        }
        //    }

        //    return null;
        //}


        //获取本地程序中的设备
        //public static List<ScreenMetaAttribute> LoadDeviceMetas()
        //{
        //    var result = new List<ScreenMetaAttribute>();

        //    //读取当前文件夹  中的device meta
        //    var files = Directory.GetFiles(Environment.CurrentDirectory);

        //    foreach (var file in files)
        //    {
        //        var fi = new FileInfo(file);

        //        if (fi.Name.ToLower().StartsWith($"screendevice.") && fi.Extension == ".dll")
        //        {
        //            var assembly = System.Reflection.Assembly.LoadFile(fi.FullName);

        //            var screen_types = assembly.GetTypes().Where(t => typeof(IScreen).IsAssignableFrom(t) && !t.IsInterface).ToList();

        //            foreach (var t in screen_types)
        //            {
        //                var attr = t.GetCustomAttributes(typeof(ScreenMetaAttribute), false).FirstOrDefault();

        //                if (attr != null)
        //                {
        //                    var meta_attr = attr as ScreenMetaAttribute;

        //                    result.Add(meta_attr);

        //                    //if (meta_attr.DeviceName.ToLower() == overrideDevice)
        //                    //{
        //                    //    //初始化类型
        //                    //    screenMeta = meta_attr;
        //                    //    screen = Activator.CreateInstance(t) as IScreen;

        //                    //    screen.SetRenderResolution(config.width, config.height);

        //                    //    return (screen, screenMeta);
        //                    //}
        //                }
        //            }

        //        }
        //    }



        //    return result;

        //}



    }
}
