using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AndroidToolkit.Wpf.Presentation.Converters;
using AndroidToolkit.Wpf.Presentation.Presenter;
using AndroidToolkit.Wpf.ViewModel;
using MahApps.Metro.Controls;
using FileDialog = AndroidToolkit.Infrastructure.Helpers.FileDialog;
using TextBox = System.Windows.Controls.TextBox;

namespace AndroidToolkit.Wpf.View
{
    /// <summary>
    /// Interaction logic for AdbView.xaml
    /// </summary>
    public partial class AdbView : MetroWindow
    {
        private readonly AdbViewModel _viewModel;

        public AdbView()
        {
            InitializeComponent();
            Logo.HeaderSubtitle.Text = "ADB";
            _viewModel = new AdbViewModel();
            this.DataContext = _viewModel;
            AddEvents();
            _FlyoutPresenter = Presentation.Presenter.FlyoutPresenter.Present;
        }

        #region AddEvent
        private void AddEvents()
        {
            this.Closing += delegate
            {

                Properties.Settings.Default.Save();
                _viewModel.Cleanup();
                GC.Collect();
            };

            this.Deactivated += delegate
            {
                GC.Collect();
            };

            this.ShowSettings.Click += (sender, args) => _FlyoutPresenter.Invoke(this, 0);

            this.ShowReboot.Click += (sender, args) => _FlyoutPresenter.Invoke(this, 1);

        }
        #endregion

        private readonly FlyoutPresenter _FlyoutPresenter;
        private delegate void FlyoutPresenter(MetroWindow context, int index);
    }
}
