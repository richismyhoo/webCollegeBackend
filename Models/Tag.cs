namespace webCollege.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();
}