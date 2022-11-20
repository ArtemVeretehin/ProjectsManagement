using Microsoft.AspNetCore.Mvc;
using ProjectsManagement.Models;
namespace ProjectsManagement.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ProjectsScreen()
        {
            List<Project> Projects = ProjectsOperations.GetProjects();
            
            return View(Projects);
        }

        /// <summary>
        /// Функция добавления проекта в БД
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="CustomerCompany_Title"></param>
        /// <param name="ExecutorCompany_Title"></param>
        /// <param name="TimeFrame"></param>
        /// <param name="Priority"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Add(string Title, string CustomerCompany_Title, string ExecutorCompany_Title, string TimeFrame, int Priority)
        {
            ProjectsOperations.AddProject(Title, CustomerCompany_Title, ExecutorCompany_Title, TimeFrame, Priority);
            return true;
        }

        /// <summary>
        /// Функция редактирования проекта в БД
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Title"></param>
        /// <param name="CustomerCompany_Title"></param>
        /// <param name="ExecutorCompany_Title"></param>
        /// <param name="TimeFrame"></param>
        /// <param name="Priority"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Edit(string Id, string Title, string CustomerCompany_Title, string ExecutorCompany_Title, string TimeFrame, int Priority)
        {
            ProjectsOperations.EditProject(Id, Title, CustomerCompany_Title, ExecutorCompany_Title, TimeFrame, Priority);
            return true;
        }

        /// <summary>
        /// Функция удаления проекта в БД
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Delete(string Id)
        {
            ProjectsOperations.DeleteProject(Id);
            return true;
        }

        /// <summary>
        /// Функция добавления сотрудников в проект (с обновлением в БД)
        /// </summary>
        /// <param name="SelectedEmployeesId"></param>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddEmployee(string[] SelectedEmployeesId, string ProjectId)
        {
            ProjectsOperations.AddEmployee(SelectedEmployeesId, ProjectId);
            return true;
        }

        /// <summary>
        /// Функция удаления сотрудников с проекта (с обновлением в БД)
        /// </summary>
        /// <param name="SelectedEmployeesId"></param>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        [HttpPost]
        public bool DeleteEmployee(string[] SelectedEmployeesId, string ProjectId)
        {
            ProjectsOperations.DeleteEmployee(SelectedEmployeesId, ProjectId);
            return true;
        }

        /// <summary>
        /// Функция изменения руководителя проекта (с обновлением в БД)
        /// </summary>
        /// <param name="SelectedEmployeesId"></param>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        [HttpPost]
        public bool ChangeLead(string SelectedEmployeeId, string ProjectId)
        {
            ProjectsOperations.ChangeLead(SelectedEmployeeId, ProjectId);
            return true;
        }


        /// <summary>
        /// Генерация частичного View-попапа для добавления сотрудников в проект
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public IActionResult EmployeesAddToPrjView(string ProjectId)
        {
            List<Employee> Employees = new List<Employee>();

            Employees = ProjectsOperations.GetEmployeesForProject(ProjectId);

            return PartialView(Employees);
        }

        /// <summary>
        /// Генерация частичного View-попапа для удаления сотрудников с проекта
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public IActionResult EmployeesDeleteFromPrjView(string ProjectId)
        {
            List<Employee> Employees = new List<Employee>();

            Employees = ProjectsOperations.GetEmployeesOnProject(ProjectId);

            return PartialView(Employees);
        }

        /// <summary>
        /// Генерация частичного View-попапа для назначения руководителя проекта
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public IActionResult EmployeesPrjLeadChangeView(string ProjectId)
        {
            List<Employee> Employees = new List<Employee>();

            Employees = ProjectsOperations.GetEmployeesForLead(ProjectId);

            return PartialView(Employees);
        }
    }
}
