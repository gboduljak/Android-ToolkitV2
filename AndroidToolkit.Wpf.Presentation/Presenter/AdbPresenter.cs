using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using AndroidToolkit.Infrastructure;
using AndroidToolkit.Infrastructure.Tools;
using AndroidToolkit.Wpf.Presentation.Converters;
using FileDialog = AndroidToolkit.Infrastructure.Helpers.FileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using TextBox = System.Windows.Controls.TextBox;

namespace AndroidToolkit.Wpf.Presentation.Presenter
{
    public class AdbPresenter
    {
        public static TextBlock Context { get; set; }

        private static AdbTools _adb;

        public static void ExecuteListDevices(object parameter)
        {
            UIParameters parameters = (UIParameters)parameter;
            if (parameters != null)
            {
                Context = parameters.Context;
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += async (sender, args) =>
                {
                    await Context.Dispatcher.InvokeAsync(async () =>
                    {
                        _adb = new AdbTools(Context);
                        await _adb.ListDevices(parameters.Context2, parameters.Bool);
                    });
                };
                worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
                worker.RunWorkerAsync();
            }
        }

        public async static void ExecuteKillAdb(object parameter)
        {
            await Task.Factory.StartNew(async () =>
            {
                AdbTools.KillAdb();
                var temp = parameter as TextBlock;
                if (temp != null)
                    await
                        temp.Dispatcher.InvokeAsync(() => temp.Text = "ADB KILLED");
                temp = null;
            });
        }

        #region UI


        public static async void ExecuteClearImmediate(object parameter)
        {
            TextBlock context = (TextBlock)parameter;
            await context.Dispatcher.InvokeAsync(() => context.Text = string.Empty);
        }

