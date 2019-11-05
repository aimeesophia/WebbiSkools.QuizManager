using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace WebbiSkools.QuizManager.FunctionalTests
{
    public class TestHelper
    {
        public static bool ElementExists(By by, IWebDriver driver)
        {
            if (driver.FindElements(by).Count != 0)
            {
                return true;
            }

            return false;
        }
    }
}
