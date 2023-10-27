namespace BusinessLayer.Exceptions;

public class EntityDeleteException : Exception
{
    public EntityDeleteException(string? message) : base(message)
    {
    }
}