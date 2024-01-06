namespace BusinessLayer.Errors;

public static class ErrorMessages
{
    public static string UserNotFound(int id)
    {
        return $"User with 'ID={id}' has no unpaid orders";
    }

    public static string OrderNotFound(int id)
    {
        return $"Order with 'ID={id}' could not be found";
    }
    public static string AuthorNotFound(int id)
    {
        return $"Author with 'ID={id}' could not be found";
    }
    public static string GenreNotFound(int id)
    {
        return $"Genre with 'ID={id}' could not be found";
    }
    public static string PublisherNotFound(int id)
    {
        return $"Publisher with 'ID={id}' could not be found";
    }
    public static string RatingNotFound(int id)
    {
        return $"Rating with 'ID={id}' could not be found";
    }
    
    public static string BookNotFound(int id)
    {
        return $"Book with 'ID={id}' could not be found";
    }
}