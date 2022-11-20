using Microsoft.EntityFrameworkCore;
namespace ProjectsManagement.Models
{
    public class EmployeesOperations
    {
        public static List<Employee> GetEmployees()
        {
            using (Context context = new Context())
            {
                return context.Employees.ToList();
            }
        }


        public async static void AddEmployee(string FirstName, string SurName, string LastName, string Email)
        {
            using (Context context = new Context())
            {
                context.Employees.Add(new Employee(FirstName, SurName, LastName, Email));
                await context.SaveChangesAsync();
            }
        }

        public async static void EditEmployee(string Id, string FirstName, string SurName, string LastName, string Email)
        {
            using (Context context = new Context())
            {
                var TargetEmployee = context.Employees.Where(Employee => Employee.Id == System.Convert.ToInt32(Id)).FirstOrDefault();
                (TargetEmployee.FirstName, TargetEmployee.SurName, TargetEmployee.LastName, TargetEmployee.Email) = (FirstName, SurName, LastName, Email);
                await context.SaveChangesAsync();
            }
        }

        public async static void DeleteEmployee(string Id)
        {
            using (Context context = new Context())
            {
                var TargetEmployee = context.Employees.Where(Employee => Employee.Id == System.Convert.ToInt32(Id)).FirstOrDefault();
                context.Employees.Remove(TargetEmployee);
                await context.SaveChangesAsync();
            }
        }
    }
}
