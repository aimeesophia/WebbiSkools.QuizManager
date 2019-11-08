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
    public class QuizzesEditTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            TestHelper.Login("EditPermissionsUser", _driver);
            _driver.Navigate().GoToUrl(TestHelper.QuizzesEditUrl);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void Edit_When_AddQuestion_Button_Is_Clicked_Adds_QuestionAndAnswersGroup_Element()
        {
            // Arrange
            var expectedNumberOfQuestionAndAnswersGroupElements = 5;

            // Act
            _driver.FindElement(By.ClassName("add-question-button")).Click();
            var actualNumberOfQuestionAndAnswersGroupElements = _driver.FindElements(By.ClassName("question-and-answers-group")).Count;

            // Assert
            Assert.AreEqual(expectedNumberOfQuestionAndAnswersGroupElements, actualNumberOfQuestionAndAnswersGroupElements);
        }

        [Test]
        public void Edit_When_AddQuestion_Button_Is_Clicked_Adds_Populated_QuestionAndAnswersGroup_Element()
        {
            // Arrange
            var expectedNumberOfQuestionTextInputElements = 5;
            var expectedNumberOfAnswerTextInputElements = 19;

            // Act
            _driver.FindElement(By.ClassName("add-question-button")).Click();
            var actualNumberOfQuestionTextInputElements = _driver.FindElements(By.ClassName("question-text-input")).Count;
            var actualNumberOfAnswerTextInputElements = _driver.FindElements(By.ClassName("answer-text-input")).Count;

            // Assert
            Assert.AreEqual(expectedNumberOfQuestionTextInputElements, actualNumberOfQuestionTextInputElements);
            Assert.AreEqual(expectedNumberOfAnswerTextInputElements, actualNumberOfAnswerTextInputElements);
        }

        [Test]
        public void Edit_When_AddAnswer_Button_Is_Clicked_Adds_Answer_Element()
        {
            // Arrange
            var expectedNumberOfAnswerElements = 17;

            // Act
            _driver.FindElement(By.ClassName("add-answer-button")).Click();
            var actualNumberOfAnswerElements = _driver.FindElements(By.ClassName("answer")).Count;

            // Assert
            Assert.AreEqual(expectedNumberOfAnswerElements, actualNumberOfAnswerElements);
        }

        [Test]
        public void Edit_When_AddAnswer_Button_Is_Clicked_Adds_Populated_Answer_Element()
        {
            // Arrange
            var expectedNumberOfAnswerTextInputElements = 17;

            // Act
            _driver.FindElement(By.ClassName("add-answer-button")).Click();
            var actualNumberOfAnswerTextInputElements = _driver.FindElements(By.ClassName("answer-text-input")).Count;

            // Assert
            Assert.AreEqual(expectedNumberOfAnswerTextInputElements, actualNumberOfAnswerTextInputElements);
        }

        [Test]
        public void Edit_When_DeleteQuestion_Button_Is_Clicked_Removes_QuestionAndAnswersGroup_Element()
        {
            // Arrange
            var expectedNumberOfQuestionAndAnswersGroupElemenets = 3;

            // Act
            _driver.FindElement(By.ClassName("delete-question-button")).Click();
            var actualNumberOfQuestionAndAnswersGroupElemenets = _driver.FindElements(By.ClassName("question-and-answers-group")).Count;

            // Assert
            Assert.AreEqual(expectedNumberOfQuestionAndAnswersGroupElemenets, actualNumberOfQuestionAndAnswersGroupElemenets);
        }

        [Test]
        public void Edit_When_DeleteAnswer_Button_Is_Clicked_Removes_Answer_Element()
        {
            // Arrange
            var expectedNumberOfAnswerElements = 15;

            // Act
            _driver.FindElement(By.ClassName("delete-answer-button")).Click();
            var actualNumberOfAnswerElements = _driver.FindElements(By.ClassName("answer")).Count;

            // Assert
            Assert.AreEqual(expectedNumberOfAnswerElements, actualNumberOfAnswerElements);
        }

        [Test]
        public void Create_When_Only_Three_Answers_Are_Present_Within_A_Question_Disables_Delete_Answer_Buttons_For_That_Question()
        {
            // Arrange
            var firstSetOfAnswers = _driver.FindElement(By.ClassName("answers"));

            // Act
            firstSetOfAnswers.FindElement(By.ClassName("delete-answer-button")).Click();
            var deleteAnswerButtons = firstSetOfAnswers.FindElements(By.ClassName("delete-answer-button"));
            var deleteAnswerButtonsEnabled = false;
            foreach (var deleteAnswerButton in deleteAnswerButtons)
            {
                if (deleteAnswerButton.Enabled)
                {
                    deleteAnswerButtonsEnabled = true;
                }
            }

            // Assert
            Assert.IsFalse(deleteAnswerButtonsEnabled);
        }

        [Test]
        public void Create_When_Delete_Answer_Buttons_Are_Disabled_Are_Enabled_When_There_Are_More_Than_3_Answers_For_That_Question()
        {
            // Arrange
            var firstSetOfAnswers = _driver.FindElement(By.ClassName("answers"));
            var firstQuestionAndAnswersGroup = _driver.FindElement(By.ClassName("question-and-answers-group"));
            firstSetOfAnswers.FindElement(By.ClassName("delete-answer-button")).Click();

            // Act
            firstQuestionAndAnswersGroup.FindElement(By.ClassName("add-answer-button")).Click();
            var deleteAnswerButtons = firstSetOfAnswers.FindElements(By.ClassName("delete-answer-button"));
            var deleteAnswerButtonsEnabled = false;
            foreach (var deleteAnswerButton in deleteAnswerButtons)
            {
                if (deleteAnswerButton.Enabled)
                {
                    deleteAnswerButtonsEnabled = true;
                }
            }

            // Assert
            Assert.IsTrue(deleteAnswerButtonsEnabled);
        }

        [Test]
        public void Create_When_Five_Answers_Are_Present_Within_A_Question_Disables_Add_Answer_Button_For_That_Question()
        {
            // Arrange
            var firstQuestionAndAnswersGroup = _driver.FindElement(By.ClassName("question-and-answers-group"));

            // Act
            firstQuestionAndAnswersGroup.FindElement(By.ClassName("add-answer-button")).Click();
            firstQuestionAndAnswersGroup.FindElement(By.ClassName("add-answer-button")).Click();
            var addAnswerButtonEnabled = firstQuestionAndAnswersGroup.FindElement(By.ClassName("add-answer-button")).Enabled;

            // Assert
            Assert.IsFalse(addAnswerButtonEnabled);
        }

        [Test]
        public void Create_When_Add_Answer_Button_Is_Disabled_Is_Enabled_When_There_Are_Less_Than_5_Answers_For_That_Question()
        {
            // Arrange
            var firstQuestionAndAnswersGroup = _driver.FindElement(By.ClassName("question-and-answers-group"));
            firstQuestionAndAnswersGroup.FindElement(By.ClassName("add-answer-button")).Click();
            firstQuestionAndAnswersGroup.FindElement(By.ClassName("add-answer-button")).Click();

            // Act
            firstQuestionAndAnswersGroup.FindElement(By.ClassName("delete-answer-button")).Click();
            var addAnswerButtonEnabled = _driver.FindElement(By.ClassName("add-answer-button")).Enabled;

            // Assert
            Assert.IsTrue(addAnswerButtonEnabled);
        }
    }
}
