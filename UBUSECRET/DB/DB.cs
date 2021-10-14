using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Main;

namespace DataAccess
{
    public class DB : IDB
    {
        private int currentUser = -1;
        private int currentSecret = -1;
        private readonly SortedList<int, User> tblUsers = new SortedList<int, User>();
        private readonly SortedList<int, Secret> tblSecrets = new SortedList<int, Secret>();

        public DB()
        {
            // Inicilización de los elementos de la base de datos
            User uAdmin = new User("Administrador", "Administración", "admin@ubusecret.es", "Password");
            uAdmin.MakeAdmin();
            uAdmin.Activate();
            uAdmin.ChangePassword("Password", "P@ssword");
            InsertUser(uAdmin);
        }

        public bool ContainsSecret(Secret secret)
        {
            if (secret is null) return false;
            return ContainsSecret(secret.Id);
        }

        public bool ContainsSecret(int id)
        {
            return tblSecrets.ContainsKey(id);
        }

        public bool ContainsUser(User user)
        {
            if (user is null) return false;
            return ContainsUser(user.Id);
        }

        public bool ContainsUser(int id)
        {
            return tblUsers.ContainsKey(id);
        }

        public bool ContainsUser(string email)
        {
            User u = ReadUser(email);
            return ContainsUser(u);
        }

        public bool DeleteSecret(int id)
        {
            return tblSecrets.Remove(id);
        }

        public bool DeleteSecret(Secret secret)
        {
            if (!ContainsSecret(secret)) return false;
            return DeleteSecret(secret.Id);
        }

        public bool DeleteUser(int id)
        {
            User u = ReadUser(id);
            return DeleteUser(u);
        }

        public bool DeleteUser(User user)
        {
            if (!ContainsUser(user)) return false;

            // Delete entries for user in secrets.
            IList<Secret> secrets = tblSecrets.Values;

            foreach (Secret secret in secrets)
            {
                bool isConsumer = secret.RemoveConsumer(user);

                // Delete secret if owner.
                if (!isConsumer && secret.IsOwner(user))
                    DeleteSecret(secret);
            }

            return tblUsers.Remove(user.Id);
        }

        public bool DeleteUser(string email)
        {
            User u = ReadUser(email);
            return DeleteUser(u);
        }

        public bool InsertSecret(Secret secret)
        {
            if (ContainsSecret(secret) || secret is null) return false;
            tblSecrets.Add(secret.Id, secret);
            return true;
        }

        public bool InsertUser(User user)
        {
            if (ContainsUser(user) || user is null) return false;
            tblUsers.Add(user.Id, user);
            return true;
        }

        private bool IndexOutsideSecrets(int index)
        {
            return index < 0 || index >= SecretCount();
        }

        private bool IndexOutsideUsers(int index)
        {
            return index < 0 || index >= UserCount();
        }

        public Secret NextSecret()
        {
            if (IndexOutsideSecrets(currentSecret + 1)) return null;
            currentSecret++;
            Console.WriteLine(currentSecret);
            return tblSecrets.ElementAt(currentSecret).Value;
        }

        public User NextUser()
        {
            if (IndexOutsideUsers(currentUser + 1)) return null;
            currentUser++;
            return tblUsers.ElementAt(currentUser).Value;
        }

        public Secret PreviousSecret()
        {
            if (IndexOutsideSecrets(currentSecret - 1)) return null;
            currentSecret--;
            return tblSecrets.ElementAt(currentSecret).Value;
        }

        public User PreviousUser()
        {
            if (IndexOutsideUsers(currentUser - 1)) return null;
            currentUser--;
            return tblUsers.ElementAt(currentUser).Value;
        }

        public Secret ReadSecret(int id)
        {
            if (!ContainsSecret(id)) return null;
            return tblSecrets[id];
        }

        public Secret ReadSecret(Secret secret)
        {
            if (!ContainsSecret(secret)) return null;
            return ReadSecret(secret.Id);
        }

        public User ReadUser(int id)
        {
            if (!ContainsUser(id)) return null;
            return tblUsers[id];
        }

        public User ReadUser(string email)
        {
            var userList = tblUsers.Where(user => user.Value.Email == email);
            if (userList.Count() == 0) return null;
            User u = userList.First().Value;
            return ReadUser(u);
        }

        public User ReadUser(User user)
        {
            if (!ContainsUser(user)) return null;
            return ReadUser(user.Id);
        }

        public int SecretCount()
        {
            return tblSecrets.Count;
        }

        public bool UpdateSecret(Secret secret)
        {
            bool containsSecret = ContainsSecret(secret);
            if (containsSecret) tblSecrets[secret.Id] = secret;
            return containsSecret;
        }

        public bool UpdateUser(User user)
        {
            bool containsUser = ContainsUser(user);
            if (containsUser) tblUsers[user.Id] = user;
            return containsUser;
        }

        public int UserCount()
        {
            return tblUsers.Count;
        }
    }
}