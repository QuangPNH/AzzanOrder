using AzzanOrder.Data.Controllers;
using AzzanOrder.Data.DTO;
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
    public class MenuItemControllerTests
    {
        private Mock<OrderingAssistSystemContext> _mockContext;
        private MenuItemController _controller;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<OrderingAssistSystemContext>();
            _controller = new MenuItemController(_mockContext.Object);
        }

        [Test]
        public async Task GetMenuItems_ReturnsAllMenuItems()
        {
            // Arrange
            var menuItems = new List<MenuItem>
            {
                new MenuItem { MenuItemId = 1, ItemName = "Item1" },
                new MenuItem { MenuItemId = 2, ItemName = "Item2" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<MenuItem>>();
            mockSet.As<IQueryable<MenuItem>>().Setup(m => m.Provider).Returns(menuItems.Provider);
            mockSet.As<IQueryable<MenuItem>>().Setup(m => m.Expression).Returns(menuItems.Expression);
            mockSet.As<IQueryable<MenuItem>>().Setup(m => m.ElementType).Returns(menuItems.ElementType);
            mockSet.As<IQueryable<MenuItem>>().Setup(m => m.GetEnumerator()).Returns(menuItems.GetEnumerator());

            _mockContext.Setup(c => c.MenuItems).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetMenuItems(null);

            // Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<MenuItem>>>(result);
            var actionResult = result.Result as OkObjectResult;
            Assert.IsNotNull(actionResult);
            var returnValue = actionResult.Value as IEnumerable<MenuItem>;
            Assert.AreEqual(2, returnValue.Count());
        }

        [Test]
        public async Task GetMenuItemByItemCategory_ReturnsMenuItems()
        {
            // Arrange
            var menuCategories = new List<MenuCategory>
            {
                new MenuCategory { MenuItemId = 1, ItemCategoryId = 1, MenuItem = new MenuItem { MenuItemId = 1, ItemName = "Item1", IsAvailable = true } },
                new MenuCategory { MenuItemId = 2, ItemCategoryId = 1, MenuItem = new MenuItem { MenuItemId = 2, ItemName = "Item2", IsAvailable = true } }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<MenuCategory>>();
            mockSet.As<IQueryable<MenuCategory>>().Setup(m => m.Provider).Returns(menuCategories.Provider);
            mockSet.As<IQueryable<MenuCategory>>().Setup(m => m.Expression).Returns(menuCategories.Expression);
            mockSet.As<IQueryable<MenuCategory>>().Setup(m => m.ElementType).Returns(menuCategories.ElementType);
            mockSet.As<IQueryable<MenuCategory>>().Setup(m => m.GetEnumerator()).Returns(menuCategories.GetEnumerator());

            _mockContext.Setup(c => c.MenuCategories).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetMenuItemByItemCategory(1);

            // Assert
            Assert.IsInstanceOf<ActionResult>(result);
            var actionResult = result as OkObjectResult;
            Assert.IsNotNull(actionResult);
            var returnValue = actionResult.Value as IEnumerable<MenuCategory>;
            Assert.AreEqual(2, returnValue.Count());
        }

        [Test]
        public async Task GetMenuItem_ReturnsMenuItem()
        {
            // Arrange
            var menuItem = new MenuItem { MenuItemId = 1, ItemName = "Item1" };
            var mockSet = new Mock<DbSet<MenuItem>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(menuItem);

            _mockContext.Setup(c => c.MenuItems).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetMenuItem(1);

            // Assert
            Assert.IsInstanceOf<ActionResult<MenuItem>>(result);
            var actionResult = result.Result as OkObjectResult;
            Assert.IsNotNull(actionResult);
            var returnValue = actionResult.Value as MenuItem;
            Assert.AreEqual(1, returnValue.MenuItemId);
        }

        [Test]
        public async Task PutMenuItem_UpdatesMenuItem()
        {
            // Arrange
            var menuItem = new MenuItem { MenuItemId = 1, ItemName = "Item1" };
            var mockSet = new Mock<DbSet<MenuItem>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(menuItem);

            _mockContext.Setup(c => c.MenuItems).Returns(mockSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _controller.PutMenuItem(menuItem);

            // Assert
            Assert.IsInstanceOf<IActionResult>(result);
            var actionResult = result as OkObjectResult;
            Assert.IsNotNull(actionResult);
            var returnValue = actionResult.Value as MenuItem;
            Assert.AreEqual(1, returnValue.MenuItemId);
        }

        [Test]
        public async Task PostMenuItem_AddsMenuItem()
        {
            // Arrange
            var menuItem = new MenuItem { MenuItemId = 1, ItemName = "Item1" };
            var mockSet = new Mock<DbSet<MenuItem>>();

            _mockContext.Setup(c => c.MenuItems).Returns(mockSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _controller.PostMenuItem(menuItem);

            // Assert
            Assert.IsInstanceOf<ActionResult<MenuItem>>(result);
            var actionResult = result.Result as OkObjectResult;
            Assert.IsNotNull(actionResult);
            var returnValue = actionResult.Value as MenuItem;
            Assert.AreEqual(1, returnValue.MenuItemId);
        }

        [Test]
        public async Task DeleteMenuItem_DeletesMenuItem()
        {
            // Arrange
            var menuItem = new MenuItem { MenuItemId = 1, ItemName = "Item1" };
            var mockSet = new Mock<DbSet<MenuItem>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(menuItem);

            _mockContext.Setup(c => c.MenuItems).Returns(mockSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _controller.DeleteMenuItem(1);

            // Assert
            Assert.IsInstanceOf<IActionResult>(result);
            var actionResult = result as OkObjectResult;
            Assert.IsNotNull(actionResult);
            Assert.AreEqual("Delete success", actionResult.Value);
        }
    }
}
