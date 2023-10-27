namespace BookHub.Models;

public class UserDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsAdmin { get; set; }
    public virtual ICollection<ModelRelated> Orders { get; set; } = new List<ModelRelated>();
    public virtual ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();
}