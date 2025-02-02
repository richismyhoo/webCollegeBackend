namespace webCollege.Models;

public class TaskEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public string CorrectAnswer { get; set; }

    public ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();
}