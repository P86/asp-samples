using EntityFrameworkCore.Data.Entities;

namespace EntityFrameworkCore.DTO
{
    public record DepartmentDto(string Name, IEnumerable<PersonDto> People) { }

    public static class DepartmentExtensions
    {
        public static DepartmentDto AsDto(this Department department) => new(department.Name, department.People?.Select(p => p.AsDto())?.ToList() ?? new List<PersonDto>());
    }
}
