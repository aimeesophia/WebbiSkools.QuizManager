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
    public class NavigationTests
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
        public void Navigation_When_Restricted_User_Navigates_To_QuizzesController_Create_Is_Redirected_To_ErrorController_AccessDenied()
        {
            // Arrange
            TestHelper.Login("RestrictedPermissionsUser", _driver);
            var expected = TestHelper.ErrorAccessDeniedUrl + "?ReturnUrl=%2FQuizzes%2FCreate";

            // Act
            _driver.Navigate().GoToUrl(TestHelper.QuizzesCreateUrl);

            // Assert
            Assert.AreEqual(expected, _driver.Url);
        }

        [Test]
        public void Navigation_When_View_User_Navigates_To_QuizzesController_Create_Is_Redirected_To_ErrorController_AccessDenied()
        {
            // Arrange
            TestHelper.Login("RestrictedPermissionsUser", _driver);
            var expected = TestHelper.ErrorAccessDeniedUrl + "?ReturnUrl=%2FQuizzes%2FCreate";

            // Act
            _driver.Navigate().GoToUrl(TestHelper.QuizzesCreateUrl);

            // Assert
            Assert.AreEqual(expected, _driver.Url);
        }

        [Test]
        public void Navigation_When_Restricted_User_Navigates_To_QuizzesController_Delete_Is_Redirected_To_ErrorController_AccessDenied()
        {
            // Arrange
            TestHelper.Login("RestrictedPermissionsUser", _driver);
            var expected = TestHelper.ErrorAccessDeniedUrl + "?ReturnUrl=%2FQuizzes%2FDelete%3Fid%3D1";

            // Act
            _driver.Navigate().GoToUrl(TestHelper.QuizzesDeleteUrl);

            // Assert
            Assert.AreEqual(expected, _driver.Url);
        }

        [Test]
        public void Navigation_When_View_User_Navigates_To_QuizzesController_Delete_Is_Redirected_To_ErrorController_AccessDenied()
        {
            // Arrange
            TestHelper.Login("ViewPermissionsUser", _driver);
            var expected = TestHelper.ErrorAccessDeniedUrl + "?ReturnUrl=%2FQuizzes%2FDelete%3Fid%3D1";

            // Act
            _driver.Navigate().GoToUrl(TestHelper.QuizzesDeleteUrl);

            // Assert
            Assert.AreEqual(expected, _driver.Url);
        }

        [Test]
        public void Navigation_When_Restricted_User_Navigates_To_QuizzesController_DeleteConfirmed_Is_Redirected_To_ErrorController_AccessDenied()
        {
            // Arrange
            TestHelper.Login("RestrictedPermissionsUser", _driver);
            var expected = TestHelper.ErrorAccessDeniedUrl + "?ReturnUrl=%2FQuizzes%2FDelete%3Fid%3D1";

            // Act
            _driver.Navigate().GoToUrl(TestHelper.QuizzesDeleteConfirmedUrl);

            // Assert
            Assert.AreEqual(expected, _driver.Url);
        }

        [Test]
        public void Navigation_When_View_User_Navigates_To_QuizzesController_DeleteConfirmed_Is_Redirected_To_ErrorController_AccessDenied()
        {
            // Arrange
            TestHelper.Login("ViewPermissionsUser", _driver);
            var expected = TestHelper.ErrorAccessDeniedUrl + "?ReturnUrl=%2FQuizzes%2FDelete%3Fid%3D1";

            // Act
            _driver.Navigate().GoToUrl(TestHelper.QuizzesDeleteConfirmedUrl);

            // Assert
            Assert.AreEqual(expected, _driver.Url);
        }

        [Test]
        public void Navigation_When_Restricted_User_Navigates_To_QuizzesController_Edit_Is_Redirected_To_ErrorController_AccessDenied()
        {
            // Arrange
            TestHelper.Login("RestrictedPermissionsUser", _driver);
            var expected = TestHelper.ErrorAccessDeniedUrl + "?ReturnUrl=%2FQuizzes%2FEdit%3Fid%3D1";

            // Act
            _driver.Navigate().GoToUrl(TestHelper.QuizzesEditUrl);

            // Assert
            Assert.AreEqual(expected, _driver.Url);
        }

        [Test]
        public void Navigation_When_View_User_Navigates_To_QuizzesController_Edit_Is_Redirected_To_ErrorController_AccessDenied()
        {
            // Arrange
            TestHelper.Login("ViewPermissionsUser", _driver);
            var expected = TestHelper.ErrorAccessDeniedUrl + "?ReturnUrl=%2FQuizzes%2FEdit%3Fid%3D1";

            // Act
            _driver.Navigate().GoToUrl(TestHelper.QuizzesEditUrl);

            // Assert
            Assert.AreEqual(expected, _driver.Url);
        }
    }
}
