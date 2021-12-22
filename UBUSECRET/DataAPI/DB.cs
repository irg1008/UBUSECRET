using Main;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAPI
{
    public class DB : IDB
    {
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

        public List<User> listUnactiveUsers()
        {
            throw new NotImplementedException();
        }

        public Secret removeSecret(int id)
        {
            throw new NotImplementedException();
        }
    }
}
