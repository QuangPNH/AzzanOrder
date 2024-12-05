using AzzanOrder.Data.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AzzanOrder.Data.Test
{
	internal class BankControllerTestSetup
	{
		internal static Mock<OrderingAssistSystemContext> GetMockContext()
		{
			var mockContext = new Mock<OrderingAssistSystemContext>();
			var mockDbSet = GetMockDbSet();
			mockContext.Setup(c => c.Banks).Returns(mockDbSet.Object);
			return mockContext;
		}

		private static Mock<DbSet<Bank>> GetMockDbSet()
		{
			var banks = new List<Bank>
			{
				new Bank
				{
					BankId = 1,
					PAYOS_CLIENT_ID = "PAYOS_CLIENT_ID1",
					PAYOS_API_KEY = "PAYOS_API_KEY1",
					PAYOS_CHECKSUM_KEY = "PAYOS_CHECKSUM_KEY1",
					Owners =
					{
						new Owner
						{
							OwnerId = 1,
							OwnerName = "Owner1",
							Phone = "01245789",
							Gmail = "0137448@gmail.com"
						}
					}
				},
				new Bank {
					BankId = 2,
					PAYOS_CLIENT_ID = "PAYOS_CLIENT_ID2",
					PAYOS_API_KEY = "PAYOS_API_KEY2",
					PAYOS_CHECKSUM_KEY = "PAYOS_CHECKSUM_KEY2",
					Owners =
					{
						new Owner
						{
							OwnerId = 2,
							OwnerName = "Owner2",
							Phone = "0435456789",
							Gmail = "4328432@gmail.com"
						}
					}
				},
				new Bank {
					BankId = 3,
					PAYOS_CLIENT_ID = "PAYOS_CLIENT_ID3",
					PAYOS_API_KEY = "PAYOS_API_KEY3",
					PAYOS_CHECKSUM_KEY = "PAYOS_CHECKSUM_KEY3",
					Owners =
					{
						new Owner
						{
							OwnerId = 3,
							OwnerName = "Owner3",
							Phone = "0123456789",
							Gmail = "5895894@gmail.com"
						}
					}
				}
			}.AsQueryable();

			var mockDbSet = new Mock<DbSet<Bank>>();

			mockDbSet.As<IQueryable<Bank>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Bank>(banks.Provider));
			mockDbSet.As<IQueryable<Bank>>().Setup(m => m.Expression).Returns(banks.Expression);
			mockDbSet.As<IQueryable<Bank>>().Setup(m => m.ElementType).Returns(banks.ElementType);
			mockDbSet.As<IQueryable<Bank>>().Setup(m => m.GetEnumerator()).Returns(banks.GetEnumerator());

			mockDbSet.As<IAsyncEnumerable<Bank>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
				.Returns(new TestAsyncEnumerator<Bank>(banks.GetEnumerator()));

			mockDbSet.Setup(d => d.FindAsync(It.IsAny<object[]>()))
				.ReturnsAsync((object[] ids) => banks.FirstOrDefault(b => b.BankId == (int)ids[0]));

			mockDbSet.Setup(d => d.AddAsync(It.IsAny<Bank>(), It.IsAny<CancellationToken>()))
	.Callback<Bank, CancellationToken>((b, ct) => banks.ToList().Add(b))
	.ReturnsAsync((Bank b, CancellationToken ct) => (EntityEntry<Bank>)null);

			mockDbSet.Setup(d => d.Remove(It.IsAny<Bank>()))
				.Callback<Bank>(b => banks.ToList().Remove(b));

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

		internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
		{
			private readonly IQueryProvider _inner;

			internal TestAsyncQueryProvider(IQueryProvider inner)
			{
				_inner = inner;
			}

			public IQueryable CreateQuery(Expression expression)
			{
				return new TestAsyncEnumerable<TEntity>(expression);
			}

			public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
			{
				return new TestAsyncEnumerable<TElement>(expression);
			}

			public object Execute(Expression expression)
			{
				return _inner.Execute(expression);
			}

			public TResult Execute<TResult>(Expression expression)
			{
				return _inner.Execute<TResult>(expression);
			}

			public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
			{
				return new TestAsyncEnumerable<TResult>(expression);
			}

			public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
			{
				var executionResult = Execute<TResult>(expression);
				if (executionResult is Task<TResult> taskResult)
				{
					return taskResult.GetAwaiter().GetResult();
				}
				return executionResult;
			}
		}

		internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
		{
			public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable)
			{ }

			public TestAsyncEnumerable(Expression expression) : base(expression)
			{ }

			public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
			{
				return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
			}

			IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);
		}
	}
}
