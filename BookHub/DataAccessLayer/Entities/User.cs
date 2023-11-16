using BookHub.Models;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Entities;

public class User : IdentityUser<int>, IModelRelated
{
    public string Name { get; set; }
    
    public ICollection<Order> Orders { get; } = new List<Order>();
    public ICollection<Book> Books { get; set; } = new List<Book>();
    public ICollection<Rating> Ratings { get; } = new List<Rating>();
    
}