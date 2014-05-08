using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AndroidToolkit.Wpf.Presentation.Controls
{
    /// <summary>
    /// Interaction logic for MainLogo.xaml
    /// </summary>
    public partial class MainLogo : UserControl
    {
        public MainLogo()
        {
            InitializeComponent();
        }
        public TextBlock HeaderSubtitle
        {
            get { return this.headerSubitle; }
        }
    }
}
