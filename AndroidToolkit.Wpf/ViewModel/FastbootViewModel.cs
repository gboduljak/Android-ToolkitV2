using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AndroidToolkit.Infrastructure.DataAccess;
using AndroidToolkit.Infrastructure.Device;
using AndroidToolkit.Infrastructure.Helpers;
using AndroidToolkit.Infrastructure.Tools;
using AndroidToolkit.Wpf.Presentation.Converters;
using AndroidToolkit.Wpf.Presentation.Presenter;
using AndroidToolkit.Wpf.View;
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

        #region Reboot

        private RelayCommand<SingleCommandParameters> _rebootCommand;
        public RelayCommand<SingleCommandParameters> RebootCommand
        {
            get
            {
                return _rebootCommand ?? (_rebootCommand = new RelayCommand<SingleCommandParameters>(FastbootPresenter.ExecuteReboot));

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
                return _rebootRecoveryCommand ?? (_rebootRecoveryCommand = new RelayCommand<SingleCommandParameters>(FastbootPresenter.ExecuteRebootRecovery));
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
                return _rebootBootloaderCommand ?? (_rebootBootloaderCommand = new RelayCommand<SingleCommandParameters>(FastbootPresenter.ExecuteRebootBootloader));
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

        #region Bootloader

        private RelayCommand<SingleCommandParameters> _lockCommand;
        public RelayCommand<SingleCommandParameters> LockCommand
        {
            get
            {
                return _lockCommand ?? (_lockCommand = new RelayCommand<SingleCommandParameters>(FastbootPresenter.Lock));
            }
            set
            {
                if (_lockCommand != value)
                {
                    RaisePropertyChanging(() => LockCommand);
                    _lockCommand = value;
                    RaisePropertyChanged(() => LockCommand);
                }
            }
        }

        private RelayCommand<SingleCommandParameters> _unlockCommand;
        public RelayCommand<SingleCommandParameters> UnlockCommand
        {
            get
            {
                return _unlockCommand ?? (_unlockCommand = new RelayCommand<SingleCommandParameters>(FastbootPresenter.Unlock));
            }
            set
            {
                if (_unlockCommand != value)
                {
                    RaisePropertyChanging(() => UnlockCommand);
                    _unlockCommand = value;
                    RaisePropertyChanged(() => UnlockCommand);
                }
            }
        }

        private RelayCommand<UIParameters> _tokenCommand;
        public RelayCommand<UIParameters> TokenCommand
        {
            get
            {
                return _tokenCommand ?? (_tokenCommand = new RelayCommand<UIParameters>(FastbootPresenter.ExecuteToken));
            }
            set
            {
                if (_tokenCommand != value)
                {
                    RaisePropertyChanging(() => this.TokenCommand);
                    _tokenCommand = value;
                    RaisePropertyChanged(() => this.TokenCommand);
                }
            }
        }



        #endregion

        #region Erase

        private RelayCommand<SingleCommandParameters> _eraseBootCommand;
        public RelayCommand<SingleCommandParameters> EraseBootCommand
        {
            get
            {
                return _eraseBootCommand ?? (_eraseBootCommand = new RelayCommand<SingleCommandParameters>(FastbootPresenter.EraseBoot));
            }
            set
            {
                if (_eraseBootCommand != value)
                {
                    RaisePropertyChanging(() => EraseBootCommand);
                    _eraseBootCommand = value;
                    RaisePropertyChanged(() => EraseBootCommand);
                }
            }
        }

        private RelayCommand<SingleCommandParameters> _eraseSystemCommand;
        public RelayCommand<SingleCommandParameters> EraseSystemCommand
        {
            get
            {
                return _eraseSystemCommand ?? (_eraseSystemCommand = new RelayCommand<SingleCommandParameters>(FastbootPresenter.EraseSystem));
            }
            set
            {
                if (_eraseSystemCommand != value)
                {
                    RaisePropertyChanging(() => EraseSystemCommand);
                    _eraseSystemCommand = value;
                    RaisePropertyChanged(() => EraseSystemCommand);
                }
            }
        }

        private RelayCommand<SingleCommandParameters> _eraseRecoveryCommand;
        public RelayCommand<SingleCommandParameters> EraseRecoveryCommand
        {
            get
            {
                return _eraseRecoveryCommand ?? (_eraseRecoveryCommand = new RelayCommand<SingleCommandParameters>(FastbootPresenter.EraseRecovery));
            }
            set
            {
                if (_eraseRecoveryCommand != value)
                {
                    RaisePropertyChanging(() => EraseRecoveryCommand);
                    _eraseRecoveryCommand = value;
                    RaisePropertyChanged(() => EraseRecoveryCommand);
                }
            }
        }

        private RelayCommand<SingleCommandParameters> _eraseCacheCommand;
        public RelayCommand<SingleCommandParameters> EraseCacheCommand
        {
            get
            {
                return _eraseCacheCommand ?? (_eraseCacheCommand = new RelayCommand<SingleCommandParameters>(FastbootPresenter.EraseCache));
            }
            set
            {
                if (_eraseCacheCommand != value)
                {
                    RaisePropertyChanging(() => EraseCacheCommand);
                    _eraseCacheCommand = value;
                    RaisePropertyChanged(() => EraseCacheCommand);
                }
            }
        }

        private RelayCommand<SingleCommandParameters> _eraseUserdataCommand;
        public RelayCommand<SingleCommandParameters> EraseUserdataCommand
        {
            get
            {
                return _eraseUserdataCommand ?? (_eraseUserdataCommand = new RelayCommand<SingleCommandParameters>(FastbootPresenter.EraseUserdata));
            }
            set
            {
                if (_eraseUserdataCommand != value)
                {
                    RaisePropertyChanging(() => EraseUserdataCommand);
                    _eraseUserdataCommand = value;
                    RaisePropertyChanged(() => EraseUserdataCommand);
                }
            }
        }

        #endregion

        #region Flash

        private RelayCommand<TwoCommandParameters> _flashSystemCommand;
        public RelayCommand<TwoCommandParameters> FlashSystemCommand
        {
            get
            {
                return _flashSystemCommand ?? (_flashSystemCommand = new RelayCommand<TwoCommandParameters>(FastbootPresenter.FlashSystem));
            }
            set
            {
                if (_flashSystemCommand != value)
                {
                    RaisePropertyChanging(() => FlashSystemCommand);
                    _flashSystemCommand = value;
                    RaisePropertyChanged(() => FlashSystemCommand);
                }
            }
        }

        private RelayCommand<TwoCommandParameters> _flashBootCommand;
        public RelayCommand<TwoCommandParameters> FlashBootCommand
        {
            get
            {
                return _flashBootCommand ?? (_flashBootCommand = new RelayCommand<TwoCommandParameters>(FastbootPresenter.FlashBoot));
            }
            set
            {
                if (_flashBootCommand != value)
                {
                    RaisePropertyChanging(() => FlashBootCommand);
                    _flashBootCommand = value;
                    RaisePropertyChanged(() => FlashBootCommand);
                }
            }
        }

        private RelayCommand<TwoCommandParameters> _flashRecoveryCommand;
        public RelayCommand<TwoCommandParameters> FlashRecoveryCommand
        {
            get
            {
                return _flashRecoveryCommand ?? (_flashRecoveryCommand = new RelayCommand<TwoCommandParameters>(FastbootPresenter.FlashRecovery));
            }
            set
            {
                if (_flashRecoveryCommand != value)
                {
                    RaisePropertyChanging(() => FlashRecoveryCommand);
                    _flashRecoveryCommand = value;
                    RaisePropertyChanged(() => FlashRecoveryCommand);
                }
            }
        }

        private RelayCommand<TwoCommandParameters> _flashBootloaderCommand;
        public RelayCommand<TwoCommandParameters> FlashBootloaderCommand
        {
            get
            {
                return _flashBootloaderCommand ?? (_flashBootloaderCommand = new RelayCommand<TwoCommandParameters>(FastbootPresenter.FlashBootloader));
            }
            set
            {
                if (_flashBootloaderCommand != value)
                {
                    RaisePropertyChanging(() => FlashBootloaderCommand);
                    _flashBootloaderCommand = value;
                    RaisePropertyChanged(() => FlashBootloaderCommand);
                }
            }
        }

        private RelayCommand<TwoCommandParameters> _flashRadioCommand;
        public RelayCommand<TwoCommandParameters> FlashRadioCommand
        {
            get
            {
                return _flashRadioCommand ?? (_flashRadioCommand = new RelayCommand<TwoCommandParameters>(FastbootPresenter.FlashRadio));
            }
            set
            {
                if (_flashRadioCommand != value)
                {
                    RaisePropertyChanging(() => FlashRadioCommand);
                    _flashRadioCommand = value;
                    RaisePropertyChanged(() => FlashRadioCommand);
                }
            }
        }

        private RelayCommand<TwoCommandParameters> _flashUnlocktokenCommand;
        public RelayCommand<TwoCommandParameters> FlashUnlocktokenCommand
        {
            get
            {
                return _flashUnlocktokenCommand ?? (_flashUnlocktokenCommand = new RelayCommand<TwoCommandParameters>(FastbootPresenter.FlashUnlockToken));
            }
            set
            {
                if (_flashUnlocktokenCommand != value)
                {
                    RaisePropertyChanging(() => FlashUnlocktokenCommand);
                    _flashUnlocktokenCommand = value;
                    RaisePropertyChanged(() => FlashUnlocktokenCommand);
                }
            }
        }

        private RelayCommand<TwoCommandParameters> _flashUserdataCommand;
        public RelayCommand<TwoCommandParameters> FlashUserdataCommand
        {
            get
            {
                return _flashUserdataCommand ?? (_flashUserdataCommand = new RelayCommand<TwoCommandParameters>(FastbootPresenter.FlashUserdata));
            }
            set
            {
                if (_flashUserdataCommand != value)
                {
                    RaisePropertyChanging(() => FlashUserdataCommand);
                    _flashUserdataCommand = value;
                    RaisePropertyChanged(() => FlashUserdataCommand);
                }
            }
        }

        private RelayCommand<TwoCommandParameters> _flashZipCommand;
        public RelayCommand<TwoCommandParameters> FlashZipCommand
        {
            get
            {
                return _flashZipCommand ?? (_flashZipCommand = new RelayCommand<TwoCommandParameters>(FastbootPresenter.FlashZip));
            }
            set
            {
                if (_flashZipCommand != value)
                {
                    RaisePropertyChanging(() => FlashZipCommand);
                    _flashZipCommand = value;
                    RaisePropertyChanged(() => FlashZipCommand);
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

        #region Restore

        private RelayCommand<HardResetParameters> _restoreCommand;

        public RelayCommand<HardResetParameters> RestoreCommand
        {
            get
            {
                return _restoreCommand ?? (_restoreCommand = new RelayCommand<HardResetParameters>(
                    (parameter) =>
                    {
                        HardResetParameters parameters = parameter;
                        BackgroundWorker worker = new BackgroundWorker();
                        var fastboot = new FastbootTools(parameters.Context);
                        worker.DoWork += async (sender, args) =>
                        {
                            string[] imgs = new string[4];
                            imgs[0] = parameters.Text;
                            imgs[1] = parameters.Text2;
                            imgs[2] = parameters.Text3;
                            imgs[3] = parameters.Text4;
                            await Application.Current.Dispatcher.InvokeAsync(() =>
                            {
                                using (Toast toast = new Toast("flashing images..."))
                                {
                                    toast.Show();
                                }
                            });
                            try
                            {
                                await fastboot.HardReset(imgs, parameters.Bool);

                                await Application.Current.Dispatcher.InvokeAsync(() =>
                                {
                                    parameters.Flyout.IsOpen = !parameters.Flyout.IsOpen;
                                    using (Toast toast = new Toast("completed :)"))
                                    {
                                        toast.Show();
                                    }
                                });
                            }
                            catch (InvalidOperationException)
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    using (Toast toast = new Toast("fastboot process has been terminated :("))
                                    {
                                        toast.Show();
                                    }
                                    parameters.Flyout.IsOpen = !parameters.Flyout.IsOpen;
                                });
                            }

                        };
                        worker.RunWorkerCompleted += (sender, args) => worker.Dispose();
                        worker.RunWorkerAsync();
                    }));
            }
            set
            {
                if (_restoreCommand != value)
                {
                    RaisePropertyChanging(() => RestoreCommand);
                    _restoreCommand = value;
                    RaisePropertyChanged(() => RestoreCommand);
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

        private TwoCommandParameters _flashParameters;
        public TwoCommandParameters FlashParameters
        {
            get { return _flashParameters ?? (_flashParameters = new TwoCommandParameters()); }
            set
            {
                if (_flashParameters != value)
                {
                    RaisePropertyChanging(() => FlashParameters);
                    this._flashParameters = value;
                    RaisePropertyChanged(() => FlashParameters);
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

        private TwoCommandParameters _executeSingleCommandParameters;
        public TwoCommandParameters ExecuteSingleCommandParameters
        {
            get { return _executeSingleCommandParameters ?? (_executeSingleCommandParameters = new TwoCommandParameters()); }
            set
            {
                if (_executeSingleCommandParameters != value)
                {
                    RaisePropertyChanging(() => ExecuteSingleCommand);
                    this._executeSingleCommandParameters = value;
                    RaisePropertyChanged(() => ExecuteSingleCommand);
                }
            }


        }


        private HardResetParameters _deviceRestoreParameters;
        public HardResetParameters DeviceRestoreParameters
        {
            get { return _deviceRestoreParameters ?? (_deviceRestoreParameters = new HardResetParameters()); }
            set
            {
                if (_deviceRestoreParameters != value)
                {
                    RaisePropertyChanging(() => DeviceRestoreParameters);
                    this._deviceRestoreParameters = value;
                    RaisePropertyChanged(() => DeviceRestoreParameters);
                }
            }
        }
        #endregion

        #endregion


    }
}
