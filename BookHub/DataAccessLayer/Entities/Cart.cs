using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Cart
{
    public int OrderId { get; set; }
    [ForeignKey("OrderId")]
    public Order Order { get; set; }
    
    public int BookId { get; set; }
    [ForeignKey("BookId")]
    public Book Book { get; set; }
}