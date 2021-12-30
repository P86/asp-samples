using EntityFrameworkCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Data
{
    public class PeopleDbContext: DbContext
    {
        public PeopleDbContext(DbContextOptions<PeopleDbContext> options)
            : base(options) { }
        
        public DbSet<Person>? People { get; set; }

        public DbSet<Department>? Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var ItDepartment = new Department() { Id = 1, Name = "IT" };
            modelBuilder.Entity<Department>().HasData(ItDepartment);
            modelBuilder.Entity<Person>().HasData(new Person() { Id = 1, FirstName = "Arek", LastName = "P", DepartmentId = ItDepartment.Id  });
            modelBuilder.Entity<Person>().HasData(new Person() { Id = 2, FirstName = "Elliot", LastName = "Alderson", DepartmentId = ItDepartment.Id });
        }
    }
}
