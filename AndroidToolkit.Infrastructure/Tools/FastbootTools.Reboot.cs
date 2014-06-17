
using System.Threading.Tasks;

namespace AndroidToolkit.Infrastructure.Tools
{
    public partial class FastbootTools
    {
        public Task Reboot(bool createNoWindow = true)
        {
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () => await _executor.Execute(new Command(string.Format("fastboot reboot")), Context, createNoWindow)));
        }

        public Task RebootRecovery(bool createNoWindow = true, string target = null)
        {
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () => await _executor.Execute(new Command(string.Format("fastboot reboot recovery")), Context, createNoWindow)));
        }

        public Task RebootBootloader(bool createNoWindow = true, string target = null)
        {
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () => await _executor.Execute(new Command(string.Format("fastboot reboot bootloader")), Context, createNoWindow)));
        }
    }
}
