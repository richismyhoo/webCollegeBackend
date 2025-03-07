    using Microsoft.EntityFrameworkCore;
    using webCollege.Models;

    namespace webCollege.Context;

    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TaskTag> TaskTags { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskTag>().HasKey(tt => new { tt.TaskId, tt.TagId });
            modelBuilder.Entity<UserTask>().HasKey(ut => new { ut.UserId, ut.TaskId });
        }
    }