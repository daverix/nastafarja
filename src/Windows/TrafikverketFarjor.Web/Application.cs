using Nancy;

namespace TrafikverketFarjor.Web
{
    public class Application : NancyModule
    {
        public Application()
        {
            Get["/"] = _ => View["NextDeparture"];

#if DEBUG
            // Configure some redirects which helps us hitting F5 from Visual Studio
            Get["/Views/NextDeparture.cshtml"] = _ => Response.AsRedirect("/");
#endif
        }
    }
}