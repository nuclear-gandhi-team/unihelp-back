namespace UniHelp.Features.Exceptions;

public class StudentAlreadyInClassException : Exception
{
    public StudentAlreadyInClassException()
    {
    }
    
    public StudentAlreadyInClassException(string message)
        : base(message)
    {
    }
    
    public StudentAlreadyInClassException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}