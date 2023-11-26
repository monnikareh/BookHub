using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using BusinessLayer.Services;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TestUtilities.Data;
using TestUtilities.MockedObjects;
using Xunit.Abstractions;

namespace BusinessLayer.Tests.Services
{
    public class GenreServiceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddServices()
            .AddMockedDbContext();

        public GenreServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        
        [Fact]
        public async Task GetGenresAsync_ReturnsCorrectNumber()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
            await MockedDBContext.PrepareDataAsync(dbContext);

            var customerService = scope.ServiceProvider.GetRequiredService<IGenreService>();

            // Act
            var result = await customerService.GetGenresAsync(null);
            var genreDetails = result.ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(TestData.GetMockedGenres().Count(), genreDetails.Count);
        }

        [Fact]
        public async Task GetGenreByIdAsync_ExistingId_ReturnsGenre()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
            await MockedDBContext.PrepareDataAsync(dbContext);
            var genreService = scope.ServiceProvider.GetRequiredService<IGenreService>();

            var genreToGet = TestData.GetMockedGenres().First();

            // Act
            var result = await genreService.GetGenreByIdAsync(genreToGet.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(genreToGet.Name, result.Name);
        }
        
    }
}
