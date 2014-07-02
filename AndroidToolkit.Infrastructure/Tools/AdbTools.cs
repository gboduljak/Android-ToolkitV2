using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using AndroidToolkit.Infrastructure.Device;
using AndroidToolkit.Infrastructure.Helpers;
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

        public async Task Execute(string[] cmds, bool createNoWindow = true, string target = null)
        {
            await Context.Dispatcher.InvokeAsync(async () =>
            {
                _cmds = new List<Command>();
                foreach (string cmd in cmds.Where(cmd => !string.IsNullOrEmpty(cmd)))
                {
                    _cmds.Add(new Command(string.Format("{0}", cmd)));
                }
                await _executor.Execute(_cmds, Context, createNoWindow);
            });
        }

        public async Task<string> BuildProp(bool createWindow, string target = null)
        {
            return await _executor.Execute(new Command("adb shell cat /system/build.prop"), createWindow);
        }

        public async Task<string> DeviceName(bool createWindow, string target = null)
        {
            if (!string.IsNullOrEmpty(target))
            {
                return await _executor.Execute(new Command(string.Format(@"adb -s {0} shell getprop ro.product.model", target)), createWindow);
            }
            return await _executor.Execute(new Command("adb shell getprop ro.product.model"), createWindow);
        }

        public async Task<string> DeviceOsVersion(bool createWindow, string target = null)
        {
            if (!string.IsNullOrEmpty(target))
            {
                return await _executor.Execute(new Command(string.Format(@"adb -s {0} shell getprop ro.build.version.release", target)), createWindow);
            }
            return await _executor.Execute(new Command("adb shell getprop ro.build.version.release"), createWindow);
        }

        public async Task<string> DeviceManufacturer(bool createWindow, string target = null)
        {
            if (!string.IsNullOrEmpty(target))
            {
                return await _executor.Execute(new Command(string.Format(@"adb -s {0} shell getprop ro.product.manufacturer", target)), createWindow);
            }
            return await _executor.Execute(new Command("adb shell getprop ro.product.manufacturer"), createWindow);
        }

        public async Task<string> DeviceCodename(bool createWindow, string target = null)
        {
            if (!string.IsNullOrEmpty(target))
            {
                return await _executor.Execute(new Command(string.Format(@"adb -s {0} shell getprop ro.product.name", target)), createWindow);
            }
            return await _executor.Execute(new Command("adb shell getprop ro.product.name"), createWindow);
        }

        public async Task<DeviceInfo> DeviceInfo(bool createWindow, string target = null)
        {
            string name =
                StringLinesRemover.ForgetLastLine(StringLinesRemover.RemoveLine(
                   await DeviceName(createWindow, target), 4));

            string codename =
              StringLinesRemover.ForgetLastLine(StringLinesRemover.RemoveLine(
                 await DeviceCodename(createWindow, target), 4));

            string manufacturer =
                StringLinesRemover.ForgetLastLine(StringLinesRemover.RemoveLine(
                    await DeviceManufacturer(createWindow, target), 4));

            string os = StringLinesRemover.ForgetLastLine(StringLinesRemover.RemoveLine(await DeviceOsVersion(createWindow, target), 4));
            string osDetails = string.Empty;
            string root = string.Empty;
                Parallel.Invoke(async () =>
                {
                    try
                    {
                        root = await _executor.Execute(new Command("adb shell su"), createWindow); root = StringLinesRemover.RemoveLine(root, 4); root = StringLinesRemover.ForgetLastLine(root);
                    }
                    catch
                    {
                        root = string.Empty;
                    }
                   
                }, () => { Thread.Sleep(200); KillAdb(); });
            string buildprop = StringLinesRemover.ForgetLastLine(StringLinesRemover.RemoveLine(await BuildProp(createWindow, target),5));
            bool isRoot = false;
            if (os.Contains("1.5"))
            {
                osDetails = "CUPCAKE";
            }
            if (os.Contains("1.6"))
            {
                osDetails = "DONUT";
            }
            if (os.Contains("2"))
            {
                osDetails = "ECLAIR";
            }
            if (os.Contains("2.2") || os.Contains("2.2.3"))
            {
                osDetails = "FROYO";
            }
            if (os.Contains("2.3"))
            {
                osDetails = "GINGERBREAD";
            }
            if (os.Contains("3.0") || os.Contains("3.1") || os.Contains("3.2"))
            {
                osDetails = "HONEYCOMB";
            }
            if (os.Contains("4.0"))
            {
                osDetails = "ICE CREAM SANDWICH";
            }
            if (os.Contains("4.1") || os.Contains("4.2") || os.Contains("4.3"))
            {
                osDetails = "JELLY BEAN";
            }
            if (os.Contains("4.4"))
            {
                osDetails = "KIT KAT";
            }
            if (root.Contains('#'))
            {
                isRoot = true;
            }
            return new DeviceInfo() { Name = name, AndroidVersionCode = os, AndroidVersionName = osDetails, BuildProp = buildprop, Codename = codename, Manufacturer = manufacturer, IsRooted = isRoot };
        }

        public async Task ListDevices(TextBox context, bool createNoWindow)
        {
            await context.Dispatcher.InvokeAsync(async () =>
                context.Text = StringLinesRemover.ForgetLastLine(StringLinesRemover.RemoveLine(await _executor.Execute(new Command("adb devices")), 5)));
        }

        public static void KillAdb()
        {
            foreach (Process proc in Process.GetProcessesByName("adb"))
            {
                TerminateProcess(proc.Handle, 0);
            }
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
       private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);
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
