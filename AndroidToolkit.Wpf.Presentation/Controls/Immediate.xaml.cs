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
    /// Interaction logic for Immediate.xaml
    /// </summary>
    public partial class Immediate : UserControl
    {
        public Immediate()
        {
            InitializeComponent();
        }

        public TextBlock Context
        {
            get { return this.context; }
        }

        public TextBox ExecuteCommandTextBox
        {
            get { return this.executeCmd; }
        }
    }
}
