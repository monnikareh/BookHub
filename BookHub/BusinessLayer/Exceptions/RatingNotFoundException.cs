namespace BusinessLayer.Exceptions;

public class RatingNotFoundException : Exception
{
    public RatingNotFoundException(string? message) : base(message)
    {
    }
}