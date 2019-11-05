using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using WebbiSkools.QuizManager.Web.Data;
using WebbiSkools.QuizManager.Web.Models;

namespace WebbiSkools.QuizManager.Tests
{
    [TestFixture]
    [Parallelizable]
    [ExcludeFromCodeCoverage]
    public class DbInitialiserTests
    {
        private QuizManagerContext _quizManagerContext;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<QuizManagerContext>()
                .UseInMemoryDatabase(databaseName: "DbInitialiserTests")
                .Options;

            _quizManagerContext = new QuizManagerContext(options);
        }

        [TearDown]
        public void TearDown()
        {
            _quizManagerContext.Database.EnsureDeleted();
        }

        [Test]
        public void Initialise_When_Database_Is_Empty_Populates_Database()
        {
            // Arrange
            var expectedNumberOfUsers = 3;
            var expectedNumberOfQuizzes = 3;
            var expectedNumberOfQuestions = 12;
            var expectedNumberOfAnswers = 48;

            // Act
            DbInitialiser.Initialise(_quizManagerContext);
            var actualNumberOfUsers = _quizManagerContext.Users.Count();
            var actualNumberOfQuizzes = _quizManagerContext.Quizzes.Count();
            var actualNumberOfQuestions = _quizManagerContext.Questions.Count();
            var actualNumberOfAnswers = _quizManagerContext.Answers.Count();

            // Assert
            Assert.AreEqual(expectedNumberOfUsers, actualNumberOfUsers);
            Assert.AreEqual(expectedNumberOfQuizzes, actualNumberOfQuizzes);
            Assert.AreEqual(expectedNumberOfQuestions, actualNumberOfQuestions);
            Assert.AreEqual(expectedNumberOfAnswers, actualNumberOfAnswers);
        }

        [Test]
        public void Initialise_When_Users_Is_Not_Empty_Does_Not_Populate_Users()
        {
            // Arrange
            _quizManagerContext.Users.Add(new User() {Username = "TestUsername", Password = "TestPassword", Role = "TestRole"});
            _quizManagerContext.SaveChanges();
            _quizManagerContext.Quizzes.Add(new Quiz() {Title = "The Software Development Lifecycle"});
            _quizManagerContext.SaveChanges();
            _quizManagerContext.Questions.Add(new Question() { QuizId = _quizManagerContext.Quizzes.Single(x => x.Title == "The Software Development Lifecycle").Id, Text = "At which point in the software development lifecycle is a system design document produced?" });
            _quizManagerContext.SaveChanges();
            _quizManagerContext.Answers.Add(new Answer() { QuestionId = _quizManagerContext.Questions.Single(x => x.Text == "At which point in the software development lifecycle is a system design document produced?").Id, Text = "Deployment/implementation." });
            _quizManagerContext.SaveChanges();

            var expectedNumberOfUsers = 1;
            var expectedNumberOfQuizzes = 1;
            var expectedNumberOfQuestions = 1;
            var expectedNumberOfAnswers = 1;

            // Act
            DbInitialiser.Initialise(_quizManagerContext);

            var actualNumberOfUsers = _quizManagerContext.Users.Count();
            var actualNumberOfQuizzes = _quizManagerContext.Quizzes.Count();
            var actualNumberOfQuestions = _quizManagerContext.Questions.Count();
            var actualNumberOfAnswers = _quizManagerContext.Answers.Count();

            // Assert
            Assert.AreEqual(expectedNumberOfUsers, actualNumberOfUsers);
            Assert.AreEqual(expectedNumberOfQuizzes, actualNumberOfQuizzes);
            Assert.AreEqual(expectedNumberOfQuestions, actualNumberOfQuestions);
            Assert.AreEqual(expectedNumberOfAnswers, actualNumberOfAnswers);
        }
    }
}
