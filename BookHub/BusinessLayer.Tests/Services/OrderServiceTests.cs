using BusinessLayer.Services;
using DataAccessLayer.Entities;
using NSubstitute;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace BusinessLayer.Tests.Services;

public class OrderServiceTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ServiceProvider _serviceProvider;
    private readonly IOrderService _orderService;
    private readonly IOrderService _orderServiceMock;
    
    public OrderServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        var services = new ServiceCollection();

        // Register actual services
        services.RegisterServices();

        // Mock and register facades
        _orderServiceMock = Substitute.For<IOrderService>();
        services.AddSingleton(_orderServiceMock);

        // ... You can add other mocks or real implementations similarly
        _serviceProvider = services.BuildServiceProvider(); // see Seminar06 for a bit more robust usage ;)

        // you can do this in tests as well
        _orderService = _serviceProvider.GetRequiredService<IOrderService>();
    }
    
    [Fact]
    public async Task GetOrder_ReturnsCorrectOrder()
    {
        var testOrderId = 1;
        
        // (testOrderId).Returns(testOrder);  // Mock the facade behavior

        var order = await _orderServiceMock.GetOrderByIdAsync(testOrderId);
        if (order == null)
        {
            _testOutputHelper.WriteLine("picka");
            return;
        }
        Assert.Equal("John Smith", order.User.Name);
        // ^ the above (expected) value should be some type of constant ... you can create it the same way as testOrderId is created and check it down here that it matches.

        // Verify the mocked method was called, if applicable in this test scenario
        await _orderServiceMock.Received().GetOrderByIdAsync(testOrderId);
    }



    
}