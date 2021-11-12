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
            Insert(Entry.LOG_IN, $"User with id: {user.Id} logged in");
        }

        public static void LogOut(User user)
        {
            Insert(Entry.LOG_OUT, $"User id: {user.Id} logged out");
        }

        public static void CreateSecret(Secret secret)
        {
            Insert(Entry.CREATE_SECRET, $"User with id: {secret.Owner.Id} create a secret with id: {secret.Id}");
        }

        public static void DeleteSecret(Secret secret)
        {
            Insert(Entry.DELETE_SECRET, $"User with id: {secret.Owner.Id} delete a secret with id: {secret.Id}");
        }

        public static void AddConsumer(User consumer, Secret secret)
        {
            Insert(Entry.ADD_CONSMER, $"Owner id: {secret.Owner.Id} added a consumer with id: {consumer.Id} giving access to a secret with id: {secret.Id}");
        }

        public static void DetatchConsumer(User consumer, Secret secret)
        {
            Insert(Entry.DETACH_CONSUMER, $"Owner id: {secret.Owner.Id} rmeoved a consumer with id: {consumer.Id} of secret with id: {secret.Id}");
        }

        public static void DetatchFromSecret(User consumer, Secret secret)
        {
            Insert(Entry.DETACH_FROM_SECRET, $"Consumer with id: {consumer.Id} of secret with id: {secret.Id} detatched itself from the secret. Owner is: {secret.Owner.Id}");
        }

        public static void CreateInvitation(InvitationLink link, Secret secret)
        {
            Insert(Entry.DETACH_FROM_SECRET, $"Owner with id: {secret.Owner.Id}. created an invitation with id: {link.Id} for the secret with id: {secret.Id}");
        }
    }
}