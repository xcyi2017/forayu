using Nancy.Testing;
using Newtonsoft.Json.Linq;

using NUnit.Framework;

namespace Wimicrogrid.Web
{
    public class Expectation
    {
        public class InitialClock
        {
            public const string Day = "Monday";
            public const string Time = "08:00:00";
        }
    }

    public class GameModuleTests
    {
        [Test]
        public void Should_return_initial_clock_setting()
        {
            var bootstrapper = new CustomBootstrapper();
            var browser = new Browser(bootstrapper);

            var result = browser.Get("/api/time", with => with.HttpRequest());

            var resultJson = JObject.Parse(result.Body.AsString());

            Assert.AreEqual(Expectation.InitialClock.Day, resultJson["Day"].ToString());
            Assert.AreEqual(Expectation.InitialClock.Time, resultJson["Time"].ToString());
        }
    }
}