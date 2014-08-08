using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Filters;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using AndroidToolkit.Data.Entities;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace AndroidToolkit.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            //ODataModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<Device>("Devices");
            //config.Routes.MapODataServiceRoute(
            //    routeName: "Devices",
            //    routePrefix: null,
            //    model: builder.GetEdmModel());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //var cors = new EnableCorsAttribute("http://localhost:8080", "*", "*");
            //config.EnableCors(cors);
            //config.EnableCors();
        }
    }
}
