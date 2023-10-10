namespace BookHub.Models.Details;

public class RatingDetail
{
     
    public virtual UserCreate User { get; set; }
    public virtual BookCreate Book { get; set; }
    public int Value { get; set; }
    public string? Comment { get; set; }
}