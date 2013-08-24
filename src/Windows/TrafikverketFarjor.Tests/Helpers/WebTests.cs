using System;
using NUnit.Framework;

namespace TrafikverketFarjor.Tests.Helpers
{
    [Category("Webtests")]
    public abstract class WebTests
    {
        private string _webTestsUrl;

        protected string WebTestsUrl
        {
            get
            {
                if (_webTestsUrl == null)
                {
                    // The default localhost URL's port should correspond to the port value entered in the 
                    // TrafikverketFarjor.Web project properties Web tab. This enabled us to run web tests locally
                    // after we have hit F5 at least once...
                    _webTestsUrl = Environment.GetEnvironmentVariable("WEBTESTS_URL") ?? "http://stage.nastafarja.se/";
                }

                return _webTestsUrl;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException();
                _webTestsUrl = value;
            }
        }

        protected string GetUrlFromSettings(string path)
        {
            return (new UriBuilder(WebTestsUrl) { Path = path }).ToString();
        }
    }
}
