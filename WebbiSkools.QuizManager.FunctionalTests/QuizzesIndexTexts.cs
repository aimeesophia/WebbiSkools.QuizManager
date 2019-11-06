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
    public class QuizzesIndexTexts
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
        public void Index_When_Restricted_User_Is_Authenticated_Does_Not_Show_Delete_Button_Above_Quizzes()
        {
            // Arrange
            TestHelper.Login("RestrictedPermissionsUser", _driver);

            // Act
            var deleteQuizButtonExists = TestHelper.ElementExists(By.ClassName("delete-quiz-button"), _driver);

            // Assert
            Assert.IsFalse(deleteQuizButtonExists);
        }

        [Test]
        public void Index_When_View_User_Is_Authenticated_Does_Not_Show_Delete_Button_Above_Quizzes()
        {
            // Arrange
            TestHelper.Login("ViewPermissionsUser", _driver);

            // Act
            var deleteQuizButtonExists = TestHelper.ElementExists(By.ClassName("delete-quiz-button"), _driver);

            // Assert
            Assert.IsFalse(deleteQuizButtonExists);
        }

        [Test]
        public void Index_When_Edit_User_Is_Authenticated_Shows_Delete_Button_Above_Quizzes()
        {
            // Arrange
            TestHelper.Login("EditPermissionsUser", _driver);

            // Act
            var deleteQuizButtonExists = TestHelper.ElementExists(By.ClassName("delete-quiz-button"), _driver);

            // Assert
            Assert.IsTrue(deleteQuizButtonExists);
        }
    }
}
