using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    await Task.Run(() => _executor.Execute(new Command(string.Format("adb logcat -v long > logcat.txt -s {0}", target)), Context, createNoWindow));
                    await Task.Run(async () =>
                    {
                        string logcat = StringLinesRemover.ForgetLastLine(StringLinesRemover.RemoveLine(await _executor.Execute(new Command(string.Format("adb logcat -v long > logcat.txt -s {0}", target)), createNoWindow), 4));
                        await logcatText.Dispatcher.InvokeAsync(() => logcatText.Text = logcat);
                    });
                });
            }
            else
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    await Context.Dispatcher.InvokeAsync(async () =>
                    {
                        await Task.Run(() => _executor.Execute(new Command(string.Format("adb logcat -v long > logcat.txt")), Context, createNoWindow));
                        await Task.Run(async () =>
                        {
                            string logcat = StringLinesRemover.ForgetLastLine(StringLinesRemover.RemoveLine(File.ReadAllText("logcat.txt"), 4));
                            await logcatText.Dispatcher.InvokeAsync(() => logcatText.Text = logcat);
                        });
                    });
                });
            }
        }
    }
}
