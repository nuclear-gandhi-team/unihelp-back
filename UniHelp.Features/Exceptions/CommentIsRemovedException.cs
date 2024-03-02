namespace UniHelp.Features.Exceptions;

public class CommentIsRemovedException : Exception
{
    public CommentIsRemovedException(string message) : base(message)
    {
    }
    
    public CommentIsRemovedException(string message, Exception innerException) : base(message, innerException)
    {
    }
    
    public CommentIsRemovedException()
    {
    }
}