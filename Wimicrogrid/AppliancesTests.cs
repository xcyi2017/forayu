using System.Linq;
using Moq;
using NUnit.Framework;

namespace Wimicrogrid
{
    [TestFixture]
    public class AppliancesTests
    {
        [Test]
        public void Should_give_different_appliance_instances_as_default()
        {
            var mockClock = new Mock<ITime>();
            var appliances = new Appliances(mockClock.Object);
            var firstDefaults = appliances.All;
            var nextDefaults = appliances.All;

            Assert.False(nextDefaults.Any(appliance => firstDefaults.Any(first => first.Id == appliance.Id)));
        }
    }
}