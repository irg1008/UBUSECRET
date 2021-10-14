using System;
using System.Security.Cryptography;
using Utils;


namespace Main
{
    public enum State
    {
        PREFETCHED,
        REQUESTED,
        AUTHORIZED,
        ACTIVE,
        BANNED
    }

    public class User : IComparable<User>
    {
        private readonly IdGen idGen = new IdGen();

        private readonly int id;
        private String name;
        private String surname;
        private String email;
        private String password;
        private String lastIP;
        private State state;
        private DateTime lastSeen;
        private bool isAdmin;

        /* CONSTRUCTOR */
        public User(String name, String surname, String email, String initPassword)
        {
            this.id = idGen.NewId();
            Name = name;
            Surname = surname;
            Email = email;
            Password = this.Hash(initPassword);
            State = State.PREFETCHED;
            IsAdmin = false;
            LastIP = null;
            LastSeen = DateTime.Now;
        }

        /* GETTERS AND SETTERS */
        public int Id => id;
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string LastIP { get => lastIP; set => lastIP = value; }
        public DateTime LastSeen { get => lastSeen; set => lastSeen = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public State State { get => state; set => state = value; }


        /* FUNCIONES */
        public bool Request()
        {
            if (State != State.PREFETCHED) return false;
            State = State.REQUESTED;
            return true;
        }

        public bool Authorize()
        {
            if (State != State.REQUESTED) return false;
            State = State.AUTHORIZED;
            return true;
        }

        public bool Activate()
        {
            if (State != State.AUTHORIZED && State != State.BANNED) return false;
            State = State.ACTIVE;
            return true;
        }

        public bool Ban()
        {
            if (State != State.ACTIVE) return false;
            State = State.BANNED;
            return true;
        }

        public void MakeAdmin()
        {
            IsAdmin = true;
        }

        public bool CheckPasword(String password)
        {
            return this.Hash(password) == Password;
        }

        public bool ChangePassword(String oldPass, String newPass)
        {
            bool isOldCorrect = this.CheckPasword(oldPass);
            if (!isOldCorrect) return false;
            
            Password = this.Hash(newPass);
            return true;
        }

        private string Hash(string password)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
            SHA256 mySHA256 = SHA256.Create();
            bytes = mySHA256.ComputeHash(bytes);
            return (System.Text.Encoding.ASCII.GetString(bytes));
        }

        public override bool Equals(object obj)
        {
            return obj is User user && (
                   Id.Equals(user.Id) ||
                   Email == user.Email);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Email, Name);
        }

        public int CompareTo(User other)
        {
            return Email.CompareTo(other.Email);
        }
    }


}
