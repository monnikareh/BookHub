using DataAccessLayer;
using DataAccessLayer.Entities;
using EntityFrameworkCore.Testing.NSubstitute.Helpers;
using Microsoft.EntityFrameworkCore;

namespace TestUtilities.MockedObjects
{
    public static class MockedDBContext
    {
        public static string RandomDBName => Guid.NewGuid().ToString();

        public static DbContextOptions<BookHubDbContext> GenerateNewInMemoryDBContextOptons()
        {
            var dbContextOptions = new DbContextOptionsBuilder<BookHubDbContext>()
                .UseInMemoryDatabase(RandomDBName)
                .Options;

            return dbContextOptions;
        }

        public static BookHubDbContext CreateFromOptions(DbContextOptions<BookHubDbContext> options)
        {
            var dbContextToMock = new BookHubDbContext(options);

            var dbContext = new MockedDbContextBuilder<BookHubDbContext>()
                .UseDbContext(dbContextToMock)
                .UseConstructorWithParameters(options)
                .MockedDbContext;

            PrepareData(dbContext);

            return dbContext;
        }

        public static void PrepareData(BookHubDbContext dbContext)
        {
            dbContext.SaveChanges();
        }

        public static async Task PrepareDataAsync(BookHubDbContext dbContext)
        {
            await dbContext.SaveChangesAsync();
        }
    }
}