namespace BusinessLayer.Exceptions;

public class AuthorsEmptyException : Exception
{
    public AuthorsEmptyException(string? message) : base(message)
    {
    }
}