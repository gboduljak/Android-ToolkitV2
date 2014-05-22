using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AndroidToolkit.Wpf.View
{
    /// <summary>
    /// Interaction logic for Toast.xaml
    /// </summary>
    public partial class Toast : Window, IDisposable
    {
        public Toast(string message)
        {
            InitializeComponent();
            this.Top = 0;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            this.TextBlock.Text = message;
            var timer = new Timer(7000);
            timer.Elapsed += (sender, args) =>
            {
                this.Dispatcher.Invoke(this.Close);
                this.Dispatcher.Invoke(timer.Dispose);
            };
            timer.Enabled = true;
        }
        public Toast(string message, string name)
        {
            InitializeComponent();
            this.Title = name;
            this.Top = 0;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            this.TextBlock.Text = message;
            var timer = new Timer(7000);
            timer.Elapsed += (sender, args) =>
            {
                this.Dispatcher.Invoke(this.Close);
                this.Dispatcher.Invoke(timer.Dispose);
            };
            timer.Enabled = true;
        }
     
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(
          IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
        }

        ~Toast()
        {
            Dispose();
        }
    }
}
