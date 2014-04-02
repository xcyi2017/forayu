using System;

namespace Wimicrogrid
{
    public class Tick
    {
        public TimeSpan Span { get; private set; }

        public Tick(TimeSpan span)
        {
            Span = span;
        }
    }
}