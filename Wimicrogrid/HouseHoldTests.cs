using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Wimicrogrid
{
    [TestFixture]
    public class HouseHoldTests
    {
        private static class FakeCommunity
        {
            public const int Empty = 0;
        }

        [Test]
        public void Should_consume_more_power_when_another_appliance_switched_on_is_added()
        {
            var anHour = new TimeSpan(1, 0, 0);
            var clock = new Clock(anHour);
            var television = new Appliance(ApplianceType.TV, clock, new Rating(3), ApplianceState.On);
            var radio = new Appliance(ApplianceType.RADIO, clock, new Rating(1), ApplianceState.On);
            var initialAppliances = new List<Appliance> {television};

            var household1 = new Household(FakeCommunity.Empty, initialAppliances, clock);
            var household2 = new Household(FakeCommunity.Empty, initialAppliances, clock);

            clock.Tick();

            household1.AddAppliance(radio);

            clock.Tick();

            Assert.That(household1.Usage, Is.GreaterThan(household2.Usage));
        }

        [Test]
        public void Should_consume_no_power_when_all_appliances_are_switched_off()
        {
            var anHour = new TimeSpan(1, 0, 0);
            var clock = new Clock(anHour);
            var television = new Appliance(ApplianceType.TV, clock, new Rating(3));
            var kettle = new Appliance(ApplianceType.KETTLE, clock, new Rating(1));
            var radio = new Appliance(ApplianceType.RADIO, clock, new Rating(1));
            var initialAppliances = new List<Appliance> { television };

            var household1 = new Household(FakeCommunity.Empty, initialAppliances, clock);
            clock.Tick();

            household1.AddAppliance(radio);
            clock.Tick();

            household1.AddAppliance(kettle);
            clock.Tick();

            Assert.That(household1.Usage, Is.EqualTo(Consumption.None));
        }
    }
}