using DataAccessLayer.Entities;

namespace BookHub.Models;

public class PublisherDetail 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<ModelRelated<Book>> Books { get; set; } = new List<ModelRelated<Book>>();
}