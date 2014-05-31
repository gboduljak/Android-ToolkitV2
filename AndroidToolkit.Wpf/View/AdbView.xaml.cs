using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using AndroidToolkit.Infrastructure.Helpers;
using AndroidToolkit.Infrastructure.Tools;
using AndroidToolkit.Wpf.Presentation.Converters;
using AndroidToolkit.Wpf.Presentation.Presenter;
using AndroidToolkit.Wpf.ViewModel;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using FileDialog = AndroidToolkit.Infrastructure.Helpers.FileDialog;
using TextBox = System.Windows.Controls.TextBox;

namespace AndroidToolkit.Wpf.View
{
    /// <summary>
    /// Interaction logic for AdbView.xaml
    /// </summary>
    public partial class AdbView : MetroWindow, IDisposable
    {
        private readonly AdbViewModel _viewModel;

        public AdbView()
        {
            InitializeComponent();
            _viewModel = new ViewModelLocator().Adb;
            this.DataContext = _viewModel;
            Header.HeaderSubtitle.Text = "ADB";
            AddEvents();
            _FlyoutPresenter = Presentation.Presenter.FlyoutPresenter.Present;
        }

        #region AddEvent
        private void AddEvents()
        {

            this.Closed += delegate
            {
                Dispose();
            };

            this.Closing += delegate
            {
                Dispose();
            };

            this.Deactivated += delegate
            {
                Dispose();
            };

            this.SideloadTile.IsEnabled = false;
            this.SideloadFile.TextChanged += (sender, args) =>
            {
                this.SideloadTile.IsEnabled = !string.IsNullOrEmpty(this.SideloadFile.Text);
            };

            this.RebootButton.Click += ButtonClickHandler;
            this.RebootRecoveryButton.Click += ButtonClickHandler;
            this.RebootBootloaderButton.Click += ButtonClickHandler;
            this.PushButton.Click += ButtonClickHandler;

            this.PushFile.PreviewDragOver += (sender, args) => args.Handled = true;
            this.PushFile.Drop += TextBoxDropHandler1;

            this.PullFile1.PreviewDragOver += (sender, args) => args.Handled = true;
            this.PullFile1.Drop += TextBoxDropHandler2;
            this.PullFile2.PreviewDragOver += (sender, args) => args.Handled = true;
            this.PullFile2.Drop += TextBoxDropHandler2;
            this.PullFile3.PreviewDragOver += (sender, args) => args.Handled = true;
            this.PullFile3.Drop += TextBoxDropHandler2;
            this.PullFile4.PreviewDragOver += (sender, args) => args.Handled = true;
            this.PullFile4.Drop += TextBoxDropHandler2;
            this.PullButton.Click += ButtonClickHandler;

            this.SideloadFile.PreviewDragOver += (sender, args) => args.Handled = true;
            this.SideloadFile.Drop += TextBoxDropHandler4;


            this.CopyButton.Click += ButtonClickHandler;
            this.MoveButton.Click += ButtonClickHandler;

            this.InstallButton.Click += ButtonClickHandler;
            this.InstallApp.PreviewDragOver += (sender, args) => args.Handled = true;
            this.InstallApp.Drop += TextBoxDropHandler3;

            this.UninstallButton.Click += ButtonClickHandler;

            this.ListApps.Click += async (sender, args) =>
            {
                _FlyoutPresenter.Invoke(this, 2);
                await this.Dispatcher.InvokeAsync(() =>
                {
                    _FlyoutPresenter.Invoke(this, 4);
                    using (Toast toast = new Toast("Working in background...", "Android Toolkit - Notification"))
                    {
                        toast.Show();
                    }
                    var timer = new System.Timers.Timer(2048);
                    timer.Elapsed += async (s, e) =>
                    {
                        await this.Dispatcher.InvokeAsync(() => StatusLabel.Content = "Working in background...");
                        await this.Dispatcher.InvokeAsync(() => _FlyoutPresenter.Invoke(this, 4));
                        timer.Dispose();
                    };
                    timer.Enabled = true;

                });
            };
            this.RefreshApps.Click += ButtonClickHandler;

            this.ListDevices.Click += (sender, args) => _FlyoutPresenter.Invoke(this, 3);

            this.ShowDevices.Click += (sender, args) => _FlyoutPresenter.Invoke(this, 3);

            this.ShowSettings.Click += (sender, args) => _FlyoutPresenter.Invoke(this, 0);

            this.ShowSettings2.Click += (sender, args) => _FlyoutPresenter.Invoke(this, 0);

            this.ShowReboot.Click += (sender, args) => _FlyoutPresenter.Invoke(this, 1);

            this.ShowBackup.Click += (sender, args) => _FlyoutPresenter.Invoke(this, 5);

            this.ShowRemoteAdb.Click += (sender, args) => _FlyoutPresenter.Invoke(this, 6);


            bool themeLight = false;

            this.LightTheme.Checked += (sender, args) =>
            {
                themeLight = true;
            };
            this.LightTheme.Unchecked += (sender, args) =>
            {
                themeLight = false;
            };

            this.AccentsComboBox.SelectionChanged += (sender, args) =>
            {
                _newAccent = AccentsComboBox.SelectedItem as Accent;
            };


            this.ApplyThemeChange.Click += async (sender, args) =>
            {
                if (AccentsComboBox.SelectedItem != null)
                {
                    ThemeManager.ChangeAppStyle(this, _newAccent,
                        themeLight
                            ? ThemeManager.AppThemes.First(x => x.Name == "BaseLight")
                            : ThemeManager.AppThemes.First(x => x.Name == "BaseDark"));
                    Header.HeaderTitle.Foreground = Brushes.Gray;
                    if (themeLight)
                    {
                        Right.Background = Brushes.White;
                        RightTop.Background = Brushes.GhostWhite;
                    }
                    else
                    {
                        Right.Background = Resources["FlyoutBackgroundBrush"] as SolidColorBrush;
                        RightTop.Background = Resources["FlyoutBackgroundBrush"] as SolidColorBrush;
                    }
                }
                else
                {
                    ThemeManager.ChangeAppStyle(this, ThemeManager.Accents.First(x => x.Name == "Blue"),
                      themeLight
                          ? ThemeManager.AppThemes.First(x => x.Name == "BaseLight")
                          : ThemeManager.AppThemes.First(x => x.Name == "BaseDark"));
                    Header.HeaderTitle.Foreground = Brushes.Gray;
                    if (themeLight)
                    {
                        Right.Background = Brushes.White;
                        RightTop.Background = Brushes.GhostWhite;
                    }
                    else
                    {
                        Right.Background = Resources["FlyoutBackgroundBrush"] as SolidColorBrush;
                        RightTop.Background = Resources["FlyoutBackgroundBrush"] as SolidColorBrush;
                    }
                }
                UiSettings.IsExpanded = false;
                UiSettings.IsEnabled = false;
                await this.ShowMessageAsync("UI Changed", "New UI settings have just been applied.");
            };


            this.RestoreFile.PreviewDragOver += (sender, args) => args.Handled = true;
            this.RestoreFile.Drop += TextBoxDropHandler5;
        }

