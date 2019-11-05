using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebbiSkools.QuizManager.FunctionalTests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ViewQuizTests
    {
        private IWebDriver _driver;
        private const string Url = "https://localhost:44302/";
        private const string LoginUrl = "https://localhost:44302/Account/Login";

        [SetUp]
        public void Setup()
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
        public void Details_Does_Not_Show_Answers_For_Users_In_Restricted_Role()
        {
            // Arrange
            _driver.Navigate().GoToUrl(LoginUrl);
            _driver.FindElement(By.CssSelector("input[name='Username']")).SendKeys("RestrictedPermissionsUser");
            _driver.FindElement(By.CssSelector("input[name='Password'")).SendKeys("password");
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            var viewQuizButtons = _driver.FindElements(By.CssSelector("[data-testid='view-quiz-button']"));

            // Act
            viewQuizButtons.First().Click();
            var answerTextsExist = TestHelper.ElementExists(By.ClassName("answer"), _driver);

            // Assert
            Assert.IsFalse(answerTextsExist);
        }
    }
}
