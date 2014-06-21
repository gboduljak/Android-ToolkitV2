using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AndroidToolkit.Infrastructure.Helpers;
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

        public async Task ListDevices(TextBox context, bool createNoWindow)
        {
            await context.Dispatcher.InvokeAsync(async () =>
                context.Text = StringLinesRemover.ForgetLastLine(StringLinesRemover.RemoveLine(await _executor.Execute(new Command("fastboot devices")), 5)));
        }

        public Task Prepare(bool createNoWindow = true)
        {
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () => await _executor.Execute(new Command("fastboot devices"), Context, createNoWindow)));
        }

        public Task Execute(string cmd, bool createNoWindow = true)
        {
            return Task.Run(() => Context.Dispatcher.InvokeAsync(async () =>
            {
                await _executor.Execute(new Command(cmd), Context, createNoWindow);
            }));
        }

        public Task Execute(IEnumerable<string> cmds, bool createNoWindow = true)
        {
            return Task.Run(() => Context.Dispatcher.InvokeAsync(async () =>
            {
                foreach (var cmd in cmds)
                {
                    await _executor.Execute(new Command(cmd), Context, createNoWindow); 
                }
            }));
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
