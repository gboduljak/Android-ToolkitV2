using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(AndroidToolkit.Web.Api.Startup))]

namespace AndroidToolkit.Web.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = new HttpConfiguration();
            ConfigureAuth(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //app.UseNinjectMiddleware(CreateKernel);
            //app.UseNinjectWebApi(webApiConfiguration);
            app.UseWebApi(webApiConfiguration);
        }

        ///// <summary>
        ///// Creates the kernel.
        ///// </summary>
        ///// <returns>The newly created kernel.</returns>
        //private static StandardKernel CreateKernel()
        //{
        //    var kernel = new StandardKernel();
        //    kernel.Load(Assembly.GetExecutingAssembly());
        //    return kernel;
        //}
    }
}
