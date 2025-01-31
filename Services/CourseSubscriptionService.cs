using webCollege.Context;
using webCollege.Models;

namespace webCollege.Services;

public class CourseSubscriptionService
{
    private readonly ApplicationContext _context;

    public CourseSubscriptionService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddCourseToUser(int userId, string courseName)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) 
            throw new KeyNotFoundException("Пользователь не найден");
        if (user.Courses.Contains(courseName))
        {
            throw new InvalidOperationException("Курс уже добавлен к пользователю");
        }
        user.Courses.Add(courseName);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveCourseFromUser(int userId, string courseName)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new KeyNotFoundException("Пользователь не найден");

        if (!user.Courses.Contains(courseName))
        {
            throw new InvalidOperationException("Курс не найден у пользователя");
        }

        user.Courses.Remove(courseName);
        await _context.SaveChangesAsync();
    }

    public async Task<List<string>> GetCourses(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user?.Courses ?? new List<string>();
    }
}