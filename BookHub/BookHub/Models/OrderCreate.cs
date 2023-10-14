namespace BookHub.Models;

public class OrderCreate
{
    public ModelRelated User { get; set; }
    public int TotalPrice { get; set; }
    public virtual ICollection<ModelRelated> Books { get; } = new List<ModelRelated>();
}