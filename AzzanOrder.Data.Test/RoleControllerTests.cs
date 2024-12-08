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
    public class RoleControllerTests
    {
        private Mock<OrderingAssistSystemContext> _mockContext;
        private RoleController _controller;
        private List<Role> _roles;

        [SetUp]
        public void Setup()
        {
            _roles = new List<Role>
            {
                new Role { RoleId = 1 },
                new Role { RoleId = 2 }
            };

            var mockSet = new Mock<DbSet<Role>>();
            mockSet.As<IQueryable<Role>>().Setup(m => m.Provider).Returns(_roles.AsQueryable().Provider);
            mockSet.As<IQueryable<Role>>().Setup(m => m.Expression).Returns(_roles.AsQueryable().Expression);
            mockSet.As<IQueryable<Role>>().Setup(m => m.ElementType).Returns(_roles.AsQueryable().ElementType);
            mockSet.As<IQueryable<Role>>().Setup(m => m.GetEnumerator()).Returns(_roles.AsQueryable().GetEnumerator());

            _mockContext = new Mock<OrderingAssistSystemContext>();
            _mockContext.Setup(c => c.Roles).Returns(mockSet.Object);

            _controller = new RoleController(_mockContext.Object);
        }

        [Test]
        public async Task GetRoles_ReturnsAllRoles()
        {
            var result = await _controller.GetRoles();
            var okResult = result.Result as OkObjectResult;
            var roles = okResult.Value as List<Role>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(2, roles.Count);
        }

        [Test]
        public async Task GetRole_ReturnsRole_WhenRoleExists()
        {
            var result = await _controller.GetRole(1);
            var okResult = result.Result as OkObjectResult;
            var role = okResult.Value as Role;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, role.RoleId);
        }

        [Test]
        public async Task GetRole_ReturnsNotFound_WhenRoleDoesNotExist()
        {
            var result = await _controller.GetRole(3);

            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task PutRole_UpdatesRole_WhenRoleExists()
        {
            var role = new Role { RoleId = 1 };
            var result = await _controller.PutRole(role);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task PostRole_AddsRole()
        {
            var role = new Role { RoleId = 3 };
            var result = await _controller.PostRole(role);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task DeleteRole_DeletesRole_WhenRoleExists()
        {
            var result = await _controller.DeleteRole(1);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task DeleteRole_ReturnsNotFound_WhenRoleDoesNotExist()
        {
            var result = await _controller.DeleteRole(3);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
