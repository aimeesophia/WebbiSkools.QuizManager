using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WebbiSkools.QuizManager.FunctionalTests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ViewQuizTests
    {
        private IWebDriver _driver;

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
            TestHelper.Login("RestrictedPermissionsUser", _driver);
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
            TestHelper.Login("ViewPermissionsUser", _driver);
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
            TestHelper.Login("EditPermissionsUser", _driver);
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
            TestHelper.Login("ViewPermissionsUser", _driver);
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
