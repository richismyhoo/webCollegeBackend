using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webCollege.Services;

namespace webCollege.Controllers;

[Route("api/users/top")]
[ApiController]
public class TopUsersController : ControllerBase
{
    private readonly TopUsersService _topUsersService;

    public TopUsersController(TopUsersService topUsersService)
    {
        _topUsersService = topUsersService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTopUsers()
    {
        try
        {
            var topStudents = await _topUsersService.GetTopStudentsAsync();
            var result = topStudents.Select(u => new TopStudentsDto
            {
                Id = u.Id,
                Name = u.Name,
                Points = u.Points
            }).ToList();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public class TopStudentsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
    }
}

