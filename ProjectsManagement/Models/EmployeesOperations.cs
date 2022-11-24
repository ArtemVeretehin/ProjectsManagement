using Microsoft.EntityFrameworkCore;
namespace ProjectsManagement.Models
{
    /// <summary>
    /// Класс-обработчик событий, связанных с сотрудниками
    /// </summary>
    public class EmployeesOperations
    {
        /// <summary>
        /// Метод для возврата списка всех сотрудников из БД
        /// </summary>
        /// <returns></returns>
        public static List<Employee> GetEmployees()
        {
            using (Context context = new Context())
            {
                return context.Employees.ToList();
            }
        }


        /// <summary>
        /// Метод для добавления сотрудника в БД
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="SurName"></param>
        /// <param name="LastName"></param>
        /// <param name="Email"></param>
        public async static void AddEmployee(string FirstName, string SurName, string LastName, string Email)
        {
            using (Context context = new Context())
            {
                context.Employees.Add(new Employee(FirstName, SurName, LastName, Email));
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Метод для редактирования данных сотрудника в БД
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="FirstName"></param>
        /// <param name="SurName"></param>
        /// <param name="LastName"></param>
        /// <param name="Email"></param>
        public async static void EditEmployee(string Id, string FirstName, string SurName, string LastName, string Email)
        {
            using (Context context = new Context())
            {
                var TargetEmployee = context.Employees.Where(Employee => Employee.Id == System.Convert.ToInt32(Id)).FirstOrDefault();
                (TargetEmployee.FirstName, TargetEmployee.SurName, TargetEmployee.LastName, TargetEmployee.Email) = (FirstName, SurName, LastName, Email);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Метод для удаления данных сотрудника из БД
        /// </summary>
        /// <param name="Id"></param>
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
