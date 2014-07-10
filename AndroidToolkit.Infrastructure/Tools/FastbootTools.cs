using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
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

        public async Task<string> ListDevices(bool createNoWindow)
        {
            return StringLinesRemover.ForgetLastLine(StringLinesRemover.RemoveLine(await _executor.Execute(new Command("fastboot devices")), 5));
        }

        public Task Prepare(bool createNoWindow = true)
        {
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () => await _executor.Execute(new Command("fastboot devices"), Context, createNoWindow)));
        }

        #region Bootloader

        public Task Lock(bool createNoWindow = true)
        {
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () => await _executor.Execute(new Command("fastboot oem lock"), Context, createNoWindow)));
        }

        public Task Unlock(bool createNoWindow = true)
        {
            return Task.Run(async () => await Context.Dispatcher.InvokeAsync(async () => await _executor.Execute(new Command("fastboot oem unlock"), Context, createNoWindow)));
        }

        //public async Task<string> GetIdentifierToken(bool createNoWindow = true)
        //{
        //    string token = string.Empty;
        //    await Task.Run(() => Parallel.Invoke(async () => await Context.Dispatcher.InvokeAsync(async () => token = StringLinesRemover.FitString(StringLinesRemover.ForgetLastLine(StringLinesRemover.RemoveLine(await _executor.Execute(new Command("fastboot oem get_identifier_token"), createNoWindow), 5)))), () => Thread.Sleep(1000), Kill));
        //    return token;
        //}

        public async Task GetIdentiferToken(TextBox tokenText, bool createNoWindow)
        {
            await tokenText.Dispatcher.InvokeAsync(() =>
                {
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += async (sender, e) => await this.ExecuteGetIdentiferToken(new Command(string.Format("fastboot oem get_identifier_token")), tokenText, createNoWindow);
                    worker.RunWorkerAsync();
                });
        }

        private Task ExecuteGetIdentiferToken(Command cmd, TextBox ctx, bool createNoWindow = true)
        {
            return Task.Run(async () =>
            {
                Process process = new Process
                {
                    StartInfo =
                    {
                        FileName = "cmd.exe",
                        UseShellExecute = false,
                        CreateNoWindow = createNoWindow,
                        RedirectStandardOutput = true,
                        RedirectStandardInput = true
                    }
                };
                process.OutputDataReceived += async (sender, args) =>
                {
                    try
                    {
                        await ctx.Dispatcher.InvokeAsync(() => ctx.Text = ctx.Text + "\n" + StringLinesRemover.RemoveCmdData(args.Data), DispatcherPriority.Background);
                    }
                    catch
                    {

                    }
                };
                process.Start();
                process.BeginOutputReadLine();
                await ctx.Dispatcher.InvokeAsync(() => ctx.Text = string.Empty);
                process.StandardInput.WriteLine(cmd.Text);
                process.WaitForExit();
            });
        }

        #endregion

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
