using System;
using Main;
using System.Text.Json;
using Utils;

namespace Invitation
{
    public class InvitationLink : ISerializable<InvitationLink>
    {

        private readonly Guid id;
        private readonly Secret secret;
        private readonly DateTime limit;

        public InvitationLink(Secret secret, DateTime limit)
        {
            // We generate a secure-random id that is hard to be url guessed.
            id = Guid.NewGuid();
            this.secret = secret;
            this.limit = limit;
        }

        public Guid Id => id;
        public Secret Secret => secret;
        public DateTime Limit => limit;

        public bool IsAccessible()
        {
            return DateTime.Now <= Limit;
        }

        public override bool Equals(object obj)
        {
            return obj is InvitationLink invitation &&
                   Id.Equals(invitation.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public string To_JSON()
        {
            throw new NotImplementedException();
        }

        public InvitationLink From_JSON(string JSONString)
        {
            throw new NotImplementedException();
        }
    }
}
