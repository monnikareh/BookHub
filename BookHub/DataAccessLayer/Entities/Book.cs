using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Book : BaseEntity
{
    
    [ForeignKey("GenreId")]
    public int GenreId { get; set; }
    public Genre Genre { get; set; }
    
    [ForeignKey("AuthorId")]
    public int AuthorId { get; set; }
    public Author Author { get; set; }
    
    [ForeignKey("PublisherId")]
    public int PublisherId{ get; set; }
    public Publisher Publisher { get; set; }
    
    public int Raring { get; set; }

    
}