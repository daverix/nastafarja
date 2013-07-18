using System.Diagnostics;
using System.Linq;
using Nancy;

namespace TrafikverketFarjor.Web.api._1._0
{
// ReSharper disable InconsistentNaming
    public class ApiV1_0 : NancyModule
// ReSharper restore InconsistentNaming
    {
        public ApiV1_0()
        {
            const string apiPrefix = "/api/1.0/";

            Get[apiPrefix + "version"] = _ =>
                {
                    var assembly = GetType().Assembly;
                    var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                    var model = new
                        {
                            assembly.GetName().Version,
                            fileVersionInfo.FileVersion,
                            fileVersionInfo.ProductVersion,
                        };

                    return Response.AsJson(model);
                };

            Get[apiPrefix + "ferrys"] = _ =>
                {
                    var model = FerryInfo.GetAll().Select(i => new
                        {
                            i.Name,
                            i.Region,
                            i.Url,
                            DepartsFrom = i.Routes.Select(r => r.DepartsFrom)
                        });

                    return Response.AsJson(model);
                };
        }
    }
}