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
            if (user == null) return false;

            foreach (User valor in this.users)
                if (valor.Email == user.Email)
                    return false;

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
            foreach (User valor in this.users)
                if (email == valor.Email)
                    return valor;

            return null;
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
            foreach (Secret valor in this.secrets)
                if (valor.Id == id)
                {
                    this.secrets.Remove(valor);
                    return valor;
                }

            return null;
        }

        public User RemoveUser(string email)
        {
            User user = this.users.Find(user => user.Email == email);

            if (user == null)
                return null;

            List<Secret> secrets = ListOwnSecrets(user);

            foreach (Secret secret in secrets) { RemoveSecret(secret.Id); };

            this.users.Remove(user);
            return user;
        }
    }
}