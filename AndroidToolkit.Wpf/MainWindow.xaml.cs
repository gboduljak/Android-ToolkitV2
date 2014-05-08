using AndroidToolkit.Wpf.ViewModel;
using MahApps.Metro.Controls;


namespace AndroidToolkit.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
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
    }
}
