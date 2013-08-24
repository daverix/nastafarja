using System;
using System.Threading;
using OpenQA.Selenium;

namespace TrafikverketFarjor.Tests.Helpers
{
    public static class BrowserDriverJsonExtensions
    {
        public static void WaitForAjaxToComplete(this IWebDriver webdriver)
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));
        }
    }
}