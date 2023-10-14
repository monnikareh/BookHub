namespace BookHub.Models;

public class OrderDetail
{
    public int Id { get; set; }
    public ModelRelated User { get; set; }
    public int TotalPrice { get; set; }
    public DateTime Date { get; set; } 
    public virtual ICollection<ModelRelated> Books { get; } = new List<ModelRelated>();
}