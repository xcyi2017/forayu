using System;

namespace Wimicrogrid
{
    public class Consumption
    {
        private readonly TimeSpan _duration;
        private readonly Rating _rating;

        public Consumption(TimeSpan duration, Rating rating)
        {
            _duration = duration;
            _rating = rating;
        }

        public static double None { get { return 0.0; } }

        public double Amount
        {
            get { return (_duration.Hours + _duration.Minutes / 60.0) * _rating.Value; }
        }
    }
}