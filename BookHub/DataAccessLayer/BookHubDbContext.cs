using System.Security.Cryptography.X509Certificates;
using DataAccessLayer.Entities;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class BookHubDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<User> Users { get; set; }

    public BookHubDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var pgpass = ConfigurationManager.AppSettings.Get("pgpass");
        Console.WriteLine($"PG password: {pgpass}");
        optionsBuilder
            .UseNpgsql($"Host=10.16.63.135;Database=pv179;Username=pv179;Password={pgpass}");
    }

    // https://docs.microsoft.com/en-us/ef/core/modeling/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }
        base.OnModelCreating(modelBuilder);
        
        // modelBuilder.Seed();
        base.OnModelCreating(modelBuilder);
    }
}