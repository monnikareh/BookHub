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
    public class PublisherServiceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddServices()
            .AddMockedDbContext();

        public PublisherServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        
        // Tests using first option
        [Fact]
        public async Task GetPublishersAsync_ReturnsCorrectNumber()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
            await MockedDBContext.PrepareDataAsync(dbContext);

            var customerService = scope.ServiceProvider.GetRequiredService<IPublisherService>();

            // Act
            var result = await customerService.GetPublishersAsync(null);
            var publisherDetails = result.ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(TestData.GetMockedPublishers().Count(), publisherDetails.Count);
        }

        [Fact]
        public async Task GetPublisherByIdAsync_ExistingId_ReturnsPublisher()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
            await MockedDBContext.PrepareDataAsync(dbContext);
            var publisherService = scope.ServiceProvider.GetRequiredService<IPublisherService>();

            var publisherToGet = TestData.GetMockedPublishers().First();

            // Act
            var result = await publisherService.GetPublisherByIdAsync(publisherToGet.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(publisherToGet.Name, result.Name);
        }
        
        // Tests using second option => NSubstitute
        [Fact]
        public async Task GetPublisherByIdAsync_WhenPublisherExists_ReturnsPublisher()
        {

            var service = Substitute.For<IPublisherService>();
            var publisher = new PublisherDetail
            {
                Id = 4,
                Name = "Penguin Books"

            };
            service.GetPublisherByIdAsync(Arg.Any<int>()).Returns(publisher);
            // Act
            var publisher2 = await service.GetPublisherByIdAsync(publisher.Id);

            // Assert
            Assert.NotNull(publisher);
            Assert.Equal(publisher.Id, publisher2.Id);
            Assert.Equal(publisher.Name, publisher2.Name);

        }
        
        [Fact]
        public async Task CreatePublisherAsync_ReturnsNewPublisher()
        {
            // Arrange
            var service = Substitute.For<IPublisherService>();
            var publisherCreate = new PublisherCreate
            {
                Name = "Ikar"
            };

            var createdPublisher = new PublisherDetail
            {
                Id = 6,
                Name = publisherCreate.Name
            };

            service.CreatePublisherAsync(Arg.Any<PublisherCreate>()).Returns(createdPublisher);

            // Act
            var result = await service.CreatePublisherAsync(publisherCreate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdPublisher.Id, result.Id);
            Assert.Equal(createdPublisher.Name, result.Name);
        }

        [Fact]
        public async Task UpdatePublisherAsync_ReturnsUpdatedPublisher()
        {
            // Arrange
            var service = Substitute.For<IPublisherService>();
            var publisherId = 4; // Assuming publisher with Id 4 exists
            var publisherUpdate = new PublisherUpdate
            {
                Name = "Updated Publisher"
            };

            var updatedPublisher = new PublisherDetail
            {
                Id = publisherId,
                Name = publisherUpdate.Name
            };

            service.UpdatePublisherAsync(publisherId, Arg.Any<PublisherUpdate>()).Returns(updatedPublisher);

            // Act
            var result = await service.UpdatePublisherAsync(publisherId, publisherUpdate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedPublisher.Id, result.Id);
            Assert.Equal(updatedPublisher.Name, result.Name);
        }

        [Fact]
        public async Task DeletePublisherAsync_WhenIdExists_DeletesPublisher()
        {
            // Arrange
            var service = Substitute.For<IPublisherService>();
            const int publisherIdToDelete = 3;

            // Act
            await service.DeletePublisherAsync(publisherIdToDelete);

            // Assert
            await service.Received(1).DeletePublisherAsync(publisherIdToDelete);
        }
        
        [Fact]
        public async Task DeletePublisherAsync_WhenNonExistingPublisherId_ReturnsFalse()
        {
            // Arrange
            var service = Substitute.For<IPublisherService>();
            const int nonExistentPublisherId = 999;

            service.DeletePublisherAsync(nonExistentPublisherId)
                .Returns(Task.FromException<PublisherNotFoundException>(
                    new PublisherNotFoundException($"Publisher with ID:'{nonExistentPublisherId}' not found")));

            // Act
            bool result;
            try
            {
                await service.DeletePublisherAsync(nonExistentPublisherId);
                result = true;
            }
            catch (PublisherNotFoundException)
            {
                result = false;
            }

            // Assert
            Assert.False(result);
        }
    }
}
