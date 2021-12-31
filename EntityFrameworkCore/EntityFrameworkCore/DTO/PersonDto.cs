using EntityFrameworkCore.Data.Entities;

namespace EntityFrameworkCore.DTO
{
    public record PersonDto(string FirstName, string LastName);

    public static class PersonExtensions
    {
        public static PersonDto AsDto(this Person person) => new(person.FirstName, person.LastName);
    }
}
