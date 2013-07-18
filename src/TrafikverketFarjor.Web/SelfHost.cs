using System;
using Microsoft.Owin.Hosting;

namespace TrafikverketFarjor.Web
{
    public class SelfHost
    {
        public static IDisposable Start(int port = 8888)
        {
            return Start("http://+:" + port);
        }

        public static IDisposable Start(string url)
        {
            return WebApp.Start<Startup>(url);
        }
    }
}