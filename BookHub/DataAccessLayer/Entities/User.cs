using BookHub.Models;

namespace DataAccessLayer.Entities;

public class User : BaseEntity, IModelRelated
{
    // max length
    public string Name { get; set; }

    public string Password { get; set; }

    public bool IsAdmin { get; set; }
    
    public virtual ICollection<Order> Orders { get; } = new List<Order>();
    public virtual ICollection<Book> Books { get; } = new List<Book>();
    
}