using System.Threading.Tasks;
using AndroidToolkit.Infrastructure.Helpers;

namespace AndroidToolkit.Infrastructure.Tools
{
    public partial class FastbootTools
    {
        public async Task<string> GetCid(bool createNoWindow = true)
        {
            return StringLinesRemover.FitString(StringLinesRemover.ForgetLastLine(StringLinesRemover.RemoveLine(await Task.Run(async () => await _executor.Execute(new Command("fastboot oem getvar cid"))), 5)));
        }

        public Task WriteCid(string cid, bool createNoWindow = true)
        {
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () =>
                await _executor.Execute(new Command(string.Format("fastboot oem writecid {0}", cid)), Context, createNoWindow)));
        }
    }

}
