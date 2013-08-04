using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using TrafikverketFarjor.Tests.Helpers;

namespace TrafikverketFarjor.Tests.AsAWebUser
{
    public class IWantASelectionOfFerrys : WebTests
    {
        [SetUp]
        public void Act()
        {
            Browser.Navigate().GoToUrl(GetUrlFromSettings("/"));
        }

        [Test]
        [TestCase("Sund Jarenleden")]
        [TestCase("Hamburgsundsleden")]
        [TestCase("Bohus Malmönleden")]
        [TestCase("Ängöleden")]
        [TestCase("Lyrleden")]
        [TestCase("Malöleden")]
        [TestCase("Gullmarsleden")]
        //[TestCase("Svanesundsleden")]
        //[TestCase("Nordöleden")]
        [TestCase("Björköleden")]
        [TestCase("Hönöleden")]
        [TestCase("Kornhallsleden")]
        public void InADropDownlist(string expectedText)
        {
            var options = Browser
                .FindElements(By.TagName("option"))
                .Where(tag => tag.Text == expectedText);

            Assert.That(options.Count(), Is.EqualTo(1));
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