        public static void ExecutePrepare(object parameter)
        {
            Context = (TextBlock)parameter;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(() => { _adb = new AdbTools(Context); });
                await Task.Run(async () => await _adb.Prepare());
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        #endregion

        #region Reboot

        public static void ExecuteReboot(object parameter)
        {
            SingleCommandParameters parameters = (SingleCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(() => { _adb = new AdbTools(Context); });
                await Task.Run(async () => await _adb.Reboot(parameters.Bool, parameters.Target));
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void ExecuteRebootRecovery(object parameter)
        {

            SingleCommandParameters parameters = (SingleCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(() => { _adb = new AdbTools(Context); });
                await Task.Run(async () => await _adb.RebootRecovery(parameters.Bool, parameters.Target));
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void ExecuteRebootBootloader(object parameter)
        {

            SingleCommandParameters parameters = (SingleCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(() => { _adb = new AdbTools(Context); });
                await Task.Run(async () => await _adb.RebootBootloader(parameters.Bool, parameters.Target));
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        #endregion

        #region File

        public static void OpenFile(object parameter)
        {
            TextBox context = parameter as TextBox;
            string file = FileDialog.ShowDialog(true);
            if (context != null) context.Dispatcher.Invoke(() => { context.Text = file; });
        }

        public static void OpenApp(object parameter)
        {
            TextBox context = parameter as TextBox;
            string file = FileDialog.ShowDialog("APK Android Package File (.apk)|*.apk", false);
            if (context != null) context.Dispatcher.Invoke(() => { context.Text = file; });
        }

        public static void SaveFile(object parameter)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            TextBox context = parameter as TextBox;
            if (context != null) context.Dispatcher.Invoke(() => { context.Text = dialog.SelectedPath; });
        }
        public static void OpenBackup(object parameter)
        {
            TextBox context = parameter as TextBox;
            string file = FileDialog.ShowDialog("Android backup (.ab)|*.ab", false);
            if (context != null) context.Dispatcher.Invoke(() => { context.Text = file; });
        }

        #endregion

        #region Apks

        public static void ExecuteInstall(object parameter)
        {
            TwoCommandParameters parameters = (TwoCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(() => { _adb = new AdbTools(Context); });
                await
                    Task.Run(
                        async () =>
                            await _adb.InstallApk(parameters.Text, parameters.Bool, parameters.Bool2, parameters.Target));
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void ExecuteUninstall(object parameter)
        {
            TwoCommandParameters parameters = (TwoCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(() => { _adb = new AdbTools(Context); });
                await
                    Task.Run(
                        async () =>
                            await _adb.RemoveApk(parameters.Text, parameters.Bool, parameters.Bool2, parameters.Target));
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void ExecuteListApps(object parameter)
        {
            UIParameters parameters = (UIParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(() => { _adb = new AdbTools(Context); });
                await
                    Task.Run(
                        async () =>
                            await _adb.ListApps(parameters.Context2, parameters.Bool, parameters.Target));
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        #endregion

        #region Copy/Move/Delete
        public static void ExecuteCopy(object parameter)
        {
            ThreeTextCommandParameters parameters = (ThreeTextCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(() => { _adb = new AdbTools(Context); });
                await
                    Task.Run(
                        async () =>
                            await _adb.Copy(parameters.Text, parameters.Text2, parameters.Bool, parameters.Target));
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void ExecuteMove(object parameter)
        {
            ThreeTextCommandParameters parameters = (ThreeTextCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(() => { _adb = new AdbTools(Context); });
                await
                    Task.Run(
                        async () =>
                            await _adb.Move(parameters.Text, parameters.Text2, parameters.Bool, parameters.Target));
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void ExecuteDelete(object parameter)
        {
            TwoCommandParameters parameters = (TwoCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(() => { _adb = new AdbTools(Context); });
                await
                    Task.Run(
                        async () =>
                            await _adb.Delete(parameters.Text, parameters.Bool, parameters.Target));
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void ExecuteSideload(object parameter)
        {
            TwoCommandParameters parameters = (TwoCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(() => { _adb = new AdbTools(Context); });
                await
                    Task.Run(
                        async () =>
                            await _adb.Sideload(parameters.Text, parameters.Bool, parameters.Target));
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void ExecuteLogcat(object parameter)
        {
            UIParameters parameters = (UIParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(() => { _adb = new AdbTools(Context); });
                await
                    Task.Run(
                        async () =>
                            await _adb.Logcat(parameters.Context2, parameters.Bool, parameters.Target));
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }
        #endregion

        public static void ExecutePush(object parameter)
        {
            ThreeTextCommandParameters parameters = (ThreeTextCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(() => { _adb = new AdbTools(Context); });
                await
                    Task.Run(
                        async () =>
                            await _adb.Push(parameters.Text.Split(','), parameters.Text2, parameters.Bool, parameters.Target));
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void ExecutePull(object parameter)
        {
            FiveTextCommandParameters parameters = (FiveTextCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    _adb = new AdbTools(Context);
                    String[] paths = new string[4];
                    if (!string.IsNullOrEmpty(parameters.Text2)) paths[0] = parameters.Text2;
                    if (!string.IsNullOrEmpty(parameters.Text3)) paths[1] = parameters.Text3;
                    if (!string.IsNullOrEmpty(parameters.Text4)) paths[2] = parameters.Text4;
                    if (!string.IsNullOrEmpty(parameters.Text5)) paths[3] = parameters.Text5;
                    await _adb.Pull(parameters.Text, parameters.Bool, paths, parameters.Target);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void Execute(object parameter)
        {
            ExecuteCommandParameters parameters = parameter as ExecuteCommandParameters;
            if (parameters != null)
            {
                Context = parameters.Context;
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += async (sender, args) =>
                {
                    await Context.Dispatcher.InvokeAsync(async () =>
                    {
                        _adb = new AdbTools(Context);
                        var cmds = new List<string>
                        {
                            parameters.Text,
                            parameters.Text2,
                            parameters.Text3,
                            parameters.Text4,
                            parameters.Text5,
                            parameters.Text6,
                            parameters.Text7,
                            parameters.Text8,
                            parameters.Text9,
                            parameters.Text10
                        };
                        await _adb.Execute(cmds.ToArray<string>(), parameters.Bool, parameters.Target);
                    });
                };
                worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
                worker.RunWorkerAsync();
            }

        }

        public static void ExecuteSingleCommand(object parameter)
        {
            TwoCommandParameters parameters = parameter as TwoCommandParameters;
            if (parameters != null)
            {
                Context = parameters.Context;
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += async (sender, args) =>
                {
                    await Context.Dispatcher.InvokeAsync(async () =>
                    {
                        _adb = new AdbTools(Context);
                        var cmds = new string[1];
                        cmds[0] = parameters.Text;
                        await _adb.Execute(cmds, parameters.Bool, parameters.Target);
                    });
                };
                worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
                worker.RunWorkerAsync();
            }
        }

        #region Backup/Restore

        public static void ExecuteBackup(object parameter)
        {
            BackupParameters parameters = parameter as BackupParameters;
            if (parameters != null)
            {
                Context = parameters.Context;
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += async (sender, args) =>
                {
                    await Context.Dispatcher.InvokeAsync(async () =>
                    {
                        _adb = new AdbTools(Context);
                        if (parameters.Context2.SelectedIndex == 0)
                        {
                            await _adb.Backup(parameters.Text, parameters.Text2, AdbBackupMode.All, parameters.Bool, parameters.Target);
                        }
                        if (parameters.Context2.SelectedIndex == 1)
                        {
                            await _adb.Backup(parameters.Text, parameters.Text2, AdbBackupMode.Apps, parameters.Bool, parameters.Target);
                        }
                        if (parameters.Context2.SelectedIndex == 2)
                        {
                            await _adb.Backup(parameters.Text, parameters.Text2, AdbBackupMode.SystemApps, parameters.Bool, parameters.Target);
                        }
                        if (parameters.Context2.SelectedIndex == 3)
                        {
                            await _adb.Backup(parameters.Text, parameters.Text2, AdbBackupMode.AppsWithoutSystemApps, parameters.Bool, parameters.Target);
                        }
                        if (parameters.Context2.SelectedIndex == 4)
                        {
                            await _adb.Backup(parameters.Text, parameters.Text2, AdbBackupMode.SDCard, parameters.Bool, parameters.Target);
                        }
                    });
                };
                worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
                worker.RunWorkerAsync();
            }
        }

        public static void ExecuteRestore(object parameter)
        {
            TwoCommandParameters parameters = parameter as TwoCommandParameters;
            if (parameters != null)
            {
                Context = parameters.Context;
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += async (sender, args) =>
                {
                    await Context.Dispatcher.InvokeAsync(async () =>
                    {
                        _adb = new AdbTools(Context);
                        await _adb.Restore(parameters.Text, parameters.Bool, parameters.Target);
                    });
                };
                worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
                worker.RunWorkerAsync();
            }
        }

        #endregion

        #region Remote

        public static void ExecuteRemoteConnect(object parameter)
        {
            ThreeTextCommandParameters parameters = parameter as ThreeTextCommandParameters;
            if (parameters != null)
            {
                Context = parameters.Context;
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += async (sender, args) =>
                {
                    await Context.Dispatcher.InvokeAsync(async () =>
                    {
                        _adb = new AdbTools(Context);
                        await _adb.RemoteConnect(parameters.Text, parameters.Text2, parameters.Bool, parameters.Target);
                    });
                };
                worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
                worker.RunWorkerAsync();
            }
        }

        public static void ExecuteRemoteDisconnect(object parameter)
        {
            SingleCommandParameters parameters = parameter as SingleCommandParameters;
            if (parameters != null)
            {
                Context = parameters.Context;
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += async (sender, args) =>
                {
                    await Context.Dispatcher.InvokeAsync(async () =>
                    {
                        _adb = new AdbTools(Context);
                        await _adb.RemoteDisconnect(parameters.Bool);
                    });
                };
                worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
                worker.RunWorkerAsync();
            }
        }


        #endregion
    }
}