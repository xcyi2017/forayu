using System;
using System.Collections.Generic;

namespace Wimicrogrid
{
    public enum ApplianceType {
        LED_BULB,
        CFL_BULB,
        INCANDESCENT_BULB,
        RADIO,
        TV,
        COMPUTER,
        FRIDGE,
        WASHING_MACHINE,
        PHONE_CHARGER,
        KETTLE
    }

    public class Appliances : List<Appliance>
    {
        private readonly Clock _clock;


        public Appliances(Clock clock)
        {
            _clock = clock;
        }

        public IEnumerable<Appliance> Default
        {
            get
            {
                yield return new Appliance(ApplianceType.LED_BULB, _clock, new Rating(1));
                yield return new Appliance(ApplianceType.CFL_BULB, _clock, new Rating(2));
                yield return new Appliance(ApplianceType.INCANDESCENT_BULB, _clock, new Rating(2));
                yield return new Appliance(ApplianceType.RADIO, _clock, new Rating(4));
                yield return new Appliance(ApplianceType.TV, _clock, new Rating(10));
                yield return new Appliance(ApplianceType.COMPUTER, _clock, new Rating(8));
                yield return new Appliance(ApplianceType.FRIDGE, _clock, new Rating(5));
                yield return new Appliance(ApplianceType.WASHING_MACHINE, _clock, new Rating(15));
                yield return new Appliance(ApplianceType.PHONE_CHARGER, _clock, new Rating(3));
            }
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

    public class Appliance
    {
        public bool On { get; private set; }
        private readonly ApplianceType _type;
        private readonly Rating _rating;
        private double _usage;

        public Appliance(ApplianceType type, Clock clock, Rating rating, bool isApplianceOn = ApplianceState.Off)
        {
            On = isApplianceOn;

            _type = type;
            _rating = rating;
            clock.Ticked += UpdateUsage; 
        }

        private void UpdateUsage(TimeSpan duration)
        {
            if (On) _usage += new Consumption(duration, _rating).Amount;
        }

        public double Usage 
        {
            get { return _usage; }
        }

        public string Name
        {
            get { return _type.ToString(); }
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
