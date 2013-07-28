using System;
using System.Linq;
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
        [Ignore("Not a test. Prints to console output for inspection.")]
        public void PrintNext10Departures()
        {
            var route = _sut.GetRoute("Hönö");
            var dateTime = DateTime.Now;
            for (var i = 0; i < 10; i++)
            {
                var next = route.NextDeparture(dateTime);
                Console.WriteLine(next.Departs);
                dateTime = (dateTime.Date + next.Departs).AddMinutes(1);
            }
        }

        [Test]
        public void NextDeparture_Take10()
        {
            // Arrange
            var route = _sut.GetRoute("Hönö");

            // Act
            var actual = route.NextDeparture(new DateTime(2013, 07, 17, 09, 20, 00, 001), 10);

            // Assert
            Assert.That(actual.Select(s => s.Departs).ToArray(), Is.EqualTo(new[]
                {
                    new TimeSpan(09, 35, 00),
                    new TimeSpan(09, 50, 00),
                    new TimeSpan(10, 05, 00),
                    new TimeSpan(10, 20, 00),
                    new TimeSpan(10, 35, 00),
                    new TimeSpan(10, 50, 00),
                    new TimeSpan(11, 05, 00),
                    new TimeSpan(11, 20, 00),
                    new TimeSpan(11, 35, 00),
                    new TimeSpan(11, 50, 00),
                }));
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