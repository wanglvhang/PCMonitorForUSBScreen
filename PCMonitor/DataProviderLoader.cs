using PCMonitor.Basic.DataProviders;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor
{
    public class DataProviderLoader
    {
        //
        public static IMonitorDataProvider Load(AppConfig config)
        {
            //反射   DataProvider.{providerName} , 当前目录下
            //var files = Directory.GetFiles(Environment.CurrentDirectory);

            IMonitorDataProvider mdp = null;

            switch (config.DataProviderName)
            {
                case "LibreHardwareMonitor":
                    mdp = new LibreDataProvider();
                    mdp.Initialize(config);
                    return mdp;
                default:
                    return null;
            }

            //foreach (var file in files)
            //{
            //    var fi = new FileInfo(file);

            //    if (fi.Name.ToLower().StartsWith($"dataprovider.") && fi.Extension == ".dll")
            //    {

            //        var assembly = System.Reflection.Assembly.LoadFile(fi.FullName);

            //        var provider_types = assembly.GetTypes().Where(t => typeof(IMonitorDataProvider).IsAssignableFrom(t) && !t.IsInterface).ToList();

            //        foreach(var type in provider_types)
            //        {
            //            //var provider_type = assembly.GetTypes().Where(t => typeof(IMonitorDataProvider).IsAssignableFrom(t)).FirstOrDefault();

            //            var attr = type.GetCustomAttributes(typeof(DataProviderMetaAttribute), false).FirstOrDefault();

            //            if (attr != null)
            //            {
            //                var meta_attr = attr as DataProviderMetaAttribute;

            //                if (meta_attr.ProviderName.ToLower() == config.DataProviderName.ToLower())
            //                {
            //                    //初始化类型
            //                    var mdp = Activator.CreateInstance(type) as IMonitorDataProvider;

            //                    mdp.Initialize(config);

            //                    //TODO: 检查分辨率是否OK
            //                    return mdp;
            //                }
            //            }
            //        }
            //    }
            //}

            //throw new Exception("can't find monitor data provider type");

        }
    }
}
