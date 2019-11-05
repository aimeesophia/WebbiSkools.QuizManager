using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using WebbiSkools.QuizManager.Web.Controllers;
using WebbiSkools.QuizManager.Web.Data;
using WebbiSkools.QuizManager.Web.Models;

namespace WebbiSkools.QuizManager.Tests
{
    [TestFixture]
    [Parallelizable]
    [ExcludeFromCodeCoverage]
    public class AccountControllerTests
    {
        private AccountController _accountController;
        private QuizManagerContext _quizManagerContext;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<QuizManagerContext>()
                .UseInMemoryDatabase(databaseName: "AccountControllerTests")
                .Options;

            _quizManagerContext = new QuizManagerContext(options);

            _accountController = new AccountController(_quizManagerContext);
        }

        [TearDown]
        public void TearDown()
        {
            _quizManagerContext.Database.EnsureDeleted();
        }

        [Test]
        public void Login_Returns_Login_View()
        {
            // Arrange
            var expected = "Login";

            // Act
            var actual = ((ViewResult) _accountController.Login()).ViewName;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Login_Post_When_ModelState_Invalid_Returns_Login_View()
        {
            // Arrange
            var expected = "Login";
            _accountController.ModelState.AddModelError("TestKey", "TestErrorMessage");

            // Act
            var actual = await _accountController.Login(new User()) as ViewResult;

            // Assert
            Assert.AreEqual(expected, actual.ViewName);
        }

        [Test]
        public async Task Login_Post_When_User_Is_Not_Found_Returns_Login_View()
        {
            // Arrange
            var expected = "Login";

            // Act
            var actual = await _accountController.Login(new User() {Username = "TestUsername", Password = "TestPassword"}) as ViewResult;

            // Assert
            Assert.AreEqual(expected, actual.ViewName);
        }
    }
}
