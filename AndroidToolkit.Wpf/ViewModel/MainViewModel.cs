using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using AndroidToolkit.Infrastructure.Tools;
using AndroidToolkit.Wpf.Presentation.Presenter;
using AndroidToolkit.Wpf.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AndroidToolkit.Wpf.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            // TODO: Complete member initialization
        }

        private void ShowAdb()
        {
            AdbView adb=new AdbView();
            adb.Show();
        }

        private RelayCommand _showAdbCommand;

        public RelayCommand ShowAdbCommand
        {
            get { return _showAdbCommand ?? (_showAdbCommand = new RelayCommand(ShowAdb)); }
            set
            {
                if (_showAdbCommand != value)
                {
                    RaisePropertyChanging(() => this.ShowAdbCommand);
                    _showAdbCommand = value;
                    RaisePropertyChanged(() => this.ShowAdbCommand);
                }
            }
        }
    }
}