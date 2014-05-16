using System;
using System.Runtime.InteropServices;
using AndroidToolkit.Wpf.ViewModel;
using MahApps.Metro.Controls;


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
            _viewModel = new MainViewModel();
            this.DataContext = _viewModel;
            this.Logo.HeaderSubtitle.Text = "HOME";
            this.Closing += delegate { _viewModel.Cleanup(); };
        }

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(
          IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
            SetProcessWorkingSetSize(
       System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        }
    }
}
