using BusinessLayer.Models;
using BusinessLayer.Services;
using BusinessLayer.Tests.MockedObjects;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TestUtilities.MockedObjects;
using Xunit;

namespace BusinessLayer.Tests.Services
{
    public class PublisherServiceTests
    {
        private MockedDependencyInjectionBuilder _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddServices()
            .AddMockedDbContext();

        [Fact]
        public async Task DoesPublishersExistAsync_ShouldReturnTrueWhenAllUsersExist()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();

            using (var scope = serviceProvider.CreateScope())
            {
                var publisherService = scope.ServiceProvider.GetRequiredService<PublisherService>();

                // Act
                var result = await publisherService.DoesPublishersExistAsync(new int[] { 1, 2, 3, 4, 5 });

                // Assert
                Assert.True(result);
            }
        }
    }
}
