using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor
{
    public interface IMonitorDataProvider
    {

        //获取监控数据
        DataForRender GetData(eMonitorDataType dataType);
        
        //获取superio风扇列表
        List<string> GetFans();

        //获取监控数据前，必须使用appconfig初始化
        void Initialize(AppConfig config);

    }
}
