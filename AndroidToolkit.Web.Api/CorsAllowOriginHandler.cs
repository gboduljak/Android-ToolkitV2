using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AndroidToolkit.Web.Api
{
    public sealed class CorsAllowOriginHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync
            (HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            if ((request.Headers.Any(h => h.Key == "Origin")) &&
                (response.Headers.All(h => h.Key != "Access-Control-Allow-Origin")))
            {
                //response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:8080");
            }
            return response;
        }
    }

}