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
    public class QuizzesCreateTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            TestHelper.Login("EditPermissionsUser", _driver);
            _driver.Navigate().GoToUrl(TestHelper.QuizzesCreateUrl);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void Create_When_AddQuestion_Button_Is_Clicked_Adds_QuestionAndAnswersGroup_Element()
        {
            // Arrange
            var expectedNumberOfQuestionAndAnswersGroupElements = 2;

            // Act
            _driver.FindElement(By.ClassName("add-question-button")).Click();
            var actualNumberOfQuestionAndAnswersGroupElements = _driver.FindElements(By.ClassName("question-and-answers-group")).Count;

            // Assert
            Assert.AreEqual(expectedNumberOfQuestionAndAnswersGroupElements, actualNumberOfQuestionAndAnswersGroupElements);
        }

        [Test]
        public void Create_When_AddQuestion_Button_Is_Clicked_Adds_Populated_QuestionAndAnswersGroup_Element()
        {
            // Arrange
            var expectedNumberOfQuestionTextInputElements = 2;
            var expectedNumberOfAnswerTextInputElements = 6;

            // Act
            _driver.FindElement(By.ClassName("add-question-button")).Click();
            var actualNumberOfQuestionTextInputElements = _driver.FindElements(By.ClassName("question-text-input")).Count;
            var actualNumberOfAnswerTextInputElements = _driver.FindElements(By.ClassName("answer-text-input")).Count;

            // Assert
            Assert.AreEqual(expectedNumberOfQuestionTextInputElements, actualNumberOfQuestionTextInputElements);
            Assert.AreEqual(expectedNumberOfAnswerTextInputElements, actualNumberOfAnswerTextInputElements);
        }

        [Test]
        public void Create_When_AddAnswer_Button_Is_Clicked_Adds_Answer_Element()
        {
            // Arrange
            var expectedNumberOfAnswerElements = 4;

            // Act
            _driver.FindElement(By.ClassName("add-answer-button")).Click();
            var actualNumberOfAnswerElements = _driver.FindElements(By.ClassName("answer")).Count;

            // Assert
            Assert.AreEqual(expectedNumberOfAnswerElements, actualNumberOfAnswerElements);
        }

        [Test]
        public void Create_When_AddAnswer_Button_Is_Clicked_Adds_Populated_Answer_Element()
        {
            // Arrange
            var expectedNumberOfAnswerTextInputElements = 4;

            // Act
            _driver.FindElement(By.ClassName("add-answer-button")).Click();
            var actualNumberOfAnswerTextInputElements = _driver.FindElements(By.ClassName("answer-text-input")).Count;

            // Assert
            Assert.AreEqual(expectedNumberOfAnswerTextInputElements, actualNumberOfAnswerTextInputElements);
        }
        
        [Test]
        public void Create_When_DeleteQuestion_Button_Is_Clicked_Removes_QuestionAndAnswersGroup_Element()
        {
            // Arrange

            // Act
            _driver.FindElement(By.ClassName("delete-question-button")).Click();
            var questionAndAnswersGroupElementExists = TestHelper.ElementExists(By.ClassName("question-and-answers-group"), _driver);

            // Assert
            Assert.IsFalse(questionAndAnswersGroupElementExists);
        }

        [Test]
        public void Create_When_DeleteAnswer_Button_Is_Clicked_Removes_Answer_Element()
        {
            // Arrange
            var expectedNumberOfAnswerElements = 4;
            _driver.FindElement(By.ClassName("add-answer-button")).Click();
            _driver.FindElement(By.ClassName("add-answer-button")).Click();

            // Act
            _driver.FindElement(By.ClassName("delete-answer-button")).Click();
            var actualNumberOfAnswerElements = _driver.FindElements(By.ClassName("answer")).Count;

            // Assert
            Assert.AreEqual(expectedNumberOfAnswerElements, actualNumberOfAnswerElements);
        }
    }
}
