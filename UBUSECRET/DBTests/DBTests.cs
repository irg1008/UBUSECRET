using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Main;
using Invitation;
using Log;

namespace Data.Tests
{
    [TestClass()]
    public class DBTests
    {
        private DB db;
        private User user;
        private Secret secret;
        private LogEntry entry;
        private InvitationLink link;
        private static readonly string adminEmail = "admin@ubusecret.es";

        [TestInitialize()]
        public void Startup()
        {
            db = DB.GetInstance();
            user = new User("User", "user@ubusecret.com", "P@ssw0rd");
            secret = new Secret("Secret", "Hidden Message", user);
            link = new InvitationLink(secret, DateTime.Now.AddDays(1));
            entry = new LogEntry(Entry.LOG_IN, $"Logged in at time {DateTime.Now}");
        }

        [TestCleanup]
        public void CleanUp()
        {
            user = null;
            secret = null;
            link = null;
            db = null;
            entry = null;
            DB.Reset();
        }

        [TestMethod()]
        public void GetInstanceTest()
        {
            Assert.IsNotNull(DB.GetInstance());
            Assert.IsInstanceOfType(DB.GetInstance(), typeof(DB));
        }

        [TestMethod()]
        public void LoadSampleDataTest()
        {
            int userCount = db.UserCount();
            int secretCount = db.SecretCount();
            Assert.AreEqual(userCount, 1);
            Assert.AreEqual(secretCount, 0);

            DB.LoadSampleData();

            userCount = db.UserCount();
            secretCount = db.SecretCount();
            Assert.AreEqual(userCount, 5);
            Assert.AreEqual(secretCount, 3);
        }

