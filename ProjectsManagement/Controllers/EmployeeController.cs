using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.Models;
namespace ProjectsManagement.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EmployeesScreen()
        {
            List<Employee> Employees = EmployeesOperations.GetEmployees();
            return View(Employees);
        }

        [HttpPost]
        public bool Add(string FirstName, string SurName, string LastName, string Email)
        {
            EmployeesOperations.AddEmployee(FirstName, SurName, LastName, Email);
            return true;
        }

        [HttpPost]
        public bool Edit(string Id,string FirstName, string SurName, string LastName, string Email)
        {
            EmployeesOperations.EditEmployee(Id,FirstName, SurName, LastName, Email);
            return true;
        }

        [HttpPost]
        public bool Delete(string Id)
        {
            EmployeesOperations.DeleteEmployee(Id);
            return true;
        }
    }
}
