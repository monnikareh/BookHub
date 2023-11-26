using BusinessLayer.Models;
using BusinessLayer.Services;
using NSubstitute;
using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using BusinessLayer.Services;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.MockedObjects;
using Xunit.Abstractions;

namespace BusinessLayer.Tests.Services;

public class OrderServiceTests
{
    
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
        .AddServices()
        .AddMockedDbContext();
    
    [Fact]
    public async Task GetOrdersAsync_ReturnsCorrectNumber()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);

        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

        // Act
        var result = await orderService.GetOrdersAsync(null, null, null, null, null, null, null);
        var orderDetails = result.ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dbContext.Orders.Count(), orderDetails.Count);
    }
    
    
    [Fact]
    public async Task GetOrderByIdAsync_ExistingId_ReturnsOrder()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();        
        
        var orderToGet = dbContext.Orders.Include(order => order.User).First();

        // Act
        var result = await orderService.GetOrderByIdAsync(orderToGet.Id);

        // Assert
        Assert.NotNull(result);

        Assert.Equal(orderToGet.TotalPrice, result.TotalPrice);
        Assert.Equal(orderToGet.Date, result.Date);
        Assert.NotNull(result.User);
        Assert.Equal(orderToGet.User.Name, result.User.Name);
    }
    
    [Fact]
    public async Task CreateOrderAsyncGetById_ReturnsOrder()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
        
        var newOrder = new OrderCreate
        {
            User = new ModelRelated
            {
                Id = 1,
                Name = "John Smith"
            },
            TotalPrice = 15m
        };

        // Act
        var result = await orderService.CreateOrderAsync(newOrder);
        var ret = await orderService.GetOrderByIdAsync(result.Id);
        // Assert
        Assert.NotNull(result);
        Assert.NotNull(ret);
        Assert.Equal(ret.TotalPrice, result.TotalPrice);
        Assert.Equal(ret.Id, result.Id);
        Assert.NotNull(result.User);
        Assert.NotNull(ret.User);
        Assert.Equal(ret.User.Name, result.User.Name);
        Assert.Equal(ret.User.Id, result.User.Id);
    }
    
    [Fact]
    public async Task UpdateOrderAsync_ReturnsUpdatedOrder()
    {
        // Arrange
        var service = Substitute.For<IOrderService>();
        const int orderId = 4; // Assuming order with Id 4 exists
        var orderUpdate = new OrderUpdate
        {
            TotalPrice = 9m
        };

        var updatedOrder = new OrderDetail
        {
            Id = orderId,
            User = new ModelRelated
            {
                Id = 1,
                Name = "John Smith"
            },
            TotalPrice = 9m
        };

        service.UpdateOrderAsync(orderId, Arg.Any<OrderUpdate>()).Returns(updatedOrder);

        // Act
        var result = await service.UpdateOrderAsync(orderId, orderUpdate);

        // Assert
        RunAsserts(updatedOrder, result);
    }

    private static void RunAsserts(OrderDetail expected, OrderDetail actual)
    {
        Assert.NotNull(actual);
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.TotalPrice, actual.TotalPrice);
        Assert.NotNull(actual.User);
        Assert.Equal(expected.User.Id, actual.User.Id);
        Assert.Equal(expected.User.Name, actual.User.Name);
    }

    
}