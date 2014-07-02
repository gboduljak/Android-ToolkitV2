using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using AndroidToolkit.Infrastructure.Tools;
using AndroidToolkit.Wpf.Presentation.Converters;
using FileDialog = AndroidToolkit.Infrastructure.Helpers.FileDialog;
using TextBox = System.Windows.Controls.TextBox;

namespace AndroidToolkit.Wpf.Presentation.Presenter
{
    public static class FastbootPresenter
    {
        public static void Prepare(object parameter)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                var textBlock = parameter as TextBlock;
                if (textBlock != null)
                    await textBlock.Dispatcher.InvokeAsync(async () =>
                    {
                        Fastboot = new FastbootTools(textBlock);
                        await Fastboot.Prepare();
                    });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void Kill(object parameter)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                var textBlock = parameter as TextBlock;
                if (textBlock != null)
                    await textBlock.Dispatcher.InvokeAsync(async () =>
                    {
                        FastbootTools.Kill();
                        await textBlock.Dispatcher.InvokeAsync(() => textBlock.Text = "FASTBOOT KILLED");
                    });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        #region UI

        public static async void ExecuteClearImmediate(object parameter)
        {
            TextBlock context = (TextBlock)parameter;
            await context.Dispatcher.InvokeAsync(() => context.Text = string.Empty);
        }

        #endregion

        #region File

        public static void OpenFile(object parameter)
        {
            TextBox context = parameter as TextBox;
            string file = FileDialog.ShowDialog(true);
            if (context != null) context.Dispatcher.Invoke(() => { context.Text = file; });
        }

        public static void OpenImg(object parameter)
        {
            TextBox context = parameter as TextBox;
            string file = FileDialog.ShowDialog("Android Flashable Image (*.img)|*.img;*|Android Flashable Zip (*.zip)|*.zip;|Android Unlocktoken Binary (*.bin)|*.bin;", false);
            if (context != null) context.Dispatcher.Invoke(() => { context.Text = file; });
        }

        public static void SaveFile(object parameter)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            TextBox context = parameter as TextBox;
            if (context != null) context.Dispatcher.Invoke(() => { context.Text = dialog.SelectedPath; });
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
                await Context.Dispatcher.InvokeAsync(() => { Fastboot = new FastbootTools(Context); });
                await Task.Run(async () => await Fastboot.Reboot(parameters.Bool));
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
                await Context.Dispatcher.InvokeAsync(() => { Fastboot = new FastbootTools(Context); });
                await Task.Run(async () => await Fastboot.RebootRecovery(parameters.Bool));
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
                await Context.Dispatcher.InvokeAsync(() => { Fastboot = new FastbootTools(Context); });
                await Task.Run(async () => await Fastboot.RebootBootloader(parameters.Bool));
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        #endregion

        #region Bootloader

        public static void Lock(object parameter)
        {
            SingleCommandParameters parameters = (SingleCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    Fastboot = new FastbootTools(Context);
                    await Fastboot.Lock(parameters.Bool);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void Unlock(object parameter)
        {
            SingleCommandParameters parameters = (SingleCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    Fastboot = new FastbootTools(Context);
                    await Fastboot.Unlock(parameters.Bool);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void ExecuteToken(object parameter)
        {
            UIParameters parameters = (UIParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(() => { Fastboot = new FastbootTools(Context); });
                await
                    Task.Run(
                        async () =>
                            await Fastboot.GetIdentiferToken(parameters.Context2, parameters.Bool));
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        #endregion

        #region Boot

        public static void Boot(object parameter)
        {
            TwoCommandParameters parameters = (TwoCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    Fastboot = new FastbootTools(Context);
                    await Fastboot.Boot(parameters.Text, parameters.Bool);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        #endregion

        #region Flash

        public static void FlashSystem(object parameter)
        {
            TwoCommandParameters parameters = (TwoCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    Fastboot = new FastbootTools(Context);
                    await Fastboot.FlashSystem(parameters.Text, parameters.Bool);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void FlashBoot(object parameter)
        {
            TwoCommandParameters parameters = (TwoCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    Fastboot = new FastbootTools(Context);
                    await Fastboot.FlashBoot(parameters.Text, parameters.Bool);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void FlashBootloader(object parameter)
        {
            TwoCommandParameters parameters = (TwoCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    Fastboot = new FastbootTools(Context);
                    await Fastboot.FlashBootloader(parameters.Text, parameters.Bool);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void FlashRecovery(object parameter)
        {
            TwoCommandParameters parameters = (TwoCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    Fastboot = new FastbootTools(Context);
                    await Fastboot.FlashRecovery(parameters.Text, parameters.Bool);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void FlashRadio(object parameter)
        {
            TwoCommandParameters parameters = (TwoCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    Fastboot = new FastbootTools(Context);
                    await Fastboot.FlashRadio(parameters.Text, parameters.Bool);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void FlashUserdata(object parameter)
        {
            TwoCommandParameters parameters = (TwoCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    Fastboot = new FastbootTools(Context);
                    await Fastboot.FlashUserdata(parameters.Text, parameters.Bool);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void FlashUnlockToken(object parameter)
        {
            TwoCommandParameters parameters = (TwoCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    Fastboot = new FastbootTools(Context);
                    await Fastboot.FlashUnlockToken(parameters.Text, parameters.Bool);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void FlashZip(object parameter)
        {
            TwoCommandParameters parameters = (TwoCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    Fastboot = new FastbootTools(Context);
                    await Fastboot.FlashZip(parameters.Text, parameters.Bool);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        #endregion

        #region Erase

        public static void EraseBoot(object parameter)
        {
            SingleCommandParameters parameters = (SingleCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    Fastboot = new FastbootTools(Context);
                    await Fastboot.EraseBoot(parameters.Bool);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void EraseSystem(object parameter)
        {
            SingleCommandParameters parameters = (SingleCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    Fastboot = new FastbootTools(Context);
                    await Fastboot.EraseSystem(parameters.Bool);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void EraseRecovery(object parameter)
        {
            SingleCommandParameters parameters = (SingleCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    Fastboot = new FastbootTools(Context);
                    await Fastboot.EraseRecovery(parameters.Bool);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void EraseCache(object parameter)
        {
            SingleCommandParameters parameters = (SingleCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    Fastboot = new FastbootTools(Context);
                    await Fastboot.EraseCache(parameters.Bool);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        public static void EraseUserdata(object parameter)
        {
            SingleCommandParameters parameters = (SingleCommandParameters)parameter;
            Context = parameters.Context;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += async (sender, args) =>
            {
                await Context.Dispatcher.InvokeAsync(async () =>
                {
                    Fastboot = new FastbootTools(Context);
                    await Fastboot.EraseUserdata(parameters.Bool);
                });
            };
            worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
            worker.RunWorkerAsync();
        }

        #endregion

        #region Execute
        public static void Execute(object parameter)
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
                        Fastboot = new FastbootTools(Context);
                        await Fastboot.Execute(parameters.Text, parameters.Bool);
                    });
                };
                worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
                worker.RunWorkerAsync();
            }

        }
        public static void Execute2(object parameter)
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
                        Fastboot = new FastbootTools(Context);
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
                        await Fastboot.Execute(cmds, parameters.Bool);
                    });
                };
                worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
                worker.RunWorkerAsync();
            }

        }

        #endregion

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
                        Fastboot = new FastbootTools(Context);
                        await Fastboot.ListDevices(parameters.Context2, parameters.Bool);
                    });
                };
                worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
                worker.RunWorkerAsync();
            }
        }

        public static TextBlock Context { get; set; }

        public static FastbootTools Fastboot { get; set; }
    }
}
