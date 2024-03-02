namespace UniHelp.Features.Exceptions;

public class UserUpdateException : Exception
{
    public UserUpdateException(string message)
        : base(message)
    {
    }
}