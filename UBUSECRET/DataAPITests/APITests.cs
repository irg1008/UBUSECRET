using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAPI;
using Main;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

/*
@"{
'Nombre': 'user',
'Email': 'user@example.com',
'LastIP': '0.0.0.0',
'LastSeen': '2021-12-24T01:58:28.5154311+01:00',
'IsAdmin': false,
'State': 2,
}"
SECRET = @"{
'Title': ,
'Owner': ,
'Consumers': ,
'Message': 
}
*/
namespace DataAPI.Tests
{
    [TestClass()]
    public class APITests
    {
        String userJSON;
        String secretJSON;

        [TestInitialize()]
        public void StartUp()
        {
            this.userJSON = @"{
                'Nombre': 'user',
                'Email': 'user@example.com',
                'LastIP': '0.0.0.0',
                'LastSeen': '2021-12-24T01:58:28.5154311+01:00',
                'IsAdmin': false,
                'State': 2,
                }";
            this.secretJSON = @"{
                'Title': 'secret',
                'Owner': {
                    'Nombre': 'user',
                    'Email': 'user@example.com',
                    'LastIP': '0.0.0.0',
                    'LastSeen': '2021-12-24T01:58:28.5154311+01:00',
                    'IsAdmin': false,
                    'State': 2,
                    },
                'Consumers': [],
                'Message': 'Example secret'
                }";

