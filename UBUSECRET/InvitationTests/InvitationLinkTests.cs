using Microsoft.VisualStudio.TestTools.UnitTesting;
using Invitation;
using System;
using System.Collections.Generic;
using System.Text;
using Main;
using System.Threading;

namespace Invitation.Tests
{
    [TestClass()]
    public class InvitationLinkTests
    {
        private InvitationLink link;
        private Secret secret;
        private static readonly int limitMilliseconds = 200;


        [TestInitialize()]
        public void Startup()
        {
            User user = new User("User", "user@ubusecret.com", "P@ssw0rd");
            secret = new Secret("Secret", "Hidden Message", user);

            // We create the invitation link.
            DateTime limit = DateTime.Now.AddMilliseconds(limitMilliseconds);
            link = new InvitationLink(secret, limit);
        }

        [TestCleanup]
        public void CleanUp()
        {
            link = null;
        }

        [TestMethod()]
        public void InvitationLinkTest()
        {
            Assert.IsNotNull(link.Id);
            Assert.IsInstanceOfType(link.Id, typeof(Guid));

            Assert.IsNotNull(link.Secret);
            Assert.IsInstanceOfType(link.Secret, typeof(Secret));

            Assert.IsNotNull(link.Limit);
            Assert.IsInstanceOfType(link.Limit, typeof(DateTime));
        }

        [TestMethod()]
        public void IsAccessibleTest()
        {
            bool isAccessible = link.IsAccessible();
            Assert.IsTrue(isAccessible);

            // Wait for link to time to reach limit.
            Thread.Sleep(limitMilliseconds);

            isAccessible = link.IsAccessible();
            Assert.IsFalse(isAccessible);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            // We create a link with same secret.
            InvitationLink additionalLink = new InvitationLink(secret, DateTime.Now.AddMilliseconds(limitMilliseconds));
            Assert.AreNotEqual(link, additionalLink);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            // Testing id and email are being used to create hashmap.
            int correctHashCode = HashCode.Combine(link.Id);
            int wrongHashCode = HashCode.Combine(link.Secret);
            int linkHashCode = link.GetHashCode();

            Assert.AreEqual(correctHashCode, linkHashCode);
            Assert.AreNotEqual(correctHashCode, wrongHashCode);
        }
    }
}