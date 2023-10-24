using System.ComponentModel.DataAnnotations.Schema;
using BookHub.Models;

namespace DataAccessLayer.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
    public double TotalPrice { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public virtual ICollection<Book> Books { get; } = new List<Book>();
    
}