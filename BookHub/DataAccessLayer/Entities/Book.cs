using System.ComponentModel.DataAnnotations.Schema;
using BookHub.Models;

namespace DataAccessLayer.Entities;

public class Book : BaseEntity, IModelRelated
{
    public String Name { get; set; }
    
    public int PublisherId{ get; set; }
    [ForeignKey("PublisherId")] 
    public virtual Publisher Publisher { get; set; } = null!;
        
    public int StockInStorage { get; set; }
    public double Price { get; set; }
    
    public int OverallRating { get; set; }
    
    public virtual ICollection<Rating> Ratings { get; } = new List<Rating>();
    public virtual ICollection<Order> Orders { get; } = new List<Order>();
    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public virtual ICollection<User> Users { get; } = new List<User>();
}