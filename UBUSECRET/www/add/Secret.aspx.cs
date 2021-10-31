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

        private bool DataIsCorrect()
        {
            bool isTitleEmpty = SecretTitle.Text == "";
            bool isMsgEmpty = SecretMessage.Text == "";

            if (!isTitleEmpty && !isMsgEmpty) return true;

            if (isTitleEmpty) SecretTitleError.Text = "Title cannot be empty";
            if (isMsgEmpty) SecretMessageError.Text = "Message cannot be empty";

            return false;
        }

        protected void CreateNewSecret(object sender, EventArgs e)
        {
            User owner = Master.GetUser();
            bool correctSecretData = DataIsCorrect();

            // Create new secret.
            if (correctSecretData)
            {
                Secret secret = new Secret(SecretTitle.Text, SecretMessage.Text, owner);
                db.InsertSecret(secret);
                Response.Redirect("/default.aspx");
            }
        }
    }
}