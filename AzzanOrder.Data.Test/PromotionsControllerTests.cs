using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzzanOrder.Data.Controllers;
using AzzanOrder.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzzanOrder.Tests.Controllers
{
    [TestFixture]
    public class PromotionsControllerTests
    {
        private Mock<OrderingAssistSystemContext> _mockContext;
        private PromotionsController _controller;
        private List<Promotion> _promotions;

        [SetUp]
        public void Setup()
        {
            _promotions = new List<Promotion>
            {
                new Promotion { PromotionId = 1, EmployeeId = 1, IsActive = true },
                new Promotion { PromotionId = 2, EmployeeId = 1, IsActive = true },
                new Promotion { PromotionId = 3, EmployeeId = 2, IsActive = false }
            };

            var mockSet = new Mock<DbSet<Promotion>>();
            mockSet.As<IQueryable<Promotion>>().Setup(m => m.Provider).Returns(_promotions.AsQueryable().Provider);
            mockSet.As<IQueryable<Promotion>>().Setup(m => m.Expression).Returns(_promotions.AsQueryable().Expression);
            mockSet.As<IQueryable<Promotion>>().Setup(m => m.ElementType).Returns(_promotions.AsQueryable().ElementType);
            mockSet.As<IQueryable<Promotion>>().Setup(m => m.GetEnumerator()).Returns(_promotions.AsQueryable().GetEnumerator());

            _mockContext = new Mock<OrderingAssistSystemContext>();
            _mockContext.Setup(c => c.Promotions).Returns(mockSet.Object);

            _controller = new PromotionsController(_mockContext.Object);
        }

        [Test]
        public async Task GetPromotionsByEmpId_ReturnsPromotions()
        {
            var result = await _controller.GetPromotionsByEmpId(1);
            Assert.IsInstanceOf<ActionResult<IEnumerable<Promotion>>>(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var promotions = okResult.Value as IEnumerable<Promotion>;
            Assert.AreEqual(2, promotions.Count());
        }

        [Test]
        public async Task GetPromotion_ReturnsPromotion()
        {
            var result = await _controller.GetPromotion(1);
            Assert.IsInstanceOf<ActionResult<Promotion>>(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var promotion = okResult.Value as Promotion;
            Assert.AreEqual(1, promotion.PromotionId);
        }

        [Test]
        public async Task PutPromotion_UpdatesPromotion()
        {
            var promotion = new Promotion { PromotionId = 1, EmployeeId = 1, IsActive = true };
            var result = await _controller.PutPromotion(promotion);
            Assert.IsInstanceOf<IActionResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var updatedPromotion = okResult.Value as Promotion;
            Assert.AreEqual(promotion.PromotionId, updatedPromotion.PromotionId);
        }

        [Test]
        public async Task PostPromotion_AddsPromotion()
        {
            var promotion = new Promotion { PromotionId = 4, EmployeeId = 3, IsActive = true };
            var result = await _controller.PostPromotion(promotion);
            Assert.IsInstanceOf<ActionResult<Promotion>>(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var addedPromotion = okResult.Value as Promotion;
            Assert.AreEqual(promotion.PromotionId, addedPromotion.PromotionId);
        }

        [Test]
        public async Task DeletePromotion_SetsPromotionInactive()
        {
            var result = await _controller.DeletePromotion(1);
            Assert.IsInstanceOf<IActionResult>(result);
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            var promotion = _promotions.FirstOrDefault(p => p.PromotionId == 1);
            Assert.IsFalse(promotion.IsActive);
        }
    }
}
