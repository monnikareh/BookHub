using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    public int TotalPrice { get; set; }

    public DateTime Date { get; set; }
    
    public ICollection<Book> Books { get; } = new List<Book>();
    
}