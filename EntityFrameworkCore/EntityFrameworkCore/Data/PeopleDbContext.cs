using EntityFrameworkCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Data
{
    public class PeopleDbContext: DbContext
    {
        public PeopleDbContext(DbContextOptions<PeopleDbContext> options)
        : base(options) { }
        
        public DbSet<Person> People { get; set; }
    }
}
