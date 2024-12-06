using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using AzzanOrder.Data.Controllers;
using AzzanOrder.Data.Models;
using AzzanOrder.Data.DTO;
using AzzanOrder.Data.Test;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System;

namespace AzzanOrder.Tests
{
    [TestFixture]
    public class VouchersControllerTests
    {
        private Mock<OrderingAssistSystemContext> _mockContext = null!;
        private VouchersController _controller = null!;

        [SetUp]
        public void SetupEach()
        {
            _mockContext = VouchersControllerTestSetup.GetMockContext();
            _controller = new VouchersController(_mockContext.Object);
        }

        [Test]
        public async Task GetAllVoucher_ReturnsAllVouchers()
        {
            // Act
            var result = await _controller.GetAllVoucher();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var vouchers = okResult.Value as List<Voucher>;
            Assert.IsNotNull(vouchers);
            Assert.AreEqual(3, vouchers.Count);
        }

        [Test]
        public async Task CheckVoucher_ReturnsTrue_WhenVoucherExists()
        {
            // Arrange
            var voucherDetailId = 1;
            var menuItemId = 1;

            // Act
            var result = await _controller.CheckVoucher(voucherDetailId, menuItemId, null);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var check = (bool)okResult.Value!;
            Assert.IsTrue(check);
        }

        [Test]
        public async Task CheckVoucher_ReturnsFalse_WhenVoucherDoesNotExist()
        {
            // Arrange
            var voucherDetailId = 99;
            var menuItemId = 99;

            // Act
            var result = await _controller.CheckVoucher(voucherDetailId, menuItemId, null);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var check = (bool)okResult.Value!;
            Assert.IsFalse(check);
        }
		[Test]
		public async Task GetVoucherByCategory_ReturnsVouchers_WhenCategoryExists()
		{
			// Arrange
			var categoryId = 1;

			// Act
			var result = await _controller.GetVoucherByCategory(categoryId);

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(result);
			var okResult = result as OkObjectResult;
			Assert.IsNotNull(okResult);


			if (okResult.Value is List<object> returnedVouchers && returnedVouchers.Count > 1)
			{
				Assert.IsInstanceOf<List<object>>(okResult.Value);
				Assert.AreEqual(2, returnedVouchers.Count);
			}
			else
			{
				Assert.IsInstanceOf<object>(okResult.Value);
			}
		}

		[Test]
		public async Task GetVoucherByCategory_ReturnsNotFound_WhenCategoryDoesNotExist()
		{
			// Arrange
			var categoryId = 99;

			// Act
			var result = await _controller.GetVoucherByCategory(categoryId);

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(result);
			var okResult = result as OkObjectResult;
			Assert.IsNotNull(okResult);
			if (okResult.Value is List<object> returnedVouchers && returnedVouchers.Count > 1)
			{
				Assert.IsInstanceOf<List<object>>(okResult.Value);
				Assert.AreEqual(2, returnedVouchers.Count);
			}
			else
			{
				Assert.IsInstanceOf<object>(okResult.Value);
			}
		}




		[Test]
		public async Task PutVoucher_UpdatesVoucher_WhenVoucherExists()
		{
			// Arrange
			var voucher = new VoucherDTO { VoucherDetailId = 1, ItemCategoryId = 1, IsActive = true };

			var mockDbSet = _mockContext.Object.Vouchers;
			Mock.Get(mockDbSet)
				.Setup(m => m.FindAsync(It.IsAny<object[]>()))
				.ReturnsAsync((object[] ids) => new Voucher { VoucherDetailId = (int)ids[0], ItemCategoryId = (int)ids[1] });

			// Act
			var result = await _controller.PutVoucher(voucher);

			// Assert
			Assert.IsInstanceOf<OkObjectResult>(result);
			_mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Test]
		public async Task PutVoucher_ReturnsNotFound_WhenVoucherDoesNotExist()
		{
			// Arrange
			var voucher = new VoucherDTO { VoucherDetailId = 99, ItemCategoryId = 99, IsActive = true };

			var mockDbSet = _mockContext.Object.Vouchers;
			Mock.Get(mockDbSet)
				.Setup(m => m.FindAsync(It.IsAny<object[]>()))
				.ReturnsAsync((object[] ids) => null);

			// Act
			var result = await _controller.PutVoucher(voucher);

			// Assert
			Assert.IsInstanceOf<NotFoundObjectResult>(result);
		}

		[Test]
        public async Task PostVoucher_AddsVoucher()
        {
            // Arrange
            var voucher = new VoucherDTO { VoucherDetailId = 4, ItemCategoryId = 4, IsActive = true };

            var mockDbSet = _mockContext.Object.Vouchers;
            Mock.Get(mockDbSet).Setup(m => m.AddAsync(It.IsAny<Voucher>(), It.IsAny<CancellationToken>())).ReturnsAsync((EntityEntry<Voucher>)null!);

            // Act
            var result = await _controller.PostVoucher(voucher);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task PostVoucher_ReturnsProblem_WhenVoucherExists()
        {
            // Arrange
            var voucher = new VoucherDTO { VoucherDetailId = 1, ItemCategoryId = 1, IsActive = true };

            // Act
            var result = await _controller.PostVoucher(voucher);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(500, objectResult?.StatusCode);
        }

        [Test]
        public async Task DeleteVoucher_DeletesVoucher_WhenVoucherExists()
        {
            // Arrange
            var voucherDetailId = 1;
            var voucher = new Voucher { VoucherDetailId = voucherDetailId, ItemCategoryId = 1, IsActive = true };

            var mockDbSet = _mockContext.Object.Vouchers;
            Mock.Get(mockDbSet).Setup(m => m.FindAsync(voucherDetailId)).ReturnsAsync(voucher);

            // Act
            var result = await _controller.DeleteVoucher(voucherDetailId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task DeleteVoucher_ReturnsNotFound_WhenVoucherDoesNotExist()
        {
            // Arrange
            var voucherDetailId = 99;

            var mockDbSet = _mockContext.Object.Vouchers;
            Mock.Get(mockDbSet).Setup(m => m.FindAsync(voucherDetailId)).ReturnsAsync(default(Voucher));

            // Act
            var result = await _controller.DeleteVoucher(voucherDetailId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
