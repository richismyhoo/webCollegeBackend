namespace webCollege.Models;

public class TaskTag
{
    public int TaskId { get; set; }
    public TaskEntity Task { get; set; }
    
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}