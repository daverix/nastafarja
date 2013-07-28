using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TrafikverketFarjor.Tests
{
    [TestFixture]
    public class OrderByTests
    {
        [Test]
        public void SortBy()
        {
            // Arrange
            var items = new List<Item<int>>
                {
                    new Item<int> {Data = 2},
                    new Item<int> {Data = 1},
                    new Item<int> {Data = 3}
                };

            // Act
            items.SortBy(i => i.Data);

            // Assert
            Assert.That(items.Select(i => i.Data).ToArray(), Is.EqualTo(new [] { 1, 2, 3}));
        }

        [Test]
        public void SortByDescending()
        {
            // Arrange
            var items = new List<Item<int>>
                {
                    new Item<int> {Data = 2},
                    new Item<int> {Data = 1},
                    new Item<int> {Data = 3}
                };

            // Act
            items.SortByDescending(i => i.Data);

            // Assert
            Assert.That(items.Select(i => i.Data).ToArray(), Is.EqualTo(new[] { 3, 2, 1 }));
        }

        private class Item<T>
        {
            public T Data { get; set; }
        }
    }
}