using System;
using System.Linq;
using NUnit.Framework;

namespace TrafikverketFarjor.Tests
{
    [TestFixture]
    public class FerryInfoTests
    {
        [TearDown]
        public void ClearCache()
        {
            FerryInfo.ClearCache();
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
        public void GetInfo(string name)
        {
            // Act
            var actual = FerryInfo.GetInfo(name);

            // Assert
            Assert.IsNotNull(actual, "#1 return value");
            Assert.AreEqual(name, actual.Name, "#2 Name");
            Assert.GreaterOrEqual(actual.Routes.Count, 1, "#3 Routes");
        }

        [Test]
        public void GetAll()
        {
            // Act
            var actual = FerryInfo.GetAll();

            // Assert
            Assert.That(actual.Count(), Is.GreaterThan(1));
        }

        [Test]
        public void Kornhallsleden()
        {
            var actual = FerryInfo.GetInfo("Kornhallsleden");

            Assert.AreEqual("Kornhall-Gunnesby-Kornhall*", actual.Routes.First().Title);
        }


        [Test]
        public void Kornhallsleden_Info()
        {
            var actual = FerryInfo.GetInfo("Kornhallsleden");

            Assert.AreEqual("Helgtrafik", actual.Info.First().Key);
            Assert.AreEqual("Midsommarafton, julafton och nyårsafton körs som lördagstrafik.", actual.Info.First().Value);
        }

        [Test]
        public void Kornhallsleden_Attributes()
        {
            var actual = FerryInfo.GetInfo("Kornhallsleden");

            Assert.AreEqual("Går endast efter kallelse.", actual.Attributes.First(a => a.Key == "A").Description);
            Assert.AreEqual("Returresa från Gunnesby genomförs direkt efter lossning och lastning.", actual.Attributes.First(a => a.Key == "*").Description);
        }

        [Test]
        public void Kornhallsleden_NextDeparture()
        {
            var nextDeparture = FerryInfo.GetInfo("Kornhallsleden")
                .Routes.First()
                .NextDeparture(new DateTime(2013, 07, 15, 08, 35, 00));

            Assert.AreEqual(new TimeSpan(08, 40, 00), nextDeparture.Departs);
        }

        [Test]
        public void Kornhallsleden_GetSchedule()
        {
            // Arrange
            var info = FerryInfo.GetInfo("Kornhallsleden");

            // Act
            var actual = info.Routes.First().GetSchedule(new DateTime(2013, 07, 14)).ToArray();

            // Assert
            Assert.AreEqual(new TimeSpan(00, 35, 00), actual[0].Departs);
            Assert.AreEqual(new TimeSpan(00, 55, 00), actual[1].Departs);
            Assert.AreEqual(new TimeSpan(01, 55, 00), actual[2].Departs);
            Assert.AreEqual("A", actual[2].Attribute);

            Assert.AreEqual(new TimeSpan(17, 15, 00), actual.First(s => s.Departs >= new TimeSpan(17, 00, 00)).Departs);
        }

        [Test]
        public void Kornhallsleden_GetSchedule_IsHoliday_ReturnsSaturdaySchedule()
        {
            // Arrange
            var info = FerryInfo.GetInfo("Kornhallsleden");

            // Act
            var actual = info.Routes.First()
                .GetSchedule(new DateTime(2013, 06, 21))
                .ToArray();

            // Assert
            Assert.AreEqual(new TimeSpan(17, 15, 00), actual.First(s => s.Departs >= new TimeSpan(17, 00, 00)).Departs);
        }
    }
}
