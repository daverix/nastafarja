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

        private readonly IDictionary<string, string> _info = new Dictionary<string, string>();
        private readonly IList<FerryInfoAttribute> _attributes = new List<FerryInfoAttribute>();

        public static FerryInfo GetInfo(string name)
        {
            lock (SyncRoot)
            {
                if (Cache.Count < 1) RefreshCache();

                FerryInfo result;
                Cache.TryGetValue(name, out result);
                return result;
            }
        }

        public static IEnumerable<FerryInfo> GetAll()
        {
            lock (SyncRoot)
            {
                if (Cache.Count < 1) RefreshCache();
                return Cache.Values;
            }
        }

        public static void RefreshCache()
        {
            lock (SyncRoot)
            {
                ClearCache();
                FindAndCreateFromManifestResources(info => Cache.Add(info.Name, info));
            }
        }

        public static void ClearCache()
        {
            lock (SyncRoot)
            {
                Cache.Clear();
            }
        }

        public static FerryInfo Create(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            return XmlReader().Read(stream);
        }

        private static void FindAndCreateFromManifestResources(Action<FerryInfo> forEach)
        {
            var type = typeof(FerryInfo);
            foreach (var resourceName in type.Assembly.GetManifestResourceNames())
            {
                if (!Regex.IsMatch(resourceName, @"FerryInfos\..*\.xml$"))
                    continue;

                var info = CreateFromManifestResource(resourceName);
                forEach(info);
            }
        }

        private static FerryInfo CreateFromManifestResource(string resourceName)
        {
            var type = typeof (FerryInfo);
            using (var stream = type.Assembly.GetManifestResourceStream(resourceName))
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

        public string Region { get; set; }
        public string Url { get; set; }

        public FerryRoute GetRoute(string departsFrom)
        {
            return Routes.FirstOrDefault(r => string.Equals(r.DepartsFrom, departsFrom, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}