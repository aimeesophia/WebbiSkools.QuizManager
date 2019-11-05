using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebbiSkools.QuizManager.FunctionalTests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class HomepageTests
    {
        private IWebDriver _driver;
        private const string Url = "https://localhost:44302/";

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
        public void Displays_Index()
        {
            // Arrange
            var expected = "Index";

            // Act
            _driver.Navigate().GoToUrl(Url);
            var actual = _driver.FindElement(By.CssSelector("[data-testid='index-text']"));

            // Assert
            Assert.AreEqual(expected, actual.Text);
        }
    }
}