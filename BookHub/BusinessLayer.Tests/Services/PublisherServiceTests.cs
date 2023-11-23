using BusinessLayer.Mapper;
using BusinessLayer.Services;
using NSubstitute;
using TestUtilities.Data;

namespace BusinessLayer.Tests.Services
{
    public class PublisherServiceTests
    {
        [Fact]
        public async Task GetPublisherByIdAsync_WhenPublisherExists_ReturnsPublisher()
        {
            var service = Substitute.For<IPublisherService>();
            var testPublisher = EntityMapper.MapPublisherToPublisherDetail(TestData.GetMockedPublishers().First());
            service.GetPublisherByIdAsync(Arg.Any<int>()).Returns(testPublisher);
            // Act
            var publisher = await service.GetPublisherByIdAsync(testPublisher.Id);

            // Assert
            Assert.NotNull(publisher);
            Assert.Equal(testPublisher.Id, publisher.Id);
            Assert.Equal(testPublisher.Name, publisher.Name);
        }
        
    }
}
