using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Wishlist : BaseEntity
{
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }
    
    public int BookId { get; set; }
    [ForeignKey("BookId")]
    public Book Book { get; set; }
}