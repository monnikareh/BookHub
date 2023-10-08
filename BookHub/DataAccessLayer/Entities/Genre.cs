namespace DataAccessLayer.Entities;

public class Genre : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Book> Books { get; } = new List<Book>();
}