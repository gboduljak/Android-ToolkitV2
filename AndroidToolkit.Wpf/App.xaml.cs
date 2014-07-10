using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using AndroidToolkit.Infrastructure.Tools;
using AndroidToolkit.Log;
using AndroidToolkit.Wpf.Presentation.Controls;
using AndroidToolkit.Wpf.View;

namespace AndroidToolkit.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += async (sender, args) =>
            {
                _ex = (Exception)args.ExceptionObject;
                await Current.Dispatcher.InvokeAsync(() =>
                {
                    Error error = new Error
                    {
                        ErrorTitle = _ex.GetType().Name,
                        ErrorContent =
                            string.Format(
                                "*** ERROR on {0} ***\n\n{1}\n\n*** ERROR ***\n\n*** INNER EXCEPTION ***\n\n{2}\n\n*** INNER EXCEPTION ***\n\n*** STACK TRACE ***\n\n{3}\n\n*** STACK TRACE ***\n\n",
                                DateTime.Now, _ex.Message, _ex.InnerException, _ex.StackTrace)
                    };
                    error.ShowDialog();
                });
            };

            Current.LoadCompleted += async (sender, e) =>
            {
                await Current.Dispatcher.InvokeAsync(async () =>
                {
                    await _adb.Prepare();
                    await _fastboot.Prepare();
                });
            };


        }

        private Exception _ex;
        private AdbTools _adb = new AdbTools();
        private FastbootTools _fastboot = new FastbootTools();

        ~App()
        {
            _ex = null;
            _adb = null;
            _fastboot = null;
        }
    }


}
