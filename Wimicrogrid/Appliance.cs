using System;

using NUnit.Framework;

namespace Wimicrogrid
{
    public class ApplianceState
    {
        public const bool On = true;
        public const bool Off = false;
    }

    public class Appliance
    {
        public bool On { get; private set; }
        private readonly Clock _clock;
        private readonly Rating _rating;

        public Appliance(Clock clock, Rating rating, bool on)
        {
            On = on;

            _clock = clock;
            _rating = rating;
        }

        public double Usage 
        {
            get { return On ? 1.0 : 0.0; }
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
            var appliance = new Appliance(clock, rating, ApplianceState.On);

            clock.Tick();

            Assert.That(appliance.Usage, Is.GreaterThan(Consumption.None));
        }

        [Test]
        public void Should_consume_no_power_when_switched_off()
        {
            var initial = DateTime.Parse("1 APR 2014");
            var clock = new Clock(initial, new TimeSpan(1, 0, 0));
            var rating = new Rating(2);
            var appliance = new Appliance(clock, rating, ApplianceState.Off);

            clock.Tick();

            Assert.That(appliance.Usage, Is.EqualTo(Consumption.None));
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
