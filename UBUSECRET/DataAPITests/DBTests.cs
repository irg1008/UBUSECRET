using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAPI;
using System;
using System.Collections.Generic;
using System.Text;
using Main;

namespace DataAPI.Tests
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
            db = new DataAPI.DB();
            user = new User("User", "user@ubusecret.com", "P@ssw0rd");
            secret = new Secret("Secret", "Hidden Message", user);
        }

        [TestCleanup]
        public void CleanUp() 
        {
            user = null;
            secret = null;
            db = null;
        }

        [TestMethod()]
        public void addSecretTest()
        {
            Assert.IsTrue(db.addSecret(secret), 
                ">> No se ha podido añadir el secreto.");
            Assert.AreEqual(secret.Id, db.getSecret(secret.Id),
                ">> No se ha encontrado el secreto añadido.");
        }

        [TestMethod()]
        public void getSecretTest()
        {
            Assert.AreEqual(null, db.getSecret(secret.Id),
                ">> El secreto no debería de poder encontrarse.");
            db.addSecret(secret);
            Assert.AreEqual(secret.Id, db.getSecret(secret.Id),
                ">> No se ha podido encontrar el secreto.");
        }

        [TestMethod()]
        public void listActiveUsersTest()
        {
            List<User> lau = db.listActiveUsers();
            
            foreach(User valor in lau)
            {
                Assert.AreEqual(valor.State, State.ACTIVE, 
                    ">> Intruso entre los usuarios activos.");
            }

            int c = 0;
            foreach(User valor in db.Users)
            {
                if(valor.State == State.ACTIVE) { c += 1; };
            }

            Assert.AreEqual(c, lau.Count, 
                ">> No coinciden las cantidades de usuarios activos.");
        }

        [TestMethod()]
        public void listInactiveUsersTest()
        {
            List<User> liu = db.listInactiveUsers();

            foreach (User valor in liu)
            {
                Assert.AreEqual(valor.State, State.INACTIVE,
                    ">> Intruso entre los usuarios inactivos.");
            }

            int c = 0;
            foreach (User valor in db.Users)
            {
                if (valor.State == State.INACTIVE) { c += 1; };
            }

            Assert.AreEqual(c, liu.Count,
                ">> No coinciden las cantidades de usuarios inactivos.");
        }


        [TestMethod()]
        public void listPendientUsersTest()
        {
            List<User> lpu = db.listPendientUsers();

            foreach (User valor in lpu)
            {
                Assert.AreEqual(valor.State, State.REQUESTED,
                    ">> Intruso entre los usuarios pendientes de validar.");
            }

            int c = 0;
            foreach (User valor in db.Users)
            {
                if (valor.State == State.REQUESTED) { c += 1; };
            }

            Assert.AreEqual(c, lpu.Count,
                ">> No coinciden las cantidades de usuarios pendientes de validar.");
        }

        [TestMethod()]
        public void listOwnSecretsTest()
        {
            Assert.Fail(); //TODO
        }

        [TestMethod()]
        public void listReceivedSecretsTest()
        {
            Assert.Fail(); //TODO
        }

        [TestMethod()]
        public void removeSecretTest()
        {
            Assert.Fail(); //TODO
        }
    }
}