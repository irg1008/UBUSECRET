using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invitation;
using Log;
using Main;

namespace Data
{
    public class DB : IDB
    {
        private int currentUser = -1;
        private int currentSecret = -1;
        private readonly SortedList<int, User> tblUsers = new SortedList<int, User>();
        private readonly SortedList<int, Secret> tblSecrets = new SortedList<int, Secret>();
        private readonly SortedList<Guid, InvitationLink> tblInvitations = new SortedList<Guid, InvitationLink>();
        private readonly SortedList<Guid, LogEntry> tblLogs = new SortedList<Guid, LogEntry>();

        private DB()
        {
            // Inicilización de los elementos de la base de datos
            User uAdmin = new User("Pepito", "admin@ubusecret.es", "P@ssword2");
            uAdmin.MakeAdmin();

            InsertUser(uAdmin);
        }

        private static DB _instance;
        private static bool _dataLoaded;

        public static DB GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DB();
                _dataLoaded = false;
            }
            return _instance;
        }

        public static void LoadSampleData()
        {
            if (_dataLoaded == false)
            {
                DB db = GetInstance();

                // User with access.
                User guest = new User("Guest", "guest@ubusecret.es", "P@ssword2");
                guest.Request();
                guest.Authorize();
                guest.Activate();
                guest.Unactivate();

                // Users hat has to be authorized and set a new password.
                User guest2 = new User("Guest 2", "guest_2@ubusecret.es", "P@ssword2");
                guest2.Request();
                // User that has to be authorized and set a new password.
                User guest3 = new User("Guest 3", "guest_3@ubusecret.es", "P@ssword2");
                guest3.Request();
                // User that has to be authorized and set a new password.
                User guest4 = new User("Guest 4", "guest_4@ubusecret.es", "P@ssword2");
                guest4.Request();

                // Sample secret of guest 1.
                Secret gSecret = new Secret("Shared secret", "This is shared secret between users.", guest);

                // Guest 1 shares secret 1 with guest 2.
                gSecret.AddConsumer(guest2);

                // We add couple of secrets to guest 2.
                Secret secretGuest2_1 = new Secret("Secret Nº1", "You should not tell this to anyone.", guest2);
                Secret secretGuest2_2 = new Secret("Secret Nº2", "No way you are able to know this.", guest2);

                // Insert all sample data to DB.
                db.InsertUser(guest);
                db.InsertUser(guest2);
                db.InsertUser(guest3);
                db.InsertUser(guest4);
                db.InsertSecret(gSecret);
                db.InsertSecret(secretGuest2_1);
                db.InsertSecret(secretGuest2_2);

                // Confirm data load.
                _dataLoaded = true;
            }
        }

        public static void Reset()
        {
            _instance = null;

        }

        public bool ContainsSecret(Secret secret)
        {
            if (secret is null) return false;
            return ContainsSecret(secret.Id);
        }

        public bool ContainsSecret(int id)
        {
            return tblSecrets.ContainsKey(id);
        }

        public bool ContainsUser(User user)
        {
            if (user is null) return false;
            return ContainsUser(user.Id);
        }

        public bool ContainsUser(int id)
        {
            return tblUsers.ContainsKey(id);
        }

        public bool ContainsUser(string email)
        {
            User u = ReadUser(email);
            return ContainsUser(u);
        }

        public bool DeleteSecret(int id)
        {
            return tblSecrets.Remove(id);
        }

        public bool DeleteSecret(Secret secret)
        {
            if (!ContainsSecret(secret)) return false;
            return DeleteSecret(secret.Id);
        }

        public bool DeleteUser(int id)
        {
            User u = ReadUser(id);
            return DeleteUser(u);
        }

        public bool DeleteUser(User user)
        {
            if (!ContainsUser(user)) return false;

            // Delete entries for user in secrets.
            IList<Secret> secrets = SecretList();

            foreach (Secret secret in secrets)
            {
                bool isConsumer = secret.RemoveConsumer(user);

                // Delete secret if owner.
                if (!isConsumer && secret.IsOwner(user))
                    DeleteSecret(secret);
            }

            return tblUsers.Remove(user.Id);
        }

        public bool DeleteUser(string email)
        {
            if (email is null) return false;
            User u = ReadUser(email);
            return DeleteUser(u);
        }

        public bool InsertSecret(Secret secret)
        {
            if (ContainsSecret(secret) || secret is null) return false;
            tblSecrets.Add(secret.Id, secret);
            return true;
        }

        public bool InsertUser(User user)
        {
            if (ContainsUser(user) || user is null) return false;
            tblUsers.Add(user.Id, user);
            return true;
        }

        public IList<User> UserList()
        {
            return tblUsers.Values;
        }

        public IList<Secret> SecretList()
        {
            return tblSecrets.Values;
        }

        private bool IndexOutsideSecrets(int index)
        {
            return index < 0 || index >= SecretCount();
        }

        private bool IndexOutsideUsers(int index)
        {
            return index < 0 || index >= UserCount();
        }

        public Secret NextSecret()
        {
            if (IndexOutsideSecrets(currentSecret + 1)) return null;
            currentSecret++;
            Console.WriteLine(currentSecret);
            return tblSecrets.ElementAt(currentSecret).Value;
        }

        public User NextUser()
        {
            if (IndexOutsideUsers(currentUser + 1)) return null;
            currentUser++;
            return tblUsers.ElementAt(currentUser).Value;
        }

        public Secret PreviousSecret()
        {
            if (IndexOutsideSecrets(currentSecret - 1)) return null;
            currentSecret--;
            return tblSecrets.ElementAt(currentSecret).Value;
        }

        public User PreviousUser()
        {
            if (IndexOutsideUsers(currentUser - 1)) return null;
            currentUser--;
            return tblUsers.ElementAt(currentUser).Value;
        }

        public Secret ReadSecret(int id)
        {
            if (!ContainsSecret(id)) return null;
            return tblSecrets[id];
        }

        public Secret ReadSecret(Secret secret)
        {
            if (!ContainsSecret(secret)) return null;
            return ReadSecret(secret.Id);
        }

        public User ReadUser(int id)
        {
            if (!ContainsUser(id)) return null;
            return tblUsers[id];
        }

        public User ReadUser(string email)
        {
            var userList = tblUsers.Where(user => user.Value.Email == email);
            if (userList.Count() == 0) return null;
            User u = userList.First().Value;
            return ReadUser(u);
        }

        public User ReadUser(User user)
        {
            if (!ContainsUser(user)) return null;
            return ReadUser(user.Id);
        }

        public int SecretCount()
        {
            return tblSecrets.Count;
        }

        public bool UpdateSecret(Secret secret)
        {
            bool containsSecret = ContainsSecret(secret);
            if (containsSecret) tblSecrets[secret.Id] = secret;
            return containsSecret;
        }

        public bool UpdateUser(User user)
        {
            bool containsUser = ContainsUser(user);
            if (containsUser) tblUsers[user.Id] = user;
            return containsUser;
        }

        public int UserCount()
        {
            return tblUsers.Count;
        }

        public List<Secret> GetUserSecrets(User user)
        {
            return SecretList().Where(secret => secret.Owner == user).ToList();
        }

        public List<Secret> GetInvitedSecrets(User user)
        {
            return SecretList().Where(secret => secret.HasAccess(user)).ToList();
        }

        public List<User> GetRequestedUsers()
        {
            return UserList().Where(user => user.State == State.REQUESTED).ToList();
        }

        public InvitationLink ReadInvitation(Guid id)
        {
            if (!ContainsInvitation(id)) return null;
            return tblInvitations[id];
        }

        public InvitationLink ReadInvitation(InvitationLink link)
        {
            if (!ContainsInvitation(link)) return null;
            return ReadInvitation(link.Id);
        }

        public bool DeleteInvitation(Guid id)
        {
            return tblInvitations.Remove(id);
        }

        public bool DeleteInvitation(InvitationLink link)
        {
            if (!ContainsInvitation(link)) return false;
            return DeleteInvitation(link.Id);
        }

        public bool InsertInvitation(InvitationLink link)
        {
            if (ContainsInvitation(link) || link is null) return false;
            tblInvitations.Add(link.Id, link);
            return true;
        }

        public bool ContainsInvitation(Guid id)
        {
            return tblInvitations.ContainsKey(id);
        }

        public bool ContainsInvitation(InvitationLink link)
        {
            if (link is null) return false;
            return ContainsInvitation(link.Id);
        }

        public int InvitationCount()
        {
            return tblInvitations.Count;
        }

        public List<LogEntry> LogList()
        {
            return tblLogs.Values.ToList();
        }

        public bool InsertLog(LogEntry entry)
        {
            if (ContainsLog(entry) || entry is null) return false;
            tblLogs.Add(entry.Id, entry);
            return true;
        }

        public int LogCount()
        {
            return tblLogs.Count;
        }

        public bool ContainsLog(Guid id)
        {
            return tblLogs.ContainsKey(id);
        }

        public bool ContainsLog(LogEntry entry)
        {
            if (entry == null) return false;
            return ContainsLog(entry.Id);
        }
    }
}