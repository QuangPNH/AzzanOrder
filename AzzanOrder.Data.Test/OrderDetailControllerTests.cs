using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzzanOrder.Data.Controllers;
using AzzanOrder.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
namespace AzzanOrder.Tests.Controllers
{
    [TestFixture]
    public class OrderDetailControllerTests
    {
        private Mock<OrderingAssistSystemContext> _mockContext;
        private OrderDetailController _controller;
        private List<OrderDetail> _orderDetails;

        [SetUp]
        public void Setup()
        {
            _orderDetails = new List<OrderDetail>
            {
                new OrderDetail { OrderDetailId = 1, MenuItemId = 1, OrderId = 1 },
                new OrderDetail { OrderDetailId = 2, MenuItemId = 2, OrderId = 2 }
            };

            var mockSet = new Mock<DbSet<OrderDetail>>();
            mockSet.As<IQueryable<OrderDetail>>().Setup(m => m.Provider).Returns(_orderDetails.AsQueryable().Provider);
            mockSet.As<IQueryable<OrderDetail>>().Setup(m => m.Expression).Returns(_orderDetails.AsQueryable().Expression);
            mockSet.As<IQueryable<OrderDetail>>().Setup(m => m.ElementType).Returns(_orderDetails.AsQueryable().ElementType);
            mockSet.As<IQueryable<OrderDetail>>().Setup(m => m.GetEnumerator()).Returns(_orderDetails.AsQueryable().GetEnumerator());

            _mockContext = new Mock<OrderingAssistSystemContext>();
            _mockContext.Setup(c => c.OrderDetails).Returns(mockSet.Object);

            _controller = new OrderDetailController(_mockContext.Object);
        }

        [Test]
        public async Task GetOrderDetails_ReturnsAllOrderDetails()
        {
            var result = await _controller.GetOrderDetails();
            var okResult = result.Result as OkObjectResult;
            var items = okResult.Value as List<OrderDetail>;

            Assert.AreEqual(2, items.Count);
        }

        [Test]
        public async Task GetOrderDetail_ReturnsOrderDetail()
        {
            var result = await _controller.GetOrderDetail(1);
            var okResult = result.Result as OkObjectResult;
            var item = okResult.Value as OrderDetail;

            Assert.AreEqual(1, item.OrderDetailId);
        }

        [Test]
        public async Task GetOrderDetail_ReturnsNotFound()
        {
            var result = await _controller.GetOrderDetail(3);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task PostOrderDetail_AddsOrderDetail()
        {
            var newOrderDetail = new OrderDetail { OrderDetailId = 3, MenuItemId = 3, OrderId = 3 };
            var result = await _controller.PostOrderDetail(newOrderDetail);
            var okResult = result.Result as OkObjectResult;
            var item = okResult.Value as OrderDetail;

            Assert.AreEqual(3, item.OrderDetailId);
            _mockContext.Verify(m => m.Add(It.IsAny<OrderDetail>()), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public async Task DeleteOrderDetail_RemovesOrderDetail()
        {
            var result = await _controller.DeleteOrderDetail(1);
            Assert.IsInstanceOf<NoContentResult>(result);

            _mockContext.Verify(m => m.Remove(It.IsAny<OrderDetail>()), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
