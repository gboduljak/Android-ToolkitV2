using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using AndroidToolkit.Infrastructure.Tools;
using AndroidToolkit.Memory;
using AndroidToolkit.Wpf.Presentation.Controls;
using AndroidToolkit.Wpf.ViewModel;
using GalaSoft.MvvmLight.Command;

namespace AndroidToolkit.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IDisposable
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = ((ViewModelLocator)Application.Current.Resources["Locator"]).Main;
            this.DataContext = _viewModel;
            this.Logo.HeaderSubtitle.Text = "HOME";
            this.Loaded += delegate
            {
                RefreshAdb.Command.Execute(this.AdbDevices);
                RefreshFastboot.Command.Execute(this.FastbootDevices);
            };
            this.Closing += delegate
            {
                _viewModel.Cleanup();
                FastbootTools.Kill();
                AdbTools.KillAdb();
            };
            this.Closed += delegate { Application.Current.Shutdown(0); };
        }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
            MemoryManager.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
        }
    }
}
