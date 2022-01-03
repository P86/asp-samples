using EntityFrameworkCore.Data;
using EntityFrameworkCore.Data.Entities;
using EntityFrameworkCore.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly PeopleDbContext dbContext;

        public DepartmentsController(PeopleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        [HttpGet]
        public IEnumerable<DepartmentDto> Get(bool details = false)
        {
            var departments = details
                ? dbContext.Departments?.Include(d => d.People)?.AsNoTracking().ToList()
                : dbContext.Departments?.AsNoTracking().ToList();
            
            return departments?.Select(d => d.AsDto()) ?? Enumerable.Empty<DepartmentDto>();
        }

        [HttpPost]
        public IEnumerable<DepartmentDto> Generate()
        {
            var department = new Department { 
                Name = "Other",
                People = new List<Person> { new Person { FirstName = "Darlene", LastName = "Alderson" }, new Person { FirstName = "Mr.", LastName = "Robot" } }
            };

            dbContext.Add(department);
            dbContext.SaveChanges();

            return new [] { department.AsDto() };
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var department = dbContext.Departments?.FirstOrDefault(d => d.Id == id);
            if (department == null)
                return NotFound();

            dbContext?.Remove(department);
            dbContext?.SaveChanges();

            return Ok();
        }
    }

}
