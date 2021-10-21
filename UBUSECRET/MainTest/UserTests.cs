using Microsoft.VisualStudio.TestTools.UnitTesting;
using Main;
using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Tests
{
    [TestClass()]
    public class UserTests
    {

        User u_a;
        User u_b;
        User u_c;

        [TestInitialize()]
        public void Startup()
        {
            u_a = new User("a", "a@user.com", "P@ssword");
            u_b = new User("b", "b@user.com", "P@ssword");
            u_c = new User("c", "c@user.com", "P@ssword");
        }

        [TestMethod()]
        public void UserTest()
        {
            Assert.AreEqual(u_b.Id, u_a.Id + 1);
            Assert.AreEqual(u_c.Id, u_b.Id + 1);

            var u = u_a;

            Assert.IsNotNull(u.Id);
            Assert.IsNotNull(u.Name);
            Assert.IsNotNull(u.Email);
            Assert.IsNotNull(u.Password);
            Assert.IsNotNull(u.State);
            Assert.AreEqual(u.State, State.PREFETCHED);
            Assert.IsFalse(u.IsAdmin);
            Assert.IsNull(u.LastIP);

            var timeFromLastSeen_ms = (DateTime.Now - u.LastSeen).TotalMilliseconds;
            var threshold_ms = 200;
            var elapsedTimeIsCorrect = timeFromLastSeen_ms < threshold_ms;

            Assert.IsTrue(elapsedTimeIsCorrect);
        }

        [TestMethod()]
        public void RequestTest()
        {
            var u = u_a;
            bool isCorrect;

            bool changeIsCorrect(State newState)
            {
                u.State = newState;
                return u.Request();
            }

            // PREFETCHED => REQUEST.
            isCorrect = changeIsCorrect(State.PREFETCHED);
            Assert.IsTrue(isCorrect);

            // REQUESTED => REQUEST.
            isCorrect = changeIsCorrect(State.REQUESTED);
            Assert.IsFalse(isCorrect);

            // AUTHORIZED => REQUEST.
            isCorrect = changeIsCorrect(State.AUTHORIZED);
            Assert.IsFalse(isCorrect);

            // ACTIVE => REQUEST.
            isCorrect = changeIsCorrect(State.ACTIVE);
            Assert.IsFalse(isCorrect);

            // BANNED => REQUEST.
            isCorrect = changeIsCorrect(State.BANNED);
            Assert.IsFalse(isCorrect);
        }

        [TestMethod()]
        public void AuthorizeTest()
        {
            var u = u_a;
            bool isCorrect;

            bool changeIsCorrect(State newState)
            {
                u.State = newState;
                return u.Authorize();
            }

            // PREFETCHED => AUTHORIZED.
            isCorrect = changeIsCorrect(State.PREFETCHED);
            Assert.IsFalse(isCorrect);

            // REQUESTED => AUTHORIZED.
            isCorrect = changeIsCorrect(State.REQUESTED);
            Assert.IsTrue(isCorrect);

            // AUTHORIZED => AUTHORIZED.
            isCorrect = changeIsCorrect(State.AUTHORIZED);
            Assert.IsFalse(isCorrect);

            // ACTIVE => AUTHORIZED.
            isCorrect = changeIsCorrect(State.ACTIVE);
            Assert.IsFalse(isCorrect);

            // BANNED => AUTHORIZED.
            isCorrect = changeIsCorrect(State.BANNED);
            Assert.IsFalse(isCorrect);
        }

        [TestMethod()]
        public void ActivateTest()
        {
            var u = u_a;
            bool isCorrect;

            bool changeIsCorrect(State newState)
            {
                u.State = newState;
                return u.Activate();
            }

            // PREFETCHED => ACTIVE.
            isCorrect = changeIsCorrect(State.PREFETCHED);
            Assert.IsFalse(isCorrect);

            // REQUESTED => ACTIVE.
            isCorrect = changeIsCorrect(State.REQUESTED);
            Assert.IsFalse(isCorrect);

            // AUTHORIZED => ACTIVE.
            isCorrect = changeIsCorrect(State.AUTHORIZED);
            Assert.IsTrue(isCorrect);

            // ACTIVE => ACTIVE.
            isCorrect = changeIsCorrect(State.ACTIVE);
            Assert.IsFalse(isCorrect);

            // BANNED => ACTIVE.
            isCorrect = changeIsCorrect(State.BANNED);
            Assert.IsTrue(isCorrect);
        }

        [TestMethod()]
        public void BanTest()
        {
            var u = u_a;
            bool isCorrect;

            bool changeIsCorrect(State newState)
            {
                u.State = newState;
                return u.Ban();
            }

            // PREFETCHED => BANNED.
            isCorrect = changeIsCorrect(State.PREFETCHED);
            Assert.IsFalse(isCorrect);

            // REQUESTED => BANNED.
            isCorrect = changeIsCorrect(State.REQUESTED);
            Assert.IsFalse(isCorrect);

            // AUTHORIZED => BANNED.
            isCorrect = changeIsCorrect(State.AUTHORIZED);
            Assert.IsFalse(isCorrect);

            // ACTIVE => BANNED.
            isCorrect = changeIsCorrect(State.ACTIVE);
            Assert.IsTrue(isCorrect);

            // BANNED => BANNED.
            isCorrect = changeIsCorrect(State.BANNED);
            Assert.IsFalse(isCorrect);
        }

        [TestMethod()]
        public void CheckPaswordTest()
        {
            User u = u_a;

            bool badTry = u.CheckPasword("B@dTry");
            bool goodTry = u.CheckPasword("P@ssword");

            Assert.IsFalse(badTry);
            Assert.IsTrue(goodTry);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            User u = u_a;

            // Testing id and email are being used to create hashmap.
            int correctHashCode = HashCode.Combine(u.Email, u.Name);
            int wrongHashCode = HashCode.Combine(u.Id, u.Name);
            int userHashCode = u.GetHashCode();

            Assert.AreEqual(correctHashCode, userHashCode);
            Assert.AreNotEqual(correctHashCode, wrongHashCode);
        }

        [TestMethod()]
        public void ChangePasswordTest()
        {
            User u = u_a;

            String invalidOldPass = "Inv@lid";
            String correctOldPass = "P@ssword";

            String newPassword = "NewP@ss";

            // Test bad change.
            bool badChange = u.ChangePassword(invalidOldPass, newPassword);
            Assert.IsFalse(badChange);
            bool didRegister = u.CheckPasword(newPassword);
            Assert.IsFalse(didRegister);

            // Test good change.
            bool goodChange = u.ChangePassword(correctOldPass, newPassword);
            Assert.IsTrue(goodChange);
            bool correctyleUpdated = u.CheckPasword(newPassword);
            Assert.IsTrue(correctyleUpdated);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            u_c.Name = u_a.Name;
            u_c.Email = u_b.Email;

            bool ab_areEquals = u_a.Equals(u_b);
            Assert.IsFalse(ab_areEquals);

            bool ac_areEquals = u_a.Equals(u_c);
            Assert.IsFalse(ac_areEquals);

            bool bc_areEquals = u_b.Equals(u_c);
            Assert.IsTrue(bc_areEquals);
        }

        [TestMethod()]
        public void CompareToTest()
        {
            User first = u_a;
            User second = u_b;

            // User a is created first.
            bool aIsFirst = first.CompareTo(second) < 0;
            Assert.IsTrue(aIsFirst);

            // User b is created last.
            bool bIsFirst = second.CompareTo(first) < 0;
            Assert.IsFalse(bIsFirst);
        }

        [TestMethod()]
        public void MakeAdminTest()
        {
            User admin = u_a;

            Assert.IsFalse(admin.IsAdmin);
            admin.MakeAdmin();
            Assert.IsTrue(admin.IsAdmin);
        }
    }
}