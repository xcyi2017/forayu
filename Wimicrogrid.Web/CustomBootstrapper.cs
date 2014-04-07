using System;

using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Wimicrogrid.Web
{
    public class CustomBootstrapper : DefaultNancyBootstrapper {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines) {
            Game.Clock = new GameClock(new TimeSpan(0, 5, 0));
        }
    }
}