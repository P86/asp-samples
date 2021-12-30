using EntityFrameworkCore.Data.Entities;

namespace EntityFrameworkCore.DTO
{
    public class DepartmentDto
    {
        public IEnumerable<PersonDto> People { get; private set; } = Enumerable.Empty<PersonDto>();
        public string Name { get; private set; } = string.Empty;

        public static DepartmentDto From(Department department)
        {
            return new DepartmentDto
            {
                Name = department.Name,
                People = department.People?.Select(p => PersonDto.From(p))?.ToList() ?? new List<PersonDto>(),
            };
        }
    }
}
