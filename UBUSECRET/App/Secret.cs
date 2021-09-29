using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class Secret
    {
        private String id;
        private String sender;
        private List<String> receiversId;
        private String message;
        private String key;

        /* CONSTRUCTORES */
        public Secret(string sender, List<string> receiversId, string message)
        {
            // TODO - generar id
            this.sender = sender;
            this.receiversId = receiversId;
            this.message = message;
            // TODO - generar clave
        }

        /* GETTERS AND SETTERS */
        public string Id { get => id; set => id = value; }
        public string Sender { get => sender; set => sender = value; }
        public List<string> ReceiversId { get => receiversId; set => receiversId = value; }
        public string Message { get => message; set => message = value; }
        public string Key { get => key; set => key = value; }

        /* FUNCIONES */

        // TODO - Yo lo haria void
        public bool attachUser(String userId)
        {
            receiversId.Add(userId);
            return true;
        }

        // TODO
        public bool dettachUser(String userId)
        {
            return receiversId.Remove(userId);
        }

        // TODO
        public void createLink(List<User> allowedUsers)
        {

        }

    }
}
