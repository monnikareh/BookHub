namespace BusinessLayer.Models;

public class BookDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<ModelRelated> Genres { get; set; } = new List<ModelRelated>();
    public ModelRelated Publisher { get; set; }
    public int StockInStorage { get; set; }
    public decimal Price { get; set; }
    public ICollection<ModelRelated> Authors { get; set; } = new List<ModelRelated>();
    public int OverallRating { get; set; }
}