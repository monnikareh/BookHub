using BookHub.Models;

namespace DataAccessLayer.Entities;

public class Genre : BaseEntity, IModelRelated
{
    public string Name { get; set; }
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}