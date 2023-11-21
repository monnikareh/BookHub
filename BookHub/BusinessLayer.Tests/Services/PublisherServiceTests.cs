using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookHub.Models;
using BusinessLayer.Exceptions;
using BusinessLayer.Mapper;
using BusinessLayer.Models;
using BusinessLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace BusinessLayer.Tests
{
    public class PublisherServiceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public PublisherServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Fact]
        public async Task GetPublishersAsync_WithNameFilter_ShouldReturnFilteredPublishers()
        {
            // Arrange
            var publisherName = "Bloomsbury";
            var dbContext = CreateDbContextWithPublishers(GetSamplePublishers());
            var publisherService = new PublisherService(dbContext);

            // Act
            var result = await publisherService.GetPublishersAsync(publisherName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetPublisherByIdAsync_ExistingPublisher_ShouldReturnPublisherDetail()
        {
            // Arrange
            var publisherId = 1;
            var dbContext = CreateDbContextWithPublishers(GetSamplePublishers());
            var publisherService = new PublisherService(dbContext);

            // Act
            var result = await publisherService.GetPublisherByIdAsync(publisherId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(publisherId, result.Id);
        }

        [Fact]
        public async Task GetPublisherByIdAsync_NonExistingPublisher_ShouldThrowPublisherNotFoundException()
        {
            // Arrange
            var publisherId = 3;
            var dbContext = CreateDbContextWithPublishers(GetSamplePublishers());
            var publisherService = new PublisherService(dbContext);

            // Act and Assert
            await Assert.ThrowsAsync<PublisherNotFoundException>(() => publisherService.GetPublisherByIdAsync(publisherId));
        }

        [Fact]
        public async Task CreatePublisherAsync_ValidPublisherCreate_ShouldReturnPublisherDetail()
        {
            // Arrange
            var dbContext = CreateDbContextWithPublishers(new List<Publisher>());
            var publisherService = new PublisherService(dbContext);
            var publisherCreate = new PublisherCreate { Name = "New Publisher" };

            // Act
            var result = await publisherService.CreatePublisherAsync(publisherCreate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(publisherCreate.Name, result.Name);
        }

        [Fact]
        public async Task UpdatePublisherAsync_ExistingPublisher_ShouldReturnUpdatedPublisherDetail()
        {
            // Arrange
            var publisherId = 1;
            var dbContext = CreateDbContextWithPublishers(GetSamplePublishers());
            var publisherService = new PublisherService(dbContext);
            var publisherUpdate = new PublisherUpdate { Name = "Updated Publisher" };

            // Act
            var result = await publisherService.UpdatePublisherAsync(publisherId, publisherUpdate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(publisherUpdate.Name, result.Name);
        }

        [Fact]
        public async Task UpdatePublisherAsync_NonExistingPublisher_ShouldThrowPublisherNotFoundException()
        {
            // Arrange
            var publisherId = 3;
            var dbContext = CreateDbContextWithPublishers(GetSamplePublishers());
            var publisherService = new PublisherService(dbContext);
            var publisherUpdate = new PublisherUpdate { Name = "Updated Publisher" };

            // Act and Assert
            await Assert.ThrowsAsync<PublisherNotFoundException>(() => publisherService.UpdatePublisherAsync(publisherId, publisherUpdate));
        }

        [Fact]
        public async Task DeletePublisherAsync_ExistingPublisher_ShouldDeletePublisher()
        {
            // Arrange
            var publisherId = 1;
            var dbContext = CreateDbContextWithPublishers(GetSamplePublishers());
            var publisherService = new PublisherService(dbContext);

            // Act
            await publisherService.DeletePublisherAsync(publisherId);

            // Assert
            Assert.Null(dbContext.Publishers.Find(publisherId));
        }

        [Fact]
        public async Task DeletePublisherAsync_NonExistingPublisher_ShouldThrowPublisherNotFoundException()
        {
            // Arrange
            var publisherId = 3;
            var dbContext = CreateDbContextWithPublishers(GetSamplePublishers());
            var publisherService = new PublisherService(dbContext);

            // Act and Assert
            await Assert.ThrowsAsync<PublisherNotFoundException>(() => publisherService.DeletePublisherAsync(publisherId));
        }

        private BookHubDbContext CreateDbContextWithPublishers(List<Publisher> publishers)
        {
            var options = new DbContextOptionsBuilder<BookHubDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new BookHubDbContext(options);
            dbContext.Publishers.AddRange(publishers);
            dbContext.SaveChanges();

            return dbContext;
        }

        private List<Publisher> GetSamplePublishers()
        {
            return new List<Publisher>
            {
                new Publisher { Id = 1, Name = "Bloomsbury" },
                new Publisher { Id = 2, Name = "Ikar" }
            };
        }
    }
}
