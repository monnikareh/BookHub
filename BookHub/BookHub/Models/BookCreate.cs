using DataAccessLayer.Entities;

namespace BookHub.Models;

public class BookCreate
{
    public string Name { get; set; }
    public ModelRelated<Genre> Genre { get; set; }
    public ModelRelated<Publisher> Publisher { get; set; }
    public int StockInStorage { get; set; }
    public int Price { get; set; }
    public ICollection<ModelRelated<Author>> Authors { get; set; } = new List<ModelRelated<Author>>();
}