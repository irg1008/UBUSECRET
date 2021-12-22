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
    public class Link
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
                //driver.Quit();// quit does not close the window
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

        private static void GoToLastSecret()
        {
            driver.FindElement(By.XPath("//*[@id=\"body_SecretList_OwnedSecrets\"]/div[last()]/button")).Click();
        }

        private static void GoToLastSharedSecret()
        {
            driver.FindElement(By.XPath("//*[@id=\"body_SecretList_InvitedSecrets\"]/div[last()]/button")).Click();
        }

        private static void LogIn(string user, string pass)
        {
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).SendKeys(user);
            driver.FindElement(By.Id("body_Form_Body_Password_Input")).SendKeys(pass);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        private static void LogOut()
        {
            driver.FindElement(By.XPath("//*[@id='LogOutBtn']")).Click();
        }

        [TestMethod]
        public void LinkTest()
        {
            driver.Navigate().GoToUrl("https://localhost:44344/auth/LogIn.aspx");

            // Log in.
            LogIn("admin@ubusecret.es", "P@ssword2");

            // Create secret.
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$SecretList$ctl00','')\"]")).Click();
            driver.FindElement(By.Id("body_SecretTitle")).SendKeys("Link Test");
            driver.FindElement(By.Id("body_SecretMessage")).SendKeys("Secret for LinkTest");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            // Enter the secret.
            GoToLastSecret();

            // Create a link.
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$ctl02','')\"]")).Click(); // Create link button.
            driver.FindElement(By.Id("body_ExpiryTime_Input")).Click(); // Date input.
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$ctl03','')\"]")).Click(); // Add time.

            // Copy generated link.
            String enlace = driver.FindElement(By.Id("body_InvitationLink")).Text;

            // Go to link being the owner.
            driver.Navigate().GoToUrl(enlace);
            Assert.AreEqual("YOU ALREADY OWN THIS SECRET. INVITATIONS CAN ONLY BE ACCEPTED BY OTHER USERS", driver.FindElement(By.XPath("//div[@id='body_IsOwner_Panel']/h2")).Text);

            // Log out.
            LogOut();

            // Go to link being logged out.
            driver.Navigate().GoToUrl(enlace);
            Assert.AreEqual("YOU NEED TO BE LOGGED IN TO ACCEPT AN INVITATION", driver.FindElement(By.XPath("//div[@id='body_NotLogged_Panel']/h2")).Text);

            // Go to log in with given button.
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$ctl01','')\"]")).Click();

            // Log in as other user.
            LogIn("guest@ubusecret.es", "P@ssword2");

            // Go to link and accept the secret.
            driver.Navigate().GoToUrl(enlace);
            driver.FindElement(By.Id("body_AcceptButton")).Click();

            // Go to secret and check name.
            GoToLastSharedSecret();
            Assert.AreEqual("LINK TEST", driver.FindElement(By.Id("body_SecretName")).Text);

            // Log out.
            LogOut();

            // Log in back as admin.
            LogIn("admin@ubusecret.es", "P@ssword2");

            // Check link secret has consumer.
            GoToLastSecret();
            Assert.AreEqual("Guest", driver.FindElement(By.XPath("//*[@id='body_ConsumerList']/div[last()]/span")).Text);

            // Log out.
            LogOut();
        }
    }
}
