using System.Collections.Generic;
using System.Linq;

namespace Wimicrogrid
{
    public class Community
    {
        public Community(int houses, Clock clock)
        {
            Households = new List<Household>();
            var defaultAppliances = new Appliances(clock).Default.ToList();

            while (Households.Count < houses)
            {
                Households.Add(new Household(Households.Count, defaultAppliances, clock));
            }
        }

        public List<Household> Households { get; private set; }
    }
}