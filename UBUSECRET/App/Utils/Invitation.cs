﻿using System;

namespace App
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
            return $"http://www.algo.com/invitation/{this.Id.ToString()}";
        }

    }
}