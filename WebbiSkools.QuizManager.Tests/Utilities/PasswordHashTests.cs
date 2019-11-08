using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using WebbiSkools.QuizManager.Web.Utilities;

namespace WebbiSkools.QuizManager.Tests.Utilities
{
    [TestFixture]
    [Parallelizable]
    [ExcludeFromCodeCoverage]
    public class PasswordHashTests
    {
        [Test]
        public void Create_Hashes_A_Password()
        {
            // Arrange
            var expected = "tW4ZMsh7Y2dJisWmumhGu7J1QxMzuqvtbQVA4ipS/zA=";

            // Act
            var actual = PasswordHash.Create("password", "testuser");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_Hashes_Same_Password_Differently_Per_User()
        {
            // Act
            var user1Actual = PasswordHash.Create("password", "testuser1");
            var user2Actual = PasswordHash.Create("password", "testuser2");

            // Assert
            Assert.AreNotEqual(user1Actual, user2Actual);
        }
    }
}
