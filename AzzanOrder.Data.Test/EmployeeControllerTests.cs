using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using AzzanOrder.Data.Controllers;
using AzzanOrder.Data.Models;
using System.Threading;
using System;

namespace AzzanOrder.Tests
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private Mock<OrderingAssistSystemContext> _mockContext;
        private Mock<DbSet<Employee>> _mockSet;
        private EmployeeController _controller;

        [SetUp]
        public void Setup()
        {
            // Step 1: Create a list of employees
            var employees = new List<Employee>
    {
        new Employee
        {
            EmployeeId = 1,
            EmployeeName = "John Doe",
            Gender = true,
            Phone = "1234567890",
            Gmail = "john.doe@example.com",
            BirthDate = new DateTime(1990, 1, 1),
            RoleId = 1,
            HomeAddress = "123 Main St, Hometown",
            WorkAddress = "456 Work St, Worktown",
            Image = "path/to/image.jpg",
            ManagerId = 2,
            OwnerId = 3,
            IsDelete = false
        },
        new Employee
        {
            EmployeeId = 2,
            EmployeeName = "Jane Doe",
            Gender = true,
            Phone = "0987654321",
            Gmail = "jane.doe@example.com",
            BirthDate = new DateTime(1990, 1, 1),
            RoleId = 1,
            HomeAddress = "123 Main St, Hometown",
            WorkAddress = "456 Work St, Worktown",
            Image = "path/to/image.jpg",
            ManagerId = 2,
            OwnerId = 3,
            IsDelete = false
        }
    }.AsQueryable();
            // Step 2: Mock the DbSet<Employee>
            _mockSet = new Mock<DbSet<Employee>>();
            _mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(employees.Provider);
            _mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            _mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            _mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());
            _mockSet.As<IAsyncEnumerable<Employee>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>())).Returns(new TestAsyncEnumerator<Employee>(employees.GetEnumerator()));

            // Step 3: Mock the OrderingAssistSystemContext
            _mockContext = new Mock<OrderingAssistSystemContext>();
            _mockContext.Setup(c => c.Employees).Returns(_mockSet.Object);

            // Initialize the controller with the mocked context
            _controller = new EmployeeController(_mockContext.Object);
        }


        [Test]
        public async Task GetEmployees_ReturnsAllEmployees()
        {
            // Act
            var result = await _controller.GetEmployees();

            // Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<Employee>>>(result);

            // Check the type of result.Result
            Assert.IsInstanceOf<OkObjectResult>(result.Result, "Expected OkObjectResult but got a different result type.");

            var actionResult = result.Result as OkObjectResult;
            Assert.IsNotNull(actionResult, "Expected OkObjectResult but got null.");

            var returnValue = actionResult?.Value as List<Employee>;
            Assert.IsNotNull(returnValue, "Expected a list of employees but got null.");
            Assert.AreEqual(2, returnValue?.Count, "Expected 2 employees but got a different count.");
        }


        [Test]
        public async Task GetEmployee_ReturnsEmployeeById()
        {
            var result = await _controller.GetEmployee(1);
            Assert.IsInstanceOf<ActionResult<Employee>>(result);
            var actionResult = result.Result as OkObjectResult;
            Assert.IsNotNull(actionResult);
            var returnValue = actionResult?.Value as Employee;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(1, returnValue?.EmployeeId);
        }

        [Test]
        public async Task GetEmployeeByPhone_ReturnsEmployeeByPhone()
        {
            var result = await _controller.GetEmployeeByPhone("1234567890");
            Assert.IsInstanceOf<ActionResult<Employee>>(result);
            var actionResult = result.Result as OkObjectResult;
            Assert.IsNotNull(actionResult);
            var returnValue = actionResult?.Value as Employee;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual("1234567890", returnValue?.Phone);
        }

        [Test]
        public async Task GetStaffByPhone_ReturnsStaffByPhone()
        {
            var result = await _controller.GetStaffByPhone("1234567890");
            Assert.IsInstanceOf<ActionResult<Employee>>(result);
            var actionResult = result.Result as OkObjectResult;
            Assert.IsNotNull(actionResult);
            var returnValue = actionResult?.Value as Employee;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual("1234567890", returnValue?.Phone);
            Assert.AreEqual("staff", returnValue?.Role?.RoleName.ToLower());
        }

        [Test]
        public async Task GetManagerByPhone_ReturnsManagerByPhone()
        {
            var result = await _controller.GetManagerByPhone("0987654321");
            Assert.IsInstanceOf<ActionResult<Employee>>(result);
            var actionResult = result.Result as OkObjectResult;
            Assert.IsNotNull(actionResult);
            var returnValue = actionResult?.Value as Employee;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual("0987654321", returnValue?.Phone);
            Assert.AreEqual("manager", returnValue?.Role?.RoleName.ToLower());
        }

        [Test]
        public async Task PutEmployee_UpdatesEmployee()
        {
            var employee = new Employee { EmployeeId = 1, EmployeeName = "John Doe Updated" };
            var result = await _controller.PutEmployee(employee);
            Assert.IsInstanceOf<OkObjectResult>(result);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public async Task PostEmployee_AddsEmployee()
        {
            var employee = new Employee { EmployeeId = 3, EmployeeName = "New Employee" };
            var result = await _controller.PostEmployee(employee);
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public async Task DeleteEmployee_DeletesEmployee()
        {
            var result = await _controller.DeleteEmployee(1);
            Assert.IsInstanceOf<OkObjectResult>(result);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }

    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public ValueTask DisposeAsync()
        {
            _inner.Dispose();
            return ValueTask.CompletedTask;
        }

        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(_inner.MoveNext());
        }

        public T Current => _inner.Current;
    }
}
