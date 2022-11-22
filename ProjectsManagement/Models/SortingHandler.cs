namespace ProjectsManagement.Models
{
    public class SortingHandler
    {
        public string sortOrder { get; set; }


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
                    ProjectsWithoutLead = Projects.Where(prj => prj.LeadEmployeeId == 0);
                    LeadEmployees = Projects
                        .Select(prj => prj.Employees
                            .Where(employee => employee.Id == prj.LeadEmployeeId)
                            .FirstOrDefault())
                        .Where(emp => emp != null);

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
                    }
                    else
                    {
                        return Projects;
                    }

                    foreach (var Project in ProjectsWithoutLead)
                    {
                        ProjectsFiltrationResult = ProjectsFiltrationResult.Prepend(Project).ToList();
                    }
                    Projects = ProjectsFiltrationResult.AsEnumerable();

                    break;

                case "LeadName_desc":
                    ProjectsWithoutLead = Projects.Where(prj => prj.LeadEmployeeId == 0);
                    LeadEmployees = Projects.Select(prj => prj.Employees.Where(employe => employe.Id == prj.LeadEmployeeId).FirstOrDefault()).Where(emp => emp != null);
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
                    }
                    else
                    {
                        return Projects;
                    }

                    foreach (var Project in ProjectsWithoutLead)
                    {
                        ProjectsFiltrationResult.Add(Project);
                    }
                    Projects = ProjectsFiltrationResult.AsEnumerable();

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
