using System.ComponentModel.DataAnnotations.Schema;
namespace ProjectsManagement.Models
{
    /// <summary>
    /// Класс, представляющий проекты компании
    /// </summary>
    public class Project
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? CustomerCompany_Title { get; set; }
        public string? ExecutorCompany_Title { get; set; }

        [Column(TypeName = "Date")]  
        public DateTime? DtStart { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? DtEnd { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public int LeadEmployeeId { get; set; }
        public int Priority { get; set; }


        public Project(string Title, string CustomerCompany_Title, string ExecutorCompany_Title, DateTime? DtStart, DateTime? DtEnd, int Priority)
        {
            this.Title = Title;
            this.CustomerCompany_Title = CustomerCompany_Title;
            this.ExecutorCompany_Title = ExecutorCompany_Title;
            this.DtStart = DtStart;
            this.DtEnd = DtEnd;
            this.Priority = Priority;
        }

        public Project(string Title, string CustomerCompany_Title, string ExecutorCompany_Title, DateTime? DtStart, DateTime? DtEnd, List<Employee> Employees, int LeadEmployeeId, int Priority)
        {
            this.Title = Title;
            this.CustomerCompany_Title = CustomerCompany_Title;
            this.ExecutorCompany_Title = ExecutorCompany_Title;
            this.DtStart = DtStart;
            this.DtEnd = DtEnd;
            this.Employees = Employees;
            this.LeadEmployeeId = LeadEmployeeId;
            this.Priority = Priority;
        }
    }
    
}
