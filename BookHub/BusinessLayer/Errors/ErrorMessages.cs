namespace BusinessLayer.Errors;


public static class ErrorMessages
{
    public static (Error err, string message) UserNotFound(int id)
    {
        return (Error.UserNotFound,$"User with 'ID={id}' has no unpaid orders");
    }

    public static (Error err, string message) OrderNotFound(int id)
    {
        return (Error.OrderNotFound, $"Order with 'ID={id}' could not be found");
    }

    public static (Error err, string message) AuthorNotFound(int id)
    {
        return (Error.AuthorNotFound,$"Author with 'ID={id}' could not be found");
    }

    public static (Error err, string message) AuthorNotFound()
    {
        return (Error.MultipleAuthorsNotFound,"One or more authors could not be found");
    }

    public static (Error err, string message) AuthorsEmpty()
    {
        return (Error.AuthorFieldEmpty,"Author field cannot be empty");
    }

    public static (Error err, string message) BooksEmpty()
    {
        return (Error.BookFieldEmpty ,"Book field cannot be empty");
    }

    public static (Error err, string message) GenreNotFound(int id)
    {
        return (Error.GenreNotFound ,$"Genre with 'ID={id}' could not be found");
    }
    
    public static (Error err, string message) GenresEmpty()
    {
        return (Error.GenreFieldEmpty,"Genre field cannot be empty");
    }

    public static (Error err, string message) GenreNotFound()
    {
        return (Error.MultipleGenresNotFound ,"One or more genre could not be found");
    }

    public static (Error err, string message) PublisherNotFound(int id)
    {
        return (Error.PublisherNotFound , $"Publisher with 'ID={id}' could not be found");
    }

    public static (Error err, string message) PublisherNotFound(int id, string name)
    {
        return  (Error.PublisherNotFound, $"Publisher 'Name={name}' <OR> 'ID={id}' could not be found");
    }

    public static (Error err, string message) GenreNotFound(int id, string name)
    {
        return (Error.GenreNotFound, $"Genre 'Name={name}' <OR> 'ID={id}' could not be found");
    }

    public static (Error err, string message) UserNotFound(int id, string name)
    {
        return (Error.UserNotFound, $"User 'Name={name}' <OR> 'ID={id}' could not be found");
    }

    public static (Error err, string message) UserAlreadyExists(string name)
    {
        return (Error.UserAlreadyExists, $"User 'Name={name}' already exists");
    }

    public static (Error err, string message) RatingNotFound(int id)
    {
        return (Error.RatingNotFound, $"Rating with 'ID={id}' could not be found");
    }

    public static (Error err, string message) BookNotFound(int id)
    {
        return (Error.BookNotFound, $"Book with 'ID={id}' could not be found");
    }

    public static (Error err, string message) BookNotFound()
    {
        return (Error.MultipleBooksNotFound,"One or more books could not be found");
    }

    public static (Error err, string message) BookNotFound(int id, string name)
    {
        return  (Error.BookNotFound, $"Book 'Name={name}' <OR> 'ID={id}' could not be found");
    }
}