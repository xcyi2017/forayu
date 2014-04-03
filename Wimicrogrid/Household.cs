using System.Collections.Generic;
using System.Linq;

namespace Wimicrogrid
{
    public class Household
    {
        private readonly Appliances _appliances;

        public Household(IEnumerable<Appliance> appliances, Clock clock)
        {
            _appliances = new Appliances(clock);

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
}