using System;
using System.Timers;

namespace Wimicrogrid.Web
{
    public class GameClock : ITime
    {
        private readonly TimeSpan _ticksize;
        private readonly Timer _timer;

        public event Clock.TickHandler Ticked;
        
        public DateTime Current { get; private set; }

        public string Day {
            get { return Current.DayOfWeek.ToString(); }
        }

        public TimeSpan Time {
            get { return Current.TimeOfDay; }
        }

        public GameClock(TimeSpan ticksize)
        {
            _ticksize = ticksize;
            _timer = new Timer(1000);
            _timer.Elapsed += TimerOnElapsed;

            Current = GameStart;
        }

        private static DateTime GameStart
        {
            get { return DateTime.Parse("7 APRIL 2014 08:00"); }
        }

        public static double SixtyMinutes
        {
            get { return 60.0; }
        }

        public void Begin()
        {
            _timer.Enabled = true;
        }

        public void Pause()
        {
            _timer.Enabled = false;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Tick();
        }

        public double ElapsedHours {
            get { return (Current - GameStart).Hours + (Current - GameStart).Minutes / SixtyMinutes; } 
        }
        
        public void Tick()
        {
            if(!_timer.Enabled) return;
            Current = Current.Add(_ticksize);

            if (Ticked == null) return;
            Ticked(_ticksize);
        }
    }
}