using BookHub.Models;
using DataAccessLayer.Entities;

namespace BookHub.Controllers;

public static class ControllerHelpers
{
  
    public static ModelRelated MaToRelated<T>(T model) where T: IModelRelated
    {
        return new ModelRelated
        {
            Id = model.Id,
            Name = model.Name,
        };
    }
    
    public static AuthorDetail MapAuthorToAuthorDetail(Author author)
    {
        return new AuthorDetail
        {
            Id = author.Id,
            Name = author.Name,
            Books = author.Books.Select(MaToRelated).ToList()
        };
    }
    
    public static GenreDetail MapGenreToGenreDetail(Genre genre)
    {
        return new GenreDetail
        {
            Name = genre.Name,
            Books = genre.Books.Select(MaToRelated).ToList()
        };
    }
    
    public static PublisherDetail MapPublisherToPublisherDetail(Publisher publisher)
    {
        return new PublisherDetail
        {
            Name = publisher.Name,
            Books = publisher.Books.Select(MaToRelated).ToList()
        };
    }
    public static BookDetail MapBookToBookDetail(Book book)
    {
        return new BookDetail
        {
            Id = book.Id,
            Name = book.Name,
            Genre = MaToRelated(book.Genre),
            Publisher = MaToRelated(book.Publisher),
            Authors = book.Authors.Select(MaToRelated).ToList(),
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
            Book = MaToRelated(rating.Book),
            User = MaToRelated(rating.User),
            Value = rating.Value,
            Comment = rating.Comment
        };
    }
}