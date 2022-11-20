
namespace ProjectsManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? SurName { get; set; }
        public string? Email { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();

        public Employee(string? FirstName, string? SurName, string? LastName, string? Email)
        {
            this.FirstName = FirstName;
            this.SurName = SurName;
            this.LastName = LastName;
            this.Email = Email;

        }
    }
}
