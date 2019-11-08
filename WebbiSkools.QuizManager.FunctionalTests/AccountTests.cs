using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics.CodeAnalysis;

namespace WebbiSkools.QuizManager.FunctionalTests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class AccountTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void Login_When_Successful_Redirects_To_QuizzesController_Index()
        {
            // Arrange
            var expected = TestHelper.IndexUrl;
            _driver.Navigate().GoToUrl(TestHelper.LoginUrl);

            // Act
            _driver.FindElement(By.CssSelector("input[name='Username']")).SendKeys("RestrictedPermissionsUser");
            _driver.FindElement(By.CssSelector("input[name='Password'")).SendKeys("password");
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            // Assert
            Assert.AreEqual(expected, _driver.Url);
        }

        [Test]
        public void Login_When_Unsuccessful_Stays_On_Login()
        {
            // Arrange
            var expected = TestHelper.LoginUrl;
            _driver.Navigate().GoToUrl(TestHelper.LoginUrl);

            // Act
            _driver.FindElement(By.CssSelector("input[name='Username']")).SendKeys("RestrictedPermissionsUser");
            _driver.FindElement(By.CssSelector("input[name='Password'")).SendKeys("incorrectpassword");
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            // Assert
            Assert.AreEqual(expected, _driver.Url);
        }

        [Test]
        public void Login_When_Username_Is_Left_Blank_Displays_Error()
        {
            // Arrange
            var expected = "The Username field is required.";
            _driver.Navigate().GoToUrl(TestHelper.LoginUrl);

            // Act
            _driver.FindElement(By.CssSelector("input[name='Password']")).SendKeys("password");
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            var validationSpan = _driver.FindElement(By.CssSelector("span[data-valmsg-for='Username']"));

            // Assert
            Assert.AreEqual(expected, validationSpan.Text);
        }

        [Test]
        public void Login_When_Password_Is_Left_Blank_Displays_Error()
        {
            // Arrange
            var expected = "The Password field is required.";
            _driver.Navigate().GoToUrl(TestHelper.LoginUrl);

            // Act
            _driver.FindElement(By.CssSelector("input[name='Username']")).SendKeys("RestrictedPermissionsUser");
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            var validationSpan = _driver.FindElement(By.CssSelector("span[data-valmsg-for='Password']"));

            // Assert
            Assert.AreEqual(expected, validationSpan.Text);
        }

        [Test]
        public void Login_When_Username_And_Password_Are_Left_Blank_Displays_Error()
        {
            // Arrange
            var usernameValidationSpanExpected = "The Username field is required.";
            var passwordValidationSpanExpected = "The Password field is required.";
            _driver.Navigate().GoToUrl(TestHelper.LoginUrl);

            // Act
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            var usernameValidationSpan = _driver.FindElement(By.CssSelector("span[data-valmsg-for='Username']"));
            var passwordValidationSpan = _driver.FindElement(By.CssSelector("span[data-valmsg-for='Password']"));

            // Assert
            Assert.AreEqual(usernameValidationSpanExpected, usernameValidationSpan.Text);
            Assert.AreEqual(passwordValidationSpanExpected, passwordValidationSpan.Text);
        }

        [Test]
        public void Logout_Returns_User_To_Login_Page()
        {
            // Arrange
            var expected = TestHelper.LoginUrl;
            TestHelper.Login("RestrictedPermissionsUser", _driver);

            // Act
            _driver.FindElement(By.CssSelector("[data-testid='navbar-logout-button']")).Click();
            var actual = _driver.Url;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Login_When_Unsuccessful_Displays_Error_Alert()
        {
            // Arrange
            _driver.Navigate().GoToUrl(TestHelper.LoginUrl);

            // Act
            _driver.FindElement(By.CssSelector("input[name='Username']")).SendKeys("TestUser");
            _driver.FindElement(By.CssSelector("input[name='Password'")).SendKeys("TestPassword");
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            var alertElementVisible = _driver.FindElement(By.ClassName("alert")).Displayed;

            // Assert
            Assert.IsTrue(alertElementVisible);
        }
    }
}
