namespace UniHelp.Features.Time;

public interface ITimeProvider
{
    DateTime Now { get; }
}