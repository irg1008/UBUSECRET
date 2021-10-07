using System;

namespace Utils
{
    class Invitation
    {
        private readonly Guid id;
        private readonly Guid secretId;
        private readonly DateTime limitDate;
        // TODO?: Only allowed to some users. private List<User> allowedUsers;

        public Invitation(Guid secretId, int daysToExpire)
        {
            this.id = Guid.NewGuid();
            this.secretId = secretId;
            this.limitDate = DateTime.Now.AddDays(daysToExpire);
        }

        public Guid Id => id;
        public Guid SecretId => secretId;
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
