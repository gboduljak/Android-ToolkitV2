using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AndroidToolkit.Web.Api.App_Start;
using Ninject;
using Ninject.Web.Common;

namespace AndroidToolkit.Web.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BeginRequest += Application_BeginRequest;
            GlobalConfiguration.Configuration.MessageHandlers.Add(new CorsAllowOriginHandler());
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "http://localhost:8080");
            if ((Request.Headers.AllKeys.Contains("Origin")) && (Request.HttpMethod == "OPTIONS"))
            {
                Response.StatusCode = 200;
                Response.Headers.Add("Access-Control-Allow-Methods", "GET, PUT, POST, DELETE");
                string sRequestedHeaders = String.Join(", ", Request.Headers.GetValues("Access-Control-Request-Headers") ?? new string[0]);
                if (!String.IsNullOrEmpty(sRequestedHeaders))
                {
                    Response.Headers.Add("Access-Control-Allow-Headers", sRequestedHeaders);
                }
                Response.End();
            }
        }

    }
}
