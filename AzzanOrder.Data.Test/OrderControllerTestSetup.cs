using AzzanOrder.Data.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzzanOrder.Data.Test
{
    internal class OrderControllerTestSetup
    {
        internal static Mock<OrderingAssistSystemContext> GetMockContext()
        {
            var mockContext = new Mock<OrderingAssistSystemContext>();
            var mockDbSet = GetMockDbSet();

            mockContext.Setup(c => c.Orders).Returns(mockDbSet.Object);

            return mockContext;
        }

        internal static Mock<DbSet<Order>> GetMockDbSet()
        {
            var orders = new List<Order>
            {
                new Order
                {
                    OrderId = 1,
                    OrderDate = new DateTime(2023, 1, 1),
                    TableId = 1,
                    Cost = 100.0,
                    Tax = 10.0,
                    MemberId = 1,
                    Status = true,
                    Member = new Member
                    {
                        MemberId = 1,
                        MemberName = "John Doe",
                        Gender = true,
                        Phone = "123-456-7890",
                        Gmail = "john.doe@example.com",
                        BirthDate = new DateTime(1990, 1, 1),
                        Address = "123 Main St",
                        Point = 100.0,
                        Image = "john_doe.jpg",
                        IsDelete = false
                    },
                    Table = new Table
                    {
                        TableId = 1,
                        Qr = "QR1",
                        Status = true,
                        EmployeeId = 1
                    }
                },
                new Order
                {
                    OrderId = 2,
                    OrderDate = new DateTime(2023, 2, 1),
                    TableId = 2,
                    Cost = 200.0,
                    Tax = 20.0,
                    MemberId = 2,
                    Status = false,
                    Member = new Member
                    {
                        MemberId = 2,
                        MemberName = "Jane Doe",
                        Gender = false,
                        Phone = "987-654-3210",
                        Gmail = "jane.doe@example.com",
                        BirthDate = new DateTime(1992, 2, 2),
                        Address = "456 Elm St",
                        Point = 200.0,
                        Image = "jane_doe.jpg",
                        IsDelete = false
                    },
                    Table = new Table
                    {
                        TableId = 2,
                        Qr = "QR2",
                        Status = false,
                        EmployeeId = 2
                    }
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Order>>();

            mockDbSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(orders.Provider);
            mockDbSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(orders.Expression);
            mockDbSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(orders.ElementType);
            mockDbSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(orders.GetEnumerator());
            mockDbSet.As<IAsyncEnumerable<Order>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Order>(orders.GetEnumerator()));

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
