namespace BusinessLayer.Models;

public class UserUpdate
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    
    public ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();

}