using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestClass]
    public class CheckLogin
    {
        private static IWebDriver driver;

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            driver = new EdgeDriver();
        }

        [ClassCleanup]
        public static void CleanupClass()
        {
            try
            {
                driver.Quit();
                driver.Close();
                driver.Dispose();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [TestInitialize]
        public void InitializeTest()
        {
            driver.Navigate().GoToUrl("https://localhost:44344/auth/LogIn.aspx");
        }

        [TestCleanup]
        public void CleanupTest()
        {
        }

        private static void LogIn(string user, string pass)
        {
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).Clear();
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).SendKeys(user);
            driver.FindElement(By.Id("body_Form_Body_Password_Input")).SendKeys(pass);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        private static void LogOut()
        {
            driver.FindElement(By.XPath("//*[@id='LogOutBtn']")).Click();
        }

        [TestMethod]
        public void CorrectLogIn()
        {
            LogIn("admin@ubusecret.es", "P@ssword2");
            Assert.AreEqual("UBUSECRET - Home", driver.Title);
            LogOut();
        }

        [TestMethod]
        [DataRow("invalid@ubusecret.es", "#Example1")] // Both but invalid email
        [DataRow("invalid@ubusecret.es", "")] // No password
        [DataRow("", "#Example1")] // Empty
        public void ValidateEmail(string email, string pass)
        {
            LogIn(email, pass);
            Assert.AreEqual("User does not exists", driver.FindElement(By.Id("body_Form_Body_EmailError")).Text);
        }

        [TestMethod]
        [DataRow("admin@ubusecret.es", "#Invalid2")] // Invalid password
        [DataRow("admin@ubusecret.es", "")] // No password
        public void ValidatePassword(string email, string pass)
        {
            LogIn(email, pass);
            Assert.AreEqual("Password is incorrect", driver.FindElement(By.Id("body_Form_Body_PasswordError")).Text);
        }
    }
}
