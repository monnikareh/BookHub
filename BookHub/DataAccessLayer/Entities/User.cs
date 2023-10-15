using System.ComponentModel.DataAnnotations;
using BookHub.Models;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Entities;

public class User : IdentityUser<int>, IModelRelated
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsAdmin { get; set; }
    
    public virtual ICollection<Order> Orders { get; } = new List<Order>();
    public virtual ICollection<Book> Books { get; } = new List<Book>();
    
}