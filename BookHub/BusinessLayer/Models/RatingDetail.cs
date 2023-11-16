namespace BusinessLayer.Models;

public class RatingDetail
{
    public int Id { get; set; }
    public ModelRelated User { get; set; }
    public ModelRelated Book { get; set; }
    public int Value { get; set; }
    public string? Comment { get; set; }
}