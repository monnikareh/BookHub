using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Inventory : BaseEntity
{
        
    [ForeignKey("BookId")]
    public int BookId { get; set; }
    public Book Book { get; set; }
    
    public int CapacityInStorage { get; set; }
    
    public int Price { get; set; }

}