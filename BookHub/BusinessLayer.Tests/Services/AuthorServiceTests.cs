using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using BusinessLayer.Services;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class AuthorServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
        .AddServices()
        .AddMockedDbContext();
    
        
    [Fact]
    public async Task GetAuthorsAsync_ReturnsCorrectNumber()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);

        var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();

        // Act
        var result = await authorService.GetAuthorsAsync(null, null, null);
        var authorDetails = result.ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dbContext.Authors.Count(), authorDetails.Count);
    }
      [Fact]
    public async Task GetAuthorByIdAsync_ExistingId_ReturnsAuthor()
    {
        // Arrange
        var serviceProvider = _serviceProviderBuilder.Create();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDbContext>();
        await MockedDbContext.PrepareDataAsync(dbContext);
        var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();

        var authorToGet = dbContext.Authors.First();

        // Act
        var result = await authorService.GetAuthorByIdAsync(authorToGet.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(authorToGet.Name, result.Name);
    }
        
    
}