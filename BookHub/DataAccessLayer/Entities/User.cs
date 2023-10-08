namespace DataAccessLayer.Entities;

public class User
{
    // max length
    public string Name { get; set; }

    public string Password { get; set; }

    public bool IsAdmin { get; set; }

    // public IEnumerable<Book> Wishlist { get; set; }
    //
    // public Dictionary<Book, int> Ratings { get; set; }

}