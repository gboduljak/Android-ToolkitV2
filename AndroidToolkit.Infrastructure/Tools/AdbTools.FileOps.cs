using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AndroidToolkit.Infrastructure.Helpers;

namespace AndroidToolkit.Infrastructure.Tools
{
    public partial class AdbTools
    {
        public async Task Push(string filePath, string location, bool createNoWindow, string target = null)
        {
            await Context.Dispatcher.InvokeAsync(async () =>
            {
                filePath = PathGenerator.Generate(filePath);
                await Task.Run(() => _executor.Execute(new Command(string.Format("adb -s {2} push {0} {1}", filePath, location, target)), Context, createNoWindow));
            });
        }
        public async Task Copy(string filePath, string location, bool createNoWindow, string target = null)
        {
            await Context.Dispatcher.InvokeAsync(async () =>
            {
                await Task.Run(() => _executor.Execute(new Command(string.Format("adb -s {2} shell cp {0} {1}", filePath, location, target)), Context, createNoWindow));
            });
        }

        public async Task Move(string filePath, string location, bool createNoWindow, string target = null)
        {
            await Context.Dispatcher.InvokeAsync(async () =>
            {
                await Task.Run(() => _executor.Execute(new Command(string.Format("adb -s {2} shell mv {0} {1}", filePath, location, target)), Context, createNoWindow));
            });
        }

        public async Task Pull(string location, bool createNoWindow, string[] paths, string target = null)
        {
            location = PathGenerator.Generate(location);
            await Context.Dispatcher.InvokeAsync(async () =>
            {
                _cmds = new List<Command>(paths.Length)
                {
                    new Command(string.Format("adb -s {2} pull {0} {1}", paths[0], location,target)),
                    new Command(string.Format("adb -s {2} pull {0} {1}", paths[1], location,target)),
                    new Command(string.Format("adb -s {2} pull {0} {1}", paths[2], location,target)),
                    new Command(string.Format("adb -s {2} pull {0} {1}", paths[3], location,target))
                };
                await Task.Run(() => _executor.Execute(_cmds, Context, createNoWindow));
            });
        }

        public async Task Delete(string filePath, bool createNoWindow, string target = null)
        {
            await Context.Dispatcher.InvokeAsync(async () =>
            {
                await Task.Run(() => _executor.Execute(new Command(string.Format("adb -s {1} shell rm {0}", filePath, target)), Context, createNoWindow));
            });
        }


        public async Task Sideload(string filePath, bool createNoWindow, string target = null)
        {
            await Context.Dispatcher.InvokeAsync(async () =>
            {
                filePath = PathGenerator.Generate(filePath);
                await Task.Run(() => _executor.Execute(new Command(string.Format("adb sideload {0} -s {1}", filePath, target)), Context, createNoWindow));
            });
        }
    }
}
