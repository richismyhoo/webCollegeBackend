using Microsoft.EntityFrameworkCore;
using webCollege.Context;
using webCollege.Models;

namespace webCollege.Services;

public class CourseService : ICourseService
{
    private readonly ApplicationContext _dbContext;

    public CourseService(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        return await _dbContext.Courses.ToListAsync();
    }

    public async Task<Course> CreateCourseAsync(Course course)
    {
        _dbContext.Courses.Add(course);
        await _dbContext.SaveChangesAsync();
        return course;
    }

    public async Task<bool> DeleteCourseAsync(int id)
    {
        var course = await _dbContext.Courses.FindAsync(id);
        if (course != null)
        {
            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }
}

public interface ICourseService
{
    Task<IEnumerable<Course>> GetAllCoursesAsync();
    Task<Course> CreateCourseAsync(Course course);
    Task<bool> DeleteCourseAsync(int id);
}