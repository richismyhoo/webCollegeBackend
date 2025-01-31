using Microsoft.EntityFrameworkCore;
using webCollege.Context;
using webCollege.Models;

namespace webCollege.Services;

public class TopUsersService
{
    private readonly ApplicationContext _context;

    public TopUsersService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetTopStudentsAsync(int topCount = 10)
    {
        return await _context.Users
            .Where(u => u.Role == "User")
            .OrderByDescending(u => u.Points)
            .Take(topCount)
            .ToListAsync();
    }
}