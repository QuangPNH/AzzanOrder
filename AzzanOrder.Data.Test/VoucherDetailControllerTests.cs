using AzzanOrder.Data.Controllers;
using AzzanOrder.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzzanOrder.Tests.Controllers
{
    [TestFixture]
    public class VoucherDetailControllerTests
    {
        private Mock<OrderingAssistSystemContext> _mockContext;
        private VoucherDetailController _controller;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<OrderingAssistSystemContext>();
            _controller = new VoucherDetailController(_mockContext.Object);
        }

        [Test]
        public async Task GetVoucherDetails_ReturnsOkResult_WithVoucherDetails()
        {
            // Arrange
            var voucherDetails = new List<VoucherDetail>
            {
                new VoucherDetail { VoucherDetailId = 1, EmployeeId = 1 },
                new VoucherDetail { VoucherDetailId = 2, EmployeeId = 2 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<VoucherDetail>>();
            mockSet.As<IQueryable<VoucherDetail>>().Setup(m => m.Provider).Returns(voucherDetails.Provider);
            mockSet.As<IQueryable<VoucherDetail>>().Setup(m => m.Expression).Returns(voucherDetails.Expression);
            mockSet.As<IQueryable<VoucherDetail>>().Setup(m => m.ElementType).Returns(voucherDetails.ElementType);
            mockSet.As<IQueryable<VoucherDetail>>().Setup(m => m.GetEnumerator()).Returns(voucherDetails.GetEnumerator());

            _mockContext.Setup(c => c.VoucherDetails).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetVoucherDetails(null);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOf<IEnumerable<VoucherDetail>>(okResult.Value);
        }

        [Test]
        public async Task GetVoucherDetail_ReturnsNotFound_WhenVoucherDetailDoesNotExist()
        {
            // Arrange
            var mockSet = new Mock<DbSet<VoucherDetail>>();
            _mockContext.Setup(c => c.VoucherDetails).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetVoucherDetail(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task PostVoucherDetail_AddsVoucherDetail()
        {
            // Arrange
            var voucherDetail = new VoucherDetail { VoucherDetailId = 1, EmployeeId = 1 };

            var mockSet = new Mock<DbSet<VoucherDetail>>();
            _mockContext.Setup(c => c.VoucherDetails).Returns(mockSet.Object);

            // Act
            var result = await _controller.PostVoucherDetail(voucherDetail);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<VoucherDetail>()), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task DeleteVoucherDetail_RemovesVoucherDetail()
        {
            // Arrange
            var voucherDetail = new VoucherDetail { VoucherDetailId = 1, EmployeeId = 1 };

            var mockSet = new Mock<DbSet<VoucherDetail>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(voucherDetail);
            _mockContext.Setup(c => c.VoucherDetails).Returns(mockSet.Object);

            // Act
            var result = await _controller.DeleteVoucherDetail(1);

            // Assert
            mockSet.Verify(m => m.Remove(It.IsAny<VoucherDetail>()), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
