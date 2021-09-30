using App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Interfaces
{
    public interface IDB
    {
        User ReadUser(Guid id);
        User DeleteUser(Guid id);
        bool InsertUser(User user);

        Secret ReadSecret(Guid id);
        Secret DeleteSecret(Guid id);
        bool insertSecret(Secret secret);

        bool attachSecret(Guid userId, Guid secretId);
        bool dettachSecret(Guid userId, Guid secretId);

        bool verifyUser(Guid userId);
        bool makeUserAdmin(Guid userId);
 }
}
