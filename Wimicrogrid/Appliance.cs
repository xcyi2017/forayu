using System;
using System.Collections.Generic;

using System.Linq;
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
        private readonly Rating _rating;
        private double _usage;

        public Appliance(Clock clock, Rating rating, bool on)
        {
            On = on;

            _rating = rating;
            clock.Ticked += UpdateUsage; 
        }

        private void UpdateUsage(TimeSpan duration, EventArgs e)
        {
            if (On) _usage += new Consumption(duration, _rating).Amount;
        }

        public double Usage 
        {
            get { return _usage; }
        }

        public void SwitchOn()
        {
            On = true;
        }
    }

    [TestFixture]
    public class ApplianceTests
    {
        [Test]
        public void Should_consume_power_when_switched_on()
        {
            var clock = new Clock(new TimeSpan(1, 0, 0));
            var rating = new Rating(2);
            var appliance = new Appliance(clock, rating, ApplianceState.On);

            clock.Tick();

            Assert.That(appliance.Usage, Is.GreaterThan(Consumption.None));
        }

        [Test]
        public void Should_consume_no_power_when_switched_off()
        {
            var clock = new Clock(new TimeSpan(1, 0, 0));
            var rating = new Rating(2);
            var appliance = new Appliance(clock, rating, ApplianceState.Off);

            clock.Tick();

            Assert.That(appliance.Usage, Is.EqualTo(Consumption.None));
        }

        [Test]
        public void Should_consume_power_only_when_switched_on()
        {
            var clock = new Clock(new TimeSpan(1, 0, 0));
            var durationOn = new TimeSpan(1, 0, 0);
            var rating = new Rating(2);
            
            var appliance = new Appliance(clock, rating, ApplianceState.Off);
            clock.Tick();
            appliance.SwitchOn();
            clock.Tick();

            Assert.That(appliance.Usage, Is.EqualTo(new Consumption(durationOn, rating).Amount));
        }
    }

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

    public class Rating
    {
        public int Value { get; private set; }

        public Rating(int value)
        {
            Value = value;
        }
    }

    public class Tick
    {
        public TimeSpan Span { get; private set; }

        public Tick(TimeSpan span)
        {
            Span = span;
        }
    }

    public class Clock
    {
        private readonly List<Tick> _ticks = new List<Tick>();

        public TimeSpan TickSize { get; private set; }
        public event TickHandler Ticked;

        public delegate void TickHandler(TimeSpan duration, EventArgs e);

        public Clock(TimeSpan tickSize)
        {
            TickSize = tickSize;
        }

        public double ElapsedHours
        {
            get
            {
                var result = new TimeSpan();
                return _ticks.Aggregate(result, (current, tick) => current.Add(tick.Span)).Hours;
            }
        }

        public void Tick()
        {
            _ticks.Add(new Tick(TickSize));

            if (Ticked == null) return;
            
            Ticked(TickSize, new EventArgs());
        }
    }

    public class Consumption
    {
        private readonly TimeSpan _duration;
        private readonly Rating _rating;

        public Consumption(TimeSpan duration, Rating rating)
        {
            _duration = duration;
            _rating = rating;
        }

        public static double None { get { return 0.0; } }

        public double Amount
        {
            get { return _duration.Hours * _rating.Value; }
        }
    }
}
