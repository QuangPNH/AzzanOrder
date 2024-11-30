using AzzanOrder.Data.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzzanOrder.Data.Test
{
    internal class EmployeeControllerTestSetup
    {
        internal static Mock<OrderingAssistSystemContext> GetMockContext()
        {
            var mockContext = new Mock<OrderingAssistSystemContext>();
            var mockDbSet = GetMockDbSet();

            mockContext.Setup(c => c.Employees).Returns(mockDbSet.Object);

            return mockContext;
        }

        internal static Mock<DbSet<Employee>> GetMockDbSet()
        {
            var employees = new List<Employee>
                {
                    new Employee
                    {
                        EmployeeId = 1987,
                        EmployeeName = "John Doe",
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
                    },
                    new Employee
                    {
                        EmployeeId = 2234,
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
                        IsDelete = false,
                        Role = new Role { RoleId = 1, RoleName = "Manager" }
                    },
                    new Employee
                    {
                        EmployeeId = 1234,
                        EmployeeName = "John Doe45",
                        Gender = true,
                        Phone = "1234567899",
                        Gmail = "john.doe45@example.com",
                        BirthDate = new DateTime(1990, 1, 1),
                        RoleId = 2,
                        HomeAddress = "123 Main St, Hometown",
                        WorkAddress = "456 Work St, Worktown",
                        Image = "path/to/image.jpg",
                        ManagerId = 2,
                        OwnerId = 3,
                        IsDelete = false,
                        Role = new Role { RoleId = 2, RoleName = "Staff" }
                    }
                }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Employee>>();

            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(employees.Provider);
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());

            mockDbSet.As<IAsyncEnumerable<Employee>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Employee>(employees.GetEnumerator()));

            return mockDbSet;
        }

        internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;

            public TestAsyncEnumerator(IEnumerator<T> inner)
            {
                _inner = inner ?? throw new ArgumentNullException(nameof(inner));
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
}
