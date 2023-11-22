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

        return new List<Order>
        {
            new Order
            {
                Id = 1,
                UserId = 1,
                TotalPrice = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                User = GetMockedUsers().First(x => x.Id == 1),
                Date = new DateTime(2023, 3, 10),
                Books = GetMockedBooks().Where(x => x.Id >= 1).ToList()
            },
            new Order
            {
                Id = 2,
                UserId = 2,
                TotalPrice = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                User = GetMockedUsers().First(x => x.Id == 2),
                Date = new DateTime(2023, 1, 24),
                Books = GetMockedBooks().Where(x => x.Id >= 1 & x.Id <= 2).ToList()
            },
            new Order
            {
                Id = 3,
                UserId = 3,
                TotalPrice = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                User = GetMockedUsers().First(x => x.Id == 3),
                Date = new DateTime(2023, 7, 5),
                Books = GetMockedBooks().Where(x => x.Id >= 1 & x.Id <= 3).ToList()
            },
            new Order
            {
                Id = 4,
                UserId = 4,
                TotalPrice = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                User = GetMockedUsers().First(x => x.Id == 4),
                Date = new DateTime(2023, 12, 1),
                Books = GetMockedBooks().Where(x => x.Id >= 1 & x.Id <= 4).ToList()
            },
            new Order
            {
                Id = 5,
                UserId = 5,
                TotalPrice = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                User = GetMockedUsers().First(x => x.Id == 5),
                Date = new DateTime(2023, 4, 11),
                Books = GetMockedBooks().Where(x => x.Id >= 1 & x.Id <= 5).ToList()
            }
        };
    }
    
    private static IEnumerable<Author> GetMockedAuthors()
    {
        return new List<Author>
        {
            new Author
            {
                Id = 1,
                Name = "J. K. Rowling",
                Books = GetMockedBooks().Where(x => x.Id >= 1).ToList()
            },
            new Author
            {
                Id = 2,
                Name = "George R. R. Martin",
                Books = GetMockedBooks().Where(x => x.Id >= 1 & x.Id <= 2).ToList()
            },
            new Author
            {
                Id = 3,
                Name = "Stephen King",
                Books = GetMockedBooks().Where(x => x.Id >= 1 & x.Id <= 3).ToList()
            },
            new Author
            {
                Id = 4,
                Name = "Agatha Christie",
                Books = GetMockedBooks().Where(x => x.Id >= 1 & x.Id <= 4).ToList()
            },
            new Author
            {
                Id = 5,
                Name = "J.R.R. Tolkien",
                Books = GetMockedBooks().Where(x => x.Id >= 1 & x.Id <= 5).ToList()
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
                Name = "Dystopian",
                Books = GetMockedBooks().Where(x => x.Id >= 1).ToList()
            },
            new Genre
            {
                Id = 2,
                Name = "Mystery",
                Books = GetMockedBooks().Where(x => x.Id >= 1 & x.Id <= 2).ToList()
            },
            new Genre
            {
                Id = 3,
                Name = "Science Fiction",
                Books = GetMockedBooks().Where(x => x.Id >= 1 & x.Id <= 3).ToList()
            },
            new Genre
            {
                Id = 4,
                Name = "Romance",
                Books = GetMockedBooks().Where(x => x.Id >= 1 & x.Id <= 4).ToList()
            },
            new Genre
            {
                Id = 5,
                Name = "Fantasy",
                Books = GetMockedBooks().Where(x => x.Id >= 1 & x.Id <= 5).ToList()
            }
        };
    }

    private static IEnumerable<Book> GetMockedBooks()
    {
        var random = new Random();
        return new List<Book>
        {
            new Book
            {
                Id = 1,
                Name = "To Kill a Mockingbird",
                PublisherId = 1,
                StockInStorage = random.Next(1, 50),
                Price = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                OverallRating = random.Next(30, 100),
                Publisher = GetMockedPublishers().First(x => x.Id == 1),
                Authors = GetMockedAuthors().Where(x => x.Id == 1).ToList(),
                Ratings = GetMockedRatings().Where(x => x.Id == 1).ToList(),
                Orders = GetMockedOrders().Where(x => x.Id == 1).ToList(),
                Genres = GetMockedGenres().Where(x => x.Id == 1).ToList(),
                Users = GetMockedUsers().Where(x => x.Id == 1).ToList()
            },
            new Book
            {
                Id = 2,
                Name = "Harry Potter",
                PublisherId = 2,
                StockInStorage = random.Next(1, 50),
                Price = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                OverallRating = random.Next(30, 100),
                Publisher = GetMockedPublishers().First(x => x.Id == 2),
                Authors = GetMockedAuthors().Where(x => x.Id >= 1 & x.Id <= 2).ToList(),
                Ratings = GetMockedRatings().Where(x => x.Id >= 1 & x.Id <= 2).ToList(),
                Orders = GetMockedOrders().Where(x => x.Id >= 1 & x.Id <= 2).ToList(),
                Genres = GetMockedGenres().Where(x => x.Id >= 1 & x.Id <= 2).ToList(),
                Users = GetMockedUsers().Where(x => x.Id >= 1 & x.Id <= 2).ToList()
            },
            new Book
            {
                Id = 3,
                Name = "Narnia",
                PublisherId = 3,
                StockInStorage = random.Next(1, 50),
                Price = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                OverallRating = random.Next(30, 100),
                Publisher = GetMockedPublishers().First(x => x.Id == 3),
                Authors = GetMockedAuthors().Where(x => x.Id >= 1 & x.Id <= 3).ToList(),
                Ratings = GetMockedRatings().Where(x => x.Id >= 1 & x.Id <= 3).ToList(),
                Orders = GetMockedOrders().Where(x => x.Id >= 1 & x.Id <= 3).ToList(),
                Genres = GetMockedGenres().Where(x => x.Id >= 1 & x.Id <= 3).ToList(),
                Users = GetMockedUsers().Where(x => x.Id >= 1 & x.Id <= 3).ToList()
            },
            new Book
            {
                Id = 4,
                Name = "Pride and Prejudice",
                PublisherId = 4,
                StockInStorage = random.Next(1, 50),
                Price = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                OverallRating = random.Next(30, 100),
                Publisher = GetMockedPublishers().First(x => x.Id == 4),
                Authors = GetMockedAuthors().Where(x => x.Id >= 1 & x.Id <= 4).ToList(),
                Ratings = GetMockedRatings().Where(x => x.Id >= 1 & x.Id <= 4).ToList(),
                Orders = GetMockedOrders().Where(x => x.Id >= 1 & x.Id <= 4).ToList(),
                Genres = GetMockedGenres().Where(x => x.Id >= 1 & x.Id <= 4).ToList(),
                Users = GetMockedUsers().Where(x => x.Id >= 1 & x.Id <= 4).ToList()
            },
            new Book
            {
                Id = 5,
                Name = "Eragon",
                PublisherId = 5,
                StockInStorage = random.Next(1, 50),
                Price = (decimal)Math.Round(random.NextDouble() * (55 - 5) + 5, 2),
                OverallRating = random.Next(30, 100),
                Publisher = GetMockedPublishers().First(x => x.Id == 5),
                Authors = GetMockedAuthors().Where(x => x.Id >= 1 & x.Id <= 5).ToList(),
                Ratings = GetMockedRatings().Where(x => x.Id >= 1 & x.Id <= 5).ToList(),
                Orders = GetMockedOrders().Where(x => x.Id >= 1 & x.Id <= 5).ToList(),
                Genres = GetMockedGenres().Where(x => x.Id >= 1 & x.Id <= 5).ToList(),
                Users = GetMockedUsers().Where(x => x.Id >= 1 & x.Id <= 5).ToList()
            },
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
                PasswordHash = Hasher.HashPassword(null, "Aa123!"),
                Books = GetMockedBooks().Where(x => x.Id >= i).ToList(),
                Ratings = GetMockedRatings().Where(x => x.Id == i).ToList(),
                Orders = GetMockedOrders().Where(x => x.Id == i).ToList()
            });
        }
        return users;
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
            "The characters are so well-developed, felt like they were real people"
        };
        for (var i = 0; i < 5; i++)
        {
            ratings.Add(new Rating
            {
                Id = i + 1,
                UserId = random.Next(1, 15),
                BookId = random.Next(1, 35),
                Value = random.Next(10, 100),
                Comment = comments[i],
                Book = GetMockedBooks().First(x => x.Id == i + 1)
            });
        }
        return ratings;
    }
}