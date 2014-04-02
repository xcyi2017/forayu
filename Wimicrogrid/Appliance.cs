using System;

namespace Wimicrogrid
{
    public class Appliance
    {
        public bool On { get; private set; }
        private readonly Rating _rating;
        private double _usage;

        public Appliance(Clock clock, Rating rating, bool on)
        {
            On = on;

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
