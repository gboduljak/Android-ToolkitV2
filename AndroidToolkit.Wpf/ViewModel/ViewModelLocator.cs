/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:AndroidToolkit.Wpf"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using AndroidToolkit.Infrastructure.Adapters;
using AndroidToolkit.Infrastructure.DataAccess;
using AndroidToolkit.Infrastructure.Utilities;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace AndroidToolkit.Wpf.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AdbViewModel>();
            SimpleIoc.Default.Register<ICommandExecutor, CommandExecutor>();
            SimpleIoc.Default.Register<ITextBlockAdapter, TextBlockAdapter>();
            SimpleIoc.Default.Register<IRemoteInfoRepository, RemoteInfoRepository>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public AdbViewModel Adb
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AdbViewModel>();
            }
        }

        #region Repositories

        public IRemoteInfoRepository RemoteInfoRepository
        {
            get { return ServiceLocator.Current.GetInstance<IRemoteInfoRepository>(); }
        }

        #endregion

        #region Utilities

        public ICommandExecutor CommandExecutor
        {
            get { return ServiceLocator.Current.GetInstance<ICommandExecutor>(); }
        }

        #endregion

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}