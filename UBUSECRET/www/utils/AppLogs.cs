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
            Insert(Entry.LOG_IN, $"User with id {user.Id} logged in");
        }

        public static void LogOut(User user)
        {
            Insert(Entry.LOG_OUT, $"User with id {user.Id} logged out");
        }

        public static void CreateSecret(Secret secret)
        {
            Insert(Entry.CREATE_SECRET, $"User with id {secret.Owner.Id} created a secret with id {secret.Id}");
        }

        public static void DeleteSecret(Secret secret)
        {
            Insert(Entry.DELETE_SECRET, $"User with id {secret.Owner.Id} deleted a secret with id {secret.Id}");
        }

        public static void AddConsumer(User consumer, Secret secret)
        {
            Insert(Entry.ADD_CONSMER, $"Owner with id {secret.Owner.Id} added a consumer with id {consumer.Id} access to secret with id {secret.Id}");
        }

        public static void DetatchConsumer(User consumer, Secret secret)
        {
            Insert(Entry.DETACH_CONSUMER, $"Owner with id {secret.Owner.Id} removed a consumer with id: {consumer.Id} access to secret with id {secret.Id}");
        }

        public static void DetatchFromSecret(User consumer, Secret secret)
        {
            Insert(Entry.DETACH_FROM_SECRET, $"Consumer with id {consumer.Id} detatched itself from the secret with id {secret.Id} owned by user with id {secret.Owner.Id}");
        }

        public static void CreateInvitation(InvitationLink link, Secret secret)
        {
            Insert(Entry.CREATE_INVITATION, $"Owner with id {secret.Owner.Id} created an invitation link with id {link.Id} to share secret with id {secret.Id}");
        }
    }
}