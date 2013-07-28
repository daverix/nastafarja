using System;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;
using TrafikverketFarjor.Web;

namespace TrafikverketFarjor.Tests.Web
{
    public abstract class WebTests
    {
        private const string DefaultSelfHostUrl = "http://localhost:8888";
        private IDisposable _selfHost;

        protected WebTests() : this(DefaultSelfHostUrl)
        {
            
        }

        protected WebTests(string selfHostUrl)
        {
            SelfHostUrl = selfHostUrl;
        }

        public IWebDriver Browser { get; private set; }
        public string SelfHostUrl { get; private set; }

        [SetUp]
        public void StartSelfHost()
        {
            _selfHost = SelfHost.Start(SelfHostUrl);
            Browser = new SimpleBrowserDriver();
        }

        [TearDown]
        public void DisposeSelfHost()
        {
            if (_selfHost != null) _selfHost.Dispose();
            if (Browser != null) Browser.Dispose();
        }

        protected void GoToPath(string path)
        {
            Browser.Navigate().GoToUrl(SelfHostUrl + path);
        }

        protected dynamic AsJson(string path)
        {
            GoToPath(path);
            return JObject.Parse(Browser.PageSource);
        }
    }
}
