using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor
{
    public interface IMonitorDataProvider
    {
        float? GetData(eMonitorDataType dataType);
    }
}
