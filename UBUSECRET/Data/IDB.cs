using Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IDB
    {

        // USER.
        // ----------
        User ReadUser(int id);
        User ReadUser(String email);
        User ReadUser(User user);

        bool DeleteUser(int id);
        bool DeleteUser(String email);
        bool DeleteUser(User user);

        bool UpdateUser(User user);

        bool InsertUser(User user);

        bool ContainsUser(User user);
        bool ContainsUser(int id);
        bool ContainsUser(String email);

        int UserCount();

        User NextUser();
        User PreviousUser();
        // ----------


        // SECRET.
        // ----------
        Secret ReadSecret(int id);
        Secret ReadSecret(Secret secret);

        bool DeleteSecret(int id);
        bool DeleteSecret(Secret secret);

        bool UpdateSecret(Secret secret);

        bool InsertSecret(Secret secret);

        bool ContainsSecret(Secret secret);
        bool ContainsSecret(int id);

        int SecretCount();

        Secret NextSecret();
        Secret PreviousSecret();
        // ----------

        List<Secret> GetUserSecrets(User user);
        List<Secret> GetInvitedSecrets(User user);
        List<User> GetRequestedUsers();
    }
}
