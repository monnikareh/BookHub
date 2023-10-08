using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Inventory : BaseEntity
{
        
    public int BookId { get; set; }
    [ForeignKey("BookId")]
    public Book Book { get; set; }
    
    public int CapacityInStorage { get; set; }
    
    public int Price { get; set; }

}