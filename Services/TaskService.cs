using Microsoft.EntityFrameworkCore;
using webCollege.Context;
using webCollege.DTOs;
using webCollege.Interfaces;
using webCollege.Models;

namespace webCollege.Services;

public class TaskService : ITaskService
{
    private readonly ApplicationContext _context;

    public TaskService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<TaskDto>> GetTasksAsync()
    {
        var tasks = await _context.Tasks
            .Include(t => t.TaskTags)
            .ThenInclude(tt => tt.Tag)
            .ToListAsync();

        return tasks.Select(t => new TaskDto
        { 
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Points = t.Points,
            Tag = t.TaskTags.Select(tt => tt.Tag.Name).FirstOrDefault() 
        }).ToList();
    }

    public async Task<AnswerResultDto> SubmitAnswerAsync(int userId, int taskId, string userAnswer)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        if (task == null)
            return new AnswerResultDto { Success = false, Message = "Задание не найдено" };

        if (string.IsNullOrWhiteSpace(task.CorrectAnswer))
            return new AnswerResultDto { Success = false, Message = "Ответ для этого задания не задан" };

        if (!task.CorrectAnswer.Equals(userAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
            return new AnswerResultDto { Success = false, Message = "Ответ неправильный" };

        var alreadyCompleted = await _context.UserTasks
            .AnyAsync(ut => ut.UserId == userId && ut.TaskId == taskId);

        if (alreadyCompleted)
            return new AnswerResultDto { Success = false, Message = "Вы уже выполнили это задание" };

        var userTask = new UserTask
        {
            UserId = userId,
            TaskId = taskId,
            CompletedAt = DateTime.UtcNow,
            Status = TasksStatus.Completed
        };
        _context.UserTasks.Add(userTask);

        var user = await _context.Users.FindAsync(userId);
        user.Points += task.Points;

        await _context.SaveChangesAsync();

        return new AnswerResultDto { Success = true, Message = "Ответ правильный! Очки начислены.", NewPoints = user.Points };
    }
}