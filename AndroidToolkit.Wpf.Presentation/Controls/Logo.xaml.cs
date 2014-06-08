using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
    /// Interaction logic for Logo.xaml
    /// </summary>
    public partial class Logo : UserControl
    {
        public Logo()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof (TextBlock), typeof (Logo), new PropertyMetadata(default(TextBlock)));

        public TextBlock Header
        {
            get { return (TextBlock) GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public TextBlock HeaderTitle
        {
            get { return this.headerTitle; }
        }

        public TextBlock HeaderSubtitle
        {
            get { return this.headerSubitle; }
        }
    }
}
