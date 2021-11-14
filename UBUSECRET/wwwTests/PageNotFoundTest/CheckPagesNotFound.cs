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
    public class CheckPagesNotFound
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
            GoLogIn();
        }

        [TestCleanup]
        public void CleanupTest()
        {
        }

        private static void GoLogIn()
        {
            driver.Navigate().GoToUrl("https://localhost:44344/auth/LogIn.aspx");
        }

        private static void LogOut()
        {
            driver.Navigate().GoToUrl("https://localhost:44344/default.aspx");
            driver.FindElement(By.XPath("//*[@id='LogOutBtn']")).Click();
        }

        private static void CheckNotFound(string text)
        {
            Assert.AreEqual("404", driver.FindElement(By.XPath("//form[@id='ctl01']/section/div/h1")).Text);
            Assert.AreEqual(text, driver.FindElement(By.XPath("//*[@id=\"body_Path\"]")).Text);
        }

        private static void LogIn(string user, string pass)
        {
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).SendKeys(user);
            driver.FindElement(By.Id("body_Form_Body_Password_Input")).SendKeys(pass);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        private static void CheckIsnotLogged()
        {
            Assert.AreEqual("LOG IN", driver.FindElement(By.XPath("//div[@id='form']/h2")).Text);
        }

        private static void GoToLastSecret()
        {
            driver.FindElement(By.XPath("//*[@id=\"body_SecretList_OwnedSecrets\"]/div[last()]/button")).Click();
        }

        private static void CreateSecret(string title, string description)
        {
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$SecretList$ctl00','')\"]")).Click();
            driver.FindElement(By.Id("body_SecretTitle")).SendKeys(title);
            driver.FindElement(By.Id("body_SecretMessage")).SendKeys(description);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        [TestMethod]
        public void LoggedTriesLoginTest()
        {
            CheckIsnotLogged();
            LogIn("admin@ubusecret.es", "P@ssword2");

            // Try to access log in page after success log.
            GoLogIn();

            // Should be 404 page.
            CheckNotFound("Not found: /auth/LogIn.aspx");

            LogOut();
        }

        [TestMethod]
        public void NonLoggedTriesSecretTest()
        {
            CheckIsnotLogged();

            // Navigate to secret.
            driver.Navigate().GoToUrl("https://localhost:44344/details/Secret.aspx?id=9");

            // Not found.
            CheckNotFound("Not found: /details/Secret.aspx");
        }

        [TestMethod]
        public void NonExistantPageTest()
        {
            driver.Navigate().GoToUrl("https://localhost:44344/invented/Page.aspx");

            CheckNotFound("Not found: /invented/Page.aspx");
        }

        [TestMethod]
        public void NonAdminTriesAdminRouteTest()
        {
            // Log in as non admin user.
            LogIn("guest@ubusecret.es", "P@ssword2");

            // Tries going to admin page.
            driver.Navigate().GoToUrl("https://localhost:44344/admin/Users.aspx");

            // Cannot access it.
            CheckNotFound("Not found: /admin/Users.aspx");

            LogOut();
        }

        [TestMethod]
        public void LoggedTriesUnnaccesibleSecretTest()
        {
            // Log in.
            LogIn("admin@ubusecret.es", "P@ssword2");

            // Go to non-existant secret
            driver.Navigate().GoToUrl("https://localhost:44344/details/Secret.aspx?id=this-secret-does-not-exist");

            // Check is ot found.
            CheckNotFound("Not found: /details/Secret.aspx");

            LogOut();
        }
    }
}
