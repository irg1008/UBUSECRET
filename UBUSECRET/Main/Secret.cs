using Utils;
using System;
using System.Collections.Generic;

namespace Main
{
    public class Secret : IComparable<Secret>
    {
        private readonly IdGen idGen = new IdGen();

        private readonly int id;
        private string title;
        private readonly string message;
        private User owner;
        private List<User> consumers;

        /* CONSTRUCTORES */
        public Secret(string title, string message, User owner)
        {
            id = idGen.NewId();
            Title = title;
            Owner = owner;
            Consumers = new List<User> { };
            this.message = message;
        }

        /* GETTERS AND SETTERS */
        public int Id => id;
        public string Title { get => title; set => title = value; }
        public User Owner { get => owner; set => owner = value; }
        public List<User> Consumers { get => consumers; set => consumers = value; }
        public string Message { get => message; }

        public bool IsOwner(User user)
        {
            return owner.Equals(user);
        }

        public int UsersWithAccess()
        {
            return Consumers.Count;
        }

        public bool HasAccess(User user)
        {
            return Consumers.Contains(user);
        }

        public bool AddConsumer(User user)
        {
            bool containsUser = HasAccess(user);
            bool isOwner = IsOwner(user);

            if (containsUser || isOwner) return false;

            Consumers.Add(user);
            return true;
        }

        public bool RemoveConsumer(User user)
        {
            bool containsUser = HasAccess(user);
            bool isOwner = IsOwner(user);

            if (!containsUser || isOwner) return false;

            return Consumers.Remove(user);
        }

        // This method exist so encryption can be easily added later.
        // And complexity can grow without changing any function call.
        public String GetMesssage()
        {
            return message;
        }

        public override bool Equals(object obj)
        {
            return obj is Secret secret &&
                   Id.Equals(secret.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title.GetHashCode());
        }

        public int CompareTo(Secret other)
        {
            return Title.CompareTo(other.Title);
        }
    }
}
