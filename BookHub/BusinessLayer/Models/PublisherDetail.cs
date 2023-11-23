namespace BusinessLayer.Models;

public class PublisherDetail 
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public ICollection<ModelRelated> Books { get; set; } = new List<ModelRelated>();
}