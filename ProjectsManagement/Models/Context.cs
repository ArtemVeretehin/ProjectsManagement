using Microsoft.EntityFrameworkCore;
namespace ProjectsManagement.Models
{
    public class Context: DbContext
    {
        public Context() => Database.EnsureCreated();

        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=ProjectManagement.db");
        }
    }
}
