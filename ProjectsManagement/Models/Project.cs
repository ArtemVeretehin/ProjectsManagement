using Microsoft.EntityFrameworkCore;
namespace ProjectsManagement.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? CustomerCompany_Title { get; set; }
        public string? ExecutorCompany_Title { get; set; }
        public string? TimeFrame { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public int LeadEmployeeId { get; set; }
        public int Priority { get; set; }
        public Project(string Title, string CustomerCompany_Title, string ExecutorCompany_Title, string TimeFrame, int Priority)
        {
            this.Title = Title;
            this.CustomerCompany_Title = CustomerCompany_Title;
            this.ExecutorCompany_Title = ExecutorCompany_Title;
            this.TimeFrame = TimeFrame;
            this.Priority = Priority;
        }
    }
    
}
