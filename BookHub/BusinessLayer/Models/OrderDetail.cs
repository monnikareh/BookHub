namespace BusinessLayer.Models;

public class OrderDetail
{
    public int Id { get; set; }
    public ModelRelated User { get; set; }
    public double TotalPrice { get; set; }
    public DateTime Date { get; set; } 
    public virtual ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();
}