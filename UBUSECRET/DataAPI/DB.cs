using Main;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAPI
{
    public class DB : IDB
    {
        List<Secret> secrets;
        List<User> users;

        public DB()
        {
            this.secrets = new List<Secret>();
            this.users = new List<User>();
        }

        public List<Secret> Secrets { get => secrets; set => secrets = value; }
        public List<User> Users { get => users; set => users = value; }

        public bool AddSecret(Secret secret)
        {
            if (secret == null || this.GetSecret(secret.Id) != null) return false;
            this.secrets.Add(secret);
            return true;
        }

        public bool AddUser(User user)
        {
            if (user == null || this.GetUser(user.Email) != null) return false;
            this.users.Add(user);
            return true;
        }

        public Secret GetSecret(int id)
        {
            if (this.secrets.Count == 0) return null;
            return this.secrets.Find(secret => secret.Id == id);
        }

        public User GetUser(string email)
        {
            if (this.users.Count == 0) return null;
            return this.users.Find(user => user.Email == email);
        }

        public List<User> ListActiveUsers()
        {
            if (this.users.Count == 0) return null;
            return this.users.FindAll(user => user.State == State.ACTIVE);
        }

        public List<Secret> ListOwnSecrets(User user)
        {
            if (this.secrets.Count == 0 || user == null) return new List<Secret>();
            return this.secrets.FindAll(secret => secret.Owner.Equals(user));
        }

        public List<User> ListPendientUsers()
        {
            if (this.users.Count == 0) return null;
            return this.users.FindAll(user => user.State == State.REQUESTED);
        }

        public List<Secret> ListReceivedSecrets(User user)
        {
            if (this.secrets.Count == 0 || user == null) return new List<Secret>();
            return this.secrets.FindAll(secret => secret.Consumers.Contains(user));
        }

        public List<User> ListUnactiveUsers()
        {
            if (this.users.Count == 0) return null;
            return this.users.FindAll(user => user.State == State.INACTIVE);
        }

        public Secret RemoveSecret(int id)
        {
            Secret secretToDelete = this.GetSecret(id);
            if (secretToDelete == null) return null;
            this.secrets.Remove(secretToDelete);
            return secretToDelete;
        }

        public User RemoveUser(string email)
        {
            User user = this.GetUser(email);
            if (user == null) return null;

            // Eliminamos los secretos cuyo dueño sea el usuario eliminado.
            List<Secret> secrets = ListOwnSecrets(user);
            secrets.ForEach((secret) => RemoveSecret(secret.Id));

            this.users.Remove(user);
            return user;
        }
    }
}