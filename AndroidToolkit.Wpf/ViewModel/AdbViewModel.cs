using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using AndroidToolkit.Infrastructure.Tools;
using AndroidToolkit.Wpf.Presentation;
using AndroidToolkit.Wpf.Presentation.Converters;
using AndroidToolkit.Wpf.Presentation.Presenter;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AndroidToolkit.Wpf.ViewModel
{
    public class AdbViewModel : ViewModelBase
    {
        public AdbViewModel()
        {
            ThreeTextCommandParameters = new ThreeTextCommandParameters();
            FiveTextCommandParameters = new FiveTextCommandParameters();
            InstallTwoCommandParameters = new TwoCommandParameters();
            UninstallTwoCommandParameters = new TwoCommandParameters();
            UiParameters = new UIParameters();
            ExecuteSingleCommandParameters=new TwoCommandParameters();
        }

        #region Commands


        #region Reboot

        private RelayCommand<SingleCommandParameters> _rebootCommand;

        public RelayCommand<SingleCommandParameters> RebootCommand
        {
            get
            {
                return _rebootCommand ?? (_rebootCommand = new RelayCommand<SingleCommandParameters>(AdbPresenter.ExecuteReboot));

            }
            set
            {
                if (_rebootCommand != value)
                {
                    RaisePropertyChanging(() => this.RebootCommand);
                    _rebootCommand = value;
                    RaisePropertyChanged(() => this.RebootCommand);
                }
            }
        }

        private RelayCommand<SingleCommandParameters> _rebootRecoveryCommand;

        public RelayCommand<SingleCommandParameters> RebootRecoveryCommand
        {
            get
            {
                return _rebootRecoveryCommand ?? (_rebootRecoveryCommand = new RelayCommand<SingleCommandParameters>(AdbPresenter.ExecuteRebootRecovery));
            }
            set
            {
                if (_rebootRecoveryCommand != value)
                {
                    RaisePropertyChanging(() => this.RebootRecoveryCommand);
                    _rebootRecoveryCommand = value;
                    RaisePropertyChanged(() => this.RebootRecoveryCommand);
                }
            }
        }

        private RelayCommand<SingleCommandParameters> _rebootBootloaderCommand;

        public RelayCommand<SingleCommandParameters> RebootBootloaderCommand
        {
            get
            {
                return _rebootBootloaderCommand ?? (_rebootBootloaderCommand = new RelayCommand<SingleCommandParameters>(AdbPresenter.ExecuteRebootBootloader));
            }
            set
            {
                if (_rebootBootloaderCommand != value)
                {
                    RaisePropertyChanging(() => this.RebootBootloaderCommand);
                    _rebootBootloaderCommand = value;
                    RaisePropertyChanged(() => this.RebootBootloaderCommand);
                }
            }
        }

        #endregion

        private RelayCommand<TextBlock> _prepareCommand;

        public RelayCommand<TextBlock> PrepareCommand
        {
            get
            {
                return _prepareCommand ?? (_prepareCommand = new RelayCommand<TextBlock>(AdbPresenter.ExecutePrepare));

            }
            set
            {
                if (_prepareCommand != value)
                {
                    RaisePropertyChanging(() => this.PrepareCommand);
                    _prepareCommand = value;
                    RaisePropertyChanged(() => this.PrepareCommand);
                }
            }
        }


        private RelayCommand<ThreeTextCommandParameters> _pushCommand;

        public RelayCommand<ThreeTextCommandParameters> PushCommand
        {
            get
            {
                return _pushCommand ?? (_pushCommand = new RelayCommand<ThreeTextCommandParameters>(AdbPresenter.ExecutePush));
            }
            set
            {
                if (_pushCommand != value)
                {
                    RaisePropertyChanging(() => this.PushCommand);
                    _pushCommand = value;
                    RaisePropertyChanged(() => this.PushCommand);
                }
            }
        }

        private RelayCommand<FiveTextCommandParameters> _pullCommand;

        public RelayCommand<FiveTextCommandParameters> PullCommand
        {
            get
            {
                return _pullCommand ?? (_pullCommand = new RelayCommand<FiveTextCommandParameters>(AdbPresenter.ExecutePull));
            }
            set
            {
                if (_pullCommand != value)
                {
                    RaisePropertyChanging(() => this.PullCommand);
                    _pullCommand = value;
                    RaisePropertyChanged(() => this.PullCommand);
                }
            }
        }


        private RelayCommand<TwoCommandParameters> _installCommand;

        public RelayCommand<TwoCommandParameters> InstallCommand
        {
            get
            {
                return _installCommand ?? (_installCommand = new RelayCommand<TwoCommandParameters>(AdbPresenter.ExecuteInstall));

            }
            set
            {
                if (_installCommand != value)
                {
                    RaisePropertyChanging(() => this.InstallCommand);
                    _installCommand = value;
                    RaisePropertyChanged(() => this.InstallCommand);
                }
            }
        }


        private RelayCommand<TwoCommandParameters> _uninstallCommand;

        public RelayCommand<TwoCommandParameters> UninstallCommand
        {
            get
            {
                return _uninstallCommand ?? (_uninstallCommand = new RelayCommand<TwoCommandParameters>(AdbPresenter.ExecuteUninstall));

            }
            set
            {
                if (_uninstallCommand != value)
                {
                    RaisePropertyChanging(() => this.UninstallCommand);
                    _uninstallCommand = value;
                    RaisePropertyChanged(() => this.UninstallCommand);
                }
            }
        }

        private RelayCommand<TwoCommandParameters> _executeSingleCommand;

        public RelayCommand<TwoCommandParameters> ExecuteSingleCommand
        {
            get { return _executeSingleCommand ?? (_executeSingleCommand = new RelayCommand<TwoCommandParameters>(AdbPresenter.ExecuteSingleCommand)); }
            set
            {
                if (_executeSingleCommand != value)
                {
                    RaisePropertyChanging(() => this.ExecuteSingleCommand);
                    _executeSingleCommand = value;
                    RaisePropertyChanged(() => this.ExecuteSingleCommand);
                }
            }
        }


        #region UICommands

        private RelayCommand<TextBlock> _clearImmediateCommand;

        public RelayCommand<TextBlock> ClearImmediateCommand
        {
            get { return _clearImmediateCommand ?? (_clearImmediateCommand = new RelayCommand<TextBlock>(AdbPresenter.ExecuteClearImmediate)); }
            set
            {
                if (_clearImmediateCommand != value)
                {
                    RaisePropertyChanging(() => this.ClearImmediateCommand);
                    _clearImmediateCommand = value;
                    RaisePropertyChanged(() => this.PrepareCommand);
                }
            }
        }

        private RelayCommand<TextBox> _openFileCommand;

        public RelayCommand<TextBox> OpenFileCommand
        {
            get { return _openFileCommand ?? (_openFileCommand = new RelayCommand<TextBox>(AdbPresenter.OpenFile)); }
            set
            {
                if (_openFileCommand != value)
                {
                    RaisePropertyChanging(() => this.ClearImmediateCommand);
                    _openFileCommand = value;
                    RaisePropertyChanged(() => this.PrepareCommand);
                }
            }
        }

        private RelayCommand<TextBox> _openAppCommand;

        public RelayCommand<TextBox> OpenAppCommand
        {
            get { return _openAppCommand ?? (_openAppCommand = new RelayCommand<TextBox>(AdbPresenter.OpenApp)); }
            set
            {
                if (_openAppCommand != value)
                {
                    RaisePropertyChanging(() => this.ClearImmediateCommand);
                    _openAppCommand = value;
                    RaisePropertyChanged(() => this.PrepareCommand);
                }
            }
        }

        private RelayCommand<TextBox> _saveFileCommand;

        public RelayCommand<TextBox> SaveFileCommand
        {
            get { return _saveFileCommand ?? (_saveFileCommand = new RelayCommand<TextBox>(AdbPresenter.SaveFile)); }
            set
            {
                if (_saveFileCommand != value)
                {
                    RaisePropertyChanging(() => this.ClearImmediateCommand);
                    _saveFileCommand = value;
                    RaisePropertyChanged(() => this.PrepareCommand);
                }
            }
        }

        #endregion

        #endregion

        #region Properties

        public UIParameters UiParameters { get; set; }

        public ThreeTextCommandParameters ThreeTextCommandParameters { get; set; }

        public FiveTextCommandParameters FiveTextCommandParameters { get; set; }

        public TwoCommandParameters InstallTwoCommandParameters { get; set; }

        public TwoCommandParameters UninstallTwoCommandParameters { get; set; }

        public TwoCommandParameters ExecuteSingleCommandParameters { get; set; }

        #endregion



    }
}

