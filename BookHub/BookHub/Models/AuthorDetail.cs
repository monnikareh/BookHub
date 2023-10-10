using DataAccessLayer.Entities;

namespace BookHub.Models.Details;

public class AuthorDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<BookDetail> Books { get; set; }
}