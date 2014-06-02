using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidToolkit.Data
{
    public struct DeviceInfo
    {
        public string Name;
        public string Manufacturer;
        public string AndroidVersionName;
        public string AndroidVersionCode;
        public string BuildProp;
        public bool IsRooted;
    }
}
