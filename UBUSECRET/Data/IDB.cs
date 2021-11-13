using Main;
using System;
using System.Collections.Generic;
using Invitation;
using Log;

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

        List<User> GetRequestedUsers();
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

        List<Secret> GetUserSecrets(User user);
        List<Secret> GetInvitedSecrets(User user);
        // ----------


        // INVITATION.
        // ----------
        InvitationLink ReadInvitation(Guid id);
        InvitationLink ReadInvitation(InvitationLink link);

        bool DeleteInvitation(Guid id);
        bool DeleteInvitation(InvitationLink link);

        // No update.

        bool InsertInvitation(InvitationLink link);

        bool ContainsInvitation(Guid id);
        bool ContainsInvitation(InvitationLink link);

        int InvitationCount();

        // No next or previous.
        // ----------

        // LOG.
        // ----------
        List<LogEntry> LogList();

        bool InsertLog(LogEntry entry);

        bool ContainsLog(int id);
        bool ContainsLog(LogEntry entry);

        int LogCount();
        // ----------
    }
}
