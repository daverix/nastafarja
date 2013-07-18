using System.Linq;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace TrafikverketFarjor.Tests.Web
{
    [TestFixture]
    public class Api1_0Tests
    {
        [TestFixture]
        public class Version : WebTests
        {
            [Test]
            public void ProductVersionProperty()
            {
                // Act
                var response = AsJson("/api/1.0/version");

                // Assert
                Assert.That(response.ProductVersion.ToString(), Is.Not.Empty);
            }

            [Test]
            public void FileVersionProperty()
            {
                // Act
                var response = AsJson("/api/1.0/version");

                // Assert
                Assert.That(response.FileVersion.ToString(), Is.StringMatching(@"^[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+$"));
            }

            [Test]
            public void VersionProperty()
            {
                // Act
                JObject response = AsJson("/api/1.0/version");

                // Assert
                Assert.That(response["Version"]["Major"].Value<int>(), Is.GreaterThanOrEqualTo(0));
                Assert.That(response["Version"]["Minor"].Value<int>(), Is.GreaterThanOrEqualTo(0));
                Assert.That(response["Version"]["Build"].Value<int>(), Is.GreaterThanOrEqualTo(0));
                Assert.That(response["Version"]["Revision"].Value<int>(), Is.GreaterThanOrEqualTo(0));
            }
        }

        [TestFixture]
        public class Info : WebTests
        {
            [Test]
            public void ItemsReturnedIsAllEmbeddedFerryInfos()
            {
                // Arrange
                var expectedNames = FerryInfo.GetAll().Select(i => i.Name).ToArray();

                // Act
                JObject response = AsJson("/api/1.0/info");
                var actual = response["Info"].Children().Select(c => c.Value<string>("Name")).ToArray();

                // Assert
                Assert.That(actual, Is.EqualTo(expectedNames));
            }

            [Test]
            public void SpecifiedItem()
            {
                // Act
                JObject response = AsJson("/api/1.0/info/Hönöleden");
                var actual = response["Info"].Children().Select(c => c.Value<string>("Name")).ToArray();

                // Assert
                Assert.That(actual.Length, Is.EqualTo(1));
                Assert.That(actual[0], Is.EqualTo("Hönöleden"));
            }
        }
    }
}