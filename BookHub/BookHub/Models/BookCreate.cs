using DataAccessLayer.Entities;

namespace BookHub.Models;

public class BookCreate
{
    public string Name { get; set; }
    public string GenreName { get; set; }
    public string PublisherName { get; set; }
    public int StockInStorage { get; set; }
    public int Price { get; set; }
    public ICollection<AuthorCreate> Authors { get; set; } = new List<AuthorCreate>();
}