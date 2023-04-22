namespace DateNight.Core.Exceptions;

public class UserDoesNotExistException : Exception
{
    public UserDoesNotExistException(string id) : base($"User '{id}' does not exist")
    {
    }
}