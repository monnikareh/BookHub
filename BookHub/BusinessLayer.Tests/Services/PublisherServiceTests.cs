using BusinessLayer.Models;
using BusinessLayer.Services;
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
        public async Task DoesPublisherExistAsync()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();

            using (var scope = serviceProvider.CreateScope())
            {
                var publisherService = scope.ServiceProvider.GetRequiredService<IPublisherService>();

                // Act
                var result = await publisherService.GetPublisherByIdAsync(1);
                
                // Assert
                Assert.True(result != null);
            }
        }
    }
}
