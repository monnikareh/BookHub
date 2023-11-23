namespace BusinessLayer.Models;

public class PublisherDetail 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();
}