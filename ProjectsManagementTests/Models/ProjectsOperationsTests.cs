using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsManagement.Models.Tests
{
    [TestClass()]
    public class ProjectsOperationsTests
    {
        [TestMethod()]
        public void GetProjectsTest()
        {
            var projects = ProjectsOperations.GetProjects();
            Assert.IsTrue(projects is List<Project>);
        }

        [TestMethod()]
        public void AddProjectTest()
        {
            //Arrange
            Project project = new Project("TestTitle", "TestCustomer", "TestExecutor", DateTime.MinValue, DateTime.MinValue, 0);

            //Act
            Context _dbContext = new Context();

            ProjectsOperations.AddProject(project.Title, project.CustomerCompany_Title, project.ExecutorCompany_Title, project.DtStart, project.DtEnd, project.Priority);

            var projectFromDataBase = _dbContext.Projects.FirstOrDefault(p =>
            p.Title == project.Title &&
            p.CustomerCompany_Title == project.CustomerCompany_Title &&
            p.ExecutorCompany_Title == project.ExecutorCompany_Title &&
            p.DtStart == project.DtStart &&
            p.DtEnd == project.DtEnd &&
            p.Priority == project.Priority);

            //Assert
            Assert.AreEqual(project.Title, projectFromDataBase.Title);
            Assert.AreEqual(project.CustomerCompany_Title, projectFromDataBase.CustomerCompany_Title);
            Assert.AreEqual(project.ExecutorCompany_Title, projectFromDataBase.ExecutorCompany_Title);
            Assert.AreEqual(project.DtStart, projectFromDataBase.DtStart);
            Assert.AreEqual(project.DtEnd, projectFromDataBase.DtEnd);
            Assert.AreEqual(project.Priority, projectFromDataBase.Priority);
        }

        [TestMethod()]
        public void EditProjectTest()
        {
            //Arrange
            Project project = new Project("TestTitle", "TestCustomer", "TestExecutor", DateTime.MinValue, DateTime.MinValue, 0);
            Project resultProject = new Project("NewTestTitle", "NewTestCustomer", "NewTestExecutor", DateTime.MaxValue, DateTime.MaxValue, 1);

            //Act
            Context _dbContext = new Context();
            var projectEntity = _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
            int projectId = projectEntity.Entity.Id;
            _dbContext.Dispose();

            ProjectsOperations.EditProject(projectId.ToString(), resultProject.Title, resultProject.CustomerCompany_Title, resultProject.ExecutorCompany_Title, 
                resultProject.DtStart, resultProject.DtEnd, resultProject.Priority);

            _dbContext = new Context();
            var projectFromDataBase = _dbContext.Projects.FirstOrDefault(p =>
            p.Id == projectId);

            //Assert
            Assert.AreEqual(resultProject.Title, projectFromDataBase.Title);
            Assert.AreEqual(resultProject.CustomerCompany_Title, projectFromDataBase.CustomerCompany_Title);
            Assert.AreEqual(resultProject.ExecutorCompany_Title, projectFromDataBase.ExecutorCompany_Title);
            Assert.AreEqual(resultProject.DtStart, projectFromDataBase.DtStart);
            Assert.AreEqual(resultProject.DtEnd, projectFromDataBase.DtEnd);
            Assert.AreEqual(resultProject.Priority, projectFromDataBase.Priority);
        }

        [TestMethod()]
        public void DeleteProjectTest()
        {
            //Arrange
            Project project = new Project("TestTitle", "TestCustomer", "TestExecutor", DateTime.MinValue, DateTime.MinValue, 0);

            //Act
            Context _dbContext = new Context();
            var projectEntity = _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
            int projectId = projectEntity.Entity.Id;

            ProjectsOperations.DeleteProject(projectId.ToString());

            var projectFromDataBase = _dbContext.Projects.FirstOrDefault(p => p.Id == projectId);

            //Assert
            Assert.IsTrue(projectFromDataBase is null);
        }

        [TestMethod()]
        public void AddEmployeeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteEmployeeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ChangeLeadTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetEmployeesForProjectTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetEmployeesForLeadTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetEmployeesOnProjectTest()
        {
            Assert.Fail();
        }
    }
}