using DataAccessLayer.Entities;

namespace BookHub.Models;

public class OrderCreate
{
    public ModelRelated<User> User { get; set; }
    public int TotalPrice { get; set; }
    public virtual ICollection<ModelRelated<Book>> Books { get; } = new List<ModelRelated<Book>>();
}