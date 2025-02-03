namespace webCollege.Models;

public class VerificationCode
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Code { get; set; }
    public DateTime ExpirationTime { get; set; }
    public User User { get; set; }
}