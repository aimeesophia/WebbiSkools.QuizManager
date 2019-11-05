using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
    }
}
