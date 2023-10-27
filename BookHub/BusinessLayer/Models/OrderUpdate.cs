namespace BookHub.Models;

public class OrderUpdate
{
    public double TotalPrice { get; set; }
    public virtual ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();
}