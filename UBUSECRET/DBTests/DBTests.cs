using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System;
using System.Collections.Generic;
using System.Text;
using Main;
using System.Threading;

namespace Data.Tests
{
    [TestClass()]
    public class DBTests
    {

        private DB db;
        private User user;
        private Secret secret;
        private static readonly string adminEmail = "admin@ubusecret.es";

        [TestInitialize()]
        public void Startup()
        {
            db = DB.GetInstance();
            user = new User("User", "user@ubusecret.com", "P@ssw0rd");
            secret = new Secret("Secret", "Hidden Message", user);
        }

        [TestCleanup]
        public void CleanUp()
        {
            DB.Reset();
            user = null;
            secret = null;
        }

        [TestMethod()]
        public void DBTest()
        {
            // Check db has 1 user on init and is admin.
            bool dbContainsAdmin = db.ContainsUser(adminEmail);
            Assert.IsTrue(dbContainsAdmin);

            int numberOfUsers = db.UserCount();
            Assert.AreEqual(numberOfUsers, 1);
        }

        [TestMethod()]
        public void ContainsSecretTest()
        {
            bool containsSecret = db.ContainsSecret(secret);
            Assert.IsFalse(containsSecret);

            // Inserting secret.
            db.InsertSecret(secret);
            containsSecret = db.ContainsSecret(secret);
            Assert.IsTrue(containsSecret);

            // Does not contain null secret.
            bool containsNull = db.ContainsSecret(null);
            Assert.IsFalse(containsNull);
        }

        [TestMethod()]
        public void ContainsSecretTest1()
        {
            bool containsSecret = db.ContainsSecret(secret);
            Assert.IsFalse(containsSecret);

            // Inserting secret.
            db.InsertSecret(secret);
            containsSecret = db.ContainsSecret(secret.Id);
            Assert.IsTrue(containsSecret);
        }

        [TestMethod()]
        public void ContainsUserTest()
        {
            bool containsUser = db.ContainsUser(user);
            Assert.IsFalse(containsUser);

            // Inserting User.
            db.InsertUser(user);
            containsUser = db.ContainsUser(user);
            Assert.IsTrue(containsUser);

            // Does not contain null user.
            bool containsNull = db.ContainsUser((User)null);
            Assert.IsFalse(containsNull);
        }

        [TestMethod()]
        public void ContainsUserTest1()
        {
            bool containsUser = db.ContainsUser(user);
            Assert.IsFalse(containsUser);

            // Inserting secret.
            db.InsertUser(user);
            containsUser = db.ContainsUser(user.Id);
            Assert.IsTrue(containsUser);
        }

        [TestMethod()]
        public void ContainsUserTest2()
        {
            bool containsUser = db.ContainsUser(user);
            Assert.IsFalse(containsUser);

            // Inserting secret.
            db.InsertUser(user);
            containsUser = db.ContainsUser(user.Email);
            Assert.IsTrue(containsUser);
        }

        [TestMethod()]
        public void DeleteSecretTest()
        {
            bool containsSecret = db.ContainsSecret(secret);
            Assert.IsFalse(containsSecret);

            // Cannot delete if not inserted.
            bool deleted = db.DeleteSecret(secret);
            Assert.IsFalse(deleted);

            // Inserting secret.
            db.InsertSecret(secret);

            // Delete.
            deleted = db.DeleteSecret(secret);
            Assert.IsTrue(deleted);
        }

        [TestMethod()]
        public void DeleteSecretTest1()
        {
            bool containsSecret = db.ContainsSecret(secret);
            Assert.IsFalse(containsSecret);

            // Cannot delete if not inserted.
            bool deleted = db.DeleteSecret(secret);
            Assert.IsFalse(deleted);

            // Inserting secret.
            db.InsertSecret(secret);

            // Delete.
            deleted = db.DeleteSecret(secret.Id);
            Assert.IsTrue(deleted);
        }

        [TestMethod()]
        public void DeleteUserTest()
        {
            bool containsUser = db.ContainsUser(user);
            Assert.IsFalse(containsUser);

            // Cannot delete if not inserted.
            bool deleted = db.DeleteUser(user);
            Assert.IsFalse(deleted);

            // Inserting secret.
            db.InsertUser(user);

            // Delete.
            deleted = db.DeleteUser(user);
            Assert.IsTrue(deleted);
        }

        [TestMethod()]
        public void DeleteUserTest1()
        {
            bool containsUser = db.ContainsUser(user);
            Assert.IsFalse(containsUser);

            // Cannot delete if not inserted.
            bool deleted = db.DeleteUser(user);
            Assert.IsFalse(deleted);

            // Inserting secret.
            db.InsertUser(user);

            // Delete.
            deleted = db.DeleteUser(user.Id);
            Assert.IsTrue(deleted);
        }

        [TestMethod()]
        public void DeleteUserTest2()
        {
            bool containsUser = db.ContainsUser(user);
            Assert.IsFalse(containsUser);

            // Cannot delete if not inserted.
            bool deleted = db.DeleteUser(user);
            Assert.IsFalse(deleted);

            // Inserting secret.
            db.InsertUser(user);

            // Delete.
            deleted = db.DeleteUser(user.Email);
            Assert.IsTrue(deleted);
        }

