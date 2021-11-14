using System;
using System.Collections.Generic;
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
    public class Validate
    {
        private static IWebDriver driver;

        [ClassInitialize]
        public static void InitializeClass(TestContext _)
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
        public void ValidateTest()
        {
            // Create id for test reruns.
            Guid userId = Guid.NewGuid();

            // Create a new user.
            driver.Navigate().GoToUrl("https://localhost:44344/auth/SignUp.aspx");
            driver.FindElement(By.Id("body_Form_Body_Name_Input")).SendKeys("Unauthorized");
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).SendKeys($"unauthorized{userId}@ubusecret.es");
            driver.FindElement(By.Id("body_Form_Body_Password_Input")).SendKeys("#Unauthorized1");
            driver.FindElement(By.Id("body_Form_Body_ConfirmPassword_Input")).SendKeys("#Unauthorized1");
            driver.FindElement(By.Id("body_Form_Bottom_Submit_Button")).Click();


            // Try to log in with unauthorized user.
            driver.Navigate().GoToUrl("https://localhost:44344/auth/LogIn.aspx");
            LogIn($"unauthorized{userId}@ubusecret.es", "#Unauthorized1");

            // Check pop up message.
            Assert.AreEqual("You need to be authorized to enter", driver.FindElement(By.Id("PopUp_Text")).Text);

            // Log in with admin.
            LogIn("admin@ubusecret.es", "P@ssword2");

            // Go to admin.
            driver.Navigate().GoToUrl("https://localhost:44344/admin/Users.aspx");

            // Authorize user.
            driver.FindElement(By.XPath("//*[@id='body_UsersTable']/tbody/tr[last()]/td[last()]/input")).Click();

            // Log out.
            LogOut();

            // Log in with user.
            LogIn($"unauthorized{userId}@ubusecret.es", "#Unauthorized1");

            // Create new password.
            driver.FindElement(By.Id("body_Form_Body_OldPassword_Input")).SendKeys("#Unauthorized1");
            driver.FindElement(By.Id("body_Form_Body_NewPassword_Input")).SendKeys("NewP@ssword2");
            driver.FindElement(By.Id("body_Form_Body_ConfirmNewPassword_Input")).SendKeys("NewP@ssword2");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            // Check correct log in.
            Assert.AreEqual("UBUSECRET - Home", driver.Title);

            // Log out.
            LogOut();
        }
    }
}
