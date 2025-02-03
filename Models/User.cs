namespace webCollege.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } = "User";
    public bool IsEmailConfirmed { get; set; } = false;
    public List<string> Courses { get; set; } = new List<string>() { "Базовый вступительный курс"};
    public int Points { get; set; } = 0;
}