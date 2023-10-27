namespace BookHub.Models;

public class UserCreate
{
    public string Name { get; set; }
    public string UserName { get; set; }

    public string Password { get; set; }
    public string Email { get; set; }
}