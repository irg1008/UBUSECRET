using Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDB
    {

        // USER.
        // ----------
        User ReadUser(Guid id);
        User ReadUser(String email);
        User ReadUser(User user);

        bool DeleteUser(Guid id);
        bool DeleteUser(String email);
        bool DeleteUser(User user);

        bool UpdateUser(User user);

        bool InsertUser(User user);

        bool ContainsUser(User user);
        bool ContainsUser(Guid id);
        bool ContainsUser(String email);

        int UserCount();

        User NextUser();
        User PreviousUser();
        // ----------


        // SECRET.
        // ----------
        Secret ReadSecret(Guid id);
        Secret ReadSecret(Secret secret);

        bool DeleteSecret(Guid id);
        bool DeleteSecret(Secret secret);

        bool UpdateSecret(Secret secret);

        bool InsertSecret(Secret secret);

        bool ContainsSecret(Secret secret);
        bool ContainsSecret(Guid id);

        int SecretCount();

        Secret NextSecret();
        Secret PreviousSecret();
        // ----------
    }
}
