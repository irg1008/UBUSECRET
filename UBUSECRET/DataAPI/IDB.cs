using Main;
using System;
using System.Collections.Generic;


namespace DataAPI
{
    public interface IDB
    {
        /// <summary>
        /// Devuelve un listado de los usuarios activos
        /// </summary>
        /// <returns>Lista con los usuarios activos (List\<User\>) </returns>
        List<User> listActiveUsers();

        List<User> listUnactiveUsers();

        List<User> listPendientUsers();

        bool addSecret(Secret secret);

        bool getSecret(int id);

        Secret removeSecret(int id);

        List<Secret> listOwnSecrets(User user);

        List<Secret> listReceivedSecrets(User user);
    }
}
