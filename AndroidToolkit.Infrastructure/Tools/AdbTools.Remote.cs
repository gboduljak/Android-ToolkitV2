using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidToolkit.Infrastructure.Tools
{
    public partial class AdbTools
    {
        public Task RemoteConnect(string ip, string port, bool createNoWindow = false, string target = null)
        {
            return Task.Run(async () =>
            {
                await Context.Dispatcher.InvokeAsync(async () => await _executor.Execute(new Command(string.Format("adb connect {0}:{1}", ip, port)), Context, createNoWindow));
            });
        }

        public Task RemoteDisconnect(bool createNoWindow = false)
        {
            return Task.Run(async () =>
            {
                await Context.Dispatcher.InvokeAsync(async () => await _executor.Execute(new Command("adb usb"), Context, createNoWindow));
            });
        }
    }
}
