using System;
using System.Collections.Generic;
using System.Linq;

namespace Wimicrogrid
{
    public class Clock
    {
        private readonly List<Tick> _ticks = new List<Tick>();

        public TimeSpan TickSize { get; private set; }
        public event TickHandler Ticked;

        public delegate void TickHandler(TimeSpan duration, EventArgs e);

        public Clock(TimeSpan tickSize)
        {
            TickSize = tickSize;
        }

        public double ElapsedHours
        {
            get
            {
                var result = new TimeSpan();
                return _ticks.Aggregate(result, (current, tick) => current.Add(tick.Span)).Hours;
            }
        }

        public void Tick()
        {
            _ticks.Add(new Tick(TickSize));

            if (Ticked == null) return;
            
            Ticked(TickSize, new EventArgs());
        }
    }
}