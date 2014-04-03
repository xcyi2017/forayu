using System;
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
    }
}