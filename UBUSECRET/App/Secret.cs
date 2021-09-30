using System;
using System.Collections.Generic;

namespace App
{
    public class Secret
    {
        private readonly Guid id;
        private String publicKey;
        private String privateKey;
        private String message;
        private User owner;
        private List<User> consumers;

        /* CONSTRUCTORES */
        public Secret(String publicKey, String privateKey, string message, User owner, List<User> consumers)
        {
            this.id = new Guid();
            PrivateKey = privateKey;
            PublicKey = publicKey;
            Message = message;
            Owner = owner;
            Consumers = consumers;
        }

        /* GETTERS AND SETTERS */
        public Guid Id => id;
        public string PrivateKey { get => privateKey; set => privateKey = value; }
        public string PublicKey { get => publicKey; set => publicKey = value; }
        public string Message { get => message; set => message = value; }
        public User Owner { get => owner; set => owner = value; }
        public List<User> Consumers { get => consumers; set => consumers = value; }


        /* FUNCIONES */

        public void Encrypt(String message)
        {
            return;
        }

        public void Decrypt(String message)
        {
            return;
        }
    }
}
