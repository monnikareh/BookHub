namespace BusinessLayer.Models;

public class UserDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public virtual ICollection<ModelRelated> Orders { get; set; } = new List<ModelRelated>();
    public virtual ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();
    public virtual ICollection<ModelRelated> Ratings { get; set; } = new List<ModelRelated>();
}