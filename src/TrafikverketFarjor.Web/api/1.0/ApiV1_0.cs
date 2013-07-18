using System;
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

            Get[apiPrefix + "info/{infoName?}"] = _ =>
                {
                    var selection = FerryInfo.GetAll();
                    if (!string.IsNullOrWhiteSpace(_.infoName))
                    {
                        selection = selection.Where(info => info.Name == _.infoName);
                    }

                    var model = new
                        {
                            Info = selection.Select(i => new
                                {
                                    i.Name,
                                    i.Region,
                                    i.Url,
                                    DepartsFrom = i.Routes.Select(r => r.DepartsFrom)
                                })
                        };

                    return Response.AsJson(model);
                };

            Get[apiPrefix + "nextDeparture/{infoName}/{departsFrom?}"] = _ =>
                {
                    string infoName = _.infoName;
                    string departsFrom = _.departsFrom;

                    var info = FerryInfo.GetInfo(infoName);
                    if (info == null) return HttpStatusCode.NotFound;

                    var route = !string.IsNullOrWhiteSpace(departsFrom)
                        ? info.GetRoute(departsFrom)
                        : info.Routes.FirstOrDefault();

                    if (route == null) return HttpStatusCode.NotFound;
                    var now = DateTime.Now;
                    int count = !Request.Query.count ? 10 : Request.Query.count;

                    var model = new
                        {
                            info.Name,
                            route.Title,
                            route.DepartsFrom,
                            route.ArrivesAt,
                            NextDepartures = route.NextDeparture(now, count)
                            .Select(s => new
                                {
                                    TimeOfDay = s.Departs.ToString(),
                                    s.Attribute,
                                })
                        };

                    return Response.AsJson(model);
                };
        }
    }
}