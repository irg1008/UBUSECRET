using System;

namespace Utils
{
    class Invitation
    {
        private readonly IdGen idGen = new IdGen();

        private readonly int id;
        private readonly int secretId;
        private readonly DateTime limitDate;
        // TODO?: Only allowed to some users. private List<User> allowedUsers;

        public Invitation(int secretId, int daysToExpire)
        {
            this.id = idGen.NewId();
            this.secretId = secretId;
            this.limitDate = DateTime.Now.AddDays(daysToExpire);
        }

        public int Id => id;
        public int SecretId => secretId;
        public DateTime LimitDate => limitDate;

        public String createLink()
        {
            int invitationHashCode = this.GetHashCode();
            return $"http://www.algo.com/invitation/{invitationHashCode}";
        }

        public override bool Equals(object obj)
        {
            return obj is Invitation invitation &&
                   Id.Equals(invitation.Id) &&
                   SecretId.Equals(invitation.SecretId) &&
                   LimitDate == invitation.LimitDate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, SecretId, LimitDate);
        }
    }
}
