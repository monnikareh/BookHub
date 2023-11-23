using BusinessLayer.Services;
using BusinessLayer.Tests.MockedObjects;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TestUtilities;

namespace BusinessLayer.Tests.services
{
    public class BookServiceTests
    {
        private MockedDependencyInjectionBuilder _serviceProviderBuilder;

        public BookServiceTests()
        {
            _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
                .AddServices()
                .AddMockedDbContext();
        }

        [Fact]
        public async Task DoesBooksExistAsync_NullBooksCount_ReturnsZero()
        {
            var serviceProvider = _serviceProviderBuilder.Create();

            using var scope = serviceProvider.CreateScope();
            var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

            // Act
            var result = await bookService.GetBooksAsync(null, null, null,null,null,null,null);

            // Assert
            Assert.True(!result.Any());
        }
    }
}