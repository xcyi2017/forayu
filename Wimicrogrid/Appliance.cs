using System;

using NUnit.Framework;

namespace Wimicrogrid
{
    public class Appliance
    {
        public Appliance(Clock clock, Rating rating)
        {
            
        }

        public double Usage 
        {
            get { return 1.0; }
        }
    }

    [TestFixture]
    public class ApplianceTests
    {
        [Test]
        public void Should_consume_power_when_switched_on()
        {
            var initial = DateTime.Parse("1 APR 2014");
            var clock = new Clock(initial, new TimeSpan(1, 0, 0));
            var rating = new Rating(2);
            var appliance = new Appliance(clock, rating);

            clock.Tick();

            Assert.That(appliance.Usage, Is.GreaterThan(Consumption.None));
        }
    }

    public class Rating
    {
        public int Value { get; private set; }

        public Rating(int value)
        {
            Value = value;
        }
    }

    public class Clock
    {
        private readonly DateTime _initial;
        private readonly TimeSpan _tickSize;
        private int _ticks = 0;

        public Clock(DateTime initial, TimeSpan tickSize)
        {
            _initial = initial;
            _tickSize = tickSize;
        }

        public void Tick()
        {
            _ticks++;
        }
    }

    public class Consumption
    {
        public static double None { get { return 0.0; } }
    }
}
