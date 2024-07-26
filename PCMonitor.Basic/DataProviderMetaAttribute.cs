using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PCMonitor
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DataProviderMetaAttribute:Attribute
    {
        public string ProviderName { get; set; }

    }
}
