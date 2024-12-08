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
    public class OwnerControllerTests
    {
        private Mock<OrderingAssistSystemContext> _mockContext;
        private OwnerController _controller;
        private List<Owner> _owners;

        [SetUp]
        public void Setup()
        {
            _owners = new List<Owner>
            {
                new Owner { OwnerId = 1, OwnerName = "Owner1", Phone = "0123456789", IsDelete = false },
                new Owner { OwnerId = 2, OwnerName = "Owner2", Phone = "0987654321", IsDelete = false }
            };

            var mockSet = new Mock<DbSet<Owner>>();
            mockSet.As<IQueryable<Owner>>().Setup(m => m.Provider).Returns(_owners.AsQueryable().Provider);
            mockSet.As<IQueryable<Owner>>().Setup(m => m.Expression).Returns(_owners.AsQueryable().Expression);
            mockSet.As<IQueryable<Owner>>().Setup(m => m.ElementType).Returns(_owners.AsQueryable().ElementType);
            mockSet.As<IQueryable<Owner>>().Setup(m => m.GetEnumerator()).Returns(_owners.AsQueryable().GetEnumerator());

            _mockContext = new Mock<OrderingAssistSystemContext>();
            _mockContext.Setup(c => c.Owners).Returns(mockSet.Object);

            _controller = new OwnerController(_mockContext.Object);
        }

        [Test]
        public async Task GetOwners_ReturnsAllOwners()
        {
            var result = await _controller.GetOwners();
            Assert.IsInstanceOf<ActionResult<IEnumerable<Owner>>>(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var owners = okResult.Value as IEnumerable<Owner>;
            Assert.AreEqual(2, owners.Count());
        }

        [Test]
        public async Task GetOwner_ReturnsOwnerById()
        {
            var result = await _controller.GetOwner(1);
            Assert.IsInstanceOf<ActionResult<Owner>>(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var owner = okResult.Value as Owner;
            Assert.AreEqual(1, owner.OwnerId);
        }

        [Test]
        public async Task GetOwnerByPhone_ReturnsOwnerByPhone()
        {
            var result = await _controller.GetOwnerByPhone("0123456789");
            Assert.IsInstanceOf<ActionResult<Owner>>(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var owner = okResult.Value as Owner;
            Assert.AreEqual("0123456789", owner.Phone);
        }

        [Test]
        public async Task PostOwner_AddsNewOwner()
        {
            var newOwner = new Owner { OwnerId = 3, OwnerName = "Owner3", Phone = "0112233445", IsDelete = false };
            var result = await _controller.PostOwner(newOwner);
            Assert.IsInstanceOf<ActionResult<Owner>>(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var owner = okResult.Value as Owner;
            Assert.AreEqual(3, owner.OwnerId);
        }

        [Test]
        public async Task DeleteOwner_SetsIsDeleteToTrue()
        {
            var result = await _controller.DeleteOwner(1);
            Assert.IsInstanceOf<IActionResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual("Delete success", okResult.Value);
            Assert.IsTrue(_owners.First(o => o.OwnerId == 1).IsDelete);
        }
    }
}
