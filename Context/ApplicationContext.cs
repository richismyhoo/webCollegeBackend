    using Microsoft.EntityFrameworkCore;
    using webCollege.Models;

    namespace webCollege.Context;

    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
    }