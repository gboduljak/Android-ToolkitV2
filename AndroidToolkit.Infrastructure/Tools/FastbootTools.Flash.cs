using System.IO;
using System.Threading.Tasks;
using AndroidToolkit.Infrastructure.Helpers;

namespace AndroidToolkit.Infrastructure.Tools
{
    public partial class FastbootTools
    {
        public Task FlashRecovery(string img, bool createNoWindow = true)
        {
            img = PathGenerator.Generate(img);
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () =>
                await _executor.Execute(new Command(string.Format("fastboot flash recovery {0}", img)),Context,createNoWindow)));
        }

        public Task FlashBoot(string img, bool createNoWindow = true)
        {
            img = PathGenerator.Generate(img);
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () =>
                await _executor.Execute(new Command(string.Format("fastboot flash boot {0}", img)), Context, createNoWindow)));
        }

        public Task FlashSystem(string img, bool createNoWindow = true)
        {
            img = PathGenerator.Generate(img);
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () =>
                await _executor.Execute(new Command(string.Format("fastboot flash system {0}", img)), Context, createNoWindow)));
        }

        public Task FlashBootloader(string img, bool createNoWindow = true)
        {
            img = PathGenerator.Generate(img);
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () =>
                await _executor.Execute(new Command(string.Format("fastboot flash bootloader {0}", img)), Context, createNoWindow)));
        }

        public Task FlashRadio(string img, bool createNoWindow = true)
        {
            img = PathGenerator.Generate(img);
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () =>
                await _executor.Execute(new Command(string.Format("fastboot flash radio {0}", img)), Context, createNoWindow)));
        }

        public Task FlashUserdata(string img, bool createNoWindow = true)
        {
            img = PathGenerator.Generate(img);
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () =>
                await _executor.Execute(new Command(string.Format("fastboot flash userdata {0}", img)), Context, createNoWindow)));
        }

        public Task FlashUnlockToken(string img, bool createNoWindow = true)
        {
            img = PathGenerator.Generate(img);
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () =>
                await _executor.Execute(new Command(string.Format("fastboot flash unlocktoken {0}", img)), Context, createNoWindow)));
        }

        public Task FlashZip(string img, bool createNoWindow = true)
        {
            img = PathGenerator.Generate(img);
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () =>
                await _executor.Execute(new Command(string.Format("fastboot flash {0}", img)), Context, createNoWindow)));
        }
    }
}
