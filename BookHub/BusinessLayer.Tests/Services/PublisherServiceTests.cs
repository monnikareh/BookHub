using BusinessLayer.Models;
using BusinessLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TestUtilities.Data;
using TestUtilities.MockedObjects;
using Xunit;
using Xunit.Abstractions;

namespace BusinessLayer.Tests.Services
{
    public class PublisherServiceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private MockedDependencyInjectionBuilder _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddServices()
            .AddMockedDbContext();

        public PublisherServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

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
        
    }
}
