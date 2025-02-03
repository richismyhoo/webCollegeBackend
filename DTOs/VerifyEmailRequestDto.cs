namespace webCollege.DTOs;

public class VerifyEmailRequestDto
{
    public int UserId { get; set; }
    public string Code { get; set; }
}