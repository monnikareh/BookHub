namespace BusinessLayer.Models;

public class OrderCreate
{
    public ModelRelated User { get; set; }
    public decimal TotalPrice { get; set; }
    public ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();
}