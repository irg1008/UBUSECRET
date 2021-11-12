using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Main;
using Data;

namespace www.add
{
    public partial class CreateSecret : System.Web.UI.Page
    {
        private DB db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = DB.GetInstance();
        }

        private string TitleErrorMsg(string title)
        {
            if (title == "") return "Title cannot be empty";
            if (title.Length > 25) return "Title length cannot exceed 25 characters";

            return "";
        }

        private string MessageErrorMsg(string msg)
        {
            if (msg == "") return "Message cannot be empty";
            if (msg.Length > 250) return "Message length cannot exceed 250 characters";
            return "";
        }

        private bool Check_Title()
        {
            string error = TitleErrorMsg(SecretTitle.Text);
            SecretTitleError.Text = error;
            return error == "";
        }

        private bool Check_Message()
        {
            string error = MessageErrorMsg(SecretMessage.Text);
            SecretMessageError.Text = error;
            return error == "";
        }

        protected void CreateNewSecret(object sender, EventArgs e)
        {
            User owner = Master.GetUser();
            bool titleIsCorrect = Check_Title();
            bool msgIsCorrect = Check_Message();

            // Create new secret.
            if (titleIsCorrect && msgIsCorrect)
            {
                Secret secret = new Secret(SecretTitle.Text, SecretMessage.Text, owner);
                db.InsertSecret(secret);
                // Update log.
                AppLogs.CreateSecret(secret);
                Response.Redirect("/default.aspx");
            }
        }
    }
}