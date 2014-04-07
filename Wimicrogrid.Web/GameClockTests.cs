using System;
using System.Threading;
using NUnit.Framework;

namespace Wimicrogrid.Web
{
    [TestFixture]
    public class GameClockTests
    {
        [Test]
        public void Should_have_advanced_six_minutes_in_one_second()
        {
            var sixMinutes = new TimeSpan(0, 6, 0);
            var clock = new GameClock(sixMinutes);
            clock.Begin();
            clock.Tick();

            Assert.AreEqual(0.1, clock.ElapsedHours);
        }

        [Test]
        public void Should_have_advanced_one_hour_in_two_seconds()
        {
            var halfHour = new TimeSpan(0, 30, 0);
            var clock = new GameClock(halfHour);
            clock.Begin();
            clock.Tick();
            clock.Tick();

            Assert.AreEqual(1.0, clock.ElapsedHours);
        }

        [Test]
        public void Should_not_advance_whilst_paused()
        {
            var halfHour = new TimeSpan(3, 00, 0);
            var clock = new GameClock(halfHour);
            clock.Begin();
            clock.Tick();
            clock.Pause();
            clock.Tick();

            Assert.AreEqual(3.0, clock.ElapsedHours);
        }

        [Test]
        public void Should_advance_with_timer() {
            var halfHour = new TimeSpan(2, 00, 0);
            var clock = new GameClock(halfHour);
            clock.Begin();

            Thread.Sleep(2500);

            Assert.AreEqual(4.0, clock.ElapsedHours);
        }    
    }
}