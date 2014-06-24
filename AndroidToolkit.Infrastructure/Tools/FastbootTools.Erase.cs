using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidToolkit.Infrastructure.Tools
{
    public partial class FastbootTools
    {
        public Task EraseSystem(bool createNoWindow = true)
        {
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () =>
                await _executor.Execute(new Command(string.Format("fastboot erase system")), Context, createNoWindow)));
        }

        public Task EraseBoot(bool createNoWindow = true)
        {
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () =>
                await _executor.Execute(new Command(string.Format("fastboot erase boot")), Context, createNoWindow)));
        }

        public Task EraseRecovery(bool createNoWindow = true)
        {
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () =>
                await _executor.Execute(new Command(string.Format("fastboot erase recovery")), Context, createNoWindow)));
        }

        public Task EraseCache(bool createNoWindow = true)
        {
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () =>
                await _executor.Execute(new Command(string.Format("fastboot erase cache")), Context, createNoWindow)));
        }

        public Task EraseUserdata(bool createNoWindow = true)
        {
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () =>
                await _executor.Execute(new Command(string.Format("fastboot erase userdata")), Context, createNoWindow)));
        }
    }
}
