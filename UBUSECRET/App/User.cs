using System;
using System.Collections.Generic;

namespace Practica_01
{
    enum Role
    {
        ADMIN,
        USER
    }

    public class User
    {
        private String id;
        private String name;
        private String surname;
        private String email;
        private String password; // Hashed String
        private List<String> attachedSecrets;
        private bool verified; // Cambiamos esto por "estado".
        private List<Role> roles;

        /* CONSTRUCTOR */
        public User(String name, String surname, String email, String password)
        {
            // TODO - generar id
            Name = name;
            Surname = surname;
            Email = email;
            attachedSecrets = new List<string>();
            // TODO - cifrado de password
        }

        /* GETTERS AND SETTERS */
        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public List<string> AttachedSecrets { get => attachedSecrets; set => attachedSecrets = value; }
        public bool Verified { get => verified; set => verified = value; }
        internal List<Role> Roles { get => roles; set => roles = value; }

        /* FUNCIONES */

        // TODO
        public bool checkPasword(String password)
        {
            return password == this.password;
        }

        // TODO - revisar
        public bool addSecret(Secret secret)
        {
            attachedSecrets.Add(secret.Id);
            return false;
        }

        // TODO - revisar
        public bool updateSecret(Secret secret)
        {
            if (attachedSecrets.Remove(secret.Id))
            {
                attachedSecrets.Add(secret.Id);
                return true;
            }
            else
                return false;
        }

        // TODO
        public bool deleteSecret(String secretId)
        {
            return attachedSecrets.Remove(secret.Id);
        }

        // TODO
        public bool refreshArrachedSecrets()
        {
            return false;
        }

        // TODO
        public bool detachFromSecret(String secretId)
        {
            return false;
        }

    }

    
}
