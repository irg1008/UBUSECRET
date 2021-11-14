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
    public class ChangePassword
    {
        private static IWebDriver driver;
        private static string oldPass;
        private static string newPass;
        private static string email;

        [ClassInitialize]
        public static void InitializeClass(TestContext _)
        {
            driver = new EdgeDriver();
            oldPass = "P@ssword2";
            newPass = "P@ssword3";
            email = "admin@ubusecret.es";
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

        private static void SubmitNewPassword(string old, string newPass, string confirmNewPass)
        {
            // Type old password.
            driver.FindElement(By.Id("body_Form_Body_OldPassword_Input")).Clear();
            driver.FindElement(By.Id("body_Form_Body_OldPassword_Input")).SendKeys(old);

            // Type the new pass twice.
            driver.FindElement(By.Id("body_Form_Body_NewPassword_Input")).Clear();
            driver.FindElement(By.Id("body_Form_Body_NewPassword_Input")).SendKeys(newPass);
            driver.FindElement(By.Id("body_Form_Body_ConfirmNewPassword_Input")).Clear();
            driver.FindElement(By.Id("body_Form_Body_ConfirmNewPassword_Input")).SendKeys(confirmNewPass);

            // Submit.
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        private static void LogInWithPass(string pass)
        {
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).Clear();
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).SendKeys(email);

            driver.FindElement(By.Id("body_Form_Body_Password_Input")).Clear();
            driver.FindElement(By.Id("body_Form_Body_Password_Input")).SendKeys(pass);

            driver.FindElement(By.XPath("//div[@id='form']/div[2]/button/p")).Click();
        }

        private static void LogOut()
        {
            driver.FindElement(By.XPath("//div[@id='Navbar']/navbar/div/div[3]/button")).Click();
        }

        private static void GoToChange()
        {
            driver.FindElement(By.LinkText("Change your password")).Click();

            // Type email and go to change password page.
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).Clear();
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).SendKeys(email);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        [TestMethod]
        public void ChangePasswordTest()
        {
            driver.Navigate().GoToUrl("https://localhost:44344/auth/LogIn.aspx");

            GoToChange();

            // Submit old password as new.
            SubmitNewPassword(oldPass, oldPass, oldPass);
            Assert.AreEqual("Please do not use the same password", driver.FindElement(By.Id("body_Form_Body_NewPasswordError")).Text);

            // Submit different new passwords.
            SubmitNewPassword(oldPass, oldPass, "P@ssword99");
            Assert.AreEqual("The new passwords do not match", driver.FindElement(By.Id("body_Form_Body_ConfirmNewPasswordError")).Text);

            // Submit new password as new.
            SubmitNewPassword(oldPass, newPass, newPass);
            Assert.AreEqual("SECRETS", driver.FindElement(By.XPath("//div[@id='body_Dashboard']/h1")).Text);

            // Log out.
            LogOut();

            // Try to log in with old pass.
            LogInWithPass(oldPass);
            Assert.AreEqual("Password is incorrect", driver.FindElement(By.Id("body_Form_Body_PasswordError")).Text);

            // Try to log in with new pass.
            LogInWithPass(newPass); Assert.AreEqual("SECRETS", driver.FindElement(By.XPath("//div[@id='body_Dashboard']/h1")).Text);

            // Log out.
            LogOut();

            // Set old password again. Doing this so the test can be executed again.
            GoToChange();
            // Revert to old pass.
            SubmitNewPassword(newPass, oldPass, oldPass);
            // finally log out to mantain initial state.
            LogOut();
        }
    }
}
