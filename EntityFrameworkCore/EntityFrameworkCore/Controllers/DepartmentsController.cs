using EntityFrameworkCore.Data;
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
                ? dbContext.Departments.Include(d => d.People).ToList()
                : dbContext.Departments.ToList();
            
            return departments.Select(d => DepartmentDto.From(d));
        }


    }
}
