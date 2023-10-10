namespace BookHub.Models;

public class GenreDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<BookDetail> Books { get; } = new List<BookDetail>();
}