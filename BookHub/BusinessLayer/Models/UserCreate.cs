namespace BookHub.Models;

public class UserCreate
{
    public string Name { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
}