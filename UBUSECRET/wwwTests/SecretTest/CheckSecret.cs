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
    public class CheckSecret
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

        private static void GoToLastSecret()
        {
            driver.FindElement(By.XPath("//*[@id=\"body_SecretList_OwnedSecrets\"]/div[last()]/button")).Click();
        }

        private static void GoToLastSharedSecret()
        {
            driver.FindElement(By.XPath("//*[@id=\"body_SecretList_InvitedSecrets\"]/div[last()]/button")).Click();
        }

        private static void CreateSecret(string title, string description)
        {
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$SecretList$ctl00','')\"]")).Click();
            driver.FindElement(By.Id("body_SecretTitle")).SendKeys(title);
            driver.FindElement(By.Id("body_SecretMessage")).SendKeys(description);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        private static void AddConsumer(string email)
        {
            driver.FindElement(By.Id("body_Consumer_Input")).SendKeys(email);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        private static bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private static void CheckSecretDoesNotExist(string secretTitle)
        {
            bool isEmpty = IsElementPresent(By.Id("body_SecretList_SharedEmpty"));

            if (!isEmpty)
                Assert.IsFalse(Regex.IsMatch(driver.FindElement(By.XPath("//*[@id=\"body_SecretList_InvitedSecrets\"]/div[last()]/div[1]/strong/span")).Text, secretTitle));
        }

        [TestMethod]
        public void DeleteSecretTest()
        {
            string secretID = Guid.NewGuid().ToString().Substring(0, 10);
            string secretTitle = $"To delete {secretID}";

            LogIn("admin@ubusecret.es", "P@ssword2");
            CreateSecret(secretTitle, "This secret will be deleted");

            // Go to secret and add consumer.
            GoToLastSecret();
            AddConsumer("guest@ubusecret.es");
            LogOut();

            // Log In with consumer.
            LogIn("guest@ubusecret.es", "P@ssword2");
            // Check user has access.
            GoToLastSharedSecret();
            Assert.AreEqual(secretTitle.ToUpper(), driver.FindElement(By.Id("body_SecretName")).Text);
            LogOut();

            // Delete secret.
            LogIn("admin@ubusecret.es", "P@ssword2");
            GoToLastSecret();
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$ctl01','')\"]")).Click();
            LogOut();

            // Check user has no longer access.
            LogIn("guest@ubusecret.es", "P@ssword2");
            CheckSecretDoesNotExist(secretTitle);
            LogOut();
        }

        [TestMethod]
        public void DetachFromSecretTest()
        {
            string secretID = Guid.NewGuid().ToString().Substring(0, 10);
            string secretTitle = $"Katalon share {secretID}";

            LogIn("admin@ubusecret.es", "P@ssword2");
            CreateSecret(secretTitle, "Secret for KatalonTests");
            GoToLastSecret();
            AddConsumer("guest@ubusecret.es");

            // Check pop up.
            Assert.AreEqual("Guest added successfully", driver.FindElement(By.Id("PopUp_Text")).Text);

            // Assert user tag.
            Assert.AreEqual("Guest", driver.FindElement(By.XPath("//*[@id='body_ConsumerList']/div[last()]/span")).Text);

            LogOut();

            // Log in with guest.
            LogIn("guest@ubusecret.es", "P@ssword2");
            GoToLastSharedSecret();
            Assert.AreEqual(secretTitle.ToUpper(), driver.FindElement(By.Id("body_SecretName")).Text);

            // Detatch from secret.
            driver.FindElement(By.Id("body_DetachButton")).Click();

            LogOut();

            // Log in with owner to check guest dissapeared.
            LogIn("admin@ubusecret.es", "P@ssword2");
            CheckSecretDoesNotExist(secretTitle);
            LogOut();
        }

        [TestMethod]
        public void DetatchConsumer()
        {
            string secretID = Guid.NewGuid().ToString().Substring(0, 10);
            string secretTitle = $"Katalon share {secretID}";

            LogIn("admin@ubusecret.es", "P@ssword2");
            CreateSecret(secretTitle, "Secret for KatalonTests");
            GoToLastSecret();
            AddConsumer("guest@ubusecret.es");

            // Then remove it.
            driver.FindElement(By.XPath("//button[@type='button']")).Click();

            LogOut();

            // Check user has no longer access.
            LogIn("guest@ubusecret.es", "P@ssword2");
            CheckSecretDoesNotExist(secretTitle);
            LogOut();
        }
    }
}
