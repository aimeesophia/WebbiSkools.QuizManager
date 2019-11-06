using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace WebbiSkools.QuizManager.FunctionalTests
{
    public class TestHelper
    {
        public const string LoginUrl = "https://localhost:44302/Account/Login";
        public const string QuizzesCreateUrl = "https://localhost:44302/Quizzes/Create";
        public const string ErrorAccessDeniedUrl = "https://localhost:44302/Error/AccessDenied";

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
