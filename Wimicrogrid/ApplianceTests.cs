using System;
using NUnit.Framework;

namespace Wimicrogrid
{
    [TestFixture]
    public class ApplianceTests
    {
        [Test]
        public void Should_consume_power_when_switched_on()
        {
            var clock = new Clock(new TimeSpan(1, 0, 0));
            var rating = new Rating(2);
            var appliance = new Appliance(ApplianceType.LED_bulb, clock, rating, ApplianceState.On);

            clock.Tick();

            Assert.That(appliance.TotalUsage, Is.GreaterThan(Consumption.None));
        }

        [Test]
        public void Should_consume_no_power_when_switched_off()
        {
            var clock = new Clock(new TimeSpan(1, 0, 0));
            var rating = new Rating(2);
            var appliance = new Appliance(ApplianceType.LED_bulb, clock, rating);

            clock.Tick();

            Assert.That(appliance.TotalUsage, Is.EqualTo(Consumption.None));
        }

        [Test]
        public void Should_consume_power_over_time_when_switched_on_once()
        {
            var clock = new Clock(new TimeSpan(1, 0, 0));
            var durationOn = new TimeSpan(1, 0, 0);
            var rating = new Rating(8);

            var appliance = new Appliance(ApplianceType.TV, clock, rating);
            clock.Tick();
            appliance.SwitchOn();
            clock.Tick();

            Assert.That(appliance.TotalUsage, Is.EqualTo(new Consumption(durationOn, rating).Amount));
        }

        [Test]
        public void Should_consume_no_power_over_time_whilst_switched_off()
        {
            var clock = new Clock(new TimeSpan(1, 0, 0));
            var rating = new Rating(4);

            var appliance = new Appliance(ApplianceType.Radio, clock, rating);
            clock.Tick();
            clock.Tick();
            clock.Tick();

            Assert.That(appliance.TotalUsage, Is.EqualTo(Consumption.None));
        }

        [Test]
        public void Should_consuming_power_only_whilst_switched_on()
        {
            var clock = new Clock(new TimeSpan(1, 0, 0));
            var rating = new Rating(15);

            var appliance = new Appliance(ApplianceType.Washing_machine, clock, rating); clock.Tick();
            appliance.SwitchOn(); clock.Tick();
            appliance.SwitchOff(); clock.Tick();
            appliance.SwitchOn(); clock.Tick();
            appliance.SwitchOff(); clock.Tick();

            Assert.That(appliance.TotalUsage, Is.EqualTo(new Consumption(new TimeSpan(0,2,0,0), rating).Amount));
        }
    }
}