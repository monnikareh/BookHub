namespace BusinessLayer.Errors;

public enum Error
{
    UserNotFound,
    OrderNotFound,
    PublisherNotFound,
    GenreNotFound,
    AuthorNotFound,
    BookNotFound,
    RatingNotFound,
    MultipleGenresNotFound,
    MultipleBooksNotFound,
    MultipleAuthorsNotFound,
    AuthorFieldEmpty,
    GenreFieldEmpty,
    BookFieldEmpty,
    UserAlreadyExists
}