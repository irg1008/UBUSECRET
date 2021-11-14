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
    public class LogUpdates
    {
        private static IWebDriver driver;
        private StringBuilder verificationErrors;

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
            verificationErrors = new StringBuilder();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            Assert.AreEqual("", verificationErrors.ToString());
        }

        private static void CreateSecret()
        {
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$SecretList$ctl00','')\"]")).Click();
            driver.FindElement(By.Id("body_SecretTitle")).SendKeys("New secret");
            driver.FindElement(By.Id("body_SecretMessage")).SendKeys("secret description");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        private static void LogIn(string user, string pass)
        {
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).SendKeys(user);
            driver.FindElement(By.Id("body_Form_Body_Password_Input")).SendKeys(pass);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        private static void AddConsumer(string email)
        {
            driver.FindElement(By.Id("body_Consumer_Input")).SendKeys(email);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        private static void GoToLastSecret()
        {
            driver.FindElement(By.XPath("//*[@id=\"body_SecretList_OwnedSecrets\"]/div[last()]/button")).Click();
        }

        private static void GoToLastSharedSecret()
        {
            driver.FindElement(By.XPath("//*[@id=\"body_SecretList_InvitedSecrets\"]/div[last()]/button")).Click();
        }

        private static void LogOut()
        {
            driver.FindElement(By.XPath("//*[@id='LogOutBtn']")).Click();
        }

        [TestMethod]
        public void LogUpdatesTest()
        {
            driver.Navigate().GoToUrl("https://localhost:44344/auth/LogIn.aspx");

            // Log in with admin.
            LogIn("admin@ubusecret.es", "P@ssword2");

            // Clear log so it's empty for the test.
            driver.FindElement(By.XPath("//button[@type='button']")).Click(); // Admin panel
            driver.FindElement(By.XPath("//button[@type='button']")).Click(); // Clear log
            driver.FindElement(By.XPath("//div[@id='Navbar']/navbar/a/h2")).Click(); // Home

            CreateSecret();

            // Add consumer.
            GoToLastSecret();
            AddConsumer("guest@ubusecret.es");

            // Create link for secret.
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$ctl02','')\"]")).Click();
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$ctl03','')\"]")).Click();

            // Log out.
            LogOut();

            // Log in with guest user.
            LogIn("guest@ubusecret.es", "P@ssword2");

            // Guest detaches from secret.
            GoToLastSharedSecret();
            driver.FindElement(By.Id("body_DetachButton")).Click();

            // Create and remove a secret.
            CreateSecret();
            GoToLastSecret();
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$ctl01','')\"]")).Click();

            // Log out.
            LogOut();

            // Log In as admin again.
            LogIn("admin@ubusecret.es", "P@ssword2");

            // Add consumer.
            GoToLastSecret();
            AddConsumer("guest@ubusecret.es");

            // Then remove it.
            driver.FindElement(By.XPath("//button[@type='button']")).Click();

            // Go to admin page to check logs.
            driver.Navigate().GoToUrl("https://localhost:44344/admin/Users.aspx");

            // Assert log reflects actions.
            Assert.AreEqual("New secret", driver.FindElement(By.XPath("//*[@id='body_LogTable']/tbody/tr[2]/td")).Text);
            Assert.AreEqual("New consumer", driver.FindElement(By.XPath("//*[@id='body_LogTable']/tbody/tr[3]/td")).Text);
            Assert.AreEqual("New invitation", driver.FindElement(By.XPath("//*[@id='body_LogTable']/tbody/tr[4]/td")).Text);
            Assert.AreEqual("Log Out", driver.FindElement(By.XPath("//*[@id='body_LogTable']/tbody/tr[5]/td")).Text);
            Assert.AreEqual("Log In", driver.FindElement(By.XPath("//*[@id='body_LogTable']/tbody/tr[6]/td")).Text);
            Assert.AreEqual("Consumer detatched itself from secret", driver.FindElement(By.XPath("//table[@id='body_LogTable']/tbody/tr[7]/td")).Text);
            Assert.AreEqual("Secret deleted", driver.FindElement(By.XPath("//*[@id='body_LogTable']/tbody/tr[9]/td")).Text);
            Assert.AreEqual("Owner detatched consumer", driver.FindElement(By.XPath("//*[@id='body_LogTable']/tbody/tr[13]/td")).Text);
        }
    }
}
