using BookHub.Models;
using DataAccessLayer.Entities;

namespace BookHub.Controllers;

public static class ControllerHelpers
{
    public static BookDetail MapBookToBookDetail(Book book)
    {
        return new BookDetail
        {
            Id = book.Id,
            Name = book.Name,
            Genre = MapGenreToGenreRelated(book.Genre),
            Publisher = MapPublisherToPublisherRelated(book.Publisher),
            Authors = book.Authors.Select(MapAuthorToAuthorRelated).ToList(),
            Price = book.Price,
            StockInStorage = book.StockInStorage
        };
    }
    
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
            Name = author.Name,
            Books = author.Books.Select(MapBookToBookRelated).ToList()
        };
    }
}