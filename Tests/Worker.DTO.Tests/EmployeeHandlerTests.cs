using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Moq;

using NUnit.Framework;

using Worker.DAL.Api;
using Worker.DAL.Entities;
using Worker.DTO.Entities;
using Worker.DTO.Handlers;
using Worker.DTO.Queries;

namespace Worker.DTO.Tests
{
    [TestFixture]
    public class EmployeeHandlerTests
    {
        [Test]
        public void Get_GetOneEmployeeById_ReturnEmployee()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(i => i.Get<Employee>(It.IsAny<int>())).Returns(new Employee { Id = 100 });

            var handler = new EmployeeHandler(mockRepository.Object);

            // Act
            var employeeDto = handler.Get(1);

            // Assert
            Assert.AreEqual(100, employeeDto.Id);
        }

        [Test]
        public void Get_GetAllEmployees_ReturnEmployees()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(i => i.Get<Employee>()).Returns((new List<Employee> { new Employee { Id = 1 }, new Employee { Id = 2 } }).AsQueryable());

            var handler = new EmployeeHandler(mockRepository.Object);
            
            // Act
            var employeesDto = handler.Get();

            // Assert
            Assert.AreEqual(2, employeesDto.Last().Id);
        }

        [Test]
        public void Get_GetFilteredEmployees_ReturnEmployees()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(i => i.Get(It.IsAny<Expression<Func<Employee, bool>>>()))
                .Returns((new List<Employee> { new Employee { Id = 1 }, new Employee { Id = 2 } }).AsQueryable());

            var handler = new EmployeeHandler(mockRepository.Object);

            // Act
            var employeesDto = handler.Get(new EmployeeQuery { Skip = 1, Take = 1 });

            // Assert
            Assert.AreEqual(2, employeesDto.First().Id);
        }

        [Test]
        public void Create_AddNewEmployee_AddEmployee()
        {
            // Arrange
            Employee employeeAdded = null;
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(i => i.Add(It.IsAny<Employee>())).Callback<Employee>(i => employeeAdded = i);

            var handler = new EmployeeHandler(mockRepository.Object);
            var employeeDto = new EmployeeDto { Id = 1 };

            // Act
            handler.Create(employeeDto);

            // Assert
            Assert.AreEqual(employeeDto.Id, employeeAdded.Id);
        }

        [Test]
        public void Update_UpdateEmployee_UpdateEmployee()
        {
            // Arrange
            var employee = new Employee { Name = "Test name" };
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(i => i.Get<Employee>(It.IsAny<int>())).Returns(employee);

            var handler = new EmployeeHandler(mockRepository.Object);
            var employeeDto = new EmployeeDto { Name = "Test neme new" };

            // Act
            handler.Update(employeeDto);

            // Assert
            Assert.AreEqual(employeeDto.Name, employee.Name);
        }

        [Test]
        public void Delete_DeleteEmployee_DeleteOneEmployee()
        {
            // Arrange
            var employees = new List<Employee> { new Employee { Id = 1 } };
            var mockRepository = new Mock<IRepository>();

            mockRepository.Setup(i => i.Get<Employee>(It.IsAny<int>())).Returns(employees.First(x => x.Id == 1));
            mockRepository.Setup(i => i.Delete(It.IsAny<Employee>())).Callback<Employee>(i => employees.Remove(i));

            var handler = new EmployeeHandler(mockRepository.Object);
            var employeeDto = new EmployeeDto { Id = 1 };

            // Act
            handler.Delete(employeeDto.Id);

            // Assert
            Assert.IsEmpty(employees);
        }

        [Test]
        public void GenerateReport_GetReport_GenerateReport()
        {
            // Arrange
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(i => i.Get(It.IsAny<Expression<Func<Employee, bool>>>()))
                .Returns((new List<Employee> { new Employee { Name = "Test name" } }).AsQueryable());
            
            var handler = new EmployeeHandler(mockRepository.Object);

            // Act
            var bytes = handler.GenerateReport();
            var str = Encoding.Unicode.GetString(bytes);

            // Assert
            Assert.IsTrue(str.Contains("Test name"));
        }

        [Test]
        public void Count_CountFilteredEmployees_CountEmployee()
        {
            // Arrange
            var employees = new List<Employee> { new Employee { Id = 1 }, new Employee { Id = 2 } };
            var mockRepository = new Mock<IRepository>();
            mockRepository.Setup(i => i.Get(It.IsAny<Expression<Func<Employee, bool>>>())).Returns(employees.AsQueryable());

            var handler = new EmployeeHandler(mockRepository.Object);

            // Act
            var amount = handler.Count(new EmployeeQuery());

            // Assert
            Assert.AreEqual(employees.Count, amount);
        }
    }
}
