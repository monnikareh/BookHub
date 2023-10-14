namespace BookHub.Models;

public class BookCreate
{
    public string Name { get; set; }
    public ModelRelated Genre { get; set; }
    public ModelRelated Publisher { get; set; }
    public int StockInStorage { get; set; }
    public int Price { get; set; }
    public ICollection<ModelRelated> Authors { get; set; } = new List<ModelRelated>();
}