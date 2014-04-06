using System;
using System.Collections.Generic;
using System.Linq;

namespace Wimicrogrid
{
    public enum ApplianceType {
        LED_bulb,
        CFL_bulb,
        Incandescent_bulb,
        Radio,
        TV,
        Computer,
        Fridge,
        Washing_machine,
        Phone_charger,
        Kettle
    }

    public static class ApplianceFactory
    {
        public static IEnumerable<RatedAppliance> GetAppliances()
        {
            yield return new RatedAppliance(ApplianceType.LED_bulb, new Rating(1));
            yield return new RatedAppliance(ApplianceType.CFL_bulb, new Rating(2));
            yield return new RatedAppliance(ApplianceType.Incandescent_bulb, new Rating(4));
            yield return new RatedAppliance(ApplianceType.Radio, new Rating(8));
            yield return new RatedAppliance(ApplianceType.TV, new Rating(16));
            yield return new RatedAppliance(ApplianceType.Computer, new Rating(16));
            yield return new RatedAppliance(ApplianceType.Fridge, new Rating(16));
            yield return new RatedAppliance(ApplianceType.Washing_machine, new Rating(128));
            yield return new RatedAppliance(ApplianceType.Phone_charger, new Rating(4));
            yield return new RatedAppliance(ApplianceType.Kettle, new Rating(8));
        }
    }

    public class RatedAppliance
    {
        public ApplianceType ApplianceType { get; private set; }
        public Rating Rating { get; private set; }

        public RatedAppliance(ApplianceType applianceType, Rating rating)
        {
            ApplianceType = applianceType;
            Rating = rating;
        }
    }

    public class Appliances : List<Appliance>
    {
        private readonly ITime _clock;

        public Appliances(ITime clock)
        {
            _clock = clock;
        }

        public IEnumerable<Appliance> Default
        {
            get
            {
                yield return MakeAppliance(ApplianceType.Incandescent_bulb);
                yield return MakeAppliance(ApplianceType.Radio);
                yield return MakeAppliance(ApplianceType.LED_bulb);
                yield return MakeAppliance(ApplianceType.LED_bulb);
                yield return MakeAppliance(ApplianceType.TV);
                yield return MakeAppliance(ApplianceType.Phone_charger);
                yield return MakeAppliance(ApplianceType.Phone_charger);
            }
        }

        public Appliance MakeAppliance(ApplianceType applianceType)
        {
            return All.Single(appliance => appliance.ApplianceType == applianceType);
        }

        public IEnumerable<Appliance> All
        {
            get { return ApplianceFactory.GetAppliances().Select(ratedAppliance => new Appliance(ratedAppliance, _clock)); }
        }
    }

    public struct ApplianceName
    {
        public string Value { get; set; }

        public ApplianceName(string value) : this()
        {
            Value = value;
        }
    }

    public static class ApplianceExtensions
    {
        public static int HouseholdIndex(this ApplianceDto dto)
        {
            return dto.Household - 1;
        }
    }

    public class ApplianceDto
    {
        public ApplianceType Type { get; set; }
        public int Household { get; set; }
    }

    public class Appliance
    {
        public bool On { get; private set; }
        private readonly ApplianceType _type;
        private readonly Rating _rating;
        private double _usage;
        private string _id;

        public Appliance(ApplianceType type, ITime clock, Rating rating, bool isApplianceOn = ApplianceState.Off)
        {
            On = isApplianceOn;

            _id = Guid.NewGuid().ToString();
            _type = type;
            _rating = rating;

            clock.Ticked += UpdateUsage; 
        }

        public Appliance(RatedAppliance ratedAppliance, ITime clock) : this(ratedAppliance.ApplianceType, clock, ratedAppliance.Rating)
        { }

        private void UpdateUsage(TimeSpan duration)
        {
            if (On) _usage += new Consumption(duration, _rating).Amount;
        }

        public string Id
        {
            get { return _id; }    
        }

        public double Usage 
        {
            get { return _usage; }
        }

        public string Name
        {
            get { return _type.ToString().Replace("_", " "); }
        }

        public ApplianceType ApplianceType
        {
            get { return _type; }
        }

        public void SwitchOn()
        {
            On = ApplianceState.On;
        }

        public void SwitchOff()
        {
            On = ApplianceState.Off;
        }
    }
}
