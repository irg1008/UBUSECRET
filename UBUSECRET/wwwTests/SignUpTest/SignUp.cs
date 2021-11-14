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
    public class SignUp
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
            driver.Navigate().GoToUrl("https://localhost:44344/auth/SignUp.aspx");
        }

        [TestCleanup]
        public void CleanupTest()
        {
        }


        [TestMethod]
        public void CorrectSingUpTest()
        {
            // Create id for test reruns.
            Guid userId = Guid.NewGuid();

            driver.FindElement(By.Id("body_Form_Body_Name_Input")).SendKeys("Ejemplo");
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).SendKeys($"{userId}@ubusecret.com");
            driver.FindElement(By.Id("body_Form_Body_Password_Input")).SendKeys("#Ejemplo1");
            driver.FindElement(By.Id("body_Form_Body_ConfirmPassword_Input")).SendKeys("#Ejemplo1");
            driver.FindElement(By.Id("body_Form_Bottom_Submit_Button")).Click();

            Assert.AreEqual("User created successfully. You will be authorized soon.", driver.FindElement(By.Id("PopUp_Text")).Text);
        }

        [TestMethod]
        public void EmptySignUpTest()
        {
            driver.FindElement(By.Id("body_Form_Bottom_Submit_Button")).Click();

            Assert.AreEqual("Name cannot be empty", driver.FindElement(By.Id("body_Form_Body_NameError")).Text);
            Assert.AreEqual("Email cannot be empty", driver.FindElement(By.Id("body_Form_Body_EmailError")).Text);
            Assert.AreEqual("Password cannot be empty", driver.FindElement(By.Id("body_Form_Body_PasswordError")).Text);
            Assert.AreEqual("This field cannot be empty", driver.FindElement(By.Id("body_Form_Body_ConfirmPasswordError")).Text);
        }

        [TestMethod]
        public void ExistingUserTest()
        {
            driver.FindElement(By.Id("body_Form_Body_Name_Input")).SendKeys("Admin");
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).SendKeys("admin@ubusecret.es");
            driver.FindElement(By.Id("body_Form_Body_Password_Input")).SendKeys("P@ssword2");
            driver.FindElement(By.Id("body_Form_Body_ConfirmPassword_Input")).SendKeys("P@ssword2");
            driver.FindElement(By.XPath("//button[@id='body_Form_Bottom_Submit_Button']/p")).Click();

            Assert.AreEqual("User already exists", driver.FindElement(By.Id("body_Form_Body_EmailError")).Text);
        }

        [TestMethod]
        public void IncorrectConfirmPasswordTest()
        {
            driver.FindElement(By.Id("body_Form_Body_Name_Input")).SendKeys("Ejemplo");
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).SendKeys("ejemplo@ubusecret.com");
            driver.FindElement(By.Id("body_Form_Body_Password_Input")).SendKeys("#Ejemplo1");
            driver.FindElement(By.Id("body_Form_Body_ConfirmPassword_Input")).SendKeys("Diferente");
            driver.FindElement(By.XPath("//button[@id='body_Form_Bottom_Submit_Button']/p")).Click();

            Assert.AreEqual("The passwords do not match", driver.FindElement(By.Id("body_Form_Body_ConfirmPasswordError")).Text);
        }

        [TestMethod]
        public void IncorrectInputsTest()
        {
            driver.FindElement(By.Id("body_Form_Body_Name_Input")).SendKeys("A");
            driver.FindElement(By.Id("body_Form_Body_Email_Input")).SendKeys("A");
            driver.FindElement(By.Id("body_Form_Body_Password_Input")).SendKeys("A");
            driver.FindElement(By.Id("body_Form_Body_ConfirmPassword_Input")).SendKeys("A");
            driver.FindElement(By.XPath("//button[@id='body_Form_Bottom_Submit_Button']/p")).Click();

            Assert.AreEqual("Incorrect email format", driver.FindElement(By.Id("body_Form_Body_EmailError")).Text);
            Assert.AreEqual("Incorrect password format", driver.FindElement(By.Id("body_Form_Body_PasswordError")).Text);

        }
    }
}
