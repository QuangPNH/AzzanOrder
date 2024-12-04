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
using AzzanOrder.Data.Test;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using AzzanOrder.Data.Test;

namespace AzzanOrder.Tests
{
	[TestFixture]
	public class FeedbackControllerTests
	{
		private static Mock<OrderingAssistSystemContext> _mockContext;
		private static Mock<DbSet<Feedback>> _mockSet;
		private FeedbackController _controller;

		[OneTimeSetUp]
		public static void setup()
		{
			_mockContext = FeedbackControllerTestSetup.GetMockContext();
			_mockSet = FeedbackControllerTestSetup.GetMockDbSet();
		}

		[SetUp]
		public void Setup()
		{
			_controller = new FeedbackController(_mockContext.Object);
		}

		[Test]
		public async Task GetFeedbacks_ReturnsAllFeedbacks()
		{
			// Act
			var result = await _controller.GetFeedbacks();

			// Assert
			Assert.IsInstanceOf<ActionResult<IEnumerable<Feedback>>>(result);

			var feedbacks = result.Value as List<Feedback>;
			Assert.IsNotNull(feedbacks);
			Assert.AreEqual(3, feedbacks.Count);
		}
	}
}
