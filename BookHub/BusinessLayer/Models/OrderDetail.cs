namespace BusinessLayer.Models;

public class OrderDetail
{
    public required int Id { get; set; }
    public required ModelRelated User { get; init; }
    public decimal TotalPrice { get; set; }
    public DateTime Date { get; set; } 
    public ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();
}