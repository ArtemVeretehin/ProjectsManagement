namespace ProjectsManagement.Models
{
    public class FiltrationHandler
    {
        public string ProjectTitleFilter { get; set; }
        public string CustomerTitleFilter { get; set; }
        public string ExecutorTitleFilter { get; set; }
        public DateTime DtStartLeftFilter { get; set; }
        public DateTime DtStartRightFilter { get; set; }
        public DateTime DtEndLeftFilter { get; set; }
        public DateTime DtEndRightFilter { get; set; }
        public string LeadEmployeeFilter { get; set; }
        public int PriorityLeftFilter { get; set; } = -1;
        public int PriorityRightFilter { get; set; } = -1;

        public IEnumerable<Project> ProjectsFiltration(IEnumerable<Project> Projects)
        {
            if (!String.IsNullOrEmpty(ProjectTitleFilter))
            {
                Projects = Projects.Where(prj => prj.Title == ProjectTitleFilter);
            }

            if (!String.IsNullOrEmpty(CustomerTitleFilter))
            {
                Projects = Projects.Where(prj => prj.CustomerCompany_Title == CustomerTitleFilter);
            }

            if (!String.IsNullOrEmpty(ExecutorTitleFilter))
            {
                Projects = Projects.Where(prj => prj.ExecutorCompany_Title == ExecutorTitleFilter);
            }


            if (DtStartLeftFilter != DateTime.MinValue)
            {
                Projects = Projects.Where(prj => prj.DtStart >= DtStartLeftFilter);
            }


            if (DtStartRightFilter != DateTime.MinValue)
            {
                Projects = Projects.Where(prj => prj.DtStart <= DtStartRightFilter);
            }

            if (DtEndLeftFilter != DateTime.MinValue)
            {
                Projects = Projects.Where(prj => prj.DtEnd >= DtEndLeftFilter);
            }
            //

            if (DtEndRightFilter != DateTime.MinValue)
            {
                Projects = Projects.Where(prj => prj.DtEnd <= DtEndRightFilter);
            }


            if (PriorityLeftFilter != -1)
            {
                Projects = Projects.Where(prj => prj.Priority >= PriorityLeftFilter);
            }

            if (PriorityRightFilter != -1)
            {
                Projects = Projects.Where(prj => prj.Priority <= PriorityRightFilter);
            }

            return Projects;
        }      

    }
}
