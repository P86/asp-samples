using EntityFrameworkCore.Data.Entities;

namespace EntityFrameworkCore.DTO
{
    public class PersonDto
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public static PersonDto From(Person person)
        {
            return new PersonDto
            {
                FirstName = person.FirstName,
                LastName = person.LastName
            };
        }
    }
}
