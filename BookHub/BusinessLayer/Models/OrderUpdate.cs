namespace BusinessLayer.Models;

public class OrderUpdate
{
    public decimal TotalPrice { get; set; }
    public virtual ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();
}