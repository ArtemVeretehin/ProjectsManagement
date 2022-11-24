namespace ProjectsManagement.Models
{
    /// <summary>
    /// Класс-обработчик событий фильтрации
    /// </summary>
    public class SortingHandler
    {
        public string sortOrder { get; set; }

        /// <summary>
        /// Функция сортировки проектов
        /// </summary>
        /// <param name="Projects"></param>
        /// <returns></returns>
        public IEnumerable<Project> ProjectsSorting(IEnumerable<Project> Projects)
        {
            IEnumerable<Employee?> LeadEmployees;
            IEnumerable<Project?> ProjectsWithoutLead;
            List<Project> ProjectsFiltrationResult = new List<Project>();

            switch (sortOrder)
            {
                case "ProjectName_desc":
                    Projects = Projects.OrderByDescending(prj => prj.Title);
                    break;
                case "CustomerName":
                    Projects = Projects.OrderBy(prj => prj.CustomerCompany_Title);
                    break;
                case "CustomerName_desc":
                    Projects = Projects.OrderByDescending(prj => prj.CustomerCompany_Title);
                    break;
                case "ExecutorName":
                    Projects = Projects.OrderBy(prj => prj.ExecutorCompany_Title);
                    break;
                case "ExecutorName_desc":
                    Projects = Projects.OrderByDescending(prj => prj.ExecutorCompany_Title);
                    break;
                case "LeadName":
                    //Сортировка по имени руководителя

                    /*
                     * Извлечение проектов без руководителя
                     * Дальше производится JOIN по проектам и списку руководителей соответственно проекты без руководителей будут потеряны
                     * Поэтому необходимо зафиксировать их
                    */
                    ProjectsWithoutLead = Projects.Where(prj => prj.LeadEmployeeId == 0);

                    //Извлечение всех руководителей проектов
                    LeadEmployees = Projects
                        .Select(prj => prj.Employees
                            .Where(employee => employee.Id == prj.LeadEmployeeId)
                            .FirstOrDefault())
                        .Where(emp => emp != null);

                    /*
                     * Если количество руководителей больше 0, производим JOIN проектов и списка руководителей, сортируем, добавляем в конец проекты без руководителей
                     * Иначе сортировка не имеет смысла, возвращаем проекты в исходном порядке
                    */
                    if (LeadEmployees.Count() != 0)
                    {
                        ProjectsFiltrationResult = Projects.Join(LeadEmployees,
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
                               )).ToList();

                        foreach (var Project in ProjectsWithoutLead)
                        {
                            ProjectsFiltrationResult = ProjectsFiltrationResult.Prepend(Project).ToList();
                        }
                        Projects = ProjectsFiltrationResult.AsEnumerable();

                    }
                    else
                    {
                        return Projects;
                    }

                    

                    break;

                case "LeadName_desc":
                    //Сортировка по имени руководителя

                    /*
                     * Извлечение проектов без руководителя
                     * Дальше производится JOIN по проектам и списку руководителей соответственно проекты без руководителей будут потеряны
                     * Поэтому необходимо зафиксировать их
                    */
                    ProjectsWithoutLead = Projects.Where(prj => prj.LeadEmployeeId == 0);

                    //Извлечение всех руководителей проектов
                    LeadEmployees = Projects.Select(prj => prj.Employees.Where(employe => employe.Id == prj.LeadEmployeeId).FirstOrDefault()).Where(emp => emp != null);

                    /*
                     * Если количество руководителей больше 0, производим JOIN проектов и списка руководителей, сортируем, добавляем в конец проекты без руководителей
                     * Иначе сортировка не имеет смысла, возвращаем проекты в исходном порядке
                    */
                    if (LeadEmployees.Count() != 0)
                    {
                        ProjectsFiltrationResult = Projects.Join(LeadEmployees,
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
                               )).ToList();

                        foreach (var Project in ProjectsWithoutLead)
                        {
                            ProjectsFiltrationResult.Add(Project);
                        }
                        Projects = ProjectsFiltrationResult.AsEnumerable();
                    }
                    else
                    {
                        return Projects;
                    }

                    break;
                case "Priority":
                    Projects = Projects.OrderBy(prj => prj.Priority);
                    break;
                case "Priority_desc":
                    Projects = Projects.OrderByDescending(prj => prj.Priority);
                    break;
                case "DtStart":
                    Projects = Projects.OrderBy(prj => prj.DtStart);
                    break;
                case "DtStart_desc":
                    Projects = Projects.OrderByDescending(prj => prj.DtStart);
                    break;
                case "DtEnd":
                    Projects = Projects.OrderBy(prj => prj.DtStart);
                    break;
                case "DtEnd_desc":
                    Projects = Projects.OrderByDescending(prj => prj.DtStart);
                    break;
                default:
                    Projects = Projects.OrderBy(prj => prj.CustomerCompany_Title);
                    break;
            }
            return Projects;
        }
    }
}
