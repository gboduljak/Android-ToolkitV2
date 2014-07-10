using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AndroidToolkit.Infrastructure.Helpers
{
    public static class ConnectionChecker
    {
        public static bool IsConnectedToInternet
        {
            get
            {
                try
                {
                    HttpWebRequest hwebRequest = (HttpWebRequest)WebRequest.Create("http://www.google.com");
                    hwebRequest.Timeout = 10000;
                    HttpWebResponse hWebResponse = (HttpWebResponse)hwebRequest.GetResponse();
                    return hWebResponse.StatusCode == HttpStatusCode.OK;
                }
                catch { return false; }
            }
        }
    }
}
