using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AndroidToolkit.Infrastructure.Tools
{
    public partial class AdbTools
    {
        public async Task Reboot(bool createNoWindow = true, string target = null)
        {
            await Context.Dispatcher.InvokeAsync(async () =>
            {
                await Task.Run(() => _executor.Execute(new Command(string.Format("adb reboot -s {0}", target)), Context, createNoWindow));
            });
        }

        public async Task RebootRecovery(bool createNoWindow = true, string target = null)
        {
            await Context.Dispatcher.InvokeAsync(async () =>
            {
                await Task.Run(() => _executor.Execute(new Command(string.Format("adb reboot recovery -s {0}", target)), Context, createNoWindow));
            });
        }

        public async Task RebootBootloader(bool createNoWindow = true, string target = null)
        {
            await Context.Dispatcher.InvokeAsync(async () =>
            {
                await Task.Run(() => _executor.Execute(new Command(string.Format("adb reboot bootloader -s {0}", target)), Context, createNoWindow));
            });
        }
    }
}
