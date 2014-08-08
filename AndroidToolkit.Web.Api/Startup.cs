using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
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
            app.UseWebApi(webApiConfiguration);
        }
    }
}