            for (int i = 0; i < 3; i++)
            {
                User user = new User("userb" + i, "user" + i + "@ubusecret.com", "P@ssword" + i);
                API.db.AddUser(user);
                API.db.AddSecret(new Secret("secretb" + i, "Secret base " + i, user));
            }
        }

        [TestCleanup()]
        public void CleanUp()
        {
            this.userJSON = null;
        }

        [TestMethod()]
        public void AddUserTest()
        {
            // Insertamos usuario.
            bool inserted = API.AddUser(userJSON);
            Assert.IsTrue(inserted, "You should be able to insert a new user");

            // Volvemos a insertar el usuario.
            bool insertedTwice = API.AddUser(userJSON);
            Assert.IsFalse(insertedTwice, "Same user cannot be inserted twice");

            // Insertamos un usuario vacio.
            bool insertedNull = API.AddUser("");
            Assert.IsFalse(insertedNull, "Null or empty users are not allowed");

            // Eliminación con id vacío o nulo.
            string emptyIdSecret = API.RemoveSecret(0);
            Assert.IsNull(emptyIdSecret, "Attempt to remove a user with id 0 should return null");
            string minusIdSecret = API.RemoveSecret(-1);
            Assert.IsNull(minusIdSecret, "Attempt to remove a secret with id less than 0 should return null");
        }

        [TestMethod()]
        public void GetUserTest()
        {
            String email = "user@example.com";
            // Comprobamos que el usuario no existe buscándolo por email.
            string userWithEmail = API.GetUser(email);
            Assert.IsNull(userWithEmail, "If email does not exist, should return null user");

            // Insertamos el usuario.
            API.AddUser(userJSON);

            // Comprobamos que ahora existe.
            userWithEmail = API.GetUser(email);
            Assert.AreEqual(userJSON, userWithEmail, "The method does not return the corerct user");

            // Buscamos un usuario con un email vacío y nulo.
            String emptyEmailUser = API.GetUser("");
            Assert.IsNull(emptyEmailUser, "Empty email should return a null user");
            String nullEmailUser = API.GetUser(null);
            Assert.IsNull(nullEmailUser, "Null email should return null user");
        }

        [TestMethod()]
        public void RemoveUserTest()
        {
            // Insertamos el usuario.
            API.AddUser(userJSON);

            User user = new User();
            user = user.From_JSON(API.GetUser("user@example.com"));

            // Insertamos un secreto con el usuario como dueño, para comprobar que se elimina al eliminar este.
            Secret tmpSecret = new Secret("Tmp secret", "", user);
            API.db.AddSecret(secretJSON);

            // Eliminamos el usuario.
            String deletedUserJSON = API.RemoveUser(user.Email);
            User deletedUser = user.From_JSON(deletedUserJSON);
            Assert.AreEqual(user.Email, deletedUser.Email, "The returned user is not consistent with the inserted one");

            // Comprobamos que está eliminado.
            String deletedUserDB = API.GetUser(deletedUser.Email);
            Assert.IsNull(deletedUserDB, "The user is not being deleted from DB");

            // Comprobamos que el secreto se ha eliminado también.
            String deletedSecret = API.GetSecret(tmpSecret.Id);
            Assert.IsNull(deletedSecret, "Secrets owned by the user to be deleted should be deleted as well");

            // Eliminación con email vacío o nulo.
            String emptyEmailUser = API.RemoveUser(user.Email);
            Assert.IsNull(emptyEmailUser, "Attempt to remove a user with empty email should return null");
            String nullEmailUser = API.RemoveUser(null);
            Assert.IsNull(nullEmailUser, "Attempt to remove a user with null email should return null");
        }

        [TestMethod()]
        [DataRow(3, 3, 0)]
        [DataRow(0, 3, 3)]
        [DataRow(3, 0, 3)]
        [DataRow(0, 0, 6)]
        [DataRow(6, 0, 0)]
        [DataRow(0, 6, 0)]
        [DataRow(2, 2, 2)]
        public void ListActiveUsersTest(int nAct, int nInact, int nPend)
        {
            List<User> users = new List<User>();

            for (int i = 0; i < nAct + nInact + nPend; i++)
                users.Add(new User("user" + i, "user" + i + "@ubusecret.com", "P@ssword" + i));

            foreach (User valor in users)
                valor.Request();

            for (int i = 0; i < nAct + nInact; i++)
            {
                users[i].Authorize();
                users[i].Activate();
            }

            for (int i = 0; i < nInact; i++)
                users[i].Unactivate();

            foreach (User valor in users)
                API.AddUser(valor.To_JSON());

            String lpu = API.ListActiveUsers();
            List<User> lista = JsonConvert.DeserializeObject<List<User>>(lpu);

            foreach (User valor in lista)
            {
                Assert.AreEqual(valor.State, State.ACTIVE,
                    ">> Intruso entre los usuarios activos.");
            }

            Assert.AreEqual(nAct, lista.Count,
                ">> No coinciden las cantidades de usuarios activos.");
        }

        [TestMethod()]
        [DataRow(3, 3, 0)]
        [DataRow(0, 3, 3)]
        [DataRow(3, 0, 3)]
        [DataRow(0, 0, 6)]
        [DataRow(6, 0, 0)]
        [DataRow(0, 6, 0)]
        [DataRow(2, 2, 2)]
        public void ListUnactiveUsersTest(int nAct, int nInact, int nPend)
        {
            List<User> users = new List<User>();

            for (int i = 0; i < nAct + nInact + nPend; i++)
                users.Add(new User("user" + i, "user" + i + "@ubusecret.com", "P@ssword" + i));

            foreach (User valor in users)
                valor.Request();

            for (int i = 0; i < nAct + nInact; i++)
            {
                users[i].Authorize();
                users[i].Activate();
            }

            for (int i = 0; i < nInact; i++)
                users[i].Unactivate();

            foreach (User valor in users)
                API.AddUser(valor.To_JSON());

            String lpu = API.ListUnactiveUsers();
            List<User> lista = JsonConvert.DeserializeObject<List<User>>(lpu);

            foreach (User valor in lista)
            {
                Assert.AreEqual(valor.State, State.INACTIVE,
                    ">> Intruso entre los usuarios inactivos.");
            }

            Assert.AreEqual(nInact, lista.Count,
                ">> No coinciden las cantidades de usuarios inactivos.");
        }

        [TestMethod()]
        [DataRow(3, 3, 0)]
        [DataRow(0, 3, 3)]
        [DataRow(3, 0, 3)]
        [DataRow(0, 0, 6)]
        [DataRow(6, 0, 0)]
        [DataRow(0, 6, 0)]
        [DataRow(2, 2, 2)]
        public void ListPendientUsersTest(int nAct, int nInact, int nPend)
        {
            List<User> users = new List<User>();

            for (int i = 0; i < nAct + nInact + nPend; i++)
                users.Add(new User("user" + i, "user" + i + "@ubusecret.com", "P@ssword" + i));

            foreach (User valor in users)
                valor.Request();

            for (int i = 0; i < nAct + nInact; i++)
            {
                users[i].Authorize();
                users[i].Activate();
            }

            for (int i = 0; i < nInact; i++)
                users[i].Unactivate();

            foreach (User valor in users)
                API.AddUser(valor.To_JSON());

            String lpu = API.ListPendientUsers();
            List<User> lista = JsonConvert.DeserializeObject<List<User>>(lpu);

            foreach (User valor in lista)
            {
                Assert.AreEqual(valor.State, State.REQUESTED,
                    ">> Intruso entre los usuarios pendientes.");
            }

            Assert.AreEqual(nPend, lista.Count,
                ">> No coinciden las cantidades de usuarios pendientes.");
        }

        [TestMethod()]
        public void AddSecretTest()
        {
            User owner = new User("owner", "o@ub.es", "P@ssword2");
            Secret toAdd = new Secret("byebye", "see ya' loser", owner);

            // Get secret JSON.
            string toAddJSON = toAdd.To_JSON();

            // Insert secret with API.
            API.AddSecret(toAdd.Id, toAddJSON);

            // Get the user in the db to ensure is inserted.
            Secret added = API.db.GetSecret(toAdd.Id);

            Assert.AreEqual(toAddJSON, added.To_JSON());
        }

        [TestMethod()]
        public void GetSecretTest()
        {
            User owner = new User("owner", "o@ub.es", "P@ssword2");
            Secret toGet = new Secret("byebye", "see ya' loser", owner);

            // Get secret JSON.
            string toGetJSON = toGet.To_JSON();

            // Insert secret.
            API.db.AddSecret(toGet);

            // Get secret with API.
            string fetchedJSON = API.GetSecret(toGet.Id);

            // Check fetched equals inserted.
            Assert.AreEqual(toGetJSON, fetchedJSON);
        }

        [TestMethod()]
        public void RemoveSecretTest()
        {
            User owner = new User("owner", "o@ub.es", "P@ssword2");
            Secret toBeRemoved = new Secret("byebye", "see ya' loser", owner);

            // Get secret JSON.
            string toBeRemovedJSON = toBeRemoved.To_JSON();

            // Insert secret to db, then delete.
            API.db.AddSecret(toBeRemoved);

            // Get from API.
            string removedJSON = API.RemoveSecret(toBeRemoved.Id);

            // Check it has been removed.
            Secret removed = API.db.RemoveSecret(toBeRemoved.Id);
            Assert.IsNull(removed, "API method should remove the secret");

            // Check recieved json and secret json are equal.
            Assert.AreEqual(toBeRemovedJSON, removedJSON, "The returned and removed secret should be the same as original");
        }

        [TestMethod()]
        public void ListOwnSecretsTest()
        {
            User owner = new User("owner", "o@ub.es", "P@ssword2");
            API.db.AddUser(owner);

            List<Secret> secrets = new List<Secret> {
                new Secret("Secreto 1", "Mensaje 1", owner),
                new Secret("Secreto 2", "Mensaje 2", owner),
                new Secret("Secreto 3", "Mensaje 3", owner),
            };

            foreach (Secret s in secrets)
            {
                API.db.AddSecret(s);
            }

            // Expected JSON from API.
            string expectedJSON = JsonConvert.SerializeObject(secrets);

            // Get json from API.
            string ownedJSON = API.ListOwnSecrets(owner.Email);

            // Assert same JSON.
            Assert.AreEqual(expectedJSON, ownedJSON, "Owned secrets should give same JSON");
        }

        [TestMethod()]
        public void ListReceivedSecretsTest()
        {
            User owner = new User("owner", "o@ub.es", "P@ssword2");
            User guest = new User("guest", "u@ub.es", "P@ssword2");
            API.db.AddUser(owner);
            API.db.AddUser(guest);

            List<Secret> secrets = new List<Secret> {
                new Secret("Secreto 1", "Mensaje 1", owner),
                new Secret("Secreto 2", "Mensaje 2", owner),
                new Secret("Secreto 3", "Mensaje 3", owner),
            };

            foreach (Secret s in secrets)
            {
                // Añadimos el invitado a los secretos antes de insertar.
                s.AddConsumer(guest);
                API.db.AddSecret(s);
            }

            // Expected JSON from API.
            string expectedJSON = JsonConvert.SerializeObject(secrets);

            // Get json from API.
            string receivedJSON = API.ListReceivedSecrets(guest.Email);

            // Assert same JSON.
            Assert.AreEqual(expectedJSON, receivedJSON, "Received secrets should give same JSON");
        }
    }
}