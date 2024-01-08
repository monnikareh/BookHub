using DataAccessLayer.Entities;

namespace BusinessLayer.Models;

public class OrderUpdate
{
    public decimal TotalPrice { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();
}