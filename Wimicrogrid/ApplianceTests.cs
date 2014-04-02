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
}