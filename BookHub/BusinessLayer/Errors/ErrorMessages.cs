using BusinessLayer.Models;

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

    public static string AuthorNotFound()
    {
        return "One or more authors could not be found";
    }

    public static string AuthorsEmpty()
    {
        return "Author field cannot be empty";
    }

    public static string BooksEmpty()
    {
        return "Book field cannot be empty";
    }

    public static string GenreNotFound(int id)
    {
        return $"Genre with 'ID={id}' could not be found";
    }

    public static string GenreNotFound()
    {
        return "One or more genre could not be found";
    }

    public static string PublisherNotFound(int id)
    {
        return $"Publisher with 'ID={id}' could not be found";
    }

    public static string PublisherNotFound(int id, string name)
    {
        return $"Publisher 'Name={name}' <OR> 'ID={id}' could not be found";
    }

    public static string GenreNotFound(int id, string name)
    {
        return $"Genre 'Name={name}' <OR> 'ID={id}' could not be found";
    }

    public static string UserNotFound(int id, string name)
    {
        return $"User 'Name={name}' <OR> 'ID={id}' could not be found";
    }

    public static string UserAlreadyExists(string name)
    {
        return $"User 'Name={name}' already exists";
    }

    public static string RatingNotFound(int id)
    {
        return $"Rating with 'ID={id}' could not be found";
    }

    public static string BookNotFound(int id)
    {
        return $"Book with 'ID={id}' could not be found";
    }

    public static string BookNotFound()
    {
        return "One or more books could not be found";
    }

    public static string BookNotFound(int id, string name)
    {
        return $"Book 'Name={name}' <OR> 'ID={id}' could not be found";
    }
}