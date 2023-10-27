namespace BusinessLayer.Exceptions;

public class EntityUpdateException : Exception
{
    public EntityUpdateException(string? message) : base(message)
    {
    }
}