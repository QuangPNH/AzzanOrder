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

namespace AzzanOrder.Data.Test
{
    internal class ItemCategoryTestSetup
    {
        internal static Mock<OrderingAssistSystemContext> GetMockContext()
        {
            var mockContext = new Mock<OrderingAssistSystemContext>();
            var mockItemCategorySet = GetMockItemCategoryDbSet();
            var mockMenuCategorySet = GetMockMenuCategoryDbSet();
            var mockVoucherSet = GetMockVoucherDbSet();

            mockContext.Setup(c => c.ItemCategories).Returns(mockItemCategorySet.Object);
            mockContext.Setup(c => c.MenuCategories).Returns(mockMenuCategorySet.Object);
            mockContext.Setup(c => c.Vouchers).Returns(mockVoucherSet.Object);

            return mockContext;
        }
        internal static Mock<DbSet<ItemCategory>> GetMockItemCategoryDbSet()
        {
            var itemCategories = new List<ItemCategory>
    {
        new ItemCategory
        {
            ItemCategoryId = 1,
            ItemCategoryName = "Category 1",
            Description = "TOPPING",
            Discount = 10.0,
            Image = "image1.png",
            EmployeeId = 1,
            IsDelete = false,
            StartDate = DateTime.Now.AddDays(-10),
            EndDate = DateTime.Now.AddDays(1),
            IsCombo = true
        },
        new ItemCategory
        {
            ItemCategoryId = 2,
            ItemCategoryName = "Category 2",
            Description = "TOPPING",
            Discount = 20.0,
            Image = "image2.png",
            EmployeeId = 2,
            IsDelete = false,
            StartDate = DateTime.Now.AddDays(-20),
            EndDate = null,
            IsCombo = false
        }
    }.AsQueryable();

            var mockDbSet = new Mock<DbSet<ItemCategory>>();

            mockDbSet.As<IQueryable<ItemCategory>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<ItemCategory>(itemCategories.Provider));
            mockDbSet.As<IQueryable<ItemCategory>>().Setup(m => m.Expression).Returns(itemCategories.Expression);
            mockDbSet.As<IQueryable<ItemCategory>>().Setup(m => m.ElementType).Returns(itemCategories.ElementType);
            mockDbSet.As<IQueryable<ItemCategory>>().Setup(m => m.GetEnumerator()).Returns(itemCategories.GetEnumerator());

            mockDbSet.As<IAsyncEnumerable<ItemCategory>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<ItemCategory>(itemCategories.GetEnumerator()));

            return mockDbSet;
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


        internal static Mock<DbSet<MenuCategory>> GetMockMenuCategoryDbSet()
        {
            var menuCategories = new List<MenuCategory>
            {
                new MenuCategory
                {
                    MenuItemId = 1,
                    ItemCategoryId = 1,
                    ItemCategory = new ItemCategory
                    {
                        ItemCategoryId = 1,
                        ItemCategoryName = "Category 1",
                        Description = "Description 1",
                        Discount = 10.0,
                        Image = "image1.png",
                        EmployeeId = 1,
                        IsDelete = false,
                        StartDate = DateTime.Now.AddDays(-10),
                        EndDate = DateTime.Now.AddDays(1),
                        IsCombo = true
                    },
                    MenuItem = new MenuItem
                    {
                        MenuItemId = 1,
                        // Add other properties of MenuItem as needed
                    }
                },
                new MenuCategory
                {
                    MenuItemId = 2,
                    ItemCategoryId = 2,
                    ItemCategory = new ItemCategory
                    {
                        ItemCategoryId = 2,
                        ItemCategoryName = "Category 2",
                        Description = "Description 2",
                        Discount = 20.0,
                        Image = "image2.png",
                        EmployeeId = 2,
                        IsDelete = false,
                        StartDate = DateTime.Now.AddDays(-20),
                        EndDate = null,
                        IsCombo = false
                    },
                    MenuItem = new MenuItem
                    {
                        MenuItemId = 2,
                        // Add other properties of MenuItem as needed
                    }
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<MenuCategory>>();

            mockDbSet.As<IQueryable<MenuCategory>>().Setup(m => m.Provider).Returns(menuCategories.Provider);
            mockDbSet.As<IQueryable<MenuCategory>>().Setup(m => m.Expression).Returns(menuCategories.Expression);
            mockDbSet.As<IQueryable<MenuCategory>>().Setup(m => m.ElementType).Returns(menuCategories.ElementType);
            mockDbSet.As<IQueryable<MenuCategory>>().Setup(m => m.GetEnumerator()).Returns(menuCategories.GetEnumerator());

            mockDbSet.As<IAsyncEnumerable<MenuCategory>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<MenuCategory>(menuCategories.GetEnumerator()));

            return mockDbSet;
        }

        internal static Mock<DbSet<Voucher>> GetMockVoucherDbSet()
        {
            var vouchers = new List<Voucher>
            {
                new Voucher
                {
                    VoucherDetailId = 1,
                    ItemCategoryId = 1,
                    IsActive = true,
                    ItemCategory = new ItemCategory
                    {
                        ItemCategoryId = 1,
                        ItemCategoryName = "Category 1",
                        Description = "Description 1",
                        Discount = 10.0,
                        Image = "image1.png",
                        EmployeeId = 1,
                        IsDelete = false,
                        StartDate = DateTime.Now.AddDays(-10),
                        EndDate = DateTime.Now.AddDays(1),
                        IsCombo = true
                    },
                    VoucherDetail = new VoucherDetail
                    {
                        VoucherDetailId = 1,
                        // Add other properties of VoucherDetail as needed
                    }
                },
                new Voucher
                {
                    VoucherDetailId = 2,
                    ItemCategoryId = 2,
                    IsActive = false,
                    ItemCategory = new ItemCategory
                    {
                        ItemCategoryId = 2,
                        ItemCategoryName = "Category 2",
                        Description = "Description 2",
                        Discount = 20.0,
                        Image = "image2.png",
                        EmployeeId = 2,
                        IsDelete = false,
                        StartDate = DateTime.Now.AddDays(-20),
                        EndDate = null,
                        IsCombo = false
                    },
                    VoucherDetail = new VoucherDetail
                    {
                        VoucherDetailId = 2,
                        // Add other properties of VoucherDetail as needed
                    }
                }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Voucher>>();

            mockDbSet.As<IQueryable<Voucher>>().Setup(m => m.Provider).Returns(vouchers.Provider);
            mockDbSet.As<IQueryable<Voucher>>().Setup(m => m.Expression).Returns(vouchers.Expression);
            mockDbSet.As<IQueryable<Voucher>>().Setup(m => m.ElementType).Returns(vouchers.ElementType);
            mockDbSet.As<IQueryable<Voucher>>().Setup(m => m.GetEnumerator()).Returns(vouchers.GetEnumerator());

            mockDbSet.As<IAsyncEnumerable<Voucher>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Voucher>(vouchers.GetEnumerator()));

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
