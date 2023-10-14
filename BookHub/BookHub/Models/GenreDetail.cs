using DataAccessLayer.Entities;

namespace BookHub.Models;

public class GenreDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<ModelRelated<Book>> Books { get; } = new List<ModelRelated<Book>>();
}