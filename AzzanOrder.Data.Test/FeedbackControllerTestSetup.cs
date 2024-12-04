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

namespace AzzanOrder.Data.Test;

public class FeedbackControllerTestSetup
{
	internal static Mock<OrderingAssistSystemContext> GetMockContext()
	{
		var mockContext = new Mock<OrderingAssistSystemContext>();
		var mockDbSet = GetMockDbSet();

		mockContext.Setup(c => c.Feedbacks).Returns(mockDbSet.Object);
		return mockContext;
	}

	internal static Mock<DbSet<Feedback>> GetMockDbSet()
	{
		var feedbacks = new List<Feedback>
		{
			new Feedback
			{
				FeedbackId = 1,
				MemberId = 1,
				Content = "Good",
				Member = new Member
				{
					MemberId = 1,
					MemberName = "John Doe",
				}
			},
			new Feedback
			{
				FeedbackId = 2,
				MemberId = 2,
				Content = "Bad",
				Member = new Member
				{
					MemberId = 2,
					MemberName = "Jane Doe",
				}
			},
			new Feedback
			{
				FeedbackId = 3,
				MemberId = 3,
				Content = "Good",
				Member = new Member
				{
					MemberId = 3,
					MemberName = "John Smith",
				}
			}
		}.AsQueryable();

		var mockDbSet = new Mock<DbSet<Feedback>>();

		mockDbSet.As<IQueryable<Feedback>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Feedback>(feedbacks.Provider));
		mockDbSet.As<IQueryable<Feedback>>().Setup(m => m.Expression).Returns(feedbacks.Expression);
		mockDbSet.As<IQueryable<Feedback>>().Setup(m => m.ElementType).Returns(feedbacks.ElementType);
		mockDbSet.As<IQueryable<Feedback>>().Setup(m => m.GetEnumerator()).Returns(feedbacks.GetEnumerator());

		mockDbSet.As<IAsyncEnumerable<Feedback>>()
			.Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
			.Returns(new TestAsyncEnumerator<Feedback>(feedbacks.GetEnumerator()));

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
			return Execute<TResult>(expression);
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
