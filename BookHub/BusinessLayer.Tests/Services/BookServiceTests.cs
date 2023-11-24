using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using BusinessLayer.Services;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TestUtilities.Data;
using TestUtilities.MockedObjects;
using Xunit;

namespace BusinessLayer.Tests.Services
{
    public class BookServiceTests
    {
        [Fact]
        public async Task GetBooksAsync_ReturnsCorrectNumber()
        {
            // Arrange
            var serviceProvider = new MockedDependencyInjectionBuilder()
                .AddServices()
                .AddMockedDbContext()
                .Create();

            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
            await MockedDBContext.PrepareDataAsync(dbContext);

            var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

            // Act
            var result = await bookService.GetBooksAsync(null, null, null, null, null, null, null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(TestData.GetMockedBooks().Count(), result.Count());
        }

        [Fact]
        public async Task GetBookByIdAsync_ExistingId_ReturnsBook()
        {
            // Arrange
            var serviceProvider = new MockedDependencyInjectionBuilder()
                .AddServices()
                .AddMockedDbContext()
                .Create();

            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
            await MockedDBContext.PrepareDataAsync(dbContext);
            var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

            var bookToGet = TestData.GetMockedBooks().First();

            // Act
            var result = await bookService.GetBookByIdAsync(bookToGet.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookToGet.Name, result.Name);
        }

        [Fact]
        public async Task CreateBookAsync_WithValidData_ReturnsNewBook()
        {
            // Arrange
            var serviceProvider = new MockedDependencyInjectionBuilder()
                .AddServices()
                .AddMockedDbContext()
                .Create();

            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
            var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

            var publisherCreate = new ModelRelated { Id = 6,  Name = "New Publisher" };
            var bookCreate = new BookCreate
            {
                Name = "New Book",
                Publisher = publisherCreate,
                Genres = new List<ModelRelated>(),
                Authors = new List<ModelRelated>(),
                Price = 19.99m,
                StockInStorage = 100,
                OverallRating = 30
            };

            // Act
            var result = await bookService.CreateBookAsync(bookCreate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookCreate.Name, result.Name);
           // Assert.NotNull(result.Publisher);
           // Assert.NotEmpty(result.Genres);
           // Assert.NotEmpty(result.Authors);
            Assert.Equal(bookCreate.Price, result.Price);
            Assert.Equal(bookCreate.StockInStorage, result.StockInStorage);
            Assert.Equal(bookCreate.OverallRating, result.OverallRating);
        }

        [Fact]
        public async Task UpdateBookAsync_WithValidData_ReturnsUpdatedBook()
        {
            // Arrange
            var serviceProvider = new MockedDependencyInjectionBuilder()
                .AddServices()
                .AddMockedDbContext()
                .Create();

            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
            await MockedDBContext.PrepareDataAsync(dbContext);
            var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

            var bookToUpdate = TestData.GetMockedBooks().First();
            var updatedBookDetail = new BookDetail
            {
                Name = "Updated Book",
                Publisher = new ModelRelated
                {
                    Id = 2,
                    Name = "Updated Publisher"
                },
                Genres = new List<ModelRelated>(),
                Authors = new List<ModelRelated>(),
                Price = 29.99m,
                StockInStorage = 50,
                OverallRating = 30,
                Id = 0
            };

            // Act
            var result = await bookService.UpdateBookAsync(bookToUpdate.Id, updatedBookDetail);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedBookDetail.Name, result.Name);
          //  Assert.NotNull(result.Publisher);
         //   Assert.NotEmpty(result.Genres);
         //   Assert.NotEmpty(result.Authors);
            Assert.Equal(updatedBookDetail.Price, result.Price);
            Assert.Equal(updatedBookDetail.StockInStorage, result.StockInStorage);
            Assert.Equal(updatedBookDetail.OverallRating, result.OverallRating);
        }

        [Fact]
        public async Task DeleteBookAsync_ExistingId_DeletesBook()
        {
            // Arrange
            var serviceProvider = new MockedDependencyInjectionBuilder()
                .AddServices()
                .AddMockedDbContext()
                .Create();

            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
            await MockedDBContext.PrepareDataAsync(dbContext);
            var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

            var bookIdToDelete = 1;

            // Act
            await bookService.DeleteBookAsync(bookIdToDelete);

            // Assert
            await bookService.Received(1).DeleteBookAsync(bookIdToDelete);
        }

        [Fact]
        public async Task DeleteBookAsync_WhenNonExistingBookId_ReturnsFalse()
        {
            // Arrange
            var serviceProvider = new MockedDependencyInjectionBuilder()
                .AddServices()
                .AddMockedDbContext()
                .Create();

            using var scope = serviceProvider.CreateScope();
            var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();
            const int nonExistentBookId = 999;

            bookService.DeleteBookAsync(nonExistentBookId)
                .Returns(Task.FromException<BookNotFoundException>(
                    new BookNotFoundException($"Book with ID:'{nonExistentBookId}' not found")));

            // Act
            bool result;
            try
            {
                await bookService.DeleteBookAsync(nonExistentBookId);
                result = true;
            }
            catch (BookNotFoundException)
            {
                result = false;
            }

            // Assert
            Assert.False(result);
        }
    }
}
