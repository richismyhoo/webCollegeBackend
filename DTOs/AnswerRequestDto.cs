namespace webCollege.DTOs;


public class AnswerRequestDto
{
    public string UserAnswer { get; set; } = string.Empty;
}

public class AnswerResponseDto
{
    public string Message { get; set; } = string.Empty;
    public int? NewPoints { get; set; }
}

public class AnswerResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int? NewPoints { get; set; }
}