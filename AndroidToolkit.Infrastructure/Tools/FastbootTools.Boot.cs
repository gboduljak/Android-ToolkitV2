using System.Threading.Tasks;
using AndroidToolkit.Infrastructure.Helpers;

namespace AndroidToolkit.Infrastructure.Tools
{
    public partial class FastbootTools
    {
        public Task Boot(string img, bool createNoWindow = true)
        {
            img = PathGenerator.Generate(img);
            return Task.Run(() => Context.Dispatcher.InvokeAsync(async () =>
                await _executor.Execute(new Command(string.Format("fastboot boot {0}", img)), Context, createNoWindow)));
        }
    }
}
