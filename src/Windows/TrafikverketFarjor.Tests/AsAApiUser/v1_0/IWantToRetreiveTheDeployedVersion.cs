using NUnit.Framework;
using Newtonsoft.Json.Linq;
using TrafikverketFarjor.Tests.Helpers;

namespace TrafikverketFarjor.Tests.AsAApiUser.v1_0
{
    public class IWantToRetreiveTheDeployedVersion : WebTests
    {
        [SetUp]
        public void Get()
        {
            Browser.Navigate().GoToUrl(GetUrlFromSettings("/api/1.0/version"));
        }

        [Test]
        public void ProductVersionProperty()
        {
            // Act
            var response = Browser.GetJObjectFromPageSource() as dynamic;

            // Assert
            Assert.That(response.ProductVersion.ToString(), Is.Not.Empty);
        }

        [Test]
        public void FileVersionProperty()
        {
            // Act
            var response = Browser.GetJObjectFromPageSource() as dynamic;

            // Assert
            Assert.That(response.FileVersion.ToString(), Is.StringMatching(@"^[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+$"));
        }

        [Test]
        public void VersionProperty()
        {
            // Act
            var response = Browser.GetJObjectFromPageSource();

            // Assert
            Assert.That(response["Version"]["Major"].Value<int>(), Is.GreaterThanOrEqualTo(0));
            Assert.That(response["Version"]["Minor"].Value<int>(), Is.GreaterThanOrEqualTo(0));
            Assert.That(response["Version"]["Build"].Value<int>(), Is.GreaterThanOrEqualTo(0));
            Assert.That(response["Version"]["Revision"].Value<int>(), Is.GreaterThanOrEqualTo(0));
        }
    }
}