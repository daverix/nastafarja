using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;

namespace TrafikverketFarjor.Tests.Helpers
{
    [Category("Webtests")]
    public abstract class WebTests
    {
        public const string Chrome = "Chrome";
        public const string Firefox = "Firefox";
        public const string InternetExplorer = "Internet Explorer";
        public const string Safari = "Safari";

        private readonly Func<IWebDriver> _browserDriverFactory;
        private string _webTestsUrl;

        protected WebTests(Func<IWebDriver> browserDriverFactory)
        {
            if (browserDriverFactory == null) throw new ArgumentNullException("browserDriverFactory");
            _browserDriverFactory = browserDriverFactory;
        }

        protected WebTests() : this(Environment.GetEnvironmentVariable("WEBTESTS_BROWSER") ?? Firefox) {}

        protected WebTests(string driverName) : this(() => GetWebDriver(driverName)) { }

        public string WebTestsUrl
        {
            get
            {
                if (_webTestsUrl == null)
                {
                    // The default localhost URL's port should correspond to the port value entered in the 
                    // TrafikverketFarjor.Web project properties Web tab. This enabled us to run web tests locally
                    // after we have hit F5 at least once...
                    _webTestsUrl = Environment.GetEnvironmentVariable("WEBTESTS_URL") ?? "http://localhost:50799";
                }

                return _webTestsUrl;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException();
                _webTestsUrl = value;
            }
        }

        public static IWebDriver GetWebDriver(string browserName)
        {
            switch (browserName)
            {
                case Chrome:
                    return new ChromeDriver();

                case Firefox:
                    return new FirefoxDriver();

                case "IE":
                case "InternetExplorer":
                case InternetExplorer:
                    return new InternetExplorerDriver();

                case Safari:
                    return new SafariDriver();
            }

            throw new ArgumentOutOfRangeException("browserName", browserName, "Not recognized.");
        }

        public IWebDriver Browser { get; set; }

        [TestFixtureSetUp]
        public void SetUpBrowserDriver()
        {
            Browser = _browserDriverFactory();
        }

        [TestFixtureTearDown]
        public void DisposeBrowser()
        {
            if (Browser != null) Browser.Dispose();
        }

        public string GetUrlFromSettings(string path)
        {
            return (new UriBuilder(WebTestsUrl) { Path = path }).ToString();
        }
    }
}
