using DataAccessLayer.Entities;

namespace BookHub.Models;

public class BookModel
{
    public String Name { get; set; }
    public String GenreName { get; set; }
    public String PublisherName { get; set; }
    public int StockInStorage { get; set; }
    public int Price { get; set; }
    public ICollection<AuthorModel> Authors { get; set; } = new List<AuthorModel>();
}