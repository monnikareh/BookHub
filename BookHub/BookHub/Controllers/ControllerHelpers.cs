using BookHub.Models;
using DataAccessLayer.Entities;

namespace BookHub.Controllers;

public static class ControllerHelpers
{
    public static ModelRelated<Book> MapBookToBookRelated(Book book)
    {
        return new ModelRelated<Book>
        {
            Id = book.Id,
            Name = book.Name,
        };
    }
    
    public static ModelRelated<Publisher> MapPublisherToPublisherRelated(Publisher publisher)
    {
        return new ModelRelated<Publisher>
        {
            Id = publisher.Id,
            Name = publisher.Name,
        };
    }
    
    public static ModelRelated<Genre> MapGenreToGenreRelated(Genre genre)
    {
        return new ModelRelated<Genre>
        {
            Id = genre.Id,
            Name = genre.Name,
        };
    }
    
    public static ModelRelated<User> MapUserToUserRelated(User user)
    {
        return new ModelRelated<User>
        {
            Id = user.Id,
            Name = user.Name,
        };
    }
    
    public static ModelRelated<Rating> MapRatingToRatingRelated(Rating rating)
    {
        return new ModelRelated<Rating>
        {
            Id = rating.Id,
            Name = rating.Value.ToString()
        };
    }
    
    public static ModelRelated<Order> MapOrderToOrderRelated(Order order)
    {
        return new ModelRelated<Order>
        {
            Id = order.Id,
            Name = order.Id.ToString(),
        };
    }
    
    public static ModelRelated<Author> MapAuthorToAuthorRelated(Author author)
    {
        return new ModelRelated<Author>
        {
            Id = author.Id,
            Name = author.Name,
        };
    }
    
    public static AuthorDetail MapAuthorToAuthorDetail(Author author)
    {
        return new AuthorDetail
        {
            Id = author.Id,
            Name = author.Name,
            Books = author.Books.Select(MapBookToBookRelated).ToList()
        };
    }
    
    public static GenreDetail MapGenreToGenreDetail(Genre genre)
    {
        return new GenreDetail
        {
            Name = genre.Name,
            Books = genre.Books.Select(MapBookToBookRelated).ToList()
        };
    }
    
    public static PublisherDetail MapPublisherToPublisherDetail(Publisher publisher)
    {
        return new PublisherDetail
        {
            Name = publisher.Name,
            Books = publisher.Books.Select(MapBookToBookRelated).ToList()
        };
    }
    public static BookDetail MapBookToBookDetail(Book book)
    {
        return new BookDetail
        {
            Id = book.Id,
            Name = book.Name,
            Genre = MapGenreToGenreRelated(book.Genre),
            Publisher = MapPublisherToPublisherRelated(book.Publisher),
            Authors = book.Authors.Select(MapAuthorToAuthorRelated).ToList(),
            OverallRating = book.OverallRating,
            Price = book.Price,
            StockInStorage = book.StockInStorage
        };
    }
    public static RatingDetail MapRatingToRatingDetail(Rating rating)
    {
        return new RatingDetail
        {
            Id = rating.Id,
            Book = MapBookToBookRelated(rating.Book),
            User = MapUserToUserRelated(rating.User),
            Value = rating.Value,
            Comment = rating.Comment
        };
    }
}