using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Wimicrogrid
{
    [TestFixture]
    public class HouseHoldTests
    {
        [Test]
        public void Should_consume_more_power_when_another_appliance_switched_on_is_added()
        {
            var anHour = new TimeSpan(1, 0, 0);
            var clock = new Clock(anHour);
            var television = new Appliance(clock, new Rating(3), ApplianceState.On);
            var radio = new Appliance(clock, new Rating(1), ApplianceState.On);
            var initialAppliances = new List<Appliance> {television};

            var household1 = new Household(initialAppliances);
            var household2 = new Household(initialAppliances);

            clock.Tick();

            household1.AddAppliance(radio);

            clock.Tick();

            Assert.That(household1.Usage, Is.GreaterThan(household2.Usage));
        }
    }

    public class Household
    {
        private readonly List<Appliance> _appliances = new List<Appliance>();

        public Household(IEnumerable<Appliance> appliances)
        {
            foreach (var appliance in appliances)
            {
                _appliances.Add(appliance);
            }
        }

        public double Usage
        {
            get {  return _appliances.Sum(appliance => appliance.Usage); }
        }

        public void AddAppliance(Appliance appliance)
        {
            _appliances.Add(appliance);
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
        public void Should_consume_power_over_time_when_switched_on_once()
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

        [Test]
        public void Should_consume_no_power_over_time_whilst_switched_off()
        {
            var clock = new Clock(new TimeSpan(1, 0, 0));
            var rating = new Rating(2);

            var appliance = new Appliance(clock, rating, ApplianceState.Off);
            clock.Tick();
            clock.Tick();
            clock.Tick();

            Assert.That(appliance.Usage, Is.EqualTo(Consumption.None));
        }

        [Test]
        public void Should_consuming_power_only_whilst_switched_on()
        {
            var clock = new Clock(new TimeSpan(1, 0, 0));
            var rating = new Rating(2);

            var appliance = new Appliance(clock, rating, ApplianceState.Off); clock.Tick();
            appliance.SwitchOn(); clock.Tick();
            appliance.SwitchOff(); clock.Tick();
            appliance.SwitchOn(); clock.Tick();
            appliance.SwitchOff(); clock.Tick();

            Assert.That(appliance.Usage, Is.EqualTo(new Consumption(new TimeSpan(0,2,0,0), rating).Amount));
        }

    }
}