using System;
using System.Collections.Generic;
using System.Linq;
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
            var timer = new Timer(4096);
            timer.Elapsed += (sender, args) =>
            {
                this.Dispatcher.Invoke(this.Close);
                this.Dispatcher.Invoke(timer.Dispose);
            };
            timer.Enabled = true;
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
