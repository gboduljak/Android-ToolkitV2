using System.Web.Http;
using AndroidToolkit.Data.Entities;
using AndroidToolkit.Data.Logic;
using AndroidToolkit.Web.Api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(AndroidToolkit.Web.Api.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(AndroidToolkit.Web.Api.App_Start.NinjectWebCommon), "Stop")]

namespace AndroidToolkit.Web.Api.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<AndroidToolkitDB>().ToSelf();
            kernel.Bind<IDeviceRepository>().To<DeviceRepository>().WithConstructorArgument("db", kernel.Get<AndroidToolkitDB>());
            kernel.Bind<IRecoveriesRepository>().To<RecoveriesRepository>().WithConstructorArgument("db", kernel.Get<AndroidToolkitDB>());
            kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>();
            kernel.Bind<UserManager<ApplicationUser>>()
                .ToSelf()
                .WithConstructorArgument(kernel.Get<IUserStore<ApplicationUser>>());
        }
    }
}
