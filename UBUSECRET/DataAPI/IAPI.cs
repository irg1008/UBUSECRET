using System;
using System.Collections.Generic;
using System.Text;

namespace DataAPI
{
    interface IAPI
    {
        // RF03
        bool AddUser(string userJSON);

        // RF04
        string GetUser(string email);

        // RF05
        string RemoveUser(string email);

        // RF06
        /// <summary>
        /// Devuelve un listado de los usuarios activos
        /// </summary>
        /// <returns>Lista con los usuarios activos (List\<User\>) </returns>
        
        // TODOS LOS STRINGS TIENEN FORMATO JSON. UNA LISTA ES PJ: [{A: 1}, {B: 2}]
        string ListActiveUsers();
        string ListUnactiveUsers();
        string ListPendientUsers();

        // RF07
        bool AddSecret(string secretJSON);

        // RF08
        string GetSecret(int id);

        // RF09
        string RemoveSecret(int id);

        // RF10
        string ListOwnSecrets(string userJSON);
        string ListReceivedSecrets(string userJSON);
    }
}
