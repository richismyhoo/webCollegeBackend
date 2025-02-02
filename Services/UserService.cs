using Microsoft.EntityFrameworkCore;
using webCollege.Context;
using webCollege.Models;

namespace webCollege.Services;

public class UserService
{
    private readonly ApplicationContext _context;

    public UserService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserAsync(int userId)
    {
        return await _context.Users
            .Where(u => u.Id == userId)
            .Select(u => new User
            {
                Id = u.Id,
                Email = u.Email,
                Name = u.Name,
                Points = u.Points
            })
            .FirstOrDefaultAsync();
    }
}