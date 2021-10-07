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
        readonly User owner = new User("Owner", "of secrets", "owner@user.com", "P@ssword");
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

            User wrongOwner = new User("Wrong", "Owner", "wrong@user.com", "P@ssword");
            User correctOwner = owner;

            Assert.AreNotEqual(wrongOwner, secretOwner);
            Assert.AreEqual(correctOwner, secretOwner);
        }

        [TestMethod()]
        public void AddConsumerTest()
        {
            Secret s = s_a;
            User newUser = new User("Consumer", "of Secret", "consumer@user.com", "P@ssword");

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
            User consumer = new User("Consumer", "of Secret", "consumer@user.com", "P@ssword");

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
            string decryptedText = secret.GetMesssage();

            Assert.AreEqual(secretText, decryptedText);
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
    }
}