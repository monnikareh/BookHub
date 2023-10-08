using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Book : BaseEntity
{
    public String Name { get; set; }
    public int GenreId { get; set; }
    [ForeignKey("GenreId")]
    public Genre Genre { get; set; } = null!;
    
    public int PublisherId{ get; set; }
    [ForeignKey("PublisherId")] 
    public Publisher Publisher { get; set; } = null!;

    public ICollection<Rating> Ratings { get; } = new List<Rating>();
        
    public int StockInStorage { get; set; }
    public int Price { get; set; }
    
    public ICollection<Order> Orders { get; } = new List<Order>();
    public ICollection<Author> Authors { get; } = new List<Author>();
    public ICollection<User> Users { get; } = new List<User>();
}