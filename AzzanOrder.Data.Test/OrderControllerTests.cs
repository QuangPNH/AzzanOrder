using AzzanOrder.Data.Controllers;
using AzzanOrder.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzzanOrder.Data.Test
{
    [TestFixture]
    internal class OrderControllerTests
    {
        private static Mock<OrderingAssistSystemContext> _mockContext;
        private OrderController _controller;

        [OneTimeSetUp]
        public static void SetupFirst()
        {
            _mockContext = OrderControllerTestSetup.GetMockContext();
        }

        [SetUp]
        public void Setup()
        {
            _controller = new OrderController(_mockContext.Object);
        }

        [Test]
        public async Task GetOrders_ReturnsAllOrders()
        {
            // Act
            var result = await _controller.GetOrders();

            // Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<Order>>>(result);

            var orders = result.Value as List<Order>;
            Assert.IsNotNull(orders);
            Assert.AreEqual(2, orders.Count);
        }

		[Test]
		public async Task GetOrder_ReturnsOrder()
		{
			// Arrange
			int orderId = 1; // Use an existing order ID from your mock data

			// Act
			var result = await _controller.GetOrder(orderId);

			// Assert
			Assert.IsInstanceOf<ActionResult<Order>>(result);

			var order = result.Value;
			Assert.IsNotNull(order);
			Assert.AreEqual(orderId, order.OrderId);
		}

		[Test]
        public async Task GetOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Act
            var result = await _controller.GetOrder(99);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

		[Test]
		public async Task PostOrder_CreatesNewOrder()
		{
			// Arrange
			var newOrder = new Order
			{
				OrderId = 3,
				OrderDate = new DateTime(2023, 3, 1),
				TableId = 3,
				Cost = 300.0,
				Tax = 30.0,
				MemberId = 3,
				Status = true,
				OrderDetails = new List<OrderDetail>
		{
			new OrderDetail { OrderId = 3, MenuItemId = 1, Quantity = 1 }
		}
			};

			// Act
			var result = await _controller.PostOrder(newOrder);

			// Assert
			Assert.IsInstanceOf<ActionResult<Order>>(result);
			var okObjectResult = result.Result as OkObjectResult;
			Assert.IsNotNull(okObjectResult);
			var order = okObjectResult.Value as Order;
			Assert.IsNotNull(order);
			Assert.AreEqual(3, order.OrderId);
		}


		[Test]
        public async Task DeleteOrder_RemovesOrder()
        {
            // Act
            var result = await _controller.DeleteOrder(1);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task DeleteOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Act
            var result = await _controller.DeleteOrder(99);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
