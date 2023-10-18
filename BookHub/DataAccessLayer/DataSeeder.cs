using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public static class DataSeeder
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        Console.WriteLine("Seeding!");
        modelBuilder.Entity<Author>().HasData(PrepareAuthors());
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().HasData(PrepareUsers());
        modelBuilder.Entity<Publisher>().HasData(PreparePublishers());
        modelBuilder.Entity<Genre>().HasData(PrepareGenres());
        modelBuilder.Entity<Book>().HasData(PrepareBooks());
        modelBuilder.Entity<Rating>().HasData(PrepareRatings());
        modelBuilder.Entity("AuthorBook").HasData(
            new { AuthorsId = 1, BooksId = 1 }, 
            new { AuthorsId = 1, BooksId = 2 },
            new { AuthorsId = 2, BooksId = 3 },
            new { AuthorsId = 3, BooksId = 4 });
        modelBuilder.Entity("BookGenre").HasData(
            new { BooksId = 1, GenresId = 5 },
            new { BooksId = 2, GenresId = 5 },
            new { BooksId = 3, GenresId = 2 },
            new { BooksId = 3, GenresId = 3 },
            new { BooksId = 4, GenresId = 5 });
        // Wishlist
        modelBuilder.Entity("BookUser").HasData(
            new { BooksId = 1, UserId = 1 },
            new { BooksId = 2, UserId = 1 },
            new { BooksId = 3, UserId = 3 },
            new { BooksId = 4, UserId = 2 });
        modelBuilder.Entity("BookOrder").HasData(
            new { BooksId = 1, UserId = 1 },
            new { BooksId = 2, UserId = 1 },
            new { BooksId = 4, UserId = 3 });
    }

    private static IEnumerable<Author> PrepareAuthors()
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
                Name = "Antoine de Saint-Exupéry"
            }
        };
    }

    private static IEnumerable<Publisher> PreparePublishers()
    {
        return new List<Publisher>
        {
            new Publisher
            {
                Id = 1,
                Name = "Bloomsbury"
            },
            new Publisher
            {
            Id = 2,
            Name = "Secker & Warburg"
            },
            new Publisher
            {
            Id = 3,
            Name = "Reynal & Hitchcock"
        }
        };
    }

    private static IEnumerable<Genre> PrepareGenres()
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
            new Genre
            {
                Id = 4,
                Name = "Horor",
            },
            new Genre
            {
                Id = 5,
                Name = "Fantasy",
            },
        };
    }

    private static IEnumerable<Book> PrepareBooks()
    {
        return new List<Book>
        {
            new Book
            {
                Id = 1,
                Name = "Harry Potter and the Philosopher's Stone",
                PublisherId = 1,
                StockInStorage = 42,
                Price = 15.3,
                OverallRating = 97,
            },
            new Book
            {
                Id = 2,
                Name = "Harry Potter and the Chamber of Secrets",
                PublisherId = 1,
                StockInStorage = 6,
                Price = 12.73,
                OverallRating = 92,
            },
            new Book
            {
                Id = 3,
                Name = "1984",
                PublisherId = 2,
                StockInStorage = 15,
                Price = 19.1,
                OverallRating = 87,
            },
            new Book
            {
                Id = 4,
                Name = "The Little Prince",
                PublisherId = 3,
                StockInStorage = 23,
                Price = 9.99,
                OverallRating = 99,
            }
        };
    }

    private static IEnumerable<User> PrepareUsers()
    {
        return new List<User>
        {
            new User
            {
                Id = 1,
                UserName = "vidlacka",
                Name = "Monca",
                IsAdmin = true
            },
            new User
            {
                Id = 2,
                UserName = "betatesting",
                Name = "Betka",
                IsAdmin = true
            },
            new User
            {
                Id = 3,
                UserName = "maromcik",
                Name = "Romik",
                IsAdmin = true
            },
        };
    }
    private static IEnumerable<Order> PrepareOrders()
    {
        return new List<Order>
        {
            new Order
            {
                Id = 1,
                UserId = 1,
                TotalPrice = 28.03,
                Date = DateTime.Today
            },
            new Order
            {
                Id = 2,
                UserId = 2,
                TotalPrice = 9.99,
                Date = DateTime.Today
            },
        };
    }
    private static IEnumerable<Rating> PrepareRatings()
    {
        return new List<Rating>
        {
            new Rating
            {
                Id = 1,
                UserId = 1,
                BookId = 4,
                Value = 98,
                Comment = "Great book but it gave me an existential crisis bigger than I had before"
            },
            new Rating
            {
                Id = 2,
                UserId = 2,
                BookId = 3,
                Value = 42,
                Comment = ""
            },
            new Rating
            {
                Id = 3,
                UserId = 3,
                BookId = 1,
                Value = 91,
                Comment = "Harry Potter and the Sorcerer's Stone is the ultimate tale of what happens when a boy who's been living in a cupboard under the stairs discovers a world of magic. I mean, who knew that an owl could deliver mail or that a cloak could make you disappear faster than an introvert at a party?"
            }
            ,
            new Rating
            {
                Id = 4,
                UserId = 1,
                BookId = 2,
                Value = 88,
                Comment = "Harry Potter and the Chamber of Secrets is like a laugh potion that will have you chuckling faster than you can say \"Expelliarmus!\" The boy who lived is back, and this time, he's got a flying car and a house-elf who's obsessed with socks. In this book, you'll discover that Hogwarts doesn't just have ghosts in the hallways; it also has diary-possessing dark lords, giant snakes with dental issues, and a loony professor who could probably talk to garden gnomes."
            }
            ,
            new Rating
            {
                Id = 5,
                UserId = 3,
                BookId = 2,
                Value = 71,
                Comment = "Apparently, Hogwarts' idea of school security is letting students fight giant serpents, navigate secret chambers, and brew potions that make them look like their arch-enemies (because that's definitely a practical skill for later life). So, if you're in the mood for a hilarious lesson on \"what not to do in school,\" Harry Potter and the Chamber of Secrets is your go-to guide. Just remember, when a disembodied voice tells you to \"follow the spiders,\" it's probably best to just stay indoors and read a good book instead."
            }
        };
    }
}