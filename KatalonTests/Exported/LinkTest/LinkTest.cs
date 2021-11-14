using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestClass]
    public class LinkTest
    {
        private static IWebDriver driver;
        private StringBuilder verificationErrors;
        private static string baseURL;
        private bool acceptNextAlert = true;
        
        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            driver = new FirefoxDriver();
            baseURL = "https://www.google.com/";
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
            verificationErrors = new StringBuilder();
        }
        
        [TestCleanup]
        public void CleanupTest()
        {
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [TestMethod]
        public void TheLinkTest()
        {
            driver.Navigate().GoToUrl("https://localhost:44344/auth/LogIn.aspx");
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).Clear();
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).SendKeys("admin@ubusecret.es");
            driver.FindElement(By.Id("body_Form_Body_Password_Input")).Clear();
            driver.FindElement(By.Id("body_Form_Body_Password_Input")).SendKeys("P@ssword2");
            driver.FindElement(By.XPath("//div[@id='form']/div[2]/button/p")).Click();
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$SecretList$ctl00','')\"]")).Click();
            driver.FindElement(By.Id("body_SecretTitle")).Clear();
            driver.FindElement(By.Id("body_SecretTitle")).SendKeys("LinkTest");
            driver.FindElement(By.Id("body_SecretMessage")).Clear();
            driver.FindElement(By.Id("body_SecretMessage")).SendKeys("Secret for LinkTest");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$SecretList$32$ctl00','')\"]")).Click();
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$ctl02','')\"]")).Click();
            driver.FindElement(By.Id("body_ExpiryTime_Input")).Click();
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$ctl03','')\"]")).Click();
            String enlace = driver.FindElement(By.Id("body_InvitationLink")).Text;
            driver.Navigate().GoToUrl(baseURL + enlace);
            try
            {
                Assert.AreEqual("You already own this secret. Invitations can only be accepted by other users", driver.FindElement(By.XPath("//div[@id='body_IsOwner_Panel']/h2")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.XPath("//div[@id='Navbar']/navbar/div/div[3]/button")).Click();
            driver.Navigate().GoToUrl(baseURL + enlace);
            try
            {
                Assert.AreEqual("You need to be logged in to accept an invitation", driver.FindElement(By.XPath("//div[@id='body_NotLogged_Panel']/h2")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$ctl01','')\"]")).Click();
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).Clear();
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).SendKeys("guest_2@ubusecret.es");
            driver.FindElement(By.Id("body_Form_Body_Password_Input")).Clear();
            driver.FindElement(By.Id("body_Form_Body_Password_Input")).SendKeys("NewP@ssword2");
            driver.FindElement(By.XPath("//div[@id='form']/div[2]/button/p")).Click();
            driver.Navigate().GoToUrl(baseURL + enlace);
            driver.FindElement(By.Id("body_AcceptButton")).Click();
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$SecretList$54$ctl00','')\"]")).Click();
            try
            {
                Assert.AreEqual("LinkTest", driver.FindElement(By.Id("body_SecretName")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).Clear();
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).SendKeys("admin@ubusecret.es");
            driver.FindElement(By.Id("body_Form_Body_Password_Input")).Clear();
            driver.FindElement(By.Id("body_Form_Body_Password_Input")).SendKeys("P@ssword2");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$SecretList$64$ctl00','')\"]")).Click();
            try
            {
                Assert.AreEqual("Guest 2", driver.FindElement(By.Id("body_2_ConsumerName")).Text);
            }
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.XPath("//button[@onclick=\"__doPostBack('ctl00$body$ctl01','')\"]")).Click();
            driver.FindElement(By.XPath("//div[@id='Navbar']/navbar/div/div[3]/button")).Click();
        }
        private bool IsElementPresent(By by)
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
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
