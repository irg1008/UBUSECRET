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
            Assert.AreEqual(secret.Id, db.GetSecret(secret.Id).Id,
                ">> No se ha encontrado el secreto añadido.");
        }

        [TestMethod()]
        public void GetSecretTest()
        {
            Assert.AreEqual(null, db.GetSecret(secret.Id),
                ">> El secreto no debería de poder encontrarse.");
            db.AddSecret(secret);
            Assert.AreEqual(secret.Id, db.GetSecret(secret.Id).Id,
                ">> No se ha podido encontrar el secreto.");
        }

        [TestMethod()]
        public void ListActiveUsersTest()
        {
            User[] users = new User[] {
                new User("user0", "user@example0.com", "P@ssword2"),
                new User("user1", "user@example1.com", "P@ssword2"),
                new User("user2", "user@example2.com", "P@ssword2"),
                new User("user3", "user@example3.com", "P@ssword2"),
                new User("user4", "user@example4.com", "P@ssword2"),
                new User("user5", "user@example5.com", "P@ssword2"),
            };

            foreach (User valor in users)
                valor.Request();

            users[0].Activate();
            users[1].Activate();
            users[2].Activate();
            users[3].Activate();

            users[0].Unactivate();
            users[1].Unactivate();

            foreach (User valor in users)
                Assert.IsTrue(db.AddUser(valor), 
                    ">> No se ha podido añadir el usuario.");

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
            User[] users = new User[] {
                new User("user0", "user@example0.com", "P@ssword2"),
                new User("user1", "user@example1.com", "P@ssword2"),
                new User("user2", "user@example2.com", "P@ssword2"),
                new User("user3", "user@example3.com", "P@ssword2"),
                new User("user4", "user@example4.com", "P@ssword2"),
                new User("user5", "user@example5.com", "P@ssword2"),
            };

            foreach (User valor in users)
                valor.Request();

            users[0].Activate();
            users[1].Activate();
            users[2].Activate();
            users[3].Activate();

            users[0].Unactivate();
            users[1].Unactivate();

            foreach (User valor in users)
                Assert.IsTrue(db.AddUser(valor),
                    ">> No se ha podido añadir el usuario.");

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
            User[] users = new User[] {
                new User("user0", "user@example0.com", "P@ssword2"),
                new User("user1", "user@example1.com", "P@ssword2"),
                new User("user2", "user@example2.com", "P@ssword2"),
                new User("user3", "user@example3.com", "P@ssword2"),
                new User("user4", "user@example4.com", "P@ssword2"),
                new User("user5", "user@example5.com", "P@ssword2"),
            };

            foreach (User valor in users)
                valor.Request();

            users[0].Activate();
            users[1].Activate();
            users[2].Activate();
            users[3].Activate();

            users[0].Unactivate();
            users[1].Unactivate();

            foreach (User valor in users)
                Assert.IsTrue(db.AddUser(valor),
                    ">> No se ha podido añadir el usuario.");

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
            User owner = new User("Owner", "owner@ofsecrets.com", "P@ssword2");

            // Recibimos la lista de secretos del usuario.
            List<Secret> ownedSecretsFromDB = db.ListOwnSecrets(owner);

            // Comprobamos que está vacía.
            Assert.AreEqual(ownedSecretsFromDB.Count, 0, "The list of secrets should be empty at start");

            // Insertamos varios secretos con el usuario como dueño.
            // Cambiar esto a DDT.
            List<Secret> ownedSecrets = new List<Secret> {
                new Secret("Secreto 1", "Mensaje 1", owner),
                new Secret("Secreto 2", "Mensaje 2", owner),
                new Secret("Secreto 3", "Mensaje 3", owner),
            };

            foreach (Secret s in ownedSecrets)
            {
                db.AddSecret(s);
            }

            // Recibimos la lista de nuevo.
            ownedSecretsFromDB = db.ListOwnSecrets(owner);

            // Comprobamos que es igual a la insertada.
            Assert.AreEqual(ownedSecrets, ownedSecretsFromDB, "Inserted owner secrets and recieved are not equal");

            // Recuperamos secretos de un usuario nulo.
            List<Secret> nullOwnerSecrets = db.ListOwnSecrets(null);
            Assert.IsNull(nullOwnerSecrets, "Cannot pass a null user to fetch his secrets");
        }

        [TestMethod()]
        public void ListReceivedSecretsTest()
        {
            User guest = new User("Guest", "Guest@ofsecrets.com", "P@ssword2");

            // Recibimos la lista de secretos accesibles por el usuario.
            List<Secret> recievedSecretsFromDB = db.ListReceivedSecrets(guest);

            // Comprobamos que está vacía.
            Assert.AreEqual(recievedSecretsFromDB.Count, 0, "The list of secrets should be empty at start");

            // Insertamos varios secretos con el usuario como invitado.
            // Cambiar esto a DDT.
            List<Secret> recievedSecrets = new List<Secret> {
                new Secret("Secreto 1", "Mensaje 1", this.user),
                new Secret("Secreto 2", "Mensaje 2", this.user),
                new Secret("Secreto 3", "Mensaje 3", this.user),
            };

            foreach (Secret s in recievedSecrets)
            {
                // Añadimos el invitado a los secretos antes de insertar.
                s.AddConsumer(guest);
                db.AddSecret(s);
            }

            // Recibimos la lista de nuevo.
            recievedSecretsFromDB = db.ListOwnSecrets(guest);

            // Comprobamos que es igual a la insertada.
            Assert.AreEqual(recievedSecrets, recievedSecretsFromDB, "Inserted guest secrets and recieved are not equal");

            // Recuperamos secretos de un usuario nulo.
            List<Secret> nullOwnerSecrets = db.ListReceivedSecrets(null);
            Assert.IsNull(nullOwnerSecrets, "Cannot pass a null user to fetch recieved secrets");
        }

        [TestMethod()]
        public void RemoveSecretTest()
        {
            Secret s = this.secret;

            // Insertamos el secreto.
            db.AddSecret(s);

            // Eliminamos el secreto.
            Secret deletedSecret = db.RemoveSecret(s.Id);
            Assert.AreEqual(s, deletedSecret, "The returned secret is not consistant with the inserted one");

            // Comprobamos que está eliminado.
            Secret deletedSecretDB = db.GetSecret(deletedSecret.Id);
            Assert.IsNull(deletedSecretDB, "The secret is not being deleted from DB");
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

            // Eliminación con id vacío o nulo.
            Secret emptyIdSecret = db.RemoveSecret(0);
            Assert.IsNull(emptyIdSecret, "Attempt to remove a user with id 0 should return null");
            Secret minusIdSecret = db.RemoveSecret(-1);
            Assert.IsNull(minusIdSecret, "Attempt to remove a secret with id less than 0 should return null");
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

            // Insertamos un secreto con el usuario como dueño, para comprobar que se elimina al eliminar este.
            Secret tmpSecret = new Secret("Tmp secret", "", u);
            db.AddSecret(tmpSecret);

            // Eliminamos el usuario.
            User deletedUser = db.RemoveUser(u.Email);
            Assert.AreEqual(u, deletedUser, "The returned user is not consistent with the inserted one");

            // Comprobamos que está eliminado.
            User deletedUserDB = db.GetUser(deletedUser.Email);
            Assert.IsNull(deletedUserDB, "The user is not being deleted from DB");

            // Comprobamos que el secreto se ha eliminado también.
            Secret deletedSecret = db.GetSecret(tmpSecret.Id);
            Assert.IsNull(deletedSecret, "Secrets owned by the user to be deleted should be deleted as well");

            // Eliminación con email vacío o nulo.
            User emptyEmailUser = db.RemoveUser(u.Email);
            Assert.IsNull(emptyEmailUser, "Attempt to remove a user with empty email should return null");
            User nullEmailUser = db.RemoveUser(null);
            Assert.IsNull(nullEmailUser, "Attempt to remove a user with null email should return null");
        }
    }
}