        [TestMethod()]
        public void ResetTest()
        {
            // We check our db object is not null.
            Assert.IsNotNull(db);

            // We insert data to be "removed" on reset.
            User user = new User("Short Lived User", "slu@ubusecret.es", "P@ssword2");
            db.InsertUser(user);

            // Check correctly inserted.
            bool containsUser = db.ContainsUser(user);
            Assert.IsTrue(containsUser);

            DB.Reset();
            // We need to get the instance again because of C# does not update the pointer to the singleton DB instance.
            db = DB.GetInstance();

            // Check data does not exist anymore.
            containsUser = db.ContainsUser(user);
            Assert.IsFalse(containsUser);
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

        [TestMethod()]
        public void UserListTest()
        {
            // Insert user.
            db.InsertUser(user);

            // Check is in list.
            IList<User> userList = db.UserList();
            bool isInList = userList.Contains(user);
            Assert.IsTrue(isInList);
        }

        [TestMethod()]
        public void SecretListTest()
        {
            // Insert secret.
            db.InsertSecret(secret);

            // Check is in list.
            IList<Secret> secretList = db.SecretList();
            bool isInList = secretList.Contains(secret);
            Assert.IsTrue(isInList);
        }

        [TestMethod()]
        public void GetInvitedSecretsTest()
        {
            User guest = new User("Guest", "guest@ubusecret.es", "P@ssword2");

            // Insert both users and secret.
            db.InsertUser(guest);
            db.InsertUser(user);
            db.InsertSecret(secret);

            // Add conumer to secret and update.
            secret.AddConsumer(guest);
            db.UpdateSecret(secret);

            // Get all secrets shared with guest.
            var sharedSecrets = db.GetInvitedSecrets(guest);
            bool secretIsCorrectlyShared = sharedSecrets.Contains(secret);
            Assert.IsTrue(secretIsCorrectlyShared);
        }

        [TestMethod()]
        public void GetUserSecretsTest()
        {
            // InsertSecretTest user and secret.
            db.InsertUser(user);
            db.InsertSecret(secret);

            var userSecrets = db.GetUserSecrets(user);
            Assert.IsTrue(userSecrets.Contains(secret));
        }

        [TestMethod()]
        public void GetRequestedUsersTest()
        {
            // Inser user on state = requested.
            user.State = State.REQUESTED;
            db.InsertUser(user);

            // Check user is in requested users.
            bool userInRequested = db.GetRequestedUsers().Contains(user);
            Console.WriteLine(db.GetRequestedUsers().Count);
            Assert.IsTrue(userInRequested);
        }

        [TestMethod()]
        public void ReadInvitationTest()
        {
            // Insert invitation.
            db.InsertInvitation(link);

            // Read invitation.
            InvitationLink l = db.ReadInvitation(link);
            Assert.AreEqual(link, l);

            // Read null invitation.
            l = db.ReadInvitation(null);
            Assert.IsNull(l);
        }

        [TestMethod()]
        public void ReadInvitationTest1()
        {
            // Insert invitation.
            db.InsertInvitation(link);

            // Read invitation.
            InvitationLink l = db.ReadInvitation(link.Id);
            Assert.AreEqual(link, l);
        }

        [TestMethod()]
        public void DeleteInvitationTest()
        {
            bool containsInv = db.ContainsInvitation(link);
            Assert.IsFalse(containsInv);

            // Cannot delete if not inserted.
            bool deleted = db.DeleteInvitation(link);
            Assert.IsFalse(deleted);

            // Inserting secret.
            db.InsertInvitation(link);

            // Delete.
            deleted = db.DeleteInvitation(link);
            Assert.IsTrue(deleted);
        }

        [TestMethod()]
        public void DeleteInvitationTest1()
        {
            bool containsInv = db.ContainsInvitation(link);
            Assert.IsFalse(containsInv);

            // Cannot delete if not inserted.
            bool deleted = db.DeleteInvitation(link);
            Assert.IsFalse(deleted);

            // Inserting secret.
            db.InsertInvitation(link);

            // Delete.
            deleted = db.DeleteInvitation(link.Id);
            Assert.IsTrue(deleted);
        }

        [TestMethod()]
        public void InsertInvitationTest()
        {
            // Insert invitation.
            bool inserted = db.InsertInvitation(link);
            Assert.IsTrue(inserted);

            // Insert again.
            inserted = db.InsertInvitation(link);
            Assert.IsFalse(inserted);

            // Insert null.
            inserted = db.InsertInvitation(null);
            Assert.IsFalse(inserted);
        }

        [TestMethod()]
        public void ContainsInvitationTest()
        {
            bool containsInv = db.ContainsInvitation(link);
            Assert.IsFalse(containsInv);

            // Inserting secret.
            db.InsertInvitation(link);
            containsInv = db.ContainsInvitation(link);
            Assert.IsTrue(containsInv);
        }

        [TestMethod()]
        public void ContainsInvitationTest1()
        {
            bool containsInv = db.ContainsInvitation(link);
            Assert.IsFalse(containsInv);

            // Inserting secret.
            db.InsertInvitation(link);
            containsInv = db.ContainsInvitation(link.Id);
            Assert.IsTrue(containsInv);
        }

        [TestMethod()]
        public void InvitationCountTest()
        {
            // Initial count should be 0.
            int count = db.InvitationCount();
            Assert.AreEqual(count, 0);

            // Insert invitation.
            db.InsertInvitation(link);
            count = db.InvitationCount();
            Assert.AreEqual(count, 1);
        }

        [TestMethod()]
        public void LogListTest()
        {
            // Insert log.
            db.InsertLog(entry);

            // Check is in list.
            IList<LogEntry> entryList = db.LogList();
            bool isInList = entryList.Contains(entry);
            Assert.IsTrue(isInList);
        }

        [TestMethod()]
        public void InsertLogTest()
        {
            // Insert log.
            bool inserted = db.InsertLog(entry);
            Assert.IsTrue(inserted);

            // Insert again.
            inserted = db.InsertLog(entry);
            Assert.IsFalse(inserted);

            // Insert null.
            inserted = db.InsertLog(null);
            Assert.IsFalse(inserted);
        }

        [TestMethod()]
        public void LogCountTest()
        {
            // Initial count should be 0.
            int count = db.LogCount();
            Assert.AreEqual(count, 0);

            // Insert entry.
            db.InsertLog(entry);
            count = db.LogCount();
            Assert.AreEqual(count, 1);
        }

        [TestMethod()]
        public void ContainsLogTest()
        {
            bool containsLog = db.ContainsLog(entry);
            Assert.IsFalse(containsLog);

            // Inserting entry.
            db.InsertLog(entry);
            containsLog = db.ContainsLog(entry);
            Assert.IsTrue(containsLog);

            // Does not contain null entry.
            bool containsNull = db.ContainsLog(null);
            Assert.IsFalse(containsNull);
        }

        [TestMethod()]
        public void ContainsLogTest1()
        {
            bool containsLog = db.ContainsLog(entry.Id);
            Assert.IsFalse(containsLog);

            // Inserting entry.
            db.InsertLog(entry);
            containsLog = db.ContainsLog(entry.Id);
            Assert.IsTrue(containsLog);
        }

        [TestMethod()]
        public void ClearLogsTest()
        {
            // Insert entry.
            db.InsertLog(entry);

            // Clear logs.
            db.ClearLogs();

            Assert.IsFalse(db.ContainsLog(entry));
        }
    }
}