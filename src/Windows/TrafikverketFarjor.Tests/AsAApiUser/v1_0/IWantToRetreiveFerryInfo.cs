using System.Linq;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using TrafikverketFarjor.Tests.Helpers;

namespace TrafikverketFarjor.Tests.AsAApiUser.v1_0
{
    [TestFixture]
    public class IWantToRetreiveFerryInfo : WebApiTests
    {
        [Test]
        public void ItemsReturnedIsAllEmbeddedFerryInfos()
        {
            // Arrange
            var expectedNames = FerryInfo.GetAll().Select(i => i.Name).ToArray();

            // Act
            var response = (JObject)GetResponseJson("/api/1.0/info");

            var actual = response["Info"].Children().Select(c => c.Value<string>("Name")).ToArray();

            // Assert
            Assert.That(actual, Is.EqualTo(expectedNames));
        }

        [Test]
        public void SpecifiedItem()
        {
            // Act
            var response = (JObject) GetResponseJson("/api/1.0/info/Hönöleden");

            var actual = response["Info"].Children().Select(c => c.Value<string>("Name")).ToArray();

            // Assert
            Assert.That(actual.Length, Is.EqualTo(1));
            Assert.That(actual[0], Is.EqualTo("Hönöleden"));
        }
    }
}