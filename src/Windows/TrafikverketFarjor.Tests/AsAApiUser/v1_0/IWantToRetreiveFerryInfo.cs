using System.Linq;
using NUnit.Framework;
using TrafikverketFarjor.Tests.Helpers;

namespace TrafikverketFarjor.Tests.AsAApiUser.v1_0
{
    [TestFixture(Firefox)]
    public class IWantToRetreiveFerryInfo : BrowserDriverTests
    {
        public IWantToRetreiveFerryInfo(string browserName) : base(browserName) { }

        [Test]
        public void ItemsReturnedIsAllEmbeddedFerryInfos()
        {
            // Arrange
            var expectedNames = FerryInfo.GetAll().Select(i => i.Name).ToArray();

            // Act
            Browser.Navigate().GoToUrl(GetUrlFromSettings("/api/1.0/info"));
            var response = Browser.GetJObjectFromPageSource();

            var actual = response["Info"].Children().Select(c => c.Value<string>("Name")).ToArray();

            // Assert
            Assert.That(actual, Is.EqualTo(expectedNames));
        }

        [Test]
        public void SpecifiedItem()
        {
            // Act
            Browser.Navigate().GoToUrl(GetUrlFromSettings("/api/1.0/info/Hönöleden"));
            var response = Browser.GetJObjectFromPageSource();

            var actual = response["Info"].Children().Select(c => c.Value<string>("Name")).ToArray();

            // Assert
            Assert.That(actual.Length, Is.EqualTo(1));
            Assert.That(actual[0], Is.EqualTo("Hönöleden"));
        }
    }
}