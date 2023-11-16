using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public static class DataSeeder
{
    private static readonly PasswordHasher<User> Hasher = new PasswordHasher<User>();

    public static void Seed(this ModelBuilder modelBuilder)
    {
        Console.WriteLine("Seeding!");
        modelBuilder.Entity<Author>().HasData(PrepareAuthors());
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().HasData(PrepareUsers());
        modelBuilder.Entity<IdentityRole<int>>().HasData(PrepareRoles());
        modelBuilder.Entity<Publisher>().HasData(PreparePublishers());
        modelBuilder.Entity<Genre>().HasData(PrepareGenres());
        modelBuilder.Entity<Book>().HasData(PrepareBooks());
        modelBuilder.Entity<Rating>().HasData(PrepareRatings());
        modelBuilder.Entity<Order>().HasData(PrepareOrders());
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
            new { BooksId = 1, UsersId = 1 },
            new { BooksId = 2, UsersId = 1 },
            new { BooksId = 3, UsersId = 3 },
            new { BooksId = 4, UsersId = 2 });
        modelBuilder.Entity("BookOrder").HasData(
            new { BooksId = 1, OrdersId = 1 },
            new { BooksId = 2, OrdersId = 1 },
            new { BooksId = 4, OrdersId = 2 });
        modelBuilder.Entity<IdentityUserRole<int>>().HasData(
            new { UserId = 1, RoleId = 1 },
            new { UserId = 2, RoleId = 1 },
            new { UserId = 3, RoleId = 1 },
            new { UserId = 4, RoleId = 2 });
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
                Name = "George R. R. Martin"
            },
            new Author
            {
                Id = 3,
                Name = "Stephen King"
            },
            new Author
            {
                Id = 4,
                Name = "Agatha Christie"
            },
            new Author
            {
                Id = 5,
                Name = "J.R.R. Tolkien"
            },
            new Author
            {
                Id = 6,
                Name = "Jane Austen"
            },
            new Author
            {
                Id = 7,
                Name = "Mark Twain"
            },
            new Author
            {
                Id = 8,
                Name = "Charles Dickens"
            },
            new Author
            {
                Id = 9,
                Name = "Harper Lee"
            },
            new Author
            {
                Id = 10,
                Name = "Leo Tolstoy"
            },
            new Author
            {
                Id = 11,
                Name = "Agnes Christie"
            },
            new Author
            {
                Id = 12,
                Name = "Ernest Hemingway"
            },
            new Author
            {
                Id = 13,
                Name = "Virginia Woolf"
            },
            new Author
            {
                Id = 14,
                Name = "Arthur Conan Doyle"
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
            },
            new Publisher
            {
                Id = 4,
                Name = "Penguin Books"
            },
            new Publisher
            {
                Id = 5,
                Name = "Random House"
            },
            new Publisher
            {
                Id = 6,
                Name = "HarperCollins"
            },
            new Publisher
            {
                Id = 7,
                Name = "Simon & Schuster"
            },
            new Publisher
            {
                Id = 8,
                Name = "Macmillan Publishers"
            },
            new Publisher
            {
                Id = 9,
                Name = "Hachette Book Group"
            },
            new Publisher
            {
                Id = 10,
                Name = "Scholastic Corporation"
            },
            new Publisher
            {
                Id = 11,
                Name = "Oxford University Press"
            },
            new Publisher
            {
                Id = 12,
                Name = "Cambridge University Press"
            },
            new Publisher
            {
                Id = 13,
                Name = "Wiley"
            },
            new Publisher
            {
                Id = 14,
                Name = "Elsevier"
            },
            new Publisher
            {
                Id = 15,
                Name = "Springer"
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
                Name = "Dystopian"
            },
            new Genre
            {
                Id = 2,
                Name = "Mystery"
            },
            new Genre
            {
                Id = 3,
                Name = "Science Fiction"
            },
            new Genre
            {
                Id = 4,
                Name = "Romance"
            },
            new Genre
            {
                Id = 5,
                Name = "Fantasy"
            },
            new Genre
            {
                Id = 6,
                Name = "Thriller"
            },
            new Genre
            {
                Id = 7,
                Name = "Historical Fiction"
            },
            new Genre
            {
                Id = 8,
                Name = "Horror"
            },
            new Genre
            {
                Id = 9,
                Name = "Adventure"
            },
            new Genre
            {
                Id = 10,
                Name = "Biography"
            },
            new Genre
            {
                Id = 11,
                Name = "Non-fiction"
            },
            new Genre
            {
                Id = 12,
                Name = "Children's"
            },
            new Genre
            {
                Id = 13,
                Name = "Young Adult"
            },
            new Genre
            {
                Id = 14,
                Name = "Humor"
            },
            new Genre
            {
                Id = 15,
                Name = "Crime"
            },
            new Genre
            {
                Id = 16,
                Name = "Historical Romance"
            },
            new Genre
            {
                Id = 17,
                Name = "Western"
            },
            new Genre
            {
                Id = 18,
                Name = "Science Fantasy"
            },
            new Genre
            {
                Id = 19,
                Name = "Self-help"
            },
            new Genre
            {
                Id = 20,
                Name = "Cookbook"
            }
        };
    }

    private static IEnumerable<Book> PrepareBooks()
    {
        var books = new List<Book>();
        var random = new Random();
        var names = new List<string> {
            "To Kill a Mockingbird",
            "1984",
            "The Great Gatsby",
            "One Hundred Years of Solitude",
            "The Catcher in the Rye",
            "Brave New World",
            "The Hobbit",
            "Pride and Prejudice",
            "The Lord of the Rings: The Fellowship of the Ring",
            "The Chronicles of Narnia: The Lion, the Witch and the Wardrobe",
            "Harry Potter and the Philosopher's Stone",
            "The Hunger Games",
            "The Da Vinci Code",
            "A Game of Thrones",
            "The Shining",
            "The Hitchhiker's Guide to the Galaxy",
            "The Alchemist",
            "War and Peace",
            "Crime and Punishment",
            "The Catch-22",
            "The Grapes of Wrath",
            "Fahrenheit 451",
            "Lord of the Flies",
            "Moby-Dick",
            "Frankenstein",
            "Alice's Adventures in Wonderland",
            "Dracula",
            "The Odyssey",
            "Romeo and Juliet",
            "Hamlet",
            "Macbeth",
            "Othello",
            "The Divine Comedy",
            "Don Quixote"};

        for (var i = 1; i <= names.Count; i++)
        {
            var randomPrice = Math.Round(random.NextDouble() * (25 - 5) + 5, 2);

            books.Add(new Book
            {
                Id = i,
                Name = names[i],
                PublisherId = 1,
                StockInStorage = random.Next(1, 50),
                Price = randomPrice,
                OverallRating = random.Next(30, 100)
            });
        }

        return books;
    }

    private static IEnumerable<User> PrepareUsers()
    {
        return new List<User>
        {
            new User
            {
                Id = 1,
                UserName = "vidlacka",
                NormalizedUserName = "VIDLACKA",
                Name = "Monca",
                Email = "m@m.com",
                NormalizedEmail = "M@M.COM",
                EmailConfirmed = true,
                SecurityStamp =  Guid.NewGuid().ToString(),
                PasswordHash = Hasher.HashPassword(null, "Aa123!")
            },
            new User
            {
                Id = 2,
                UserName = "betatesting",
                NormalizedUserName = "BETATESTING",
                Name = "Betka",
                Email = "b@b.com",
                NormalizedEmail = "B@B.COM",
                EmailConfirmed = true,
                SecurityStamp =  Guid.NewGuid().ToString(),
                PasswordHash = Hasher.HashPassword(null, "Aa123!")
            },
            new User
            {
                Id = 3,
                UserName = "maromcik",
                NormalizedUserName = "MAROMCIK",
                Name = "Romik",
                Email = "r@r.com",
                NormalizedEmail = "R@R.COM",
                EmailConfirmed = true,
                SecurityStamp =  Guid.NewGuid().ToString(),
                PasswordHash = Hasher.HashPassword(null, "Aa123!")
            },
            new User
            {
                Id = 4,
                UserName = "jozis",
                NormalizedUserName = "JOZIS",
                Name = "Jozko Gulas",
                Email = "j@j.com",
                NormalizedEmail = "J@J.COM",
                EmailConfirmed = true,
                SecurityStamp =  Guid.NewGuid().ToString(),
                PasswordHash = Hasher.HashPassword(null, "Aa123!")
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
                TotalPrice = 28.03m,
            },
            new Order
            {
                Id = 2,
                UserId = 2,
                TotalPrice = 9.99m,
            },
        };
    }

    private static IEnumerable<IdentityRole<int>> PrepareRoles()
    {
        return new List<IdentityRole<int>>
        {
            new IdentityRole<int>
            {
                Id = 1,
                Name = UserRoles.Admin,
                NormalizedName = UserRoles.Admin.ToUpper(),
            },
            new IdentityRole<int>
            {
                Id = 2,
                Name = UserRoles.User,
                NormalizedName = UserRoles.User.ToUpper(),
            }
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
                Comment =
                    "Harry Potter and the Sorcerer's Stone is the ultimate tale of what happens when a boy who's been living in a cupboard under the stairs discovers a world of magic. I mean, who knew that an owl could deliver mail or that a cloak could make you disappear faster than an introvert at a party?"
            },
            new Rating
            {
                Id = 4,
                UserId = 1,
                BookId = 2,
                Value = 88,
                Comment =
                    "Harry Potter and the Chamber of Secrets is like a laugh potion that will have you chuckling faster than you can say \"Expelliarmus!\" The boy who lived is back, and this time, he's got a flying car and a house-elf who's obsessed with socks. In this book, you'll discover that Hogwarts doesn't just have ghosts in the hallways; it also has diary-possessing dark lords, giant snakes with dental issues, and a loony professor who could probably talk to garden gnomes."
            },
            new Rating
            {
                Id = 5,
                UserId = 3,
                BookId = 2,
                Value = 71,
                Comment =
                    "Apparently, Hogwarts' idea of school security is letting students fight giant serpents, navigate secret chambers, and brew potions that make them look like their arch-enemies (because that's definitely a practical skill for later life). So, if you're in the mood for a hilarious lesson on \"what not to do in school,\" Harry Potter and the Chamber of Secrets is your go-to guide. Just remember, when a disembodied voice tells you to \"follow the spiders,\" it's probably best to just stay indoors and read a good book instead."
            }
        };
    }
}