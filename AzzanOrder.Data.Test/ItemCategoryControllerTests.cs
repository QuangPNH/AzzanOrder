using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzzanOrder.Data.Controllers;
using AzzanOrder.Data.Models;
using AzzanOrder.Data.Test;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace AzzanOrder.Data.Tests.Controllers
{
    [TestFixture]
    public class ItemCategoryControllerTests
    {
        private ItemCategoryController _controller;
        private Mock<OrderingAssistSystemContext> _mockContext;

        [SetUp]
        public void Setup()
        {
            _mockContext = ItemCategoryTestSetup.GetMockContext();
            _controller = new ItemCategoryController(_mockContext.Object);
        }

        [Test]
        public async Task GetAllItemCategoriesValid_ReturnsAllValidItemCategories()
        {
            // Act
            var result = await _controller.GetAllItemCategoriesValid(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ActionResult<IEnumerable<ItemCategory>>>(result);
            var itemCategories = result.Value as List<ItemCategory>;
            Assert.IsNotNull(itemCategories);
            Assert.AreEqual(2, itemCategories.Count);
        }

        [Test]
        public async Task GetAllItemCategories_ReturnsAllItemCategories()
        {
            // Act
            var result = await _controller.GetAllItemCategories(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var itemCategories = okResult.Value as List<ItemCategory>;
            Assert.AreEqual(2, itemCategories.Count);
        }

        [Test]
        public async Task GetAllBaseItemCategories_ReturnsAllBaseItemCategories()
        {
            // Act
            var result = await _controller.GetAllBaseItemCategories(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var itemCategories = okResult.Value as List<ItemCategory>;
            Assert.AreEqual(2, itemCategories.Count);
        }

        [Test]
        public async Task GetItemCategories_ReturnsItemCategories()
        {
            // Act
            var result = await _controller.GetItemCategories(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ActionResult<IEnumerable<ItemCategory>>>(result);
            var itemCategories = result.Value as List<ItemCategory>;
            Assert.AreEqual(2, itemCategories.Count);
        }

        [Test]
        public async Task GetByMenuItemId_ReturnsItemCategoriesByMenuItemId()
        {
            // Act
            var result = await _controller.GetByMenuItemId(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ActionResult<IEnumerable<ItemCategory>>>(result);
            var itemCategories = result.Value as List<ItemCategory>;
            Assert.AreEqual(1, itemCategories.Count);
        }

        [Test]
        public async Task GetItemCategory_ReturnsItemCategoryById()
        {
            // Act
            var result = await _controller.GetItemCategory(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ActionResult<ItemCategory>>(result);
            var itemCategory = result.Value as ItemCategory;
            Assert.IsNotNull(itemCategory);
            Assert.AreEqual(1, itemCategory.ItemCategoryId);
        }

        [Test]
        public async Task GetItemCategoryByVoucherDetail_ReturnsItemCategoriesByVoucherDetailId()
        {
            // Act
            var result = await _controller.GetItemCategoryByVoucherDetail(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ActionResult<IEnumerable<ItemCategory>>>(result);
            var itemCategories = result.Value as List<ItemCategory>;
            Assert.AreEqual(1, itemCategories.Count);
        }

        [Test]
        public async Task PutItemCategory_UpdatesItemCategory()
        {
            var updatedItemCategory = new ItemCategory
            {
                ItemCategoryId = 1,
                Description = "Updated Description"
            };

            var result = await _controller.PutItemCategory(updatedItemCategory);
            var okResult = result as OkObjectResult;
            var itemCategory = okResult.Value as ItemCategory;

            Assert.IsNotNull(okResult);
            Assert.AreEqual("Updated Description", itemCategory.Description);
        }

        [Test]
        public async Task PostItemCategory_AddsNewItemCategory()
        {
            var newItemCategory = new ItemCategory
            {
                Description = "New Category"
            };

            var result = await _controller.PostItemCategory(newItemCategory);
            var okResult = result.Result as OkObjectResult;
            var itemCategory = okResult.Value as ItemCategory;

            Assert.IsNotNull(okResult);
            Assert.AreEqual("New Category", itemCategory.Description);
        }

        [Test]
        public async Task DeleteItemCategory_DeletesItemCategory()
        {
            var result = await _controller.DeleteItemCategory(1);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual("Delete Success", okResult.Value);
        }

        [Test]
        public async Task DeleteItemCategory_ItemCategoryDoesNotExist()
        {
            // Act
            var result = await _controller.DeleteItemCategory(999); // Assuming 999 is a non-existent item category ID
            var notFoundResult = result as NotFoundResult;

            // Assert
            Assert.IsNotNull(notFoundResult);
        }
    }
}
