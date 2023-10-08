namespace DataAccessLayer.Entities;

public class Author : BaseEntity
{
    public string Name { get; set; }
    
    public ICollection<Book> Books { get; } = new List<Book>();
}