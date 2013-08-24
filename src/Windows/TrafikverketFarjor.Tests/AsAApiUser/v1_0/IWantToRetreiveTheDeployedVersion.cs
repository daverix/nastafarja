using NUnit.Framework;
using Newtonsoft.Json.Linq;
using TrafikverketFarjor.Tests.Helpers;

namespace TrafikverketFarjor.Tests.AsAApiUser.v1_0
{
    [TestFixture]
    public class IWantToRetreiveTheDeployedVersion : WebApiTests
    {
        private dynamic _response;

        [SetUp]
        public void Get()
        {
            _response = GetResponseJson("/api/1.0/version");
        }

        [Test]
        public void ProductVersionProperty()
        {
            // Assert
            Assert.That(_response.ProductVersion.ToString(), Is.Not.Empty);
        }

        [Test]
        public void FileVersionProperty()
        {
            // Assert
            Assert.That(_response.FileVersion.ToString(), Is.StringMatching(@"^[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+$"));
        }

        [Test]
        public void VersionProperty()
        {
            // Act
            var response = (JObject)_response;

            // Assert
            Assert.That(response["Version"]["Major"].Value<int>(), Is.GreaterThanOrEqualTo(0));
            Assert.That(response["Version"]["Minor"].Value<int>(), Is.GreaterThanOrEqualTo(0));
            Assert.That(response["Version"]["Build"].Value<int>(), Is.GreaterThanOrEqualTo(0));
            Assert.That(response["Version"]["Revision"].Value<int>(), Is.GreaterThanOrEqualTo(0));
        }
    }
}