﻿using App.Utils;
using System;
using System.Collections.Generic;

namespace App
{
    public class Secret
    {
        private readonly RSAEncryption RSA = new RSAEncryption();


        private readonly Guid id;
        private string title;
        private byte[] message;
        private User owner;
        private List<User> consumers;

        /* CONSTRUCTORES */
        public Secret(string title, string message, User owner)
        {
            id = Guid.NewGuid() ;
            Title = title;
            Message = RSA.EncryptText(message);
            Owner = owner;
            Consumers = new List<User> { };
        }

        /* GETTERS AND SETTERS */
        public Guid Id => id;
        public string Title { get => title; set => title = value; }
        public byte[] Message { get => message; set => message = value; }
        public User Owner { get => owner; set => owner = value; }
        public List<User> Consumers { get => consumers; set => consumers = value; }

        public bool IsOwner(User user)
        {
            return owner.Equals(user);
        }

        public bool AddConsumer(User user)
        {
            bool containsUser = Consumers.Contains(user);
            bool isOwner = IsOwner(user);

            if (containsUser || isOwner) return false;

            Consumers.Add(user);
            return true;
        }

        public bool RemoveConsumer(User user)
        {
            bool containsUser = Consumers.Contains(user);
            bool isOwner = IsOwner(user);

            if (!containsUser || isOwner) return false;

            return Consumers.Remove(user);
        }

        public String GetMesssage() {
            return RSA.DecryptText(Message);
        }

        /* FUNCIONES */
        public override bool Equals(object obj)
        {
            return obj is Secret secret &&
                   Id.Equals(secret.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
