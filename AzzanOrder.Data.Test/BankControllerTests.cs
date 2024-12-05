using NUnit.Framework;
using Moq;
using AzzanOrder.Data.Controllers;
using AzzanOrder.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace AzzanOrder.Data.Test
{
	public class BankControllerTests
	{
		private BankController _controller;
		private Mock<OrderingAssistSystemContext> _mockContext;

		[SetUp]
		public void Setup()
		{
			_mockContext = BankControllerTestSetup.GetMockContext();
			_controller = new BankController(_mockContext.Object);
		}

		[Test]
		public async Task GetBanks_ReturnsAllBanks()
		{
			// Act
			var result = await _controller.GetBanks();

			// Assert
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<ActionResult<IEnumerable<Bank>>>(result);
			var banks = result.Value as List<Bank>;
			Assert.AreEqual(3, banks.Count);
		}

		[Test]
		public async Task GetBank_ReturnsBankById()
		{
			// Act
			var result = await _controller.GetBank(1);

			// Assert
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<ActionResult<Bank>>(result);
			var bank = result.Value;
			Assert.AreEqual(1, bank.BankId);
		}

		[Test]
		public async Task GetBank_ReturnsNotFound_WhenBankDoesNotExist()
		{
			// Act
			var result = await _controller.GetBank(99);

			// Assert
			Assert.IsInstanceOf<ActionResult<Bank>>(result);
			Assert.IsInstanceOf<NotFoundResult>(result.Result);
		}

		[Test]
		public async Task PostBank_AddsNewBank()
		{
			var newBank = new Bank
			{
				PAYOS_CLIENT_ID = "PAYOS_CLIENT_ID4",
				PAYOS_API_KEY = "PAYOS_API_KEY4",
				PAYOS_CHECKSUM_KEY = "PAYOS_CHECKSUM_KEY4"
			};

			var result = await _controller.PostBank(newBank);
			var okResult = result.Result as OkObjectResult;
			var bank = okResult.Value as Bank;

			Assert.IsNotNull(okResult);
			Assert.AreEqual("PAYOS_CLIENT_ID4", bank.PAYOS_CLIENT_ID);
			Assert.AreEqual("PAYOS_API_KEY4", bank.PAYOS_API_KEY);
		}

		[Test]
		public async Task PutBank_UpdatesExistingBank()
		{
			var updatedBank = new Bank
			{
				BankId = 1,
				PAYOS_CLIENT_ID = "Updated_CLIENT_ID",
				PAYOS_API_KEY = "Updated_API_KEY",
				PAYOS_CHECKSUM_KEY = "Updated_CHECKSUM_KEY"
			};

			var result = await _controller.PutBank(updatedBank);
			var okResult = result as OkObjectResult;
			var bank = okResult.Value as Bank;

			Assert.IsNotNull(okResult);
			Assert.AreEqual("Updated_CLIENT_ID", bank.PAYOS_CLIENT_ID);
		}

		[Test]
		public async Task DeleteBank_RemovesBank()
		{
			var result = await _controller.DeleteBank(1);
			var okResult = result as OkObjectResult;

			Assert.IsNotNull(okResult);
			Assert.AreEqual("Delete Success", okResult.Value);
		}

		[Test]
		public async Task DeleteBank_RemovesBank_BankDoesNotExist()
		{
			// Act
			var result = await _controller.DeleteBank(999); // Assuming 999 is a non-existent bank ID
			var notFoundResult = result as NotFoundResult;

			// Assert
			Assert.IsNotNull(notFoundResult);
		}
	}
}
