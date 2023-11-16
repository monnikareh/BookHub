namespace BusinessLayer.Models;

public class BookCreate
{
    public string Name { get; set; }
    public ICollection<ModelRelated> Genres { get; set; } = new List<ModelRelated>();
    public ModelRelated Publisher { get; set; }
    public int StockInStorage { get; set; }
    public int OverallRating { get; set; }
    public double Price { get; set; }
    public ICollection<ModelRelated> Authors { get; set; } = new List<ModelRelated>();
}