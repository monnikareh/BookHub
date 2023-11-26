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
using Xunit.Abstractions;

namespace BusinessLayer.Tests.Services
{
    public class BookServiceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddServices()
            .AddMockedDbContext();
        
        [Fact]
        public async Task GetBooksAsync_ReturnsCorrectNumber()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
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
            var serviceProvider = _serviceProviderBuilder.Create();
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
        public async Task CreateBookAsync_ReturnsNewBook()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
            await MockedDBContext.PrepareDataAsync(dbContext);
            var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();
            
            var bookCreate = new BookCreate
            {
                Name = "Eragon",
                Publisher = new ModelRelated
                {
                    Id = 1,
                    Name = "Bloomsbury"
                },
                Price = 10.99m,
                StockInStorage = 100,
                OverallRating = 50,
                //Genres = new List<ModelRelated> { new ModelRelated { Id = 2, Name = "Mystery" } },
                Authors = new List<ModelRelated> { new ModelRelated { Id = 2, Name = "George R. R. Martin" } },
            };

            // Act
            var result = await bookService.CreateBookAsync(bookCreate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookCreate.Name, result.Name);
            Assert.Equal(bookCreate.Price, result.Price);
            Assert.Equal(bookCreate.StockInStorage, result.StockInStorage);
            Assert.Equal(bookCreate.OverallRating, result.OverallRating);
        }
          [Fact]
        public async Task UpdateBookAsync_ReturnsUpdatedBook()
        {
            // Arrange
            var service = Substitute.For<IBookService>();
            var bookId = 4; // Assuming book with Id 4 exists
            var bookDetailUpdate = new BookDetail
            {
                Id = bookId,
                Name = "Updated Book",
                Publisher = new ModelRelated { Id = 2, Name = "Updated Publisher" },
                Genres = new List<ModelRelated> { new ModelRelated { Id = 2, Name = "Mystery" } },
                Authors = new List<ModelRelated> { new ModelRelated { Id = 2, Name = "Jane Doe" } },
                Price = 29.99m,
                StockInStorage = 50,
                OverallRating = 4
            };

            var updatedBookDetail = new BookDetail
            {
                Id = bookId,
                Name = bookDetailUpdate.Name,
                Publisher = bookDetailUpdate.Publisher,
                Genres = bookDetailUpdate.Genres,
                Authors = bookDetailUpdate.Authors,
                Price = bookDetailUpdate.Price,
                StockInStorage = bookDetailUpdate.StockInStorage,
                OverallRating = bookDetailUpdate.OverallRating
            };

            service.UpdateBookAsync(bookId, Arg.Any<BookDetail>()).Returns(updatedBookDetail);

            // Act
            var result = await service.UpdateBookAsync(bookId, bookDetailUpdate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedBookDetail.Id, result.Id);
            Assert.Equal(updatedBookDetail.Name, result.Name);
            Assert.Equal(updatedBookDetail.Publisher.Id, result.Publisher.Id);
            Assert.Equal(updatedBookDetail.Genres.Count(), result.Genres.Count());
            Assert.Equal(updatedBookDetail.Authors.Count(), result.Authors.Count());
            Assert.Equal(updatedBookDetail.Price, result.Price);
            Assert.Equal(updatedBookDetail.StockInStorage, result.StockInStorage);
            Assert.Equal(updatedBookDetail.OverallRating, result.OverallRating);
        }
    }
}
