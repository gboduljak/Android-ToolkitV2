using System;

namespace AndroidToolkit.Infrastructure.Device
{
    [Serializable]
    public class RemoteInfo
    {
        public string Address { get; set; }
        public string DeviceName { get; set; }
    }
}