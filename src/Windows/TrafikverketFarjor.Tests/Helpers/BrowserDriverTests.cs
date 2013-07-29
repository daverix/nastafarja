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
    public abstract class BrowserDriverTests
    {
        public const string Chrome = "Chrome";
        public const string Firefox = "Firefox";
        public const string InternetExplorer = "Internet Explorer";
        public const string Safari = "Safari";

        private readonly Func<IWebDriver> _browserDriverFactory;

        protected BrowserDriverTests(Func<IWebDriver> browserDriverFactory)
        {
            if (browserDriverFactory == null) throw new ArgumentNullException("browserDriverFactory");
            _browserDriverFactory = browserDriverFactory;
        }

        protected BrowserDriverTests() : this(() => new FirefoxDriver()) {}

        protected BrowserDriverTests(string driverName) : this(() => GetWebDriver(driverName)) {}

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
            return (new UriBuilder(Settings.Default.WebTestsRootUrl) {Path = path}).ToString();
        }
    }
}
