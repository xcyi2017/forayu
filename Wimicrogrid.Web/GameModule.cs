using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;

namespace Wimicrogrid.Web {
    public class GameModule : NancyModule {
        public static class GameDefaults
        {
            public const int NumberOfHouses = 8;
        }

        public GameModule()
        {
            Get["/api/time"] = response => JsonConvert.SerializeObject(Game.Clock);

            Get["/api/start"] = response =>
            {
                Game.Clock.Begin();
                return "Clock started...";
            };

            Get["/api/stop"] = response =>
            {
                Game.Clock.Pause();
                return "Clock stopped...";
            };

            Get["/api/community"] = response =>
            {
                Game.Community = new Community(GameDefaults.NumberOfHouses, Game.Clock);
                return Game.Community.AsJson();
            };

            Get["/api/consumption"] = response => Game.Community.Usage.AsJson();

            Get["/api/household/{id}"] = household =>
            {
                if (Game.Community == null) 
                    return HttpStatusCode.NotFound;

                var selectedHousehold = (Household) Game.Community.Households[household.id - 1];
                return selectedHousehold.AsJson();
            };

            Post["/api/appliances"] = _ =>
            {
                var addingAppliance = this.Bind<ApplianceDto>();
                var appliance = new Appliances(Game.Clock).MakeAppliance(addingAppliance.Type);
                var selectedHousehold = Game.Community.Households[addingAppliance.HouseholdIndex()];
                selectedHousehold.AddAppliance(appliance);
                return appliance.AsJson();
            };

            Put["/api/appliances/{id}"] = appliance => 
            {
                foreach (var household in Game.Community.Households) 
                {
                    household.SwitchOnOff(appliance.id);
                }
                return HttpStatusCode.OK;
            };

            Delete["/api/appliances/{id}"] = appliance => {
                foreach (var household in Game.Community.Households) {
                    household.RemoveAppliance(appliance.id);
                }
                return HttpStatusCode.OK;
            };
        }
    }
}   