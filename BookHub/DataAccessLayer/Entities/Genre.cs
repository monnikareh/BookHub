namespace DataAccessLayer.Entities;

public class Genre : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<Book> Books { get; } = new List<Book>();
}