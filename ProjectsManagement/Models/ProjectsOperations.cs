using Microsoft.EntityFrameworkCore;
namespace ProjectsManagement.Models
{
    public class ProjectsOperations
    {
        /// <summary>
        /// Функция для получения списка проектов
        /// </summary>
        /// <returns></returns>
        public static List<Project> GetProjects()
        {
            using (Context context = new Context())
            {
                return context.Projects.Include(prj => prj.Employees).ToList();
            }
        }

        /// <summary>
        /// Функция для добавления нового проекта в БД
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="CustomerCompany_Title"></param>
        /// <param name="ExecutorCompany_Title"></param>
        /// <param name="TimeFrame"></param>
        /// <param name="Priority"></param>
        public async static void AddProject(string Title, string CustomerCompany_Title, string ExecutorCompany_Title, string? DtStart, string? DtEnd, int Priority)
        {
            using (Context context = new Context())
            {
                context.Projects.Add(new Project(Title, CustomerCompany_Title, ExecutorCompany_Title, DtStart , DtEnd, Priority));
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Функция для изменения данных проекта в БД
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Title"></param>
        /// <param name="CustomerCompany_Title"></param>
        /// <param name="ExecutorCompany_Title"></param>
        /// <param name="TimeFrame"></param>
        /// <param name="Priority"></param>
        public async static void EditProject(string Id, string Title, string CustomerCompany_Title, string ExecutorCompany_Title, string? DtStart, string? DtEnd, int Priority)
        {
            using (Context context = new Context())
            {

                var TargetProject = context.Projects.Where(Project => Project.Id == System.Convert.ToInt32(Id)).FirstOrDefault();
                (TargetProject.Title, TargetProject.CustomerCompany_Title, TargetProject.ExecutorCompany_Title, TargetProject.DtStart, TargetProject.DtEnd, TargetProject.Priority) = (Title, CustomerCompany_Title, ExecutorCompany_Title, DtStart, DtEnd, Priority);                
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Функция для удаления проекта из БД
        /// </summary>
        /// <param name="Id"></param>
        public async static void DeleteProject(string Id)
        {
            using (Context context = new Context())
            {
                var TargetProject = context.Projects.Where(Project => Project.Id == System.Convert.ToInt32(Id)).FirstOrDefault();
                context.Projects.Remove(TargetProject);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Функция для добавления сотрудника на проект (с изменением БД)
        /// </summary>
        /// <param name="EmployeesId"></param>
        /// <param name="ProjectId"></param>
        public async static void AddEmployee(string[] EmployeesId, string ProjectId)
        {
            using (Context context = new Context())
            {
                var TargetProject = context.Projects.Where(Project => Project.Id == System.Convert.ToInt32(ProjectId)).FirstOrDefault();
                var SelectedEmployees = EmployeesId.Select(Id => context.Employees.Where(employee => employee.Id == System.Convert.ToInt32(Id)).FirstOrDefault());

                foreach (var employee in SelectedEmployees)
                {
                    TargetProject.Employees.Add(employee);
                    //employee.Projects.Add(TargetProject);
                }

                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Функция для удаления сотрудника с проекта (с изменением БД)
        /// </summary>
        /// <param name="EmployeesId"></param>
        /// <param name="ProjectId"></param>
        public async static void DeleteEmployee(string[] EmployeesId, string ProjectId)
        {
            using (Context context = new Context())
            {
                context.Projects.Include(prj => prj.Employees).ToList();

                var TargetProject = context.Projects.Where(Project => Project.Id == System.Convert.ToInt32(ProjectId)).FirstOrDefault();
                var SelectedEmployees = EmployeesId.Select(Id => context.Employees.Where(employee => employee.Id == System.Convert.ToInt32(Id)).FirstOrDefault());

                foreach (var employee in SelectedEmployees)
                {
                    TargetProject.Employees.Remove(employee);     
                }
               
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Функция для удаления сотрудника с проекта (с изменением БД)
        /// </summary>
        /// <param name="EmployeesId"></param>
        /// <param name="ProjectId"></param>
        public async static void ChangeLead(string EmployeeId, string ProjectId)
        {
            using (Context context = new Context())
            {
                context.Projects.Where(prj => prj.Id == System.Convert.ToInt32(ProjectId)).FirstOrDefault().LeadEmployeeId = System.Convert.ToInt32(EmployeeId);

                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Получение списка сотрудников, которые могут быть добавлены на проект
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public static List<Employee> GetEmployeesForProject(string ProjectId)
        {
            using (Context context = new Context())
            {
                context.Employees.Include(employee => employee.Projects).ToList();


                var EmployeesOnProject = context.Employees
                    .Select(employee => employee)
                    .Where(employee => employee.Projects
                        .Select(prj => prj)
                        .Where(prj => prj.Id == System.Convert.ToInt32(ProjectId))
                        .Count() != 0);

                return context.Employees.Except(EmployeesOnProject).ToList();
            }
        }

        /// <summary>
        /// Получение списка сотрудников, которые могут быть назначены руководителем проекта (все сотрудники на проекте, кроме текущего руководителя)
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public static List<Employee> GetEmployeesForLead(string ProjectId)
        {
            using (Context context = new Context())
            {
                context.Employees.Include(employee => employee.Projects).ToList();

                int ProjectLeadId = context.Projects.Select(prj => prj).Where(prj => prj.Id == System.Convert.ToInt32(ProjectId)).Select(prj => prj.LeadEmployeeId).FirstOrDefault();

                var EmployeesOnProject = context.Employees
                    .Select(employee => employee)
                    .Where(employee => employee.Projects
                        .Select(prj => prj)
                        .Where(prj => prj.Id == System.Convert.ToInt32(ProjectId))
                        .Count() != 0);

                var LeadEmployee = context.Employees
                    .Select(employee => employee)
                    .Where(employee => employee.Id == ProjectLeadId);

                return EmployeesOnProject.Except(LeadEmployee).ToList();
            }
        }

        /// <summary>
        /// Получения полного списка сотрудников на проекте
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public static List<Employee> GetEmployeesOnProject(string ProjectId)
        {
            using (Context context = new Context())
            {
                context.Employees.Include(employee => employee.Projects).ToList();             

                var EmployeesOnProject = context.Employees
                    .Select(employee => employee)
                    .Where(employee => employee.Projects
                        .Select(prj => prj)
                        .Where(prj => prj.Id == System.Convert.ToInt32(ProjectId))
                        .Count() != 0);

                return EmployeesOnProject.ToList();
            }
        }
    }
}
