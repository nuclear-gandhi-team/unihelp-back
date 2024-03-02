namespace UniHelp.Features.Exceptions;

public class TaskIsNotFinishedException : Exception
{
    public TaskIsNotFinishedException(string message) : base(message)
    {
    }
    
    public TaskIsNotFinishedException(string message, Exception innerException) : base(message, innerException)
    {
    }
    
    public TaskIsNotFinishedException()
    {
    }
}