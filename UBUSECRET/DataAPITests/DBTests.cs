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

        [TestInitialize()]
        public void Startup()
        {
            db = new DB();
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
        public void AddSecretTest()
        {
            Assert.IsTrue(db.AddSecret(secret),
                ">> No se ha podido añadir el secreto.");
            Assert.AreEqual(secret.Id, db.GetSecret(secret.Id),
                ">> No se ha encontrado el secreto añadido.");
        }

        [TestMethod()]
        public void GetSecretTest()
        {
            Assert.AreEqual(null, db.GetSecret(secret.Id),
                ">> El secreto no debería de poder encontrarse.");
            db.AddSecret(secret);
            Assert.AreEqual(secret.Id, db.GetSecret(secret.Id),
                ">> No se ha podido encontrar el secreto.");
        }

        [TestMethod()]
        public void ListActiveUsersTest()
        {
            List<User> lau = db.ListActiveUsers();

            foreach (User valor in lau)
            {
                Assert.AreEqual(valor.State, State.ACTIVE,
                    ">> Intruso entre los usuarios activos.");
            }

            int c = 0;
            foreach (User valor in db.Users)
            {
                if (valor.State == State.ACTIVE) { c += 1; };
            }

            Assert.AreEqual(c, lau.Count,
                ">> No coinciden las cantidades de usuarios activos.");
        }

        [TestMethod()]
        public void ListUnactiveUsersTest()
        {
            List<User> liu = db.ListUnactiveUsers();

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
        public void ListPendientUsersTest()
        {
            List<User> lpu = db.ListPendientUsers();

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

        /// ^^^ TEST SIMÓN - IMPLEMENTACIÓN IVÁN ^^^
        /// vvv TEST IVÁN - IMPLEMENTACIÓN SIMÓN vvv

        [TestMethod()]
        public void ListOwnSecretsTest()
        {
            User owner = this.user;

            List<Secret> ownedSecrets = db.ListOwnSecrets(owner);

        }

        [TestMethod()]
        public void ListReceivedSecretsTest()
        {
            Assert.Fail(); //TODO
        }

        [TestMethod()]
        public void RemoveSecretTest()
        {
            Assert.Fail(); //TODO
        }

        [TestMethod()]
        public void AddUserTest()
        {
            User u = this.user;

            // Insertamos usuario.
            bool inserted = db.AddUser(u);
            Assert.IsTrue(inserted, "You should be able to insert a new user");

            // Volvemos a insertar el usuario.
            bool insertedTwice = db.AddUser(u);
            Assert.IsFalse(insertedTwice, "Same user cannot be inserted twice");

            // Insertamos un usuario nulo.
            bool insertedNull = db.AddUser(null);
            Assert.IsFalse(insertedNull, "Null or empty users are not allowed");
        }

        [TestMethod()]
        public void GetUserTest()
        {
            User u = this.user;

            // Comprobamos que el usuario no existe buscándolo por email.
            User userWithEmail = db.GetUser(u.Email);
            Assert.IsNull(userWithEmail, "If email does not exist, should return null user");

            // Insertamos el usuario.
            db.AddUser(u);

            // Comprobamos que ahora existe.
            userWithEmail = db.GetUser(u.Email);
            Assert.AreEqual(u, userWithEmail, "The method does not return the corerct user");

            // Buscamos un usuario con un email vacío y nulo.
            User emptyEmailUser = db.GetUser("");
            Assert.IsNull(emptyEmailUser, "Empty email should return a null user");
            User nullEmailUser = db.GetUser(null);
            Assert.IsNull(nullEmailUser, "Null email should return null user");
        }

        [TestMethod()]
        public void RemoveUserTest()
        {
            User u = this.user;

            // Insertamos el usuario.
            db.AddUser(u);

            // Eliminamos el usuario.
            User deletedUser = db.RemoveUser(u.Email);
            Assert.AreEqual(u, deletedUser, "The returned user is not consistent with the inserted one");

            // Eliminación con email vacío o nulo.
            User emptyEmailUser = db.RemoveUser(u.Email);
            Assert.IsNull(emptyEmailUser, "Attempt to remove a user with empty email should return null");
            User nullEmailUser = db.RemoveUser(null);
            Assert.IsNull(nullEmailUser, "Attempt to remove a user with null email should return null");
        }
    }
}
