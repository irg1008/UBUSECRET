using Microsoft.VisualStudio.TestTools.UnitTesting;
using Main;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Main.Tests
{
    [TestClass()]
    public class SecretTests
    {
        readonly User owner = new User("Owner", "owner@user.com", "P@ssword");
        Secret s_a;
        Secret s_b;

        [TestInitialize()]
        public void Startup()
        {
            s_a = new Secret("A", "Secret a", owner);
            s_b = new Secret("B", "Secret b", owner);
        }

        [TestMethod()]
        public void SecretTest()
        {
            Assert.AreEqual(s_b.Id, s_a.Id + 1);

            Secret s = s_a;

            Assert.IsNotNull(s.Id);
            Assert.IsNotNull(s.Title);
            Assert.IsNotNull(s.Message);
            Assert.IsTrue(s.Message is byte[]); // Encrypted.
            Assert.IsNotNull(s.Owner);
            Assert.IsNotNull(s.Consumers);
        }

        [TestMethod()]
        public void IsOwnerTest()
        {
            Secret s = s_a;
            User secretOwner = s.Owner;

            User wrongOwner = new User("Wrong", "wrong@user.com", "P@ssword");
            User correctOwner = owner;

            Assert.AreNotEqual(wrongOwner, secretOwner);
            Assert.AreEqual(correctOwner, secretOwner);
        }

        [TestMethod()]
        public void AddConsumerTest()
        {
            // Also tests the HasAccess method.
            Secret s = s_a;
            User newUser = new User("Consumer", "consumer@user.com", "P@ssword");

            bool addingOwnerAsConsumer = s.AddConsumer(owner);
            bool addingCorrectconsumer = s.AddConsumer(newUser);
            bool addingConsumerTwice = s.AddConsumer(newUser);

            Assert.IsFalse(addingOwnerAsConsumer);
            Assert.IsTrue(addingCorrectconsumer);
            Assert.IsFalse(addingConsumerTwice);
        }

        [TestMethod()]
        public void RemoveConsumerTest()
        {
            Secret s = s_a;
            User consumer = new User("Consumer", "consumer@user.com", "P@ssword");

            // Add consumer.
            Assert.IsTrue(s.UsersWithAccess() == 0);
            bool consumerAdded = s.AddConsumer(consumer);
            Assert.IsTrue(consumerAdded);
            Assert.IsTrue(s.UsersWithAccess() == 1);

            // Remove consumer.
            bool consumerRemoved = s.RemoveConsumer(consumer);
            Assert.IsTrue(consumerRemoved);
            Assert.IsTrue(s.UsersWithAccess() == 0);

            // Try to remove secret owner.
            User owner = s.Owner;
            bool ownerRemoved = s.RemoveConsumer(owner);
            Assert.IsFalse(ownerRemoved);
        }

        [TestMethod()]
        public void GetMesssageTest()
        {
            string secretText = "This is a secret tsst 🤫";
            Secret secret = new Secret("Secret", secretText, owner);

            Assert.AreEqual(secretText, secret.GetMesssage());

            // Testing longer string.
            secretText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            secret = new Secret("Secret", secretText, owner);

            Assert.AreEqual(secretText, secret.GetMesssage());

        }

        [TestMethod()]
        public void EqualsTest()
        {
            // Without changing anything they should be diferent. This is
            // because Id is read only and only parameter in comparassion.
            bool ab_areEquals = s_a.Equals(s_b);
            Trace.WriteLine(s_a.Id);
            Trace.WriteLine(s_b.Id);
            Assert.IsFalse(ab_areEquals);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Secret s = s_a;

            int correctHashCode = HashCode.Combine(s.Title);
            int wrongHashCode = HashCode.Combine(s.Message);
            int secretrHashCode = s.GetHashCode();

            Assert.AreEqual(correctHashCode, secretrHashCode);
            Assert.AreNotEqual(correctHashCode, wrongHashCode);
        }

        [TestMethod()]
        public void CompareToTest()
        {
            Secret first = s_a;
            Secret second = s_b;

            first.Title = "a";
            second.Title = "b";

            bool aIsFirst = first.CompareTo(second) < 0;
            Assert.IsTrue(aIsFirst);

            second.Title = "a";
            bool aSameB = first.CompareTo(second) == 0;
            Assert.IsTrue(aSameB);
        }

        [TestMethod()]
        public void HasAccessTest()
        {
            User consumer = new User("Consumer", "consumer@ubusecret.es", "P@ssword2");
            s_a.AddConsumer(consumer);
            Assert.IsTrue(s_a.HasAccess(consumer));
        }
    }
}