using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                      await window.ShowMessageAsync("Notification","ADB is already opened.");
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

        private static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }
    }
}
