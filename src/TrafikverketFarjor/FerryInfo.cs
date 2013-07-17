using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TrafikverketFarjor
{
    public class FerryInfo
    {
        private static readonly object SyncRoot = new object();
        private static readonly Dictionary<string, FerryInfo> Cache = new Dictionary<string, FerryInfo>();
        private static bool _allHasBeenCached;

        private readonly IDictionary<string, string> _info = new Dictionary<string, string>();
        private readonly IList<FerryInfoAttribute> _attributes = new List<FerryInfoAttribute>();

        public static FerryInfo GetInfo(string name)
        {
            lock (SyncRoot)
            {
                FerryInfo result;
                if (Cache.TryGetValue(name, out result))
                    return result;

                result = CreateFromManifestResourceByName(name);
                if (result == null) return null;

                Cache.Add(name, result);
                return result;
            }
        }

        public static IEnumerable<FerryInfo> GetAll()
        {
            lock (SyncRoot)
            {
                if (_allHasBeenCached) return Cache.Values;

                var type = typeof (FerryInfo);
                var result = type.Assembly.GetManifestResourceNames()
                                 .Select(resourceName =>
                                     {
                                         var match = Regex.Match(resourceName, @"FerryInfos\.(.*)\.xml$");
                                         return match.Success ? match.Groups[1].Value : null;
                                     })
                                 .Where(name => !string.IsNullOrWhiteSpace(name))
                                 .Select(name => GetInfo(name));
                _allHasBeenCached = true;
                return result;
            }
        }

        public static FerryInfo Create(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            return XmlReader().Read(stream);
        }

        private static FerryInfo CreateFromManifestResourceByName(string name)
        {
            var type = typeof (FerryInfo);
            using (var stream = type.Assembly.GetManifestResourceStream(typeof(FerryInfo), string.Format("FerryInfos.{0}.xml", name)))
            {
                return stream == null ? null : Create(stream);
            }
        }

        private static FerryInfoXmlReader XmlReader()
        {
            return new FerryInfoXmlReader();
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