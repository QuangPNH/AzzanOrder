using NUnit.Framework;
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
	public class VouchersControllerTestSetup
	{
		internal static Mock<OrderingAssistSystemContext> GetMockContext(List<Voucher>? vouchers = null)
		{
			var mockContext = new Mock<OrderingAssistSystemContext>();
			var mockDbSet = GetMockDbSet(vouchers);

			mockContext.Setup(c => c.Vouchers).Returns(mockDbSet.Object);
			return mockContext;
		}

		internal static Mock<DbSet<Voucher>> GetMockDbSet(List<Voucher>? vouchers = null)
		{
			vouchers ??= new List<Voucher>
			{
				new Voucher
				{
					VoucherDetailId = 1,
					ItemCategoryId = 1,
					IsActive = true,
					ItemCategory = new ItemCategory
					{
						ItemCategoryId = 1,
						ItemCategoryName = "Category1",
						MenuCategories = new List<MenuCategory>
						{
new MenuCategory { MenuItemId = 1, ItemCategoryId = 1, ItemCategory = new ItemCategory { ItemCategoryName = "Menu1" } }
						}
					},
					VoucherDetail = new VoucherDetail
					{
						VoucherDetailId = 1,
						EmployeeId = 1
					}
				},
				new Voucher
				{
					VoucherDetailId = 2,
					ItemCategoryId = 2,
					IsActive = true,
					ItemCategory = new ItemCategory
					{
						ItemCategoryId = 2,
						ItemCategoryName = "Category2",
						MenuCategories = new List<MenuCategory>
						{
new MenuCategory { MenuItemId = 2, ItemCategoryId = 2, ItemCategory = new ItemCategory { ItemCategoryName = "Menu2" } }
						}
					},
					VoucherDetail = new VoucherDetail
					{
						VoucherDetailId = 2,
						EmployeeId = 2
					}
				},
				new Voucher
				{
					VoucherDetailId = 3,
					ItemCategoryId = 3,
					IsActive = true,
					ItemCategory = new ItemCategory
					{
						ItemCategoryId = 3,
						ItemCategoryName = "Category3",
						MenuCategories = new List<MenuCategory>
						{
new MenuCategory { MenuItemId = 3, ItemCategoryId = 3, ItemCategory = new ItemCategory { ItemCategoryName = "Menu3" } }
						}
					},
					VoucherDetail = new VoucherDetail
					{
						VoucherDetailId = 3,
						EmployeeId = 3
					}
				}
			};

			var vouchersQueryable = vouchers.AsQueryable();

			var mockDbSet = new Mock<DbSet<Voucher>>();

			mockDbSet.As<IQueryable<Voucher>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Voucher>(vouchersQueryable.Provider));
			mockDbSet.As<IQueryable<Voucher>>().Setup(m => m.Expression).Returns(vouchersQueryable.Expression);
			mockDbSet.As<IQueryable<Voucher>>().Setup(m => m.ElementType).Returns(vouchersQueryable.ElementType);
			mockDbSet.As<IQueryable<Voucher>>().Setup(m => m.GetEnumerator()).Returns(vouchersQueryable.GetEnumerator());

			mockDbSet.As<IAsyncEnumerable<Voucher>>()
				.Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
				.Returns(new TestAsyncEnumerator<Voucher>(vouchersQueryable.GetEnumerator()));

			mockDbSet.Setup(m => m.AddAsync(It.IsAny<Voucher>(), It.IsAny<CancellationToken>()))
				.Callback<Voucher, CancellationToken>((v, ct) => vouchers.Add(v))
				.ReturnsAsync((EntityEntry<Voucher>)null!);

			mockDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
				.ReturnsAsync((object[] ids) => vouchers.FirstOrDefault(v => v.VoucherDetailId == (int)ids[0] && v.ItemCategoryId == (int)ids[1]));

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
