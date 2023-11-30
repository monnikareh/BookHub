namespace BusinessLayer.Models;

public class AuthorUpdate
{
    public string Name { get; set; }
    public ICollection<ModelRelated> Books { get; set; }
}