using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webCollege.Services;

namespace webCollege.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("self")]
    public async Task<IActionResult> GetSelfUser()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var user = await _userService.GetUserAsync(userId);

        if (user == null)
        {
            return NotFound(new { Message = "Пользователь не найден" });
        }

        return Ok(new
        {
            id = user.Id,
            email = user.Email,
            name = user.Name,
            points = user.Points,
            courses = user.Courses.ToList()
        });
    }
}