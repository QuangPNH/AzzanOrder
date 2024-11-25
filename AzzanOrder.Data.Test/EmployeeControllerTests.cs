﻿using System.Collections.Generic;
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
using AzzanOrder.Data.Test;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AzzanOrder.Tests
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private static Mock<OrderingAssistSystemContext> _mockContext;
        private static Mock<DbSet<Employee>> _mockSet;
        private EmployeeController _controller;

        [OneTimeSetUp]
        public static void setup()
        {
            _mockContext = EmployeeControllerTestSetup.GetMockContext();
            _mockSet = EmployeeControllerTestSetup.GetMockDbSet();

        }

        [SetUp]
        public void Setup()
        {
            _controller = new EmployeeController(_mockContext.Object);
        }

        [Test]
        public async Task GetEmployees_ReturnsAllEmployees()
        {
            // Act
            var result = await _controller.GetEmployees();

            // Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<Employee>>>(result);

            var returnValue = result?.Value as List<Employee>;
            Assert.IsNotNull(returnValue, "Expected a list of employees but got null.");
            Assert.AreEqual(2, returnValue?.Count, "Expected 2 employees but got a different count.");
        }


        [Test]
        public async Task GetEmployeeByPhone_ReturnsEmployeeByPhone()
        {
            var result = await _controller.GetEmployeeByPhone("1234567890");
            Assert.IsInstanceOf<ActionResult<Employee>>(result);
            var returnValue = result?.Value as Employee;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual("1234567890", returnValue?.Phone);
        }

        [Test]
        public async Task GetStaffByPhone_ReturnsStaffByPhone()
        {
            _controller = new EmployeeController(_mockContext.Object);
            var result = await _controller.GetStaffByPhone("1234567890");
            Assert.IsInstanceOf<ActionResult<Employee>>(result);
            var returnValue = result?.Value as Employee;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual("1234567890", returnValue?.Phone);
        }

        [Test]
        public async Task GetManagerByPhone_ReturnsManagerByPhone()
        {
            var result = await _controller.GetManagerByPhone("0987654321");
            Assert.IsInstanceOf<ActionResult<Employee>>(result);
            var returnValue = result?.Value as Employee;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual("0987654321", returnValue?.Phone);
        }

        [Test]
        public async Task PutEmployee_UpdatesEmployee()
        {
            var employee = new Employee
            {
                EmployeeId = 1987,
                EmployeeName = "John Doe Updated",
                Gender = true,
                Phone = "1234567890",
                Gmail = "john.doe@example.com",
                BirthDate = new DateTime(1990, 1, 1),
                RoleId = 2,
                HomeAddress = "123 Main St, Hometown",
                WorkAddress = "456 Work St, Worktown",
                Image = "path/to/image.jpg",
                ManagerId = 2,
                OwnerId = 3,
                IsDelete = false,
                Role = new Role { RoleId = 2, RoleName = "Staff" }
            };
            var result = await _controller.PutEmployee(employee);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task PostEmployee_AddsEmployee()
        {
            var employee = new Employee
            {
                EmployeeId = 3987,
                EmployeeName = "John Doe 3",
                Gender = true,
                Phone = "1234567890",
                Gmail = "john.doe@example.com",
                BirthDate = new DateTime(1990, 1, 1),
                RoleId = 2,
                HomeAddress = "123 Main St, Hometown",
                WorkAddress = "456 Work St, Worktown",
                Image = "path/to/image.jpg",
                ManagerId = 2,
                OwnerId = 3,
                IsDelete = false,
                Role = new Role { RoleId = 2, RoleName = "Staff" }
            };

            _mockSet.Setup(m => m.AddAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()))
                    .Returns((Employee emp, CancellationToken token) =>
                    {
                        _mockContext.Object.Employees.Add(emp);
                        return new ValueTask<EntityEntry<Employee>>(Task.FromResult<EntityEntry<Employee>>(null!));
                    });

            _mockContext.Setup(m => m.Employees).Returns(_mockSet.Object);

            var result = await _controller.PostEmployee(employee);
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        }

        [Test]
        public async Task DeleteEmployee_DeletesEmployee()
        {
            var result = await _controller.DeleteEmployee(1987);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
        [Test]
        public async Task GetEmployee_ReturnsEmployeeById()
        {
            // Act
            var result = await _controller.GetEmployee(1987);

            // Assert
            Assert.IsInstanceOf<ActionResult<Employee>>(result);
            var returnValue = result?.Value as Employee;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(1987, returnValue?.EmployeeId);
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
