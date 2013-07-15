using System;
using System.Collections.Generic;
using System.Linq;

namespace TrafikverketFarjor
{
    public class FerryInfo
    {
        private readonly IDictionary<string, string> _info = new Dictionary<string, string>();
        private readonly IList<FerryInfoAttribute> _attributes = new List<FerryInfoAttribute>();

        public static FerryInfo GetInfo(string name)
        {
            var type = typeof (FerryInfo);
            using (var stream = type.Assembly.GetManifestResourceStream(type, string.Format("FerryInfos.{0}.xml", name)))
            {
                if (stream == null) return null;
                return (new FerryInfoXmlReader()).Read(stream);
            }
        }

        public FerryInfo()
        {
            Routes = new List<FerryRoute>();
        }

        public string Name { get; set; }
        public List<FerryRoute> Routes { get; set; }

        public IList<FerryInfoAttribute> Attributes
        {
            get { return _attributes; }
        }

        public IDictionary<string, string> Info
        {
            get { return _info; }
        }

        public string DepartsFrom { get; set; }
        public string ArrivesAt { get; set; }

        public FerryRoute GetRoute(string departsFrom)
        {
            return Routes.FirstOrDefault(r => string.Equals(r.DepartsFrom, departsFrom, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}