using System.IO;
using System.Net;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace TrafikverketFarjor.Tests.Helpers
{
    [TestFixture]
    [Category("WebApiTests")]
    public abstract class WebApiTests : WebTests
    {
        protected HttpWebResponse GetResponse(string path)
        {
            return GetResponseByFullUrl(GetUrlFromSettings(path));
        }

        protected string GetResponseString(string path)
        {
            return GetResponseStringByFullUrl(GetUrlFromSettings(path));
        }

        protected dynamic GetResponseJson(string path)
        {
            return GetResponseJsonByFullUrl(GetUrlFromSettings(path));
        }

        protected HttpWebResponse GetResponseByFullUrl(string url)
        {
            return WebRequest.CreateHttp(url).GetResponse() as HttpWebResponse;
        }

        protected string GetResponseStringByFullUrl(string url)
        {
            using (var response = GetResponseByFullUrl(url))
            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                    return reader.ReadToEnd();
            }
        }

        protected dynamic GetResponseJsonByFullUrl(string url)
        {
            return JObject.Parse(GetResponseStringByFullUrl(url));
        }
    }
}