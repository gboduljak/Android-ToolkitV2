using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidToolkit.Infrastructure.Helpers;

namespace AndroidToolkit.Infrastructure.Tools
{
    public partial class FastbootTools
    {

        public Task HardReset(string[] imgs, bool createNoWindow = true)
        {
            _cmds = new List<Command>
                {
                    new Command(string.Format("fastboot flash boot {0}", PathGenerator.Generate(imgs[0]))),
                    new Command(string.Format("fastboot flash system {0}", PathGenerator.Generate(imgs[1]))),
                    new Command(string.Format("fastboot flash recovery {0}", PathGenerator.Generate(imgs[2]))),
                    new Command(string.Format("fastboot flash userdata {0}", PathGenerator.Generate(imgs[3]))),
                    new Command("fastboot reboot")
                };
            return _executor.Execute(_cmds, Context, createNoWindow);
        }
    }
}
