using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectsManagement.Models;
namespace ProjectsManagement.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ProjectsScreen(string sortOrder, FilterObject filter)
        {
            ViewBag.ProjectNameSortParam = String.IsNullOrEmpty(sortOrder) ? "ProjectName_desc" : "";
            ViewBag.CustomerNameSortParam = (sortOrder == "CustomerName") ? "CustomerName_desc" : "CustomerName";
            ViewBag.ExecutorNameSortParam = (sortOrder == "ExecutorName") ? "ExecutorName_desc" : "ExecutorName";
            ViewBag.LeadNameSortParam = (sortOrder == "LeadName") ? "LeadName_desc" : "LeadName";
            ViewBag.DtStartSortParam = (sortOrder == "DtStart") ? "DtStart_desc" : "DtStart";
            ViewBag.DtEndSortParam = (sortOrder == "DtEnd") ? "DtEnd_desc" : "DtEnd";
            ViewBag.PrioritySortParam = (sortOrder == "Priority") ? "Priority_desc" : "Priority";


            

            IEnumerable<Project> Projects = ProjectsOperations.GetProjects();


            IEnumerable<Employee> LeadEmployees;
            switch (sortOrder)
            {
                case "ProjectName_desc":
                    Projects = Projects.OrderByDescending(prj => prj.Title);
                    ViewBag.sortType = "Название проекта, по убыванию";
                    break;
                case "CustomerName":
                    Projects = Projects.OrderBy(prj => prj.CustomerCompany_Title);
                    ViewBag.sortType = "Компания-заказчик, по возрастанию";
                    break;
                case "CustomerName_desc":
                    Projects = Projects.OrderByDescending(prj => prj.CustomerCompany_Title);
                    ViewBag.sortType = "Компания-заказчик, по убыванию";
                    break;
                case "ExecutorName":
                    Projects = Projects.OrderBy(prj => prj.ExecutorCompany_Title);
                    ViewBag.sortType = "Компания-исполнитель, по возрастанию";
                    break;
                case "ExecutorName_desc":
                    Projects = Projects.OrderByDescending(prj => prj.ExecutorCompany_Title);
                    ViewBag.sortType = "Компания-исполнитель, по убыванию";
                    break;


                case "LeadName":

                    LeadEmployees = Projects.Select(prj => prj.Employees.Where(employe => employe.Id == prj.LeadEmployeeId).FirstOrDefault());

                    Projects = Projects.Join(LeadEmployees,
                       prj => prj.LeadEmployeeId,
                       leadEmployee => leadEmployee.Id,
                       (prj, leadEmployee) => new
                       {
                           Title = prj.Title,
                           CustomerCompany_Title = prj.CustomerCompany_Title,
                           ExecutorCompany_Title = prj.ExecutorCompany_Title,
                           DtStart = prj.DtStart,
                           DtEnd = prj.DtEnd,
                           Employees = prj.Employees,
                           LeadEmployeeId = prj.LeadEmployeeId,
                           Priority = prj.Priority,
                           leadEmployee_lastName = leadEmployee.LastName
                       })
                       .OrderBy(prj_employee => prj_employee.leadEmployee_lastName)
                       .Select(prj_employee => new Project(
                           prj_employee.Title,
                           prj_employee.CustomerCompany_Title,
                           prj_employee.ExecutorCompany_Title,
                           prj_employee.DtStart,
                           prj_employee.DtEnd,
                           prj_employee.Employees,
                           prj_employee.LeadEmployeeId,
                           prj_employee.Priority
                           ));
                    ViewBag.sortType = "Руководитель проекта, по возрастанию";
                    break;

                case "LeadName_desc":

                    LeadEmployees = Projects.Select(prj => prj.Employees.Where(employe => employe.Id == prj.LeadEmployeeId).FirstOrDefault());

                    Projects = Projects.Join(LeadEmployees,
                       prj => prj.LeadEmployeeId,
                       leadEmployee => leadEmployee.Id,
                       (prj, leadEmployee) => new
                       {
                           Title = prj.Title,
                           CustomerCompany_Title = prj.CustomerCompany_Title,
                           ExecutorCompany_Title = prj.ExecutorCompany_Title,
                           DtStart = prj.DtStart,
                           DtEnd = prj.DtEnd,
                           Employees = prj.Employees,
                           LeadEmployeeId = prj.LeadEmployeeId,
                           Priority = prj.Priority,
                           leadEmployee_lastName = leadEmployee.LastName
                       })
                       .OrderByDescending(prj_employee => prj_employee.leadEmployee_lastName)
                       .Select(prj_employee => new Project(
                           prj_employee.Title,
                           prj_employee.CustomerCompany_Title,
                           prj_employee.ExecutorCompany_Title,
                           prj_employee.DtStart,
                           prj_employee.DtEnd,
                           prj_employee.Employees,
                           prj_employee.LeadEmployeeId,
                           prj_employee.Priority
                           ));
                    ViewBag.sortType = "Руководитель проекта, по убыванию";
                    break;

                case "Priority":
                    Projects = Projects.OrderBy(prj => prj.Priority);
                    ViewBag.sortType = "Приоритет, по возрастанию";
                    break;
                case "Priority_desc":
                    Projects = Projects.OrderByDescending(prj => prj.Priority);
                    ViewBag.sortType = "Приоритет, по убыванию";
                    break;

                case "DtStart":
                    Projects = Projects.OrderBy(prj => prj.DtStart);
                    ViewBag.sortType = "Дата начала проекта, по возрастанию";
                    break;
                case "DtStart_desc":
                    Projects = Projects.OrderByDescending(prj => prj.DtStart);
                    ViewBag.sortType = "Дата начала проекта, по убыванию";
                    break;

                case "DtEnd":
                    Projects = Projects.OrderBy(prj => prj.DtStart);
                    ViewBag.sortType = "Дата окончания проекта, по возрастанию";
                    break;
                case "DtEnd_desc":
                    Projects = Projects.OrderByDescending(prj => prj.DtStart);
                    ViewBag.sortType = "Дата окончания проекта, по убыванию";
                    break;

                default:
                    Projects = Projects.OrderBy(prj => prj.CustomerCompany_Title);
                    ViewBag.sortType = "Название проекта, по возрастанию";
                    break;
            }


            //Фильтрация
            ViewBag.Filter = (filter is not null) ? filter : null;
            Projects = filter.ProjectsFiltration(Projects);
            


            return View(Projects.ToList());
        }

        /// <summary>
        /// Функция добавления проекта в БД
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="CustomerCompany_Title"></param>
        /// <param name="ExecutorCompany_Title"></param>
        /// <param name="DtStart"></param>
        /// <param name="DtEnd"></param>
        /// <param name="Priority"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Add(string Title, string CustomerCompany_Title, string ExecutorCompany_Title, DateTime? DtStart, DateTime? DtEnd, int Priority)
        {
            ProjectsOperations.AddProject(Title, CustomerCompany_Title, ExecutorCompany_Title, DtStart, DtEnd, Priority);
            return true;
        }

        /// <summary>
        /// Функция редактирования проекта в БД
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Title"></param>
        /// <param name="CustomerCompany_Title"></param>
        /// <param name="ExecutorCompany_Title"></param>
        /// <param name="DtStart"></param>
        /// <param name="DtEnd"></param>
        /// <param name="Priority"></param>
        /// <returns></returns>
        [HttpPost]
        public bool Edit(string Id, string Title, string CustomerCompany_Title, string ExecutorCompany_Title, DateTime? DtStart, DateTime? DtEnd, int Priority)
        {
            ProjectsOperations.EditProject(Id, Title, CustomerCompany_Title, ExecutorCompany_Title, DtStart, DtEnd, Priority);
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

        public IActionResult ProjectsFiltrationSettingsView()
        {
            IEnumerable<Project> Projects = ProjectsOperations.GetProjects();

            SelectList ProjectTitles = new SelectList(Projects.OrderBy(prj => prj.Title).Select(prj => prj.Title).Distinct().ToList());
            SelectList CustomerTitles = new SelectList(Projects.OrderBy(prj => prj.CustomerCompany_Title).Select(prj => prj.CustomerCompany_Title).Distinct().ToList());
            SelectList ExecutorTitles = new SelectList(Projects.OrderBy(prj => prj.ExecutorCompany_Title).Select(prj => prj.ExecutorCompany_Title).Distinct().ToList());

            ViewBag.ProjectTitles = ProjectTitles;
            ViewBag.CustomerTitles = CustomerTitles;
            ViewBag.ExecutorTitles = ExecutorTitles;


            return PartialView(Projects.ToList());
        }
    }
}
