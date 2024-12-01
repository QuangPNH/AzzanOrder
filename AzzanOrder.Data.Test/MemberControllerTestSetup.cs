using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using AzzanOrder.Data.Models;
using System;

namespace AzzanOrder.Data.Test
{
    internal static class MemberControllerTestSetup
    {
        public static Mock<OrderingAssistSystemContext> GetMockContext()
        {
            var mockContext = new Mock<OrderingAssistSystemContext>();
            var mockSet = GetMockDbSet();
            mockContext.Setup(c => c.Members).Returns(mockSet.Object);
            return mockContext;
        }

        public static Mock<DbSet<Member>> GetMockDbSet()
        {
            var members = new List<Member>
            {
                new Member
                {
                    MemberId = 1231,
                    MemberName = "John Doe",
                    Gender = true,
                    Phone = "1234567890",
                    Gmail = "john.doe@example.com",
                    BirthDate = new DateTime(1990, 1, 1),
                    Address = "123 Main St",
                    Point = 100.5,
                    Image = "john_doe.png",
                    IsDelete = false
                },
                new Member
                {
                    MemberId = 2312,
                    MemberName = "Jane Smith",
                    Gender = false,
                    Phone = "0987654321",
                    Gmail = "jane.smith@example.com",
                    BirthDate = new DateTime(1985, 5, 15),
                    Address = "456 Elm St",
                    Point = 200.0,
                    Image = "jane_smith.png",
                    IsDelete = false
                },
                new Member
                {
                    MemberId = 3123,
                    MemberName = "Alice Johnson",
                    Gender = false,
                    Phone = "1122334455",
                    Gmail = "alice.johnson@example.com",
                    BirthDate = new DateTime(1992, 12, 25),
                    Address = "789 Oak St",
                    Point = 150.75,
                    Image = "alice_johnson.png",
                    IsDelete = false
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Member>>();
            mockSet.As<IQueryable<Member>>().Setup(m => m.Provider).Returns(members.Provider);
            mockSet.As<IQueryable<Member>>().Setup(m => m.Expression).Returns(members.Expression);
            mockSet.As<IQueryable<Member>>().Setup(m => m.ElementType).Returns(members.ElementType);
            mockSet.As<IQueryable<Member>>().Setup(m => m.GetEnumerator()).Returns(members.GetEnumerator());

            mockSet.As<IAsyncEnumerable<Member>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<System.Threading.CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Member>(members.GetEnumerator()));

            return mockSet;
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
