namespace DataAccessLayer.Entities;

public class User : BaseEntity
{
    // max length
    public string Name { get; set; }

    public string Password { get; set; }

    public bool IsAdmin { get; set; }
    
    public ICollection<Order> Orders { get; } = new List<Order>();
    public ICollection<Book> Books { get; } = new List<Book>();
    
}