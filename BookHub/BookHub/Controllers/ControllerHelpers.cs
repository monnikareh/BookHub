using BookHub.Models;
using BookHub.Models.Details;
using DataAccessLayer.Entities;

namespace BookHub.Controllers;

public static class ControllerHelpers
{
    public static BookDetail MapBookToBookModel(Book book)
    {
        return new BookDetail
        {
            Id = book.Id,
            Name = book.Name,
            GenreName = book.Genre.Name,
            PublisherName = book.Publisher.Name,
            Authors = book.Authors.Select(a => new AuthorCreate { Name = a.Name }).ToList(),
            Price = book.Price,
            StockInStorage = book.StockInStorage
        };
    }
    
    public static AuthorDetail MapAuthorToAuthorDetail(Author author)
    {
        return new AuthorDetail
        {
            Name = author.Name,
            Books = author.Books.Select(MapBookToBookModel).ToList()
        };
    }
}