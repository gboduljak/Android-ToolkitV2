using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AndroidToolkit.Memory;
using AndroidToolkit.Wpf.ViewModel;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace AndroidToolkit.Wpf.View
{
    /// <summary>
    /// Interaction logic for FastbootView.xaml
    /// </summary>
    public partial class FastbootView : MetroWindow, IDisposable
    {
        private readonly FastbootViewModel _viewModel;

        public FastbootView()                                    
        {
            InitializeComponent();
            _viewModel = ((ViewModelLocator)Application.Current.Resources["Locator"]).Fastboot;
            this.DataContext = _viewModel;
            Header.HeaderSubtitle.Text = "FASTBOOT";
            _FlyoutPresenter = Presentation.Presenter.FlyoutPresenter.Present;
            AddEvents();
        }

        #region AddEvents
        private void AddEvents()
        {
            this.Closed += delegate
            {
                Dispose();
            };

            this.Closing += delegate
            {
                KillFastboot.Command.Execute(null);
                Dispose();
            };

            this.Deactivated += delegate
            {
                Dispose();
            };

            #region Theme
            UiSlider.MouseDoubleClick += RestoreScalingFactor;
            this.ShowSettings.Click += (sender, args) => _FlyoutPresenter.Invoke(this, 0);
            this.ShowSettings2.Click += (sender, args) => _FlyoutPresenter.Invoke(this, 0);

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



            #endregion

            #region Reboot

            this.ShowReboot.Click += (sender, args) => _FlyoutPresenter.Invoke(this,1);

            #endregion

            #region Boot
            this.BootImg.PreviewDragOver += (sender, args) => args.Handled = true;
            this.BootImg.Drop += TextBoxDropHandler3;

            #endregion

            #region Bootloder

            this.StartIdentifierToken.Click += (sender, args) => _FlyoutPresenter.Invoke(this, 4);
            #endregion

            #region Flash
            this.FlashImage.PreviewDragOver += (sender, args) =>
            {
                FlashDragBorder.Brush = (Brush)base.Resources["AccentColorBrush"];
                args.Handled = true;
            };
            this.FlashImage.Drop += FlashTextBoxHandler;
            #endregion

            #region Devices

            this.ListDevices.Click += (sender, args) => _FlyoutPresenter.Invoke(this, 2);

            this.ShowDevices.Click += (sender, args) => _FlyoutPresenter.Invoke(this, 2);

            this.RefreshDevices.Click += (sender, args) =>
            {
                _FlyoutPresenter.Invoke(this, 2);
                _FlyoutPresenter.Invoke(this, 2);
            };

            #endregion

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
                if (path == ".img")
                {
                    tb.Text = temp;
                }
                else
                {
                    await this.ShowMessageAsync("Invalid file", "Dropped file must be an Android Flashable Image (.img)");
                }


            }
        }
        private async void FlashTextBoxHandler(object sender, DragEventArgs e)
        {
            object text = e.Data.GetData(DataFormats.FileDrop);
            var tb = sender as TextBox;
            if (tb != null)
            {
                string temp = string.Format("{0}", ((string[])text)[0]);
                string path = System.IO.Path.GetExtension(temp);
                if (path == ".img")
                {
                    tb.Text = temp;
                    FlashDragBorder.Brush = Brushes.Gray;
                }
                else if (path == ".zip")
                {
                    tb.Text = temp;
                    FlashDragBorder.Brush = Brushes.Gray;
                }
                else if (path == ".bin")
                {
                    tb.Text = temp;
                    FlashDragBorder.Brush = Brushes.Gray;
                }
                else
                {
                    await this.ShowMessageAsync("Invalid file", "Dropped file must be an Android Flashable File (.img, .zip, .bin)");
                    FlashDragBorder.Brush = Brushes.Gray;
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
                if (path == ".bin")
                {
                    tb.Text = temp;
                }
                else
                {
                    await this.ShowMessageAsync("Invalid file", "Dropped file must be an android unlocktoken binary (.bin)");
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

        #region Scaling
        protected override void OnPreviewMouseWheel(MouseWheelEventArgs args)
        {
            base.OnPreviewMouseWheel(args);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                UiSlider.Value += (args.Delta > 0) ? 0.1 : -0.1;
            }
        }
        protected override void OnPreviewMouseDown(MouseButtonEventArgs args)
        {
            base.OnPreviewMouseDown(args);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (args.MiddleButton == MouseButtonState.Pressed)
                {
                    RestoreScalingFactor(UiSlider, args);
                }
            }
        }
        private void RestoreScalingFactor(object sender, MouseButtonEventArgs args)
        {
            ((Slider)sender).Value = 1.0;
        }
        #endregion

        private readonly FlyoutPresenter _FlyoutPresenter;
        private delegate void FlyoutPresenter(MetroWindow context, int index);

        #region IDisposable

        public void Dispose()
        {
            _viewModel.Cleanup();
            GC.Collect();
            GC.SuppressFinalize(this);
            MemoryManager.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
        }

        ~FastbootView()
        {
            Dispose();
        }

        #endregion

    }
}
