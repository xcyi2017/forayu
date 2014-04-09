using System.Collections.Generic;
using System.Linq;

namespace Wimicrogrid
{
    public class Community
    {
        private PowerUsage _usage;

        public Community(int houses, ITime clock)
        {
            _usage = new PowerUsage();

            Households = new List<Household>();

            while (Households.Count < houses)
            {
                Households.Add(new Household(Households.Count, new Appliances(clock).Default, clock));
            }
        }

        public List<Household> Households { get; private set; }

        public PowerUsage Usage {
            get {
                _usage = new PowerUsage(TotalUsage, CurrentUsage, _usage);
                return _usage;
            }
        }

        private double TotalUsage {
            get { return Households.Sum(household => household.Usage.Total); }
        }

        private double CurrentUsage
        {
            get { return Households.Sum(household => household.Usage.Current); }
        }
    }
}