using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webCollege.Services;

namespace webCollege.Controllers;

[ApiController]
[Route("api/tasks")]

public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _taskService.GetTasksAsync();
        return Ok(tasks);
    }

    [Authorize]
    [HttpPost("{taskId}/answer")]
    public async Task<IActionResult> SubmitAnswer([FromRoute] int taskId, [FromBody] AnswerRequest request)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var result = await _taskService.SubmitAnswerAsync(userId, taskId, request.UserAnswer);

        if (!result.Success)
            return BadRequest(new { message = result.Message });

        return Ok(new { message = result.Message, newPoints = result.NewPoints });
    }
}

public class AnswerRequest
{
    public string UserAnswer { get; set; }
}