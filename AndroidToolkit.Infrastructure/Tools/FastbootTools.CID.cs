using System.Threading.Tasks;

namespace AndroidToolkit.Infrastructure.Tools
{
    public partial class FastbootTools
    {
        public Task<string> GetCid(bool createNoWindow = true)
        {
            return Task.Run(async () => await _executor.Execute(new Command("fastboot oem getvar cid")));
        }

        public Task WriteCid(string cid, bool createNoWindow = true)
        {
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () =>
                await _executor.Execute(new Command(string.Format("fastboot oem writecid {0}", cid)), Context, createNoWindow)));
        }
    }

}
