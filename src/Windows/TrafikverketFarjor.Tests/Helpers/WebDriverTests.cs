using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;

namespace TrafikverketFarjor.Tests.Helpers
{
    [Category("WebDriverTests")]
    public abstract class WebDriverTests : WebTests
    {
        public const string Chrome = "Chrome";
        public const string Firefox = "Firefox";
        public const string InternetExplorer = "Internet Explorer";
        public const string Safari = "Safari";

        private readonly Func<IWebDriver> _browserDriverFactory;

        protected WebDriverTests(Func<IWebDriver> browserDriverFactory)
        {
            if (browserDriverFactory == null) throw new ArgumentNullException("browserDriverFactory");
            _browserDriverFactory = browserDriverFactory;
        }

        protected WebDriverTests() : this(Environment.GetEnvironmentVariable("WEBTESTS_BROWSER") ?? InternetExplorer) {}

        protected WebDriverTests(string driverName) : this(() => GetWebDriver(driverName)) { }

        protected IWebDriver Browser { get; set; }

        public static IWebDriver GetWebDriver(string browserName)
        {
            if (browserName == null) throw new ArgumentNullException("browserName");
            if (string.IsNullOrWhiteSpace(browserName)) throw new ArgumentException("BrowserName cannot be null, empty or whitespace.");

            browserName = browserName.Trim();

            var webdriverFactories = new Dictionary<string, Func<IWebDriver>>
                {
                    {Chrome, () => new ChromeDriver()},

                    {Firefox, () => new FirefoxDriver()},

                    {"IE", () => new InternetExplorerDriver()},
                    {"InternetExplorer", () => new InternetExplorerDriver()},
                    {InternetExplorer, () => new InternetExplorerDriver()},

                    {Safari, () => new SafariDriver()}
                };

            foreach (var webdriverFactory in webdriverFactories)
            {
                if (webdriverFactory.Key.Equals(browserName, StringComparison.InvariantCulture))
                    return webdriverFactory.Value();
            }

            throw new ArgumentOutOfRangeException("browserName", browserName, "Not recognized.");
        }

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
    }
}