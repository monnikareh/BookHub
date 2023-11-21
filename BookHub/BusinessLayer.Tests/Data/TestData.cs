using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Tests.Data;

public class TestData
{
    private static readonly PasswordHasher<User> Hasher = new PasswordHasher<User>();
    public static List<Publisher> GetMockedPublishers()
    {
        return new List<Publisher>
        {
            new Publisher
            {
                Id = 1,
                Name = "Bloomsbury",
                Books = GetMockedBooks().Where(x => x.Id == 1).ToList()
            },
            new Publisher
            {
                Id = 2,
                Name = "Secker & Warburg",
                Books = GetMockedBooks().Where(x => x.Id >= 1 && x.Id <= 2).ToList()
            },
            new Publisher
            {
                Id = 3,
                Name = "Reynal & Hitchcock",
                Books = GetMockedBooks().Where(x => x.Id >= 1 && x.Id <= 3).ToList()
            },
            new Publisher
            {
                Id = 4,
                Name = "Penguin Books",
                Books = GetMockedBooks().Where(x => x.Id >= 1 && x.Id <= 4).ToList()

            },
            new Publisher
            {
                Id = 5,
                Name = "Random House",
                Books = GetMockedBooks().Where(x => x.Id >= 1 && x.Id <= 5).ToList()
            }
        };
    }

    public static List<Order> GetMockedOrders()
    {
        var random = new Random();
        var orders = new List<Order>();
        for (var i = 1; i <= 5; i++)
        {
            var randomPrice = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2);
            orders.Add(new Order
            {
                Id = i,
                UserId = random.Next(1, 15),
                TotalPrice = randomPrice
            });
        };
        return orders;
    }
    
    private static IEnumerable<Author> GetMockedAuthors()
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
            }
        };
    }

    private static IEnumerable<Genre> GetMockedGenres()
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
            }
        };
    }

    private static IEnumerable<Book> GetMockedBooks()
    {
        return new List<Book>
        {
            new Book
            {
                Id = 1,
                Name = "To Kill a Mockingbird",
                PublisherId = 1,
                StockInStorage = 20,
                Price = 12.5,
                OverallRating = 40,
                Publisher = GetMockedPublishers().First(x => x.Id == 1),
                Authors = GetMockedAuthors().Where(x => x.Id == 1).ToList(),
                Ratings = GetMockedRatings().Where(x => x.Id == 1).ToList(),
                Orders = GetMockedOrders().Where(x => x.Id == 1).ToList(),
                Genres = GetMockedGenres().Where(x => x.Id == 1).ToList(),
                Users = GetMockedUsers().Where(x => x.Id == 1).ToList()
            }
        };
    }

    private static IEnumerable<User> GetMockedUsers()
    {
        var users = new List<User>();
        var names = new List<string>
        {
            "Roman Mario",
            "Beth Story",
            "Monika Reha",
            "John Smith",
            "James Bond",
            "Filip Strong",
            "Random Guy",
            "Jack Black",
            "Tom Smart",
            "Ali Willy",
            "Rubber Duck",
            "Olaf Snow",
            "Good Programmer",
            "Tim King",
            "Adam Queen"
        };
        for (var i = 0; i < names.Count; i++)
        {
            var name = names[i].Substring(0, names[i].IndexOf(' '));
            var email = $"{name}@gmail.com";
            users.Add(new User
            {
                Id = i + 1,
                UserName = name.ToLower(),
                NormalizedUserName = name.ToUpper(),
                Name = names[i],
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = Hasher.HashPassword(null, "Aa123!")
            });
        }

        return users;
    }
    
    private static IEnumerable<IdentityRole<int>> GetMockedRoles()
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

    public static List<Rating> GetMockedRatings()
    {
        var random = new Random();
        var ratings = new List<Rating>();
        var comments = new List<string>
        {
            "Great book but it gave me an existential crisis bigger than I had before",
            "Couldn't put it down, finished it in one sitting!",
            "The plot twists in this book are mind-blowing",
            "A classic that everyone should read",
            "The characters are so well-developed, felt like they were real people",
            "This book made me laugh and cry, a roller coaster of emotions",
            "The writing style is beautiful, every sentence is a work of art",
            "I couldn't guess the ending, kept me guessing until the last page",
            "I wish there was a sequel, I'm not ready to say goodbye to these characters",
            "The themes explored in this book are thought-provoking",
            "The pacing is perfect, kept me engaged from start to finish",
            "This book challenged my perspective on life",
            "The world-building is exceptional, I felt like I was there",
            "A must-read for book lovers",
            "The author's storytelling is captivating",
            "This book is a page-turner, couldn't stop reading",
            "The dialogue between characters is witty and realistic",
            "I've recommended this book to all my friends",
            "It left me with a book hangover, couldn't stop thinking about it",
            "",
            "Couldn't get into the story, found it boring from the start",
            "The characters felt one-dimensional and uninteresting",
            "The plot was predictable, I expected more twists",
            "I didn't connect with the protagonist, lacked depth",
            "The writing style was confusing and hard to follow",
            "This book didn't live up to the hype, very disappointing",
            "The ending felt rushed and unresolved",
            "Too much exposition, not enough action",
            "I found the dialogue unrealistic and forced",
        };
        for (var i = 0; i < 10; i++)
        {
            ratings.Add(new Rating
            {
                Id = i + 1,
                UserId = random.Next(1, 15),
                BookId = random.Next(1, 35),
                Value = random.Next(10, 100),
                Comment = comments[i % comments.Count]
            });
        }
        return ratings;
    }
}