using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Book : BaseEntity
{
    
    public int GenreId { get; set; }
    [ForeignKey("GenreId")]
    public Genre Genre { get; set; }
    
    public int AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public Author Author { get; set; }
    
    public int PublisherId{ get; set; }
    [ForeignKey("PublisherId")] 
    public Publisher Publisher { get; set; }
    
    public int Rating { get; set; }
        
    public int StockInStorage { get; set; }
    
    public int Price { get; set; }

    
}