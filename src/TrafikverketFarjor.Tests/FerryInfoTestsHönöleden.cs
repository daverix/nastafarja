using System;
using NUnit.Framework;

namespace TrafikverketFarjor.Tests
{
    [TestFixture]
    public class FerryInfoTestsHönöleden
    {
        private FerryInfo _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = FerryInfo.GetInfo("Hönöleden");
        }

        [Test]
        public void AttributeA_NotMondays()
        {
            // Act
            var actual = _sut
                .GetRoute("Hönö")
                .NextDeparture(new DateTime(2013, 07, 15));

            // Assert
            Assert.AreNotEqual(new TimeSpan(00, 25, 00), actual.Departs);
        }

        [Test]
        [TestCase(2013, 07, 16)]
        [TestCase(2013, 07, 17)]
        [TestCase(2013, 07, 18)]
        [TestCase(2013, 07, 19)]
        public void AttributeA_TuesdayToFriday(int year, int month, int day)
        {
            // Act
            var actual = _sut
                .GetRoute("Hönö")
                .NextDeparture(new DateTime(year, month, day));

            // Assert
            Assert.AreEqual(new TimeSpan(00, 25, 00), actual.Departs);
        }

        [Test]
        public void AttributeB_Saturday()
        {
            // Act
            var actual = _sut
                .GetRoute("Hönö")
                .NextDeparture(new DateTime(2013, 07, 13));

            // Assert
            Assert.AreEqual(new TimeSpan(00, 25, 00), actual.Departs);
        }

        [Test]
        public void AttributeB_NotSundays()
        {
            // Act
            var actual = _sut
                .GetRoute("Hönö")
                .NextDeparture(new DateTime(2013, 07, 14));

            // Assert
            Assert.AreNotEqual(new TimeSpan(00, 25, 00), actual.Departs);
        }

        [Test]
        public void AttributeB_NotHolidays()
        {
            // Act
            var actual = _sut
                .GetRoute("Hönö")
                .NextDeparture(new DateTime(2013, 06, 22));

            // Assert
            Assert.AreNotEqual(new TimeSpan(00, 25, 00), actual.Departs);
        }

        [Test]
        public void AttributeC_NotNewYearsDay()
        {
            // Act
            var actual = _sut
                .GetRoute("Hönö")
                .NextDeparture(new DateTime(2013, 01, 01, 10, 00, 00));

            // Assert
            Assert.AreNotEqual(new TimeSpan(10, 05, 00), actual.Departs);
        }

        [Test]
        public void AttributeC_NotChristmasEve()
        {
            // Act
            var actual = _sut
                .GetRoute("Hönö")
                .NextDeparture(new DateTime(2013, 12, 24, 10, 00, 00));

            // Assert
            Assert.AreNotEqual(new TimeSpan(10, 05, 00), actual.Departs);
        }

        [Test]
        public void AttributeC_NotChristmasDay()
        {
            // Act
            var actual = _sut
                .GetRoute("Hönö")
                .NextDeparture(new DateTime(2013, 12, 25, 10, 00, 00));

            // Assert
            Assert.AreNotEqual(new TimeSpan(10, 05, 00), actual.Departs);
        }

        [Test]
        public void AttributeC_NotNewYearsEve()
        {
            // Act
            var actual = _sut
                .GetRoute("Hönö")
                .NextDeparture(new DateTime(2013, 12, 31, 10, 00, 00));

            // Assert
            Assert.AreNotEqual(new TimeSpan(10, 05, 00), actual.Departs);
        }

        [Test]
        [TestCase(2013, 07, 13, Result = "19:20:00")]
        [TestCase(2013, 07, 14, Result = "19:20:00")]
        [TestCase(2013, 06, 21, Result = "19:20:00")]
        [TestCase(2013, 06, 22, Result = "19:20:00")]
        [TestCase(2013, 12, 24, Result = "19:50:00")]
        public string AttributeD(int year, int month, int day)
        {
            // Act
            var actual = _sut
                .GetRoute("Hönö")
                .NextDeparture(new DateTime(year, month, day, 19, 00, 00));

            // Assert
            return actual.Departs.ToString();
        }

        [Test]
        [TestCase(2013, 07, 13, Result = "20:20:00")]
        [TestCase(2013, 07, 14, Result = "20:20:00")]
        [TestCase(2013, 06, 21, Result = "20:20:00")]
        [TestCase(2013, 06, 22, Result = "20:20:00")]
        [TestCase(2013, 12, 24, Result = "20:50:00")]
        public string AttributeE(int year, int month, int day)
        {
            // Act
            var actual = _sut
                .GetRoute("Hönö")
                .NextDeparture(new DateTime(year, month, day, 20, 00, 00));

            // Assert
            return actual.Departs.ToString();
        }
    }
}