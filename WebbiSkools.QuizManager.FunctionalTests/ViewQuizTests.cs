using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
        public void Details_When_Restricted_User_Signed_In_Does_Not_Show_Answers()
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

        [Test]
        public void Details_When_View_User_Signed_In_Shows_View_Answers_Buttons()
        {
            // Arrange
            _driver.Navigate().GoToUrl(LoginUrl);
            _driver.FindElement(By.CssSelector("input[name='Username']")).SendKeys("ViewPermissionsUser");
            _driver.FindElement(By.CssSelector("input[name='Password'")).SendKeys("password");
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            var viewQuizButtons = _driver.FindElements(By.CssSelector("[data-testid='view-quiz-button']"));

            // Act
            viewQuizButtons.First().Click();
            var viewAnswersButtonExists = TestHelper.ElementExists(By.CssSelector("[data-testid='view-answers-button']"), _driver);

            // Assert
            Assert.IsTrue(viewAnswersButtonExists);
        }

        [Test]
        public void Details_When_Edit_User_Signed_In_Shows_View_Answers_Buttons()
        {
            // Arrange
            _driver.Navigate().GoToUrl(LoginUrl);
            _driver.FindElement(By.CssSelector("input[name='Username']")).SendKeys("EditPermissionsUser");
            _driver.FindElement(By.CssSelector("input[name='Password'")).SendKeys("password");
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            var viewQuizButtons = _driver.FindElements(By.CssSelector("[data-testid='view-quiz-button']"));

            // Act
            viewQuizButtons.First().Click();
            var viewAnswersButtonExists = TestHelper.ElementExists(By.CssSelector("[data-testid='view-answers-button']"), _driver);

            // Assert
            Assert.IsTrue(viewAnswersButtonExists);
        }

        [Test]
        public void Details_When_User_Clicks_View_Answers_Shows_Answers()
        {
            // Arrange
            _driver.Navigate().GoToUrl(LoginUrl);
            _driver.FindElement(By.CssSelector("input[name='Username']")).SendKeys("EditPermissionsUser");
            _driver.FindElement(By.CssSelector("input[name='Password'")).SendKeys("password");
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            var viewQuizButtons = _driver.FindElements(By.CssSelector("[data-testid='view-quiz-button']"));
            viewQuizButtons.First().Click();
            var viewAnswersButtons = _driver.FindElements(By.CssSelector("[data-testid='view-answers-button']"));

            // Act
            viewAnswersButtons.First().Click();
            var answersBubbles = _driver.FindElements(By.CssSelector("[data-testid='answers-bubble']"));
            var answersBubbleVisible = answersBubbles.First().Displayed;

            // Assert
            Assert.IsTrue(answersBubbleVisible);
        }
    }
}
