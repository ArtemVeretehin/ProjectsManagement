using Microsoft.EntityFrameworkCore;
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
    public class EmployeesOperationsTests
    {
        [TestMethod()]
        public void GetEmployeesTest()
        {
            var employees = EmployeesOperations.GetEmployees();
            Assert.IsTrue(employees is List<Employee>);
        }

        [TestMethod()]
        public void AddEmployeeTest()
        { 
            //Arrange
            Employee employee = new Employee("TestName", "TestSurName", "TestLastName", "TestMail");

            //Act
            Context _dbContext = new Context();
            
            EmployeesOperations.AddEmployee(employee.FirstName, employee.SurName, employee.LastName, employee.Email);

            var employeeFromDataBase = _dbContext.Employees.FirstOrDefault(e =>
            e.FirstName == employee.FirstName &&
            e.SurName == employee.SurName &&
            e.LastName == employee.LastName &&
            e.Email == employee.Email);

            //Assert
            Assert.AreEqual(employee.FirstName, employeeFromDataBase.FirstName);
            Assert.AreEqual(employee.SurName, employeeFromDataBase.SurName);
            Assert.AreEqual(employee.LastName, employeeFromDataBase.LastName);
            Assert.AreEqual(employee.Email, employeeFromDataBase.Email);
        }

        [TestMethod()]
        public void EditEmployeeTest()
        {
            //Arrange
            Employee employee = new Employee("TestName", "TestSurName", "TestLastName", "TestMail");
            Employee resultEmployee = new Employee("NewTestName", "NewTestSurName", "NewTestLastName", "NewTestMail");

            //Act
            Context _dbContext = new Context();
            var employeeEntity = _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
            int employeeId = employeeEntity.Entity.Id;
            _dbContext.Dispose();

            EmployeesOperations.EditEmployee(employeeId.ToString(), resultEmployee.FirstName, resultEmployee.SurName, resultEmployee.LastName, resultEmployee.Email);
            
            _dbContext = new Context();
            var employeeFromDataBase = _dbContext.Employees.FirstOrDefault(e =>
            e.Id == employeeId);

            //Assert
            Assert.AreEqual(resultEmployee.FirstName, employeeFromDataBase.FirstName);
            Assert.AreEqual(resultEmployee.SurName, employeeFromDataBase.SurName);
            Assert.AreEqual(resultEmployee.LastName, employeeFromDataBase.LastName);
            Assert.AreEqual(resultEmployee.Email, employeeFromDataBase.Email);
        }

        [TestMethod()]
        public void DeleteEmployeeTest()
        {
            //Arrange
            Employee employee = new Employee("TestName", "TestSurName", "TestLastName", "TestMail");

            //Act
            Context _dbContext = new Context();
            var employeeEntity = _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
            int employeeId = employeeEntity.Entity.Id;

            EmployeesOperations.DeleteEmployee(employeeId.ToString());

            var employeeFromDataBase = _dbContext.Employees.FirstOrDefault(e => e.Id == employeeId);

            //Assert
            Assert.IsTrue(employeeFromDataBase is null);
        }
    }
}