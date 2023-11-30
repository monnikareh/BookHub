using System.ComponentModel.DataAnnotations.Schema;
using BookHub.Models;

namespace DataAccessLayer.Entities;

public class Book : BaseEntity, IModelRelated
{
    public string Name { get; set; }
    
    public int PublisherId{ get; set; }
    [ForeignKey("PublisherId")] 
    public Publisher Publisher { get; set; } = null!;
        
    public int StockInStorage { get; set; }
    public double Price { get; set; }
    
    public int OverallRating { get; set; }
    
    public ICollection<Rating> Ratings { get; } = new List<Rating>();
    public ICollection<Order> Orders { get; } = new List<Order>();
    public ICollection<Author> Authors { get; set; } = new List<Author>();
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public ICollection<User> Users { get; } = new List<User>();
}