using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using OpenQA.Selenium;

namespace WebbiSkools.QuizManager.FunctionalTests
{
    [ExcludeFromCodeCoverage]
    public class TestHelper
    {
        public const string LoginUrl = "https://localhost:5001/Account/Login";
        public const string QuizzesCreateUrl = "https://localhost:5001/Quizzes/Create";
        public const string QuizzesDeleteUrl = "https://localhost:5001/Quizzes/Delete?id=1";
        public const string QuizzesDeleteConfirmedUrl = "https://localhost:5001/Quizzes/Delete?id=1";
        public const string QuizzesDetailsUrl = "https://localhost:5001/Quizzes/Details?id=1";
        public const string QuizzesEditUrl = "https://localhost:5001/Quizzes/Edit?id=1";
        public const string ErrorAccessDeniedUrl = "https://localhost:5001/Error/AccessDenied";
        public const string IndexUrl = "https://localhost:5001/";

        public static bool ElementExists(By by, IWebDriver driver)
        {
            if (driver.FindElements(by).Count != 0)
            {
                return true;
            }

            return false;
        }

        public static void Login(string username, IWebDriver driver)
        {
            driver.Navigate().GoToUrl(LoginUrl);
            driver.FindElement(By.CssSelector("input[name='Username']")).SendKeys(username);
            driver.FindElement(By.CssSelector("input[name='Password'")).SendKeys("password");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }
    }
}
