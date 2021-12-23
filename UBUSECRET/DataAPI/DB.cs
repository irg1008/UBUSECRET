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

        public bool addSecret(Secret secret)
        {
            throw new NotImplementedException();
        }

        public bool getSecret(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> listActiveUsers()
        {
            throw new NotImplementedException();
        }

        public List<Secret> listOwnSecrets(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> listPendientUsers()
        {
            throw new NotImplementedException();
        }

        public List<Secret> listReceivedSecrets(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> listInactiveUsers()
        {
            throw new NotImplementedException();
        }

        public Secret removeSecret(int id)
        {
            throw new NotImplementedException();
        }
    }
}
