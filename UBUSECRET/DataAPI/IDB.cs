using Main;
using System;
using System.Collections.Generic;


namespace DataAPI
{
    public interface IDB
    {
        List<User> listUsers();

        List<User> listActiveUsers();

        List<User> listUnactiveUsers();

        bool addSecret(Secret secret);

        bool getSecret(int id);

        Secret removeSecret(int id);

        List<Secret> listSecrets();

        List<Secret> listOwnSecrets(User user);

        List<Secret> listReceivedSecrets(User user);
    }
}
