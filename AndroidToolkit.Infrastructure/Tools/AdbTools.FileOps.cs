using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using AndroidToolkit.Infrastructure.Adapters;
using AndroidToolkit.Infrastructure.Helpers;
using AndroidToolkit.Infrastructure.Utilities;

namespace AndroidToolkit.Infrastructure.Tools
{
    public partial class AdbTools
    {
        public async Task Push(string[] filePaths, string location, bool createNoWindow, string target = null)
        {
            if (!string.IsNullOrEmpty(target))
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    _cmds = new List<Command>(filePaths.Length);
                    foreach (var finalPath in filePaths)
                    {
                        _cmds.Add(new Command(string.Format("adb -s {2} push {0} {1}", finalPath, location, target)));
                    }
                    await Task.Run(() => _executor.Execute(_cmds, Context, createNoWindow));
                });
            }
            else
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    _cmds = new List<Command>(filePaths.Length);
                    foreach (var finalPath in filePaths)
                    {
                        _cmds.Add(new Command(string.Format("adb push {0} {1}", finalPath, location)));
                    }
                    await Task.Run(() => _executor.Execute(_cmds, Context, createNoWindow));
                });
            }
        }

        public async Task Copy(string filePath, string location, bool createNoWindow, string target = null)
        {
            if (!string.IsNullOrEmpty(target))
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    await Task.Run(() => _executor.Execute(new Command(string.Format("adb -s {2} shell cp {0} {1}", filePath, location, target)), Context, createNoWindow));
                });
            }
            else
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    await Task.Run(() => _executor.Execute(new Command(string.Format("adb shell cp {0} {1}", filePath, location)), Context, createNoWindow));
                });
            }
        }

        public async Task Move(string filePath, string location, bool createNoWindow, string target = null)
        {
            if (!string.IsNullOrEmpty(target))
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    await Task.Run(() => _executor.Execute(new Command(string.Format("adb -s {2} shell mv {0} {1}", filePath, location, target)), Context, createNoWindow));
                });
            }
            else
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    await Task.Run(() => _executor.Execute(new Command(string.Format("adb shell mv {0} {1}", filePath, location)), Context, createNoWindow));
                });
            }


        }

        public async Task Pull(string location, bool createNoWindow, string[] paths, string target = null)
        {
            if (!string.IsNullOrEmpty(target))
            {
                location = PathGenerator.Generate(location);
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    _cmds = new List<Command>(paths.Length);
                    foreach (string path in paths.Where(x => !string.IsNullOrEmpty(x)))
                    {
                        _cmds.Add(new Command(string.Format("adb -s {2} pull {0} {1}", path, location, target)));
                    }
                    await Task.Run(() => _executor.Execute(_cmds, Context, createNoWindow));
                });
            }
            else
            {
                location = PathGenerator.Generate(location);
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    _cmds = new List<Command>(paths.Length);
                    foreach (string path in paths.Where(x => !string.IsNullOrEmpty(x)))
                    {
                        _cmds.Add(new Command(string.Format("adb pull {0} {1}", path, location)));
                    }
                    await Task.Run(() => _executor.Execute(_cmds, Context, createNoWindow));
                });
            }
        }

        public async Task Delete(string filePath, bool createNoWindow, string target = null)
        {
            if (!string.IsNullOrEmpty(target))
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    await Task.Run(() => _executor.Execute(new Command(string.Format("adb -s {1} shell rm {0}", filePath, target)), Context, createNoWindow));
                });
            }
            else
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    await Task.Run(() => _executor.Execute(new Command(string.Format("adb shell rm {0}", filePath)), Context, createNoWindow));
                });
            }
        }

        public async Task Sideload(string filePath, bool createNoWindow, string target = null)
        {
            if (!string.IsNullOrEmpty(target))
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    filePath = PathGenerator.Generate(filePath);
                    await Task.Run(() => _executor.Execute(new Command(string.Format("adb sideload {0} -s {1}", filePath, target)), Context, createNoWindow));
                });
            }
            else
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    filePath = PathGenerator.Generate(filePath);
                    await Task.Run(() => _executor.Execute(new Command(string.Format("adb sideload {0} ", filePath)), Context, createNoWindow));
                });
            }
        }

        public async Task Logcat(TextBox logcatText, bool createNoWindow, string target = null)
        {
            if (!string.IsNullOrEmpty(target))
            {
                await logcatText.Dispatcher.InvokeAsync(() =>
                {
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += async (sender, e) => await this.ExecuteLogcat(new Command(string.Format("adb -s {0} shell logcat",target)), logcatText, createNoWindow);
                    worker.RunWorkerAsync();
                });
            }
            else
            {
                await logcatText.Dispatcher.InvokeAsync(() =>
                {
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += async (sender, e) => await this.ExecuteLogcat(new Command(string.Format("adb shell logcat")), logcatText, createNoWindow);
                    worker.RunWorkerAsync();
                });
            }
        }

        private Task ExecuteLogcat(Command cmd, TextBox ctx, bool createNoWindow = true)
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
    }
}
