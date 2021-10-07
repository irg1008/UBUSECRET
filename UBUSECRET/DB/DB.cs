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
        private int currentUser = 0;
        private int currentSecret = 0;
        private readonly SortedList<Guid, User> tblUsers = new SortedList<Guid, User>();
        private readonly SortedList<Guid, Secret> tblSecrets = new SortedList<Guid, Secret>();

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
            return ContainsSecret(secret.Id);
        }

        public bool ContainsSecret(Guid id)
        {
            return tblSecrets.ContainsKey(id);
        }

        public bool ContainsUser(User user)
        {
            return ContainsUser(user.Id);
        }

        public bool ContainsUser(Guid id)
        {
            return tblUsers.ContainsKey(id);
        }

        public bool ContainsUser(string email)
        {
            User u = ReadUser(email);
            return ContainsUser(u);
        }

        public bool DeleteSecret(Guid id)
        {
            return tblSecrets.Remove(id);
        }

        public bool DeleteSecret(Secret secret)
        {
            return DeleteSecret(secret.Id);
        }

        public bool DeleteUser(Guid id)
        {
            User u = ReadUser(id);

            // Delete entries for user in secrets.
            IList<Secret> secrets = tblSecrets.Values;

            foreach (Secret secret in secrets)
            {
                bool isConsumer = secret.RemoveConsumer(u);

                // Delete secret if owner.
                if (!isConsumer && secret.IsOwner(u))
                    DeleteSecret(secret);
            }

            return DeleteUser(u);
        }

        public bool DeleteUser(User user)
        {
            return tblUsers.Remove(user.Id);
        }

        public bool DeleteUser(string email)
        {
            User u = ReadUser(email);
            return DeleteUser(u);
        }

        public bool InsertSecret(Secret secret)
        {
            tblSecrets.Add(secret.Id, secret);
            return true;
        }

        public bool InsertUser(User user)
        {
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

        public Secret ReadSecret(Guid id)
        {
            return tblSecrets[id];
        }

        public Secret ReadSecret(Secret secret)
        {
            return ReadSecret(secret.Id);
        }

        public User ReadUser(Guid id)
        {
            return tblUsers[id];
        }

        public User ReadUser(string email)
        {
            User u = tblUsers.Where(user => user.Value.Email == email).First().Value;
            return ReadUser(u);
        }

        public User ReadUser(User user)
        {
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