using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Wishlist : BaseEntity
{
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [ForeignKey("BookId")]
    public int BookId { get; set; }
    public Book Book { get; set; }
}