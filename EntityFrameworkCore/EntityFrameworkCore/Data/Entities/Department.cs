namespace EntityFrameworkCore.Data.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Person> People { get; set; }
    }
}
