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
            throw new NotImplementedException();
        }

        public bool AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Secret GetSecret(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string email)
        {

            throw new NotImplementedException();
        }

        public List<User> ListActiveUsers()
        {
            throw new NotImplementedException();
        }

        public List<Secret> ListOwnSecrets(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> ListPendientUsers()
        {
            throw new NotImplementedException();
        }

        public List<Secret> ListReceivedSecrets(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> ListUnactiveUsers()
        {
            throw new NotImplementedException();
        }

        public Secret RemoveSecret(int id)
        {
            throw new NotImplementedException();
        }

        public User RemoveUser(string email)
        {
            throw new NotImplementedException();
        }
    }
}
