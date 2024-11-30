using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using AzzanOrder.Data.Controllers;
using AzzanOrder.Data.Models;
using System;

namespace AzzanOrder.Data.Test
{
    [TestFixture]
    public class MemberControllerTests
    {
        private static Mock<OrderingAssistSystemContext> _mockContext;
        private MemberController _controller;

        [OneTimeSetUp]
        public static void SetupFirst()
        {
            _mockContext = MemberControllerTestSetup.GetMockContext();
        }

        [SetUp]
        public void Setup()
        {
            _controller = new MemberController(_mockContext.Object);
        }

        [Test]
        public async Task GetMembers_ReturnsAllMembers()
        {
            // Act
            var result = await _controller.GetMembers();

            // Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<Member>>>(result);
            var members = result.Value as List<Member>;
            Assert.IsNotNull(members);
            Assert.AreEqual(3, members.Count);
        }

        [Test]
        public async Task GetMember_ReturnsMemberById()
        {
            // Act
            var result = await _controller.GetMember(1231);

            // Assert
            Assert.IsInstanceOf<ActionResult<Member>>(result);
            var member = result.Value as Member;
            Assert.IsNotNull(member);
            Assert.AreEqual(1231, member.MemberId);
        }

        [Test]
        public async Task GetMember_ReturnsNotFound_WhenMemberDoesNotExist()
        {
            // Act
            var result = await _controller.GetMember(99);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task GetMemberByPhone_ReturnsMember()
        {
            // Act
            var result = await _controller.GetMemberByPhone("1234567890");

            // Assert
            Assert.IsInstanceOf<ActionResult<Member>>(result);
            var member = result.Value as Member;
            Assert.IsNotNull(member);
            Assert.AreEqual("1234567890", member.Phone);
        }

        [Test]
        public async Task Register_ReturnsBadRequest_WhenPhoneIsInvalid()
        {
            // Act
            var result = await _controller.Register("invalid_phone");

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Register_ReturnsBadRequest_WhenPhoneIsAlreadyRegistered()
        {
            // Act
            var result = await _controller.Register("1234567890");

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Register_CreatesNewMember()
        {
            // Arrange
            var newPhone = "0987654123";

            // Act
            var result = await _controller.Register(newPhone);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var member = okResult.Value as Member;
            Assert.IsNotNull(member);
            Assert.AreEqual(newPhone, member.Phone);

            // Cleanup
            await _controller.DeleteOwner(member.MemberId);
        }

        [Test]
        public async Task PutMember_UpdatesMember()
        {
            // Arrange
            var member = new Member
            {
                MemberId = 1231,
                MemberName = "Updated Name",
                Gender = true,
                Phone = "1234567890",
                Gmail = "updated.email@example.com",
                BirthDate = new DateTime(1990, 1, 1),
                Address = "123 Updated Address",
                Point = 100.5,
                Image = "updated_image.png",
                IsDelete = false
            };

            // Act
            var result = await _controller.PutMember(member);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var updatedMember = okResult.Value as Member;
            Assert.IsNotNull(updatedMember);
            Assert.AreEqual(member.MemberId, updatedMember.MemberId);
            Assert.AreEqual(member.MemberName, updatedMember.MemberName);
            Assert.AreEqual(member.Gender, updatedMember.Gender);
            Assert.AreEqual(member.Phone, updatedMember.Phone);
            Assert.AreEqual(member.Gmail, updatedMember.Gmail);
            Assert.AreEqual(member.BirthDate, updatedMember.BirthDate);
            Assert.AreEqual(member.Address, updatedMember.Address);
            Assert.AreEqual(member.Point, updatedMember.Point);
            Assert.AreEqual(member.Image, updatedMember.Image);
            Assert.AreEqual(member.IsDelete, updatedMember.IsDelete);
        }

        [Test]
        public async Task UpdatePoints_UpdatesMemberPoints()
        {
            // Act
            var result = await _controller.UpdatePoints(1231, 50.0);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var member = okResult.Value as Member;
            Assert.IsNotNull(member);
            Assert.AreEqual(150.5, member.Point);
        }

        [Test]
        public async Task PostMember_CreatesNewMember()
        {
            // Arrange
            var newMember = new Member
            {
                MemberId = 4,
                MemberName = "New Member",
                Phone = "5555555555"
            };

            // Act
            var result = await _controller.PostMember(newMember);

            // Assert
            Assert.IsInstanceOf<ActionResult<Member>>(result);
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            var member = createdAtActionResult.Value as Member;
            Assert.IsNotNull(member);
            Assert.AreEqual(4, member.MemberId);
        }

        [Test]
        public async Task DeleteOwner_DeletesMember()
        {
            // Act
            var result = await _controller.DeleteOwner(1231);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task PutImage_UpdatesMemberImage()
        {
            // Act
            var result = await _controller.PutImage(1231, "new_image.png");

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}
