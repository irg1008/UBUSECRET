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
            try
            {
                return this.secrets.Find(secret => secret.Id == id);
            }
            catch
            {
                return null;
            }
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
            return this.users.FindAll(user => user.State == State.ACTIVE);
        }

        public List<Secret> ListOwnSecrets(User user)
        {
            List<Secret> retorno = new List<Secret>();

            foreach (Secret valor in this.secrets)
                if (valor.Owner.Email == user.Email)
                    retorno.Add(valor);

            return retorno;
        }

        public List<User> ListPendientUsers()
        {
            return this.users.FindAll(user => user.State == State.REQUESTED);
        }

        public List<Secret> ListReceivedSecrets(User user)
        {
            List<Secret> retorno = new List<Secret>();

            foreach (Secret secret in this.secrets)
                foreach (User consumer in secret.Consumers)
                    if (consumer.Email == user.Email)
                        retorno.Add(secret);

            return retorno;
        }

        public List<User> ListUnactiveUsers()
        {
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
            foreach (User valor in this.users)
                if (valor.Email == email)
                {
                    this.users.Remove(valor);
                    return valor;
                }

            return null;
        }
    }
}
