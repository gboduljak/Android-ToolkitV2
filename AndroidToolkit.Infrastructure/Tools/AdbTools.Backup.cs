using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidToolkit.Infrastructure.Helpers;

namespace AndroidToolkit.Infrastructure.Tools
{
    public partial class AdbTools
    {
        public async Task Backup(string name, string location, AdbBackupMode mode, bool createNoWindow, string target = null)
        {
            await Context.Dispatcher.InvokeAsync(async () =>
            {
                string pathlocation = PathGenerator.Generate(location, name);
                await Task.Run(async () =>
                {
                    if (mode == AdbBackupMode.All)
                    {
                        await _executor.Execute(new Command(string.Format("adb -s {1} backup -all -f {0} ", pathlocation, target)), Context, createNoWindow);
                    }
                    else if (mode == AdbBackupMode.Apps)
                    {
                        await _executor.Execute(new Command(string.Format("adb -s {1} backup -apk -f {0} ", pathlocation, target)), Context, createNoWindow);
                    }
                    else if (mode == AdbBackupMode.AppsWithoutSystemApps)
                    {
                        await _executor.Execute(new Command(string.Format("adb -s {1} backup -apk -nosystem -f {0} ", pathlocation, target)), Context, createNoWindow);
                    }
                    else if (mode == AdbBackupMode.SystemApps)
                    {
                        await _executor.Execute(new Command(string.Format("adb -s {1} backup -apk -system -f {0} ", pathlocation, target)), Context, createNoWindow);
                    }
                    else
                    {
                        await _executor.Execute(new Command(string.Format("adb -s {1} pull /sdcard/ {0} ", location, target)), Context, createNoWindow);
                    }
                });
            });
        }
        public async Task Restore(string name, bool createNoWindow, string target = null)
        {
            await Context.Dispatcher.InvokeAsync(async () =>
            {
                await Task.Run(async () =>
                {
                    await _executor.Execute(new Command(string.Format("adb -s {1} restore {0} ", name, target)), Context, createNoWindow);
                });
            });
        }
    }
}