        [TestMethod()]
        public void InsertSecretTest()
        {
            // Insert secret.
            bool inserted = db.InsertSecret(secret);
            Assert.IsTrue(inserted);

            // Insert again.
            inserted = db.InsertSecret(secret);
            Assert.IsFalse(inserted);

            // Insert null.
            inserted = db.InsertSecret(null);
            Console.WriteLine(inserted);
            Assert.IsFalse(inserted);
        }

        [TestMethod()]
        public void InsertUserTest()
        {
            // Insert user.
            bool inserted = db.InsertUser(user);
            Assert.IsTrue(inserted);

            // Insert again.
            inserted = db.InsertUser(user);
            Assert.IsFalse(inserted);

            // Insert null.
            inserted = db.InsertUser(null);
            Assert.IsFalse(inserted);
        }

        [TestMethod()]
        public void NextSecretTest()
        {

            // Insert secrets.
            Secret s_1 = new Secret("a", "secret a", user);
            Secret s_2 = new Secret("b", "secret b", user);
            db.InsertSecret(s_1);
            db.InsertSecret(s_2);

            // Get first.
            Secret next = db.NextSecret();
            Assert.AreEqual(next, s_1);

            // Get second.
            next = db.NextSecret();
            Assert.AreEqual(next, s_2);

            // Next should be null.
            next = db.NextSecret();
            Assert.IsNull(next);
        }

        [TestMethod()]
        public void NextUserTest()
        {
            // Assert first user is admin.
            User next = db.NextUser();
            Assert.AreEqual(next.Email, adminEmail);

            // Insert user and get.
            db.InsertUser(user);
            next = db.NextUser();
            Assert.AreEqual(next, user);

            // Next should be null.
            next = db.NextUser();
            Assert.IsNull(next);
        }

        [TestMethod()]
        public void PreviousSecretTest()
        {
            Secret s_1 = new Secret("a", "secret a", user);
            Secret s_2 = new Secret("b", "secret b", user);

            // Insert both and get both.
            db.InsertSecret(s_1);
            db.InsertSecret(s_2);
            db.NextSecret();
            db.NextSecret();

            // Get previous should be s_1;
            Secret previous = db.PreviousSecret();
            Assert.AreEqual(previous, s_1);

            // Previous should be null.
            previous = db.PreviousSecret();
            Assert.IsNull(previous);
        }

        [TestMethod()]
        public void PreviousUserTest()
        {
            // Insert second user and get all.
            db.InsertUser(user);
            db.NextUser();
            db.NextUser();

            // Previous should be admin.
            User previous = db.PreviousUser();
            Assert.AreEqual(previous.Email, adminEmail);

            // Previous should be null.
            previous = db.PreviousUser();
            Assert.IsNull(previous);
        }

        [TestMethod()]
        public void ReadSecretTest()
        {
            // Insert secret.
            db.InsertSecret(secret);

            // Read secret.
            Secret s = db.ReadSecret(secret);
            Assert.AreEqual(secret, s);

            // Read null secret.
            s = db.ReadSecret(null);
            Assert.IsNull(s);
        }

        [TestMethod()]
        public void ReadSecretTest1()
        {
            // Insert secret.
            db.InsertSecret(secret);

            // Read secret.
            Secret s = db.ReadSecret(secret.Id);
            Assert.AreEqual(secret, s);
        }

        [TestMethod()]
        public void ReadUserTest()
        {
            // Insert user.
            db.InsertUser(user);

            // Read secret.
            User u = db.ReadUser(user);
            Assert.AreEqual(user, u);

            // Read null user.
            u = db.ReadUser((User)null);
            Assert.IsNull(u);
        }

        [TestMethod()]
        public void ReadUserTest1()
        {
            // Insert user.
            db.InsertUser(user);

            // Read user.
            User u = db.ReadUser(user.Id);
            Assert.AreEqual(user, u);

            // Read null user.
            u = db.ReadUser((string)null);
            Assert.IsNull(u);
        }

        [TestMethod()]
        public void ReadUserTest2()
        {
            // Insert user.
            db.InsertUser(user);

            // Read user.
            User u = db.ReadUser(user.Email);
            Assert.AreEqual(user, u);
        }

        [TestMethod()]
        public void SecretCountTest()
        {
            // Initial count should be 0.
            int count = db.SecretCount();
            Assert.AreEqual(count, 0);

            // Insert secret.
            db.InsertSecret(secret);
            count = db.SecretCount();
            Assert.AreEqual(count, 1);
        }

        [TestMethod()]
        public void UpdateSecretTest()
        {
            // Can only update existing secrets.
            bool updated = db.UpdateSecret(secret);
            Assert.IsFalse(updated);

            // Insert user.
            db.InsertSecret(secret);

            // Change user and update.
            secret.Title = "NewTitle";
            updated = db.UpdateSecret(secret);
            Assert.IsTrue(updated);
        }

        [TestMethod()]
        public void UpdateUserTest()
        {
            // Can only update existing users.
            bool updated = db.UpdateUser(user);
            Assert.IsFalse(updated);

            // Insert user.
            db.InsertUser(user);

            // Change user and update.
            user.Name = "NewName";
            updated = db.UpdateUser(user);
            Assert.IsTrue(updated);
        }

        [TestMethod()]
        public void UserCountTest()
        {
            // Initial count should be 1. (Admin).
            int count = db.UserCount();
            Assert.AreEqual(count, 1);

            // Insert secret.
            db.InsertUser(user);
            count = db.UserCount();
            Assert.AreEqual(count, 2);
        }
    }
}