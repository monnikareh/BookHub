namespace BusinessLayer.Models;

public class BookDetail
{
    public int Id { get; set; }
    public required string Name { get; init; }
    public ICollection<ModelRelated> Genres { get; init; } = new List<ModelRelated>();
    public required ModelRelated Publisher { get; init; }
    public int StockInStorage { get; init; }
    public decimal Price { get; init; }
    public ICollection<ModelRelated> Authors { get; init; } = new List<ModelRelated>();
    public int OverallRating { get; init; }
}