using System;
using NUnit.Framework;

namespace Wimicrogrid
{
    [TestFixture]
    public class ClockTests
    {
        [Test]
        public void Should_calculate_elapsed_time_based_on_single_tick()
        {
            var ticksize = new TimeSpan(1,0,0);
            var clock = new Clock(ticksize);
            clock.Tick();
            const double expectedElapsedHours = 1.0;
            Assert.AreEqual(expectedElapsedHours, clock.ElapsedHours);
        }

        [Test]
        public void Should_calculate_elapsed_time_based_on_multiple_ticks()
        {
            var ticksize = new TimeSpan(2, 0, 0);
            var clock = new Clock(ticksize);
            clock.Tick();
            clock.Tick();
            clock.Tick();
            const double expectedElapsedHours = 6.0;
            Assert.AreEqual(expectedElapsedHours, clock.ElapsedHours);
        }
    }
}