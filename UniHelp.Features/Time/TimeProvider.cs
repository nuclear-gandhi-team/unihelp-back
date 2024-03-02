namespace UniHelp.Features.Time;

public class TimeProvider : ITimeProvider
{
    public DateTime Now { get; } = DateTime.Now;
}