namespace BookHub.Models;

public class AuthorCreate
{
    public string Name { get; set; }
    public ICollection<ModelRelated> Books { get; set; }
}