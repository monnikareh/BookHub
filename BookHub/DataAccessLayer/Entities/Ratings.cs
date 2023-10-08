using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Ratings
{
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [ForeignKey("BookId")]
    public int BookId { get; set; }
    public Book Book { get; set; }
    
    public int Rating { get; set; }
    
    public string? Comment { get; set; }
}