using Microsoft.EntityFrameworkCore;
using webCollege.Context;
using webCollege.DTOs;
using webCollege.Models;

namespace webCollege.Services;

public class TagService
{
    private readonly ApplicationContext _context;

    public TagService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<TagDto>> GetTagsAsync()
    {
        var tags = await _context.Tags.ToListAsync();

        return tags.Select(tag => new TagDto
        {
            Id = tag.Id,
            Name = tag.Name
        }).ToList();
    }
}