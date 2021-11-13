using Data;
using Log;
using Main;
using Invitation;

namespace www
{
    public class AppLogs
    {
        private static readonly DB db = DB.GetInstance();

        private static void Insert(Entry e, string msg)
        {
            LogEntry entry = new LogEntry(e, msg);
            db.InsertLog(entry);
        }

        public static void LogIn(User user)
        {
            Insert(Entry.LOG_IN, $"User ({user.Id}) logged in");
        }

        public static void LogOut(User user)
        {
            Insert(Entry.LOG_OUT, $"User ({user.Id}) logged out");
        }

        public static void CreateSecret(Secret secret)
        {
            Insert(Entry.CREATE_SECRET, $"User ({secret.Owner.Id}) created a secret ({secret.Id})");
        }

        public static void DeleteSecret(Secret secret)
        {
            Insert(Entry.DELETE_SECRET, $"User({secret.Owner.Id}) deleted a secret ({secret.Id})");
        }

        public static void AddConsumer(User consumer, Secret secret)
        {
            Insert(Entry.ADD_CONSMER, $"Owner ({secret.Owner.Id}) added a consumer ({consumer.Id}) access to a secret ({secret.Id})");
        }

        public static void DetatchConsumer(User consumer, Secret secret)
        {
            Insert(Entry.DETACH_CONSUMER, $"Owner ({secret.Owner.Id}) removed a consumer ({consumer.Id}) access to a secret ({secret.Id})");
        }

        public static void DetatchFromSecret(User consumer, Secret secret)
        {
            Insert(Entry.DETACH_FROM_SECRET, $"Consumer ({consumer.Id} detatched itself from a secret ({secret.Id}) owned by {secret.Owner.Name} ({secret.Owner.Id})");
        }

        public static void CreateInvitation(InvitationLink link, Secret secret)
        {
            Insert(Entry.CREATE_INVITATION, $"Owner ({secret.Owner.Id}) created an invitation link ({link.Id}) to share a secret ({secret.Id})");
        }
    }
}