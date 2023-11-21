using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    
}