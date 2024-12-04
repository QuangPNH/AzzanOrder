using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using AzzanOrder.Data.Controllers;
using AzzanOrder.Data.Models;
using AzzanOrder.Data.Test;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System;

namespace AzzanOrder.Tests
{
	[TestFixture]
	public class FeedbackControllerTests
	{
		private Mock<OrderingAssistSystemContext> _mockContext = null!;
		private FeedbackController _controller = null!;

		[SetUp]
		public void SetupEach()
		{
			_mockContext = FeedbackControllerTestSetup.GetMockContext();
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
			Assert.AreEqual(3, feedbacks.Count());
		}

		[Test]
		public async Task GetFeedback_ReturnsFeedback_WhenFeedbackExists()
		{
			// Arrange
			var feedbackId = 1;
			var feedback = new Feedback { FeedbackId = feedbackId, MemberId = 1 };
			var mockDbSet = FeedbackControllerTestSetup.GetMockDbSetWithFeedbacks(new List<Feedback> { feedback });

			_mockContext.Setup(c => c.Feedbacks).Returns(mockDbSet.Object);

			// Act
			var result = await _controller.GetFeedback(feedbackId);

			// Assert
			Assert.IsInstanceOf<ActionResult<Feedback>>(result);
			Assert.IsNotNull(result.Value);
			Assert.AreEqual(feedbackId, result.Value?.FeedbackId);
		}

		[Test]
		public async Task GetFeedback_ReturnsNotFound_WhenFeedbackDoesNotExist()
		{
			// Arrange
			var feedbackId = 99;
			var mockDbSet = FeedbackControllerTestSetup.GetMockDbSetWithFeedbacks(new List<Feedback>());

			_mockContext.Setup(c => c.Feedbacks).Returns(mockDbSet.Object);

			// Act
			var result = await _controller.GetFeedback(feedbackId);

			// Assert
			Assert.IsInstanceOf<ActionResult<Feedback>>(result);
			Assert.IsInstanceOf<NotFoundResult>(result.Result);
		}

		[Test]
		public async Task GetFeedbackByMember_ReturnsFeedback_WhenFeedbackExists()
		{
			// Arrange
			var memberId = 1;
			var feedback = new Feedback { FeedbackId = 1, MemberId = memberId };
			var mockDbSet = FeedbackControllerTestSetup.GetMockDbSetWithFeedbacks(new List<Feedback> { feedback });

			_mockContext.Setup(c => c.Feedbacks).Returns(mockDbSet.Object);

			// Act
			var result = await _controller.GetFeedbackByMember(memberId);

			// Assert
			Assert.IsInstanceOf<ActionResult<Feedback>>(result);
			Assert.IsNotNull(result.Value);
			Assert.AreEqual(memberId, result.Value?.MemberId);
		}

		[Test]
		public async Task GetFeedbackByMember_ReturnsNotFound_WhenFeedbackDoesNotExist()
		{
			// Arrange
			var memberId = 99;
			var mockDbSet = FeedbackControllerTestSetup.GetMockDbSetWithFeedbacks(new List<Feedback>());

			_mockContext.Setup(c => c.Feedbacks).Returns(mockDbSet.Object);

			// Act
			var result = await _controller.GetFeedbackByMember(memberId);

			// Assert
			Assert.IsInstanceOf<ActionResult<Feedback>>(result);
			Assert.IsInstanceOf<NotFoundResult>(result.Result);
		}

		[Test]
		public async Task PutFeedback_UpdatesFeedback_WhenFeedbackExists()
		{
			// Arrange
			var feedback = new Feedback { FeedbackId = 1, MemberId = 1, Content = "Updated Content" };
			var feedbacks = new List<Feedback>
			{
				new Feedback { FeedbackId = 1, MemberId = 1, Content = "Good" }
			};
			var mockDbSet = FeedbackControllerTestSetup.GetMockDbSetWithFeedbacks(feedbacks);

			mockDbSet.Setup(m => m.FindAsync(feedback.FeedbackId)).ReturnsAsync(feedback);
			_mockContext.Setup(c => c.Feedbacks).Returns(mockDbSet.Object);
			_mockContext.Setup(c => c.Feedbacks.Any(e => e.FeedbackId == feedback.FeedbackId)).Returns(true);

			// Act
			var result = await _controller.PutFeedback(feedback);

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(result);
			_mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Test]
		public async Task PutFeedback_ReturnsNotFound_WhenFeedbackDoesNotExist()
		{
			// Arrange
			var feedback = new Feedback { FeedbackId = 99, MemberId = 1, Content = "Updated Content" };
			var feedbacks = new List<Feedback>
			{
				new Feedback { FeedbackId = 1, MemberId = 1, Content = "Good" }
			};
			var mockDbSet = FeedbackControllerTestSetup.GetMockDbSetWithFeedbacks(feedbacks);

			mockDbSet.Setup(m => m.FindAsync(feedback.FeedbackId)).ReturnsAsync(default(Feedback));
			_mockContext.Setup(c => c.Feedbacks).Returns(mockDbSet.Object);

			// Act
			var result = await _controller.PutFeedback(feedback);

			// Assert
			Assert.IsInstanceOf<NotFoundObjectResult>(result);
		}

		[Test]
		public async Task PostFeedback_AddsFeedback()
		{
			// Arrange
			var feedback = new Feedback { FeedbackId = 4, MemberId = 4, Content = "New Feedback" };
			var feedbacks = new List<Feedback>
			{
				new Feedback { FeedbackId = 1, MemberId = 1, Content = "Good" }
			};
			var mockDbSet = FeedbackControllerTestSetup.GetMockDbSetWithFeedbacks(feedbacks);

			mockDbSet.Setup(m => m.AddAsync(feedback, It.IsAny<CancellationToken>())).ReturnsAsync((EntityEntry<Feedback>)null!);
			_mockContext.Setup(c => c.Feedbacks).Returns(mockDbSet.Object);

			// Act
			var result = await _controller.PostFeedback(feedback);

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(result.Result);
			_mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Test]
		public async Task PostFeedback_ReturnsBadRequest_WhenFeedbackExists()
		{
			// Arrange
			var feedback = new Feedback { FeedbackId = 1, MemberId = 1, Content = "Duplicate Feedback" };
			var feedbacks = new List<Feedback>
			{
				new Feedback { FeedbackId = 1, MemberId = 1, Content = "Good" }
			};
			var mockDbSet = FeedbackControllerTestSetup.GetMockDbSetWithFeedbacks(feedbacks);

			_mockContext.Setup(c => c.Feedbacks).Returns(mockDbSet.Object);

			// Act
			var result = await _controller.PostFeedback(feedback);

			// Assert
			Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
		}

		[Test]
		public async Task DeleteFeedback_DeletesFeedback_WhenFeedbackExists()
		{
			// Arrange
			var feedbackId = 1;
			var feedback = new Feedback { FeedbackId = feedbackId, MemberId = 1 };
			var mockDbSet = FeedbackControllerTestSetup.GetMockDbSetWithFeedbacks(new List<Feedback> { feedback });

			mockDbSet.Setup(m => m.FindAsync(feedbackId)).ReturnsAsync(feedback);
			_mockContext.Setup(c => c.Feedbacks).Returns(mockDbSet.Object);

			// Act
			var result = await _controller.DeleteFeedback(feedbackId);

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(result);
			_mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Test]
		public async Task DeleteFeedback_ReturnsNotFound_WhenFeedbackDoesNotExist()
		{
			// Arrange
			var feedbackId = 99;
			var mockDbSet = FeedbackControllerTestSetup.GetMockDbSetWithFeedbacks(new List<Feedback>());

			mockDbSet.Setup(m => m.FindAsync(feedbackId)).ReturnsAsync(default(Feedback));
			_mockContext.Setup(c => c.Feedbacks).Returns(mockDbSet.Object);

			// Act
			var result = await _controller.DeleteFeedback(feedbackId);

			// Assert
			Assert.IsInstanceOf<NotFoundResult>(result);
		}
	}
}
