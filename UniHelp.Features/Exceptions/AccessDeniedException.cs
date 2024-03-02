namespace UniHelp.Features.Exceptions;

public class AccessDeniedException : Exception
{
    public AccessDeniedException(string message) : base(message)
    {
    }
    
    public AccessDeniedException(string message, Exception innerException) : base(message, innerException)
    {
    }
    
    public AccessDeniedException()
    {
    }
}