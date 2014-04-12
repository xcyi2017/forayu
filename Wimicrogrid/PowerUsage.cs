namespace Wimicrogrid
{
    public class PowerUsage
    {
        public static double None = 0.0;

        public double Total { get; private set; }
        public double Current { get; private set; }
        public double Peak { get; private set; }

        public PowerUsage()
        {
            Current = None;
            Peak = None;
        }

        public PowerUsage(double total, double current, PowerUsage usage)
        {
            Total = total;
            Current = current;
            Peak = (current > usage.Peak) ? current : usage.Peak;
        }
    }
}