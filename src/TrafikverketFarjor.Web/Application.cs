using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace TrafikverketFarjor.Web.Modules
{
    public class Application : NancyModule
    {
        public Application()
        {
            Get["/"] = _ => View["NextDeparture"];
        }
    }
}