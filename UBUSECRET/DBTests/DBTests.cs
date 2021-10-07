using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Tests
{
    [TestClass()]
    public class DBTests
    {

        private DB db;

        [TestInitialize()]
        public void Startup()
        {
            db = new DB();
        }

        [TestCleanup]
        public void CleanUp()
        {
            db = null;
        }

        [TestMethod()]
        public void DBTest()
        {
            string adminEmail = "admin@ubusecret.es";

            // Check db has 1 user on init and is admin.
            bool dbContainsAdmin = db.ContainsUser(adminEmail);
            Assert.IsTrue(dbContainsAdmin);

            int numberOfUsers = db.UserCount();
            Assert.AreEqual(numberOfUsers, 1);
        }

        [TestMethod()]
        public void ContainsSecretTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ContainsSecretTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ContainsUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ContainsUserTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ContainsUserTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteSecretTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteSecretTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteUserTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteUserTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InsertSecretTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InsertUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void NextSecretTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void NextUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PreviousSecretTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PreviousUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSecretTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadSecretTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadUserTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadUserTest2()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SecretCountTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateSecretTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UserCountTest()
        {
            Assert.Fail();
        }
    }
}