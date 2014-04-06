using System;
using System.Linq;
using NUnit.Framework;

namespace Wimicrogrid
{
    [TestFixture]
    public class CommuntityTests
    {
        private Clock _clock;

        [SetUp]
        public void Setup()
        {
            var aMinute = new TimeSpan(0, 1, 0);
            _clock = new Clock(aMinute);
        }

        [Test]
        public void Should_begin_with_eight_households()
        {
            const int expectedHouseCount = 8;

            var newCommunity = new Community(expectedHouseCount, _clock);
            Assert.AreEqual(expectedHouseCount, newCommunity.Households.Count);
        }

        [Test]
        public void Should_construct_households_with_unique_appliances()
        {
            const int houseCount = 2;

            var newCommunity = new Community(houseCount, _clock);
            var firstHousesAppliances = newCommunity.Households.First().Appliances;
            var secondHousesAppliances = newCommunity.Households.Last().Appliances;

            Assert.False(firstHousesAppliances.Any(first => secondHousesAppliances.Any(second => first.Id == second.Id)));
        }
    }
}