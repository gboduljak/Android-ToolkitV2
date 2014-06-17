using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AndroidToolkit.Infrastructure.Utilities;

namespace AndroidToolkit.Infrastructure.Tools
{
    public partial class FastbootTools
    {
        public FastbootTools()
        {
            _executor = new CommandExecutor();
            _cmd = new Command();
            _cmds = new List<Command>();
        }

        public FastbootTools(TextBlock context)
        {
            Context = context;
            _executor = new CommandExecutor();
            _cmd = new Command();
            _cmds = new List<Command>();
        }

        public Task Prepare(bool createNoWindow = true)
        {
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () => await _executor.Execute(new Command("fastboot devices"), Context, createNoWindow)));
        }

        public static void Kill()
        {
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Count(); i++)
            {
                if (processes[i].ProcessName.ToLower().Contains("fastboot"))
                {
                    try
                    {
                        processes[i].Kill();
                    }
                    catch
                    {

                    }
                    return;
                }
            }
        }

        #region Fields

        private ICommandExecutor _executor;

        private Command _cmd;

        private IList<Command> _cmds;

        protected TextBlock Context;

        #endregion

        #region Destructor

        ~FastbootTools()
        {
            this._executor = null;
            this.Context = null;
            this._cmd = null;
            this._cmds.Clear();
            this._cmds = null;
            GC.Collect();
        }
        #endregion
    }
}
