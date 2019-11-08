using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics.CodeAnalysis;

namespace WebbiSkools.QuizManager.FunctionalTests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class NavbarTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void Navbar_When_User_Is_Authenticated_Shows_Welcome_Message()
        {
            // Arrange
            var expected = "Hi, RestrictedPermissionsUser!";
            TestHelper.Login("RestrictedPermissionsUser", _driver);

            // Act
            var actual = _driver.FindElement(By.CssSelector("[data-testid='navbar-welcome-message']"));

            // Assert
            Assert.AreEqual(expected, actual.Text);
        }

        [Test]
        public void Navbar_When_User_Signs_Out_Removes_Welcome_Message()
        {
            // Arrange
            TestHelper.Login("RestrictedPermissionsUser", _driver);

            // Act
            _driver.FindElement(By.CssSelector("[data-testid='navbar-logout-button']")).Click();
            var navbarWelcomeMessageExists = TestHelper.ElementExists(By.CssSelector("[data-testid='navbar-welcome-message']"), _driver);

            // Assert
            Assert.IsFalse(navbarWelcomeMessageExists);
        }

        [Test]
        public void Navbar_When_Edit_User_Is_Authenticated_Shows_Create_Button()
        {
            // Arrange
            TestHelper.Login("EditPermissionsUser", _driver);

            // Act
            var navbarCreateButtonExists = TestHelper.ElementExists(By.CssSelector("[data-testid='navbar-create-button']"), _driver);

            // Assert
            Assert.IsTrue(navbarCreateButtonExists);
        }

        [Test]
        public void Navbar_When_View_User_Is_Authenticated_Does_Not_Show_Create_Button()
        {
            // Arrange
            TestHelper.Login("ViewPermissionsUser", _driver);

            // Act
            var navbarCreateButtonExists = TestHelper.ElementExists(By.CssSelector("[data-testid='navbar-create-button']"), _driver);

            // Assert
            Assert.IsFalse(navbarCreateButtonExists);
        }

        [Test]
        public void Navbar_When_Restricted_User_Is_Authenticated_Does_Not_Show_Create_Button()
        {
            // Arrange
            TestHelper.Login("RestrictedPermissionsUser", _driver);

            // Act
            var navbarCreateButtonExists = TestHelper.ElementExists(By.CssSelector("[data-testid='navbar-create-button']"), _driver);

            // Assert
            Assert.IsFalse(navbarCreateButtonExists);
        }
    }
}
