using System;
using System.Security.Cryptography;
using Utils;
using Newtonsoft.Json;

namespace Main
{
    public enum State
    {
        PREFETCHED,
        REQUESTED,
        AUTHORIZED,
        INACTIVE,
        ACTIVE,
        BANNED
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class User : IComparable<User>, ISerializable<User>
    {
        private readonly IdGen idGen = new IdGen();

        private readonly int id;
        private string name;
        private string email;
        private string password;
        private string lastIP;
        private State state;
        private DateTime lastSeen;
        private bool isAdmin;

        /* CONSTRUCTOR */
        public User() { }

        public User(string name, string email, string initPassword)
        {
            this.id = idGen.NewId();
            Name = name;
            Email = email;
            Password = this.Hash(initPassword);
            State = State.PREFETCHED;
            IsAdmin = false;
            LastIP = null;
            LastSeen = DateTime.Now;
        }

        /* GETTERS AND SETTERS */
        public int Id => id;
        [JsonProperty]
        public string Name { get => name; set => name = value; }
        [JsonProperty]
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        [JsonProperty]
        public string LastIP { get => lastIP; set => lastIP = value; }
        [JsonProperty]
        public DateTime LastSeen { get => lastSeen; set => lastSeen = value; }
        [JsonProperty]
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        [JsonProperty]
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

        public bool Unactivate()
        {
            if (State != State.ACTIVE) return false;
            State = State.INACTIVE;
            return true;
        }

        public bool Activate()
        {
            if (State != State.AUTHORIZED && State != State.INACTIVE) return false;
            State = State.ACTIVE;
            return true;
        }

        public bool Ban()
        {
            if (State != State.ACTIVE && State != State.INACTIVE) return false;
            State = State.BANNED;
            return true;
        }

        public bool Unban()
        {
            if (State != State.BANNED) return false;
            State = State.INACTIVE;
            return true;
        }

        public void MakeAdmin()
        {
            IsAdmin = true;

            // If new user.
            if (State == State.PREFETCHED)
            {
                // Set to inactive.
                State = State.INACTIVE;
            }
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
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes((String) password);
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

        public string To_JSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        public User From_JSON(string JSONString)
        {
            return JsonConvert.DeserializeObject<User>(JSONString);
        }
    }


}
