using BusinessLayer.Models;

namespace BookHub.Models;

public class PublisherUpdate
{
    public string Name { get; set; }
    public ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();
}