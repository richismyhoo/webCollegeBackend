using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webCollege.Services;

namespace webCollege.Controllers;

[Authorize]
[Route("api/users/courses")]
[ApiController]
public class CourseSubcriptionController : ControllerBase
{
    private readonly CourseSubscriptionService _subscriptionService;

    public CourseSubcriptionController(CourseSubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddCourse([FromBody] CourseRequest request)
    {
        try
        {
            var userId = HttpContext.User.GetUserId();
            await _subscriptionService.AddCourseToUser(userId, request.CourseName);
            return Ok(new { Message = "Курс успешно добавлен к пользователю" });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { Message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveCourse([FromBody] CourseRequest request)
    {
        try
        {
            var userId = HttpContext.User.GetUserId();
            await _subscriptionService.RemoveCourseFromUser(userId, request.CourseName);
            return Ok(new { Message = "Курс успешно удален у пользователя" });
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public class CourseRequest
    {
        public string CourseName { get; set; }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        try
        {
            var userId = HttpContext.User.GetUserId();
            var courses = await _subscriptionService.GetCourses(userId);
            return Ok(new { Courses = courses});
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}