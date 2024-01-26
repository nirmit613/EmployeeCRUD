using Microsoft.EntityFrameworkCore;

namespace EmployeeCrud.Models.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set;}
        public DbSet<Hobby>Hobbies { get; set;}
        public DbSet<Designation> Designations { get; set;}
        public DbSet<EmployeeHobby> EmployeesHobbies { get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeHobbies)
                .WithOne(h => h.Employee)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
