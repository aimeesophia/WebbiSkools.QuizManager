using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WebbiSkools.QuizManager.Web.Controllers;
using WebbiSkools.QuizManager.Web.Data;
using WebbiSkools.QuizManager.Web.Models;

namespace WebbiSkools.QuizManager.Tests.Controllers
{
    [TestFixture]
    [Parallelizable]
    [ExcludeFromCodeCoverage]
    public class QuizzesControllerTests
    {
        private QuizzesController _quizzesController;
        private QuizManagerContext _quizManagerContext;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<QuizManagerContext>()
                .UseInMemoryDatabase(databaseName: "QuizzesControllerTests")
                .Options;

            _quizManagerContext = new QuizManagerContext(options);

            _quizzesController = new QuizzesController(_quizManagerContext);
        }

        [TearDown]
        public void TearDown()
        {
            _quizManagerContext.Database.EnsureDeleted();
        }

        [Test]
        public async Task Index_Returns_View_With_Populated_Model()
        {
            // Arrange
            await AddQuizToDatabase();
            var expected = new List<Quiz>()
            {
                GetQuiz()
            };

            // Act
            var actual = await _quizzesController.Index() as ViewResult;

            // Assert
            actual.Model.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task Details_When_Id_Is_Null_Returns_NotFound_Result()
        {
            // Arrange

            // Act
            var result = await _quizzesController.Details(null);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Details_When_Id_Does_Not_Exist_In_Database_Returns_NotFound_Result()
        {
            // Arrange

            // Act
            var result = await _quizzesController.Details(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Details_When_Quiz_Is_Found_In_Database_Returns_View_With_Populated_Model()
        {
            // Arrange
            await AddQuizToDatabase();
            var expected = GetQuiz();

            // Act
            var actual = await _quizzesController.Details(1) as ViewResult;

            // Assert
            actual.Model.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Create_Returns_View()
        {
            // Arrange
            var expected = "Create";

            // Act
            var actual = _quizzesController.Create() as ViewResult;

            // Assert
            Assert.AreEqual(expected, actual.ViewName);
        }

        [Test]
        public async Task Create_When_ModelState_IsValid_True_Returns_Index_View()
        {
            // Arrange
            var quiz = GetQuiz();
            var expected = "Index";

            // Act
            var actual = await _quizzesController.Create(quiz) as RedirectToActionResult;

            // Assert
            Assert.AreEqual(expected, actual.ActionName);
        }

        [Test]
        public async Task Create_When_ModelState_IsValid_False_Returns_Create_View()
        {
            // Arrange
            var quiz = GetQuiz();
            _quizzesController.ModelState.AddModelError("TestKey", "TestErrorMessage");
            var expected = "Create";

            // Act
            var actual = await _quizzesController.Create(quiz) as ViewResult;

            // Assert
            Assert.AreEqual(expected, actual.ViewName);
        }

        [Test]
        public async Task Create_When_ModelState_IsValid_False_Returns_Create_View_With_Quiz()
        {
            // Arrange
            var quiz = GetQuiz();
            var expected = GetQuiz();
            _quizzesController.ModelState.AddModelError("TestKey", "TestErrorMessage");

            // Act
            var actual = await _quizzesController.Create(quiz) as ViewResult;

            // Assert
            actual.Model.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task Edit_When_Id_Is_Null_Returns_NotFoundResult()
        {
            // Arrange

            // Act
            var result = await _quizzesController.Edit(null);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Edit_When_Id_Does_Not_Exist_In_Database_Returns_NotFoundResult()
        {
            // Arrange

            // Act
            var result = await _quizzesController.Edit(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Edit_When_Id_Does_Exist_In_Database_Returns_Edit_View()
        {
            // Arrange
            await AddQuizToDatabase();

            var expected = "Edit";

            // Act
            var actual = await _quizzesController.Edit(1) as ViewResult;

            // Assert
            Assert.AreEqual(expected, actual.ViewName);
        }

        [Test]
        public async Task Edit_When_Id_Does_Exist_In_Database_Returns_Edit_View_With_Quiz()
        {
            // Arrange
            await AddQuizToDatabase();
            var expected = GetQuiz();

            // Act
            var actual = await _quizzesController.Edit(1) as ViewResult;

            // Assert
            actual.Model.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task Edit_When_Id_Does_Not_Match_Quiz_Id_Returns_NotFoundResult()
        {
            // Arrange
            var quiz = GetQuiz();

            // Act
            var result = await _quizzesController.Edit(2, quiz);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Edit_When_ModelState_IsValid_False_Returns_Edit_View()
        {
            // Arrange
            var quiz = GetQuiz();
            _quizzesController.ModelState.AddModelError("TestKey", "TestErrorMessage");
            var expected = "Edit";

            // Act
            var actual = await _quizzesController.Edit(1, quiz) as ViewResult;

            // Assert
            Assert.AreEqual(expected, actual.ViewName);
        }

        [Test]
        public async Task Edit_When_ModelState_IsValid_False_Returns_Edit_View_With_Quiz()
        {
            // Arrange
            var quiz = GetQuiz();
            var expected = GetQuiz();
            _quizzesController.ModelState.AddModelError("TestKey", "TestErrorMessage");

            // Act
            var actual = await _quizzesController.Edit(1, quiz) as ViewResult;

            // Assert
            actual.Model.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public async Task Delete_When_Id_Is_Null_Returns_NotFoundResult()
        {
            // Arrange

            // Act
            var result = await _quizzesController.Delete(null);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Delete_When_Id_Does_Not_Exist_In_Database_Returns_NotFoundResult()
        {
            // Arrange

            // Act
            var result = await _quizzesController.Delete(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Delete_When_Id_Does_Exist_In_Database_Returns_Delete_View()
        {
            // Arrange
            await AddQuizToDatabase();
            var expected = "Delete";

            // Act
            var actual = await _quizzesController.Delete(1) as ViewResult;

            // Assert
            Assert.AreEqual(expected, actual.ViewName);
        }

        [Test]
        public async Task Delete_When_Id_Does_Exist_In_Database_Returns_Delete_View_With_Quiz()
        {
            // Arrange
            await AddQuizToDatabase();
            var expected = GetQuiz();

            // Act
            var actual = await _quizzesController.Delete(1) as ViewResult;

            // Assert
            actual.Model.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task DeleteConfirmed_When_Id_Does_Not_Exist_In_Database_Returns_Index_View()
        {
            // Arrange
            var expected = "Index";

            // Act
            var actual = await _quizzesController.DeleteConfirmed(1) as RedirectToActionResult;

            // Assert
            Assert.AreEqual(expected, actual.ActionName);
        }

        [Test]
        public async Task DeleteConfirmed_When_Id_Does_Exist_In_Database_Returns_Index_View()
        {
            // Arrange
            await AddQuizToDatabase();
            var expected = "Index";

            // Act
            var actual = await _quizzesController.DeleteConfirmed(1) as RedirectToActionResult;

            // Assert
            Assert.AreEqual(expected, actual.ActionName);
        }

        private async Task AddQuizToDatabase()
        {
            _quizManagerContext.Quizzes.Add(new Quiz() { Id = 1, Title = "TestQuizTitle" });
            _quizManagerContext.Questions.Add(new Question() { Id = 1, QuizId = 1, Text = "TestQuestionText" });
            _quizManagerContext.Answers.Add(new Answer() { Id = 1, QuestionId = 1, Text = "TestAnswerText" });
            await _quizManagerContext.SaveChangesAsync();
        }

        private Quiz GetQuiz()
        {
            return new Quiz()
            {
                Id = 1,
                Title = "TestQuizTitle",
                Questions = new List<Question>()
                {
                    new Question()
                    {
                        Id = 1,
                        QuizId = 1,
                        Text = "TestQuestionText",
                        Answers = new List<Answer>()
                        {
                            new Answer()
                            {
                                Id = 1,
                                QuestionId = 1,
                                Text = "TestAnswerText"
                            }
                        }
                    }
                }
            };
        }
    }
}
