using Main;
using System;
using System.Collections.Generic;


namespace DataAPI
{
    public interface IDB
    {
        // RF03
        bool AddUser(User user);

        // RF04
        User GetUser(String email);

        // RF05
        User RemoveUser(String email);

        // RF06
        /// <summary>
        /// Devuelve un listado de los usuarios activos
        /// </summary>
        /// <returns>Lista con los usuarios activos (List\<User\>) </returns>
        List<User> ListActiveUsers();
        List<User> ListUnactiveUsers();
        List<User> ListPendientUsers();

        // RF07
        bool AddSecret(Secret secret);

        // RF08
        Secret GetSecret(int id);

        // RF09
        Secret RemoveSecret(int id);

        // RF10
        List<Secret> ListOwnSecrets(User user);
        List<Secret> ListReceivedSecrets(User user);
    }
}
