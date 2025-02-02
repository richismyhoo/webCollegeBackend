using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webCollege.Services;

namespace webCollege.Controllers;

[ApiController]
[Route("api/tags")]
public class TagsController : ControllerBase
{
    private readonly TagService _tagService;

    public TagsController(TagService tagService)
    {
        _tagService = tagService;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var tags = await _tagService.GetTagsAsync();
        return Ok(tags);
    }
}