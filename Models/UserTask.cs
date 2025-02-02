namespace webCollege.Models;

public class UserTask
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TaskId { get; set; }
    public DateTime CompletedAt { get; set; }
    public TasksStatus Status { get; set; } = TasksStatus.Pending;
    
    public User User { get; set; }
    public TaskEntity Task { get; set; }
}

public enum TasksStatus
{
    Pending,
    Completed
}