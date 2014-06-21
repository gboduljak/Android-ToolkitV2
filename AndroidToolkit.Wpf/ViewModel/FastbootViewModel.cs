using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AndroidToolkit.Infrastructure.DataAccess;
using AndroidToolkit.Infrastructure.Device;
using AndroidToolkit.Infrastructure.Helpers;
using AndroidToolkit.Infrastructure.Tools;
using AndroidToolkit.Wpf.Presentation.Converters;
using AndroidToolkit.Wpf.Presentation.Presenter;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro;

namespace AndroidToolkit.Wpf.ViewModel
{
    public class FastbootViewModel : ViewModelBase
    {
        #region ICommands

        #region UI

        private RelayCommand<TextBlock> _killFastbootCommand;
        public RelayCommand<TextBlock> KillFastbootCommand
        {
            get
            {
                return _killFastbootCommand ?? (_killFastbootCommand = new RelayCommand<TextBlock>(FastbootPresenter.Kill));

            }
            set
            {
                if (_killFastbootCommand != value)
                {
                    RaisePropertyChanging(() => KillFastbootCommand);
                    _killFastbootCommand = value;
                    RaisePropertyChanged(() => KillFastbootCommand);
                }
            }
        }

        private RelayCommand<TextBlock> _prepareCommand;
        public RelayCommand<TextBlock> PrepareCommand
        {
            get
            {
                return _prepareCommand ?? (_prepareCommand = new RelayCommand<TextBlock>(FastbootPresenter.Prepare));

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

        private RelayCommand<TextBox> _openImgCommand;

        public RelayCommand<TextBox> OpenImgCommand
        {
            get { return _openImgCommand ?? (_openImgCommand = new RelayCommand<TextBox>(FastbootPresenter.OpenImg)); }
            set
            {
                if (_openImgCommand != value)
                {
                    RaisePropertyChanging(() => this.OpenImgCommand);
                    _openImgCommand = value;
                    RaisePropertyChanged(() => this.OpenImgCommand);
                }
            }
        }


        #endregion

        #region Execute

        private RelayCommand<TwoCommandParameters> _executeSingleCommand;
        public RelayCommand<TwoCommandParameters> ExecuteSingleCommand
        {
            get { return _executeSingleCommand ?? (_executeSingleCommand = new RelayCommand<TwoCommandParameters>(FastbootPresenter.Execute)); }
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

        private RelayCommand<ExecuteCommandParameters> _executeCommand;
        public RelayCommand<ExecuteCommandParameters> ExecuteCommands
        {
            get { return _executeCommand ?? (_executeCommand = new RelayCommand<ExecuteCommandParameters>(FastbootPresenter.Execute2)); }
            set
            {
                if (_executeCommand != value)
                {
                    RaisePropertyChanging(() => this.ExecuteCommands);
                    _executeCommand = value;
                    RaisePropertyChanged(() => this.ExecuteCommands);
                }
            }
        }

        #endregion

        #region Boot

        private RelayCommand<TwoCommandParameters> _bootCommand;
        public RelayCommand<TwoCommandParameters> BootCommand
        {
            get
            {
                return _bootCommand ?? (_bootCommand = new RelayCommand<TwoCommandParameters>(FastbootPresenter.Boot));

            }
            set
            {
                if (_bootCommand != value)
                {
                    RaisePropertyChanging(() => BootCommand);
                    _bootCommand = value;
                    RaisePropertyChanged(() => BootCommand);
                }
            }
        }

        #endregion

        #region ListDevices

        private RelayCommand<UIParameters> _listDevicesCommand;
        public RelayCommand<UIParameters> ListDevicesCommand
        {
            get
            {
                return _listDevicesCommand ??
                       (_listDevicesCommand = new RelayCommand<UIParameters>(FastbootPresenter.ExecuteListDevices));
            }
            set
            {
                if (_listDevicesCommand != value)
                {
                    RaisePropertyChanging(() => this.ListDevicesCommand);
                    _listDevicesCommand = value;
                    RaisePropertyChanged(() => this.ListDevicesCommand);
                }
            }

        }

        private RelayCommand<UIParameters> _refreshDevicesCommand;
        public RelayCommand<UIParameters> RefreshDevicesCommand
        {
            get
            {
                return _refreshDevicesCommand ??
                       (_refreshDevicesCommand = new RelayCommand<UIParameters>(FastbootPresenter.ExecuteListDevices));
            }
            set
            {
                if (_refreshDevicesCommand != value)
                {
                    RaisePropertyChanging(() => this.RefreshDevicesCommand);
                    _refreshDevicesCommand = value;
                    RaisePropertyChanged(() => this.RefreshDevicesCommand);
                }
            }

        }

        #endregion

        #endregion

        #region Properties

        private ObservableCollection<Accent> _accents;

        public ObservableCollection<Accent> Accents
        {
            get { return _accents ?? (_accents = new ObservableCollection<Accent>(ThemeManager.Accents)); }
            set
            {
                if (_accents != value)
                {
                    RaisePropertyChanging(() => Accents);
                    this._accents = value;
                    RaisePropertyChanged(() => Accents);
                }
            }


        }

        #region Parameters

        private TwoCommandParameters _bootParameters;
        public TwoCommandParameters BootParameters
        {
            get { return _bootParameters ?? (_bootParameters = new TwoCommandParameters()); }
            set
            {
                if (_bootParameters != value)
                {
                    RaisePropertyChanging(() => BootParameters);
                    this._bootParameters = value;
                    RaisePropertyChanged(() => BootParameters);
                }
            }


        }

        private ExecuteCommandParameters _executeCommandsParameters;
        public ExecuteCommandParameters ExecuteCommandsParameters
        {
            get { return _executeCommandsParameters ?? (_executeCommandsParameters = new ExecuteCommandParameters()); }
            set
            {
                if (_executeCommandsParameters != value)
                {
                    RaisePropertyChanging(() => ExecuteCommandsParameters);
                    this._executeCommandsParameters = value;
                    RaisePropertyChanged(() => ExecuteCommandsParameters);
                }
            }


        }

        #endregion

        #endregion


    }
}
