using DataAccessLayer.Entities;

namespace BookHub.Models;

public class AuthorDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<ModelRelated<Book>> Books { get; set; }
}