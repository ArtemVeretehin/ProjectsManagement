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

        /// <summary>
        /// Метод контроллера, возвращающий вкладку для работы с сотрудниками
        /// </summary>
        /// <returns></returns>
        public IActionResult EmployeesScreen()
        {
            List<Employee> Employees = EmployeesOperations.GetEmployees();
            return View(Employees);
        }

        //Метод контроллера, инициализирующий добавление сотрудника в БД
        [HttpPost]
        public bool Add(string FirstName, string SurName, string LastName, string Email)
        {
            EmployeesOperations.AddEmployee(FirstName, SurName, LastName, Email);
            return true;
        }

        //Метод контроллера, инициализирующий изменение данных сотрудника в БД
        [HttpPost]
        public bool Edit(string Id,string FirstName, string SurName, string LastName, string Email)
        {
            EmployeesOperations.EditEmployee(Id,FirstName, SurName, LastName, Email);
            return true;
        }

        //Метод контроллера, инициализирующий удаление данных сотрудника из БД
        [HttpPost]
        public bool Delete(string Id)
        {
            EmployeesOperations.DeleteEmployee(Id);
            return true;
        }
    }
}
