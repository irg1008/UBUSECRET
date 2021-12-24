using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Main;

namespace DataAPI
{
    public static class API
    {
        public static DB db = new DB();

        public static bool AddUser(string userJSON) {
            throw new NotImplementedException();
        }

        public static string GetUser(string email)
        {
            throw new NotImplementedException();
        }

        public static string RemoveUser(string email)
        {
            throw new NotImplementedException();
        }

        public static string ListActiveUsers()
        {
            throw new NotImplementedException();
        }

        public static string ListUnactiveUsers()
        {
            throw new NotImplementedException();
        }

        public static string ListPendientUsers()
        {
            throw new NotImplementedException();
        }

        public static bool AddSecret(string secretJSON)
        {
            throw new NotImplementedException();
        }

        public static string GetSecret(int id)
        {
            Secret s = API.db.GetSecret(id);
            return s.To_JSON();
        }

        public static string RemoveSecret(int id)
        {
            Secret s = API.db.RemoveSecret(id);
            return s.To_JSON();
        }

        public static string ListOwnSecrets(string email)
        {
            User u = API.db.GetUser(email);
            List<Secret> secrets = API.db.ListOwnSecrets(u);
            return JsonConvert.SerializeObject(secrets); ;
        }

        public static string ListReceivedSecrets(string email)
        {
            User u = API.db.GetUser(email);
            List<Secret> secrets = API.db.ListReceivedSecrets(u);
            return JsonConvert.SerializeObject(secrets);
        }
    }
}
