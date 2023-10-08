using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class BookHubDbContext : DbContext
{
    public BookHubDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        // var dbPath = Path.Join(Environment.GetFolderPath(folder), "seminar02.db");

        optionsBuilder
            .UseNpgsql(
                "Host=10.16.63.135;Database=pv179;Username=pv179;Password=wi6wIN6KhvuGGkq0DWM6YDiU0qi9PAWpTpHGzCqBRQjKzK2rO6PKv3KhOZQN6ian");
    }

    // https://docs.microsoft.com/en-us/ef/core/modeling/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Setup the delete method for all of the entities using reflexion
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.SetNull;
        }
        base.OnModelCreating(modelBuilder);


        /* one-to-many relationship */

        // modelBuilder.Entity<Post>()
        //     .HasOne(post => post.Creator)
        //     .WithMany(user => user.Posts)
        //     .HasForeignKey(post => post.UserId)
        //     .OnDelete(DeleteBehavior.Cascade);
        //
        // modelBuilder.Seed();
        // base.OnModelCreating(modelBuilder);
    }
}