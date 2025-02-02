using webCollege.DTOs;

namespace webCollege.Interfaces;

public interface ITaskService
{
    Task<List<TaskDto>> GetTasksAsync();
    Task<AnswerResultDto> SubmitAnswerAsync(int userId, int taskId, string userAnswer);
}
