namespace BookHub.Models;

public class GenreUpdate
{
    public string Name { get; set; }
    public virtual ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();
}