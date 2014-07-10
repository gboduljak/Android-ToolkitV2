using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AndroidToolkit.Infrastructure.Tools;
using AndroidToolkit.Wpf.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace AndroidToolkit.Wpf.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private RelayCommand<MetroWindow> _showAdbCommand;
        public RelayCommand<MetroWindow> ShowAdbCommand
        {
            get
            {
                return _showAdbCommand ?? (_showAdbCommand = new RelayCommand<MetroWindow>(async (window) =>
                {
                    if (!IsWindowOpen<AdbView>())
                    {
                        new AdbView().Show();
                    }
                    else
                    {
                        await window.ShowMessageAsync("Notification", "ADB is already opened.");
                    }
                }));
            }
            set
            {
                if (this._showAdbCommand != value)
                {
                    RaisePropertyChanging(() => this.ShowAdbCommand);
                    this._showAdbCommand = value;
                    RaisePropertyChanged(() => this.ShowAdbCommand);
                }
            }
        }

        private RelayCommand<MetroWindow> _showFastbootCommand;
        public RelayCommand<MetroWindow> ShowFastbootCommand
        {
            get
            {
                return _showFastbootCommand ?? (_showFastbootCommand = new RelayCommand<MetroWindow>(async (window) =>
                {
                    if (!IsWindowOpen<FastbootView>())
                    {
                        new FastbootView().Show();
                    }
                    else
                    {
                        await window.ShowMessageAsync("Notification", "Fastboot is already opened.");
                    }
                }));
            }
            set
            {
                if (this._showFastbootCommand != value)
                {
                    RaisePropertyChanging(() => ShowFastbootCommand);
                    this._showFastbootCommand = value;
                    RaisePropertyChanged(() => ShowFastbootCommand);
                }
            }
        }


        private RelayCommand<MetroWindow> _showDeviceRestoreCommand;
        public RelayCommand<MetroWindow> ShowDeviceRestoreCommand
        {
            get
            {
                return _showDeviceRestoreCommand ?? (_showDeviceRestoreCommand = new RelayCommand<MetroWindow>(async (window) =>
                {
                    if (!IsWindowOpen<FastbootView>())
                    {
                        new FastbootView(5).Show();
                    }
                    else
                    {
                        await window.ShowMessageAsync("Notification", "Fastboot is already opened.");
                    }
                }));
            }
            set
            {
                if (this._showDeviceRestoreCommand != value)
                {
                    RaisePropertyChanging(() => ShowDeviceRestoreCommand);
                    this._showDeviceRestoreCommand = value;
                    RaisePropertyChanged(() => ShowDeviceRestoreCommand);
                }
            }
        }

        #region ListDevices

        private RelayCommand<TransitioningContentControl> _listAdbDevicesCommand;
        public RelayCommand<TransitioningContentControl> ListAdbDevicesCommand
        {
            get
            {
                return _listAdbDevicesCommand ?? (_listAdbDevicesCommand = new RelayCommand<TransitioningContentControl>(
                    async (control) =>
                    {
                        await control.Dispatcher.InvokeAsync(() => control.Content = new TextBlock()
                        {
                            Style = Application.Current.Resources["FooterTextStyle"] as Style,
                            Text = "GETTING LIST..."
                        });
                        await control.Dispatcher.InvokeAsync(async () => control.Content = new TextBox().Text = await Adb.ListDevices(false));
                    }));
            }

            set
            {
                if (this._listAdbDevicesCommand != value)
                {
                    RaisePropertyChanging(() => ListAdbDevicesCommand);
                    this._listAdbDevicesCommand = value;
                    RaisePropertyChanged(() => ListAdbDevicesCommand);
                }
            }
        }

        private RelayCommand<TransitioningContentControl> _listFastbootDevicesCommand;
        public RelayCommand<TransitioningContentControl> ListFastbootDevicesCommand
        {
            get
            {
                return _listFastbootDevicesCommand ?? (_listFastbootDevicesCommand = new RelayCommand<TransitioningContentControl>(
                    async (control) =>
                    {
                        await control.Dispatcher.InvokeAsync(() => control.Content= new TextBlock()
                        {
                            Style = Application.Current.Resources["FooterTextStyle"] as Style,
                            Text = "GETTING LIST..."
                        } );
                        await control.Dispatcher.InvokeAsync(async () => control.Content = new TextBox().Text = await Fastboot.ListDevices(false));
                    }));
            }

            set
            {
                if (this._listFastbootDevicesCommand != value)
                {
                    RaisePropertyChanging(() => ListFastbootDevicesCommand);
                    this._listFastbootDevicesCommand = value;
                    RaisePropertyChanged(() => ListFastbootDevicesCommand);
                }
            }
        }
        #endregion

        #region Tools

        private AdbTools _adb;
        public AdbTools Adb
        {
            get { return _adb ?? (_adb = new AdbTools()); }
            set
            {
                if (_adb != value)
                {
                    RaisePropertyChanging(() => Adb);
                    this._adb = value;
                    RaisePropertyChanged(() => Adb);
                }
            }
        }

        private FastbootTools _fastboot;
        public FastbootTools Fastboot
        {
            get { return _fastboot ?? (_fastboot = new FastbootTools()); }
            set
            {
                if (_fastboot != value)
                {
                    RaisePropertyChanging(() => Fastboot);
                    this._fastboot = value;
                    RaisePropertyChanged(() => Fastboot);
                }
            }
        }

        #endregion

        private static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }
    }
}
