namespace EntityFrameworkCore.Data.Entities
{
    public class Department
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public IEnumerable<Person> People {get;set;}
    }
}
