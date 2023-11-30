namespace BusinessLayer.Exceptions;

public class BooksEmptyException : Exception
{
    public BooksEmptyException(string? message) : base(message)
    {
    }
}