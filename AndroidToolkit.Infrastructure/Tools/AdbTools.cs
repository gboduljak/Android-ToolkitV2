using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using AndroidToolkit.Infrastructure.Utilities;

namespace AndroidToolkit.Infrastructure.Tools
{
    public partial class AdbTools
    {
        private ICommandExecutor _executor;

        private Command _cmd;

        private IList<Command> _cmds;

        public AdbTools(TextBlock context)
        {
            this.Context = context;
            _cmd = new Command();
            _cmds = new List<Command>();
            _executor = new CommandExecutor();
        }

        public AdbTools()
        {
            _cmd = new Command();
            _cmds = new List<Command>();
            _executor = new CommandExecutor();
        }

        public TextBlock Context { get; set; }

        public async Task Prepare(bool createNoWindow = true)
        {
            await Context.Dispatcher.InvokeAsync(async () =>
             {
                 await _executor.Execute(new Command("adb devices"), Context, createNoWindow);
             });
        }

        public async Task Execute(bool createNoWindow = true, string target = null, params string[] cmds)
        {
            await Context.Dispatcher.InvokeAsync(async () =>
            {
                _cmds = new List<Command>();
                for (int i = 0; i < cmds.Count(); i++)
                {
                    _cmds.Add(new Command(string.Format("{0} ", cmds[i])));
                }
                await _executor.Execute(_cmds, Context, createNoWindow);
            });
        }

        public async Task<bool> CheckRoot(string targetId = null, bool createNoWindow = true, string target = null)
        {
            string output = string.Empty;
            if (!string.IsNullOrEmpty(target))
            {
                await Context.Dispatcher.InvokeAsync(() => Parallel.Invoke(async () => { output = await _executor.Execute(new Command(string.Format("adb -s {0} shell su", target)), createNoWindow); }, () =>
                {
                    Thread.Sleep(2000);
                    KillAdb();
                }));
            }
            else
            {
                await Context.Dispatcher.InvokeAsync(() => Parallel.Invoke(async () => { output = await _executor.Execute(new Command("adb shell su"), createNoWindow); }, () =>
                {
                    Thread.Sleep(2000);
                    KillAdb();
                }));
            }
            return output.Contains('#');
        }

        private static void KillAdb()
        {
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Count(); i++)
            {
                if (processes[i].ProcessName.ToLower().Contains("adb"))
                {
                    processes[i].Kill();
                    return;
                }
            }
        }

        ~AdbTools()
        {
            this._executor = null;
            this.Context = null;
            this._cmd = null;
            this._cmds.Clear();
            this._cmds = null;
            GC.Collect();
        }


    }
}
