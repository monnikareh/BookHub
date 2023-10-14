using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Book : BaseEntity
{
    public String Name { get; set; }
    public int GenreId { get; set; }
    [ForeignKey("GenreId")]
    public virtual Genre Genre { get; set; } = null!;
    
    public int PublisherId{ get; set; }
    [ForeignKey("PublisherId")] 
    public virtual Publisher Publisher { get; set; } = null!;

    public virtual ICollection<Rating> Ratings { get; } = new List<Rating>();
        
    public int StockInStorage { get; set; }
    public int Price { get; set; }
    
    public int OverallRating { get; set; }
    
    public virtual ICollection<Order> Orders { get; } = new List<Order>();
    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
    public virtual ICollection<User> Users { get; } = new List<User>();
}