using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebbiSkools.QuizManager.FunctionalTests
{
    public class AccountLoginTests
    {
        private IWebDriver _driver;
        private const string Url = "https://localhost:44302/Account/Login";

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }

        [Test]
        public void Login_When_Successful_Redirects_To_HomeController_Index()
        {
            // Arrange
            var expected = "https://localhost:44302/";
            _driver.Navigate().GoToUrl(Url);

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
            var expected = "https://localhost:44302/Account/Login";
            _driver.Navigate().GoToUrl(Url);

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
            _driver.Navigate().GoToUrl(Url);

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
            _driver.Navigate().GoToUrl(Url);

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
            _driver.Navigate().GoToUrl(Url);

            // Act
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            var usernameValidationSpan = _driver.FindElement(By.CssSelector("span[data-valmsg-for='Username']"));
            var passwordValidationSpan = _driver.FindElement(By.CssSelector("span[data-valmsg-for='Password']"));

            // Assert
            Assert.AreEqual(usernameValidationSpanExpected, usernameValidationSpan.Text);
            Assert.AreEqual(passwordValidationSpanExpected, passwordValidationSpan.Text);
        }
    }
}
