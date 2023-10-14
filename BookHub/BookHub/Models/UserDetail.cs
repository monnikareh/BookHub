using DataAccessLayer.Entities;

namespace BookHub.Models;

public class UserDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
    public virtual ICollection<ModelRelated<Order>> Orders { get; set; } = new List<ModelRelated<Order>>();
    public virtual ICollection<ModelRelated<Book>> Books { get; set; } = new List<ModelRelated<Book>>();
}