using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace TrafikverketFarjor.Tests.Helpers
{
    public static class BrowserDriverJsonExtensions
    {
        public static void WaitForAjaxToComplete(this IWebDriver webdriver)
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
        }

        public static JObject GetJObjectFromPageSource(this IWebDriver webdriver)
        {
            return JObject.Parse(GetJsonCodeFromPageSource(webdriver));
        }

        public static string GetJsonCodeFromPageSource(this IWebDriver webdriver)
        {
            if (webdriver == null) throw new ArgumentNullException("webdriver");

            var browserImplementations = new Dictionary<Type, Func<IWebDriver, string>>
                {
                    {typeof (ChromeDriver), GetJsonCodeFromChromeDriver},
                    {typeof (FirefoxDriver), GetJsonCodeFromChromeDriver}
                };

            Func<IWebDriver, string> getJsonCodeImpl;
            return !browserImplementations.TryGetValue(webdriver.GetType(), out getJsonCodeImpl)
                ? webdriver.PageSource
                : getJsonCodeImpl(webdriver);
        }

        private static string GetJsonCodeFromChromeDriver(IWebDriver webdriver)
        {
            return webdriver.FindElement(By.TagName("pre")).Text;
        }
    }
}