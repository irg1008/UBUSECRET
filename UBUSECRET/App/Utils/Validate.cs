using System;
using System.Text.RegularExpressions;

namespace App
{
    class Validate
    {
        // https://ihateregex.io/expr/password
        public static bool Password(String password)
        {
            Regex regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            return regex.IsMatch(password);
        }

        // https://ihateregex.io/expr/email
        public static bool Email(String email)
        {
            Regex regex = new Regex(@"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+");
            return regex.IsMatch(email);
        }

        // https://ihateregex.io/expr/username
        public static bool Username(String username)
        {
            Regex regex = new Regex(@"^[a-z0-9_-]{3,15}$");
            return regex.IsMatch(username);
        }

        // https://ihateregex.io/expr/ip
        public static bool IPAddress(String ipAddress)
        {
            Regex regex = new Regex(@"(\b25[0-5]|\b2[0-4][0-9]|\b[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}");
            return regex.IsMatch(ipAddress);
        }

        // https://ihateregex.io/expr/url
        public static bool URL(String url)
        {
            Regex regex = new Regex(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()!@:%_\+.~#?&\/\/=]*)");
            return regex.IsMatch(url);
        }

        // https://ihateregex.io/expr/uuid
        public static bool UUID(String uuid)
        {
            Regex regex = new Regex(@"^[0-9a-fA-F]{8}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{12}$");
            return regex.IsMatch(uuid);
        }
    }
}
