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
                .UseInMemoryDatabase(databaseName: "TestQuizManager")
                .Options;

            _quizManagerContext = new QuizManagerContext(options);
        }

        [TearDown]
        public void TearDown()
        {
            _quizManagerContext.Database.EnsureDeleted();
        }

        [Test]
        public void Initialise_When_Users_Is_Empty_Populates_Users()
        {
            // Arrange
            var expected = 3;

            // Act
            DbInitialiser.Initialise(_quizManagerContext);
            var actual = _quizManagerContext.Users.Count();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Initialise_When_Users_Is_Not_Empty_Does_Not_Populate_Users()
        {
            // Arrange
            _quizManagerContext.Users.Add(new User() {Username = "TestUsername", Password = "TestPassword", Role = "TestRole"});
            _quizManagerContext.SaveChanges();
            var expected = 1;

            // Act
            DbInitialiser.Initialise(_quizManagerContext);
            var actual = _quizManagerContext.Users.Count();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