        private Accent _newAccent = ThemeManager.Accents.First(x => x.Name == "Blue");
        private void TextBoxDropHandler1(object sender, DragEventArgs e)
        {
            object text = e.Data.GetData(DataFormats.FileDrop);
            var tb = sender as TextBox;
            if (tb == null) return;
            var textPaths = text as string[];
            if (textPaths != null)
            {
                if (textPaths.Length == 1)
                {
                    tb.Text = tb.Text + textPaths[0];
                }
                else
                {
                    if (tb.Text.Contains(','))
                    {
                        tb.Text = tb.Text.Remove(tb.Text.IndexOf(','));
                    }
                    for (int i = 0; i < textPaths.Length; i++)
                    {
                        tb.Text = tb.Text + "\n," + textPaths[i];
                    }
                }
            }

        }
        private void TextBoxDropHandler2(object sender, DragEventArgs e)
        {
            object text = e.Data.GetData(DataFormats.FileDrop);
            var tb = sender as TextBox;
            if (tb != null)
            {
                tb.Text = string.Format("{0}", ((string[])text)[0]);
            }
        }
        private async void TextBoxDropHandler3(object sender, DragEventArgs e)
        {
            object text = e.Data.GetData(DataFormats.FileDrop);
            var tb = sender as TextBox;
            if (tb != null)
            {
                string temp = string.Format("{0}", ((string[])text)[0]);
                string path = System.IO.Path.GetExtension(temp);
                if (path == ".apk")
                {
                    tb.Text = temp;
                }
                else
                {
                    await this.ShowMessageAsync("Invalid file", "Dropped file must be an android package file (.apk)");
                }


            }
        }
        private async void TextBoxDropHandler4(object sender, DragEventArgs e)
        {
            object text = e.Data.GetData(DataFormats.FileDrop);
            var tb = sender as TextBox;
            if (tb != null)
            {
                string temp = string.Format("{0}", ((string[])text)[0]);
                string path = System.IO.Path.GetExtension(temp);
                if (path == ".zip")
                {
                    tb.Text = temp;
                }
                else
                {
                    await this.ShowMessageAsync("Invalid file", "Dropped file must be an android zip (.zip)");
                }


            }
        }
        private async void TextBoxDropHandler5(object sender, DragEventArgs e)
        {
            object text = e.Data.GetData(DataFormats.FileDrop);
            var tb = sender as TextBox;
            if (tb != null)
            {
                string temp = string.Format("{0}", ((string[])text)[0]);
                string path = System.IO.Path.GetExtension(temp);
                if (path == ".ab")
                {
                    tb.Text = temp;
                }
                else
                {
                    await this.ShowMessageAsync("Invalid file", "Dropped file must be an android backup (.ab)");
                }


            }
        }
        private async void ButtonClickHandler(object sender, RoutedEventArgs e)
        {
            await this.Dispatcher.InvokeAsync(() =>
            {
                _FlyoutPresenter.Invoke(this, 4);
                using (Toast toast = new Toast("Working in background...", "Android Toolkit - Notification"))
                {
                    toast.Show();
                }
                var timer = new System.Timers.Timer(2048);
                timer.Elapsed += async (s, args) =>
                {
                    await this.Dispatcher.InvokeAsync(() => StatusLabel.Content = "Working in background...");
                    await this.Dispatcher.InvokeAsync(() => _FlyoutPresenter.Invoke(this, 4));
                    timer.Dispose();
                };
                timer.Enabled = true;

            });
        }
        #endregion

        private readonly FlyoutPresenter _FlyoutPresenter;
        private delegate void FlyoutPresenter(MetroWindow context, int index);

        ~AdbView()
        {
            this.Dispose();
        }

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(
          IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        public void Dispose()
        {
            _viewModel.Cleanup();
            GC.Collect();
            GC.SuppressFinalize(this);
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
        }
    }
}
