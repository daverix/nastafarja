using System;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using TrafikverketFarjor.Web;

namespace TrafikverketFarjor.Tests.Helpers
{
    public abstract class SelfHostedWebTests : BrowserDriverTests
    {
        private const string DefaultSelfHostUrl = "http://localhost:8888";
        private IDisposable _selfHost;

        protected SelfHostedWebTests()
            : this(DefaultSelfHostUrl)
        {
            
        }

        protected SelfHostedWebTests(string selfHostUrl)
        {
            SelfHostUrl = selfHostUrl;
        }

        public string SelfHostUrl { get; private set; }

        [SetUp]
        public void StartSelfHost()
        {
            _selfHost = SelfHost.Start(SelfHostUrl);
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