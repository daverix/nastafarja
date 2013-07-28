using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using TrafikverketFarjor.Tests.Helpers;

namespace TrafikverketFarjor.Tests.AsAWebUser
{
    [TestFixture(Firefox)]
    public class IWantASelectionOfFerrys : BrowserDriverTests
    {
        public IWantASelectionOfFerrys(string browserName) : base(browserName) {}

        [SetUp]
        public void Act()
        {
            Browser.Navigate().GoToUrl(GetUrlFromSettings("/"));
        }

        [Test]
        public void InADropDownlist()
        {
            var options = Browser
                .FindElement(By.Id("ferryInfo"))
                .FindElements(By.TagName("option"));

            Assert.That(options.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void ToDisplayNextTenDeparturesWhenISelectOne()
        {
            Browser
                .FindElement(By.Id("ferryInfo"))
                .FindElements(By.TagName("option"))
                .First(p => p.Text == "Hönöleden")
                .Click();

            Browser
                .FindElement(By.Id("ferryRoute"))
                .FindElements(By.TagName("option"))
                .First(p => p.Text == "Hönö")
                .Click();

            Browser
                .WaitForAjaxToComplete();

            var nextDepartures = Browser
                .FindElement(By.TagName("ul"))
                .FindElements(By.TagName("li"));

            Assert.That(nextDepartures.Count(), Is.EqualTo(10));
        }
    }
}
