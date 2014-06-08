using System;

namespace AndroidToolkit.Infrastructure.Device
{
    [Serializable]
    public class DeviceInfo
    {
        public string Name { get; set; }
        public string Codename { get; set; }
        public string Manufacturer { get; set; }
        public string AndroidVersionName { get; set; }
        public string AndroidVersionCode { get; set; }
        public string BuildProp { get; set; }
        public bool IsRooted { get; set; }
    }
}
