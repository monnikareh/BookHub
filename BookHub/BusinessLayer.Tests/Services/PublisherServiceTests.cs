using BusinessLayer.Models;
using BusinessLayer.Services;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TestUtilities.Data;
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
        public async Task GetPublisherByIdAsync_WhenPublisherExists_ReturnsPublisher()
        {
            // Arrange
            var options = MockedDBContext.GenerateNewInMemoryDBContextOptions();
            using var context = MockedDBContext.CreateFromOptions(options);
            MockedDBContext.PrepareData(context);
        
            // If you have specific publishers you want to add for testing, you can do so here
            // For example, to add a publisher with ID 1:
            // MockedDBContext.AddPublisherWithId(context, 1);

            var service = new PublisherService(context);

            // Act
            var publisher = await service.GetPublisherByIdAsync(1);

            // Assert
            Assert.NotNull(publisher);
            Assert.Equal(1, publisher.Id);
        }
        
    }
}
