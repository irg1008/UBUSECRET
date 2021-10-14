using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Tests
{
    [TestClass()]
    public class ValidateTests
    {

        [TestMethod()]
        public void PasswordTest()
        {
            Assert.IsFalse(Validate.Password("Sh0rt_"));
            Assert.IsFalse(Validate.Password("Password"));
            Assert.IsFalse(Validate.Password("Passw0rd"));
            Assert.IsFalse(Validate.Password("p@ssw0rd"));
            Assert.IsTrue(Validate.Password("P@ssw0rd"));
        }

        [TestMethod()]
        public void EmailTest()
        {
            Assert.IsFalse(Validate.Email("user"));
            Assert.IsFalse(Validate.Email("user.com"));
            Assert.IsFalse(Validate.Email("user@.com"));
            Assert.IsFalse(Validate.Email("user@ubusecret."));
            Assert.IsFalse(Validate.Email("user@ubusecret."));
            Assert.IsTrue(Validate.Email("user@ubusecret.com"));
        }

        [TestMethod()]
        public void UsernameTest()
        {
            Assert.IsFalse(Validate.Username("hi"));
            Assert.IsTrue(Validate.Username("name"));
            Assert.IsFalse(Validate.Username("Name"));
            Assert.IsFalse(Validate.Username("name surname"));
            Assert.IsTrue(Validate.Username("name_surname"));
            Assert.IsFalse(Validate.Username("A_too_long_Username"));
            Assert.IsFalse(Validate.Username("#badpassword"));
        }

        [TestMethod()]
        public void IPAddressTest()
        {
            Assert.IsTrue(Validate.IPAddress("192.168.1.1"));
            Assert.IsTrue(Validate.IPAddress("127.0.0.1"));
            Assert.IsTrue(Validate.IPAddress("0.0.0.0"));
            Assert.IsTrue(Validate.IPAddress("255.255.255.255"));
            Assert.IsFalse(Validate.IPAddress("256.256.256.256"));
            Assert.IsFalse(Validate.IPAddress("999.999.999.999"));
            Assert.IsFalse(Validate.IPAddress("1.2.3"));
            Assert.IsTrue(Validate.IPAddress("1.2.3.4"));
            Assert.IsFalse(Validate.IPAddress("1a.2.3.4"));
        }

        [TestMethod()]
        public void URLTest()
        {
            Assert.IsFalse(Validate.URL("abcdef"));
            Assert.IsFalse(Validate.URL("www.whatever.com"));
            Assert.IsTrue(Validate.URL("https://www.youtube.com/watch?v=dQw4w9WgXcQ"));
            Assert.IsTrue(Validate.URL("https://www.facebook.com/"));
            Assert.IsTrue(Validate.URL("https://www.google.com/"));
            Assert.IsTrue(Validate.URL("https://example.com/1234/"));
            Assert.IsFalse(Validate.URL("https://this-shouldn't.match@example.com"));
            Assert.IsTrue(Validate.URL("http://www.example.com/"));
        }

        [TestMethod()]
        public void UUIDTest()
        {
            Assert.IsTrue(Validate.UUID("123e4567-e89b-12d3-a456-426655440000"));
            Assert.IsTrue(Validate.UUID("c73bcdcc-2669-4bf6-81d3-e4ae73fb11fd"));
            Assert.IsTrue(Validate.UUID("C73BCDCC-2669-4Bf6-81d3-E4AE73FB11FD"));
            Assert.IsFalse(Validate.UUID("c73bcdcc-2669-4bf6-81d3-e4an73fb11fd"));
            Assert.IsFalse(Validate.UUID("c73bcdcc26694bf681d3e4ae73fb11fd"));
            Assert.IsFalse(Validate.UUID("definitely-not-a-uuid"));
        }
    }
}