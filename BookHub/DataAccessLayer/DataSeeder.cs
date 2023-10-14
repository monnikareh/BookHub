using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public static class DataSeeder
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        Console.WriteLine("Seeding!");
        var authors = PrepareAuthors();
        var publishers = PreparePublishers();
        var genres = PrepareGenres();
        var books = PrepareBooks();
        modelBuilder.Entity<Author>().HasData(authors);
        modelBuilder.Entity<Publisher>().HasData(publishers);
        modelBuilder.Entity<Genre>().HasData(genres);
        modelBuilder.Entity<Book>().HasData(books);
        modelBuilder.Entity("AuthorBook").HasData(new { AuthorsId = 2, BooksId = 1 });
        modelBuilder.Entity("BookGenre").HasData(new { BooksId = 1, GenresId = 1 },
            new { BooksId = 1, GenresId = 2 },
            new { BooksId = 1, GenresId = 3 });
    }

    private static List<Author> PrepareAuthors()
    {
        return new List<Author>
        {
            new Author
            {
                Id = 1,
                Name = "J. K. Rowling"
            },
            new Author
            {
                Id = 2,
                Name = "George Orwell"
            },
            new Author
            {
                Id = 3,
                Name = "Peter Popluhar"
            }
        };
    }

    private static List<Publisher> PreparePublishers()
    {
        return new List<Publisher>
        {
            new Publisher
            {
                Id = 1,
                Name = "Secker & Warburg"
            }
        };
    }

    private static List<Genre> PrepareGenres()
    {
        return new List<Genre>
        {
            new Genre
            {
                Id = 1,
                Name = "Dystopian",
            },
            new Genre
            {
                Id = 2,
                Name = "Political fiction",
            },
            new Genre
            {
                Id = 3,
                Name = "Social science fiction",
            },
        };
    }

    private static List<Book> PrepareBooks()
    {
        return new List<Book>
        {
            new Book
            {
                Id = 1,
                Name = "1984",
                PublisherId = 1,
                StockInStorage = 21,
                Price = 15.3,
                OverallRating = 100,
            }
        };
    }
}