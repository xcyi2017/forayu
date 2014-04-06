using System.Collections.Generic;

namespace Wimicrogrid
{
    public class Community
    {
        public Community(int houses, ITime clock)
        {
            Households = new List<Household>();

            while (Households.Count < houses)
            {
                Households.Add(new Household(Households.Count, new Appliances(clock).Default, clock));
            }
        }

        public List<Household> Households { get; private set; }
    }
}