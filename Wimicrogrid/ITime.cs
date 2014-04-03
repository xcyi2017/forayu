namespace Wimicrogrid
{
    public interface ITime
    {
        event Clock.TickHandler Ticked;
        double ElapsedHours { get; }
        void Tick();
    }
}