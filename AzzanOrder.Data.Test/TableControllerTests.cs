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
    public class TableControllerTests
    {
        private Mock<OrderingAssistSystemContext> _mockContext;
        private TableController _controller;
        private List<Table> _tables;

        [SetUp]
        public void Setup()
        {
            _tables = new List<Table>
            {
                new Table { TableId = 1, EmployeeId = 1, Status = true },
                new Table { TableId = 2, EmployeeId = 2, Status = false }
            };

            var mockSet = new Mock<DbSet<Table>>();
            mockSet.As<IQueryable<Table>>().Setup(m => m.Provider).Returns(_tables.AsQueryable().Provider);
            mockSet.As<IQueryable<Table>>().Setup(m => m.Expression).Returns(_tables.AsQueryable().Expression);
            mockSet.As<IQueryable<Table>>().Setup(m => m.ElementType).Returns(_tables.AsQueryable().ElementType);
            mockSet.As<IQueryable<Table>>().Setup(m => m.GetEnumerator()).Returns(_tables.AsQueryable().GetEnumerator());

            _mockContext = new Mock<OrderingAssistSystemContext>();
            _mockContext.Setup(c => c.Tables).Returns(mockSet.Object);

            _controller = new TableController(_mockContext.Object);
        }

        [Test]
        public async Task GetTables_ReturnsAllTables()
        {
            var result = await _controller.GetTables();
            var okResult = result.Result as OkObjectResult;
            var tables = okResult.Value as List<Table>;

            Assert.AreEqual(2, tables.Count);
        }

        [Test]
        public async Task GetTable_ReturnsTableById()
        {
            var result = await _controller.GetTable(1);
            var okResult = result.Result as OkObjectResult;
            var table = okResult.Value as Table;

            Assert.AreEqual(1, table.TableId);
        }

        [Test]
        public async Task GetTable_ReturnsNotFound_WhenTableDoesNotExist()
        {
            var result = await _controller.GetTable(3);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task PostTable_AddsNewTable()
        {
            var newTable = new Table { TableId = 3, EmployeeId = 3, Status = true };
            var result = await _controller.PostTable(newTable);
            var okResult = result.Result as OkObjectResult;
            var table = okResult.Value as Table;

            Assert.AreEqual(3, table.TableId);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }

        [Test]
        public async Task PutTable_UpdatesExistingTable()
        {
            var updatedTable = new Table { TableId = 1, EmployeeId = 1, Status = false };
            var result = await _controller.PutTable(updatedTable);
            var okResult = result as OkObjectResult;
            var table = okResult.Value as Table;

            Assert.AreEqual("Closed", table.Status);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }

        [Test]
        public async Task DeleteTable_DeletesTable()
        {
            var result = await _controller.DeleteTable(1);
            var okResult = result as OkObjectResult;

            Assert.AreEqual("Delete success", okResult.Value);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }
    }
}
