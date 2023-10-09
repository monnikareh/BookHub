using DataAccessLayer.Entities;

namespace BookHub.Models.Details;

public class AuthorDetail
{
    public string Name { get; set; }
    public ICollection<String> BookNames { get; set; }
}