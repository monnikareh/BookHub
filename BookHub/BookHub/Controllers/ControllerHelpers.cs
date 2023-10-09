using BookHub.Models;
using BookHub.Models.Details;
using DataAccessLayer.Entities;

namespace BookHub.Controllers;

public class ControllerHelpers
{
    public static BookModel MapBookToBookModel(Book book)
    {
        return new BookModel
        {
            Name = book.Name,
            GenreName = book.Genre.Name,
            PublisherName = book.Publisher.Name,
            Authors = book.Authors.Select(a => new AuthorModel { Name = a.Name }).ToList(),
            Price = book.Price,
            StockInStorage = book.StockInStorage
        };
    }
    
    public static AuthorDetail MapAuthorToAuthorDetail(Author author)
    {
        return new AuthorDetail
        {
            Name = author.Name,
            BookNames = author.Books.Select(b => b.Name).ToList()
        };
    }
}