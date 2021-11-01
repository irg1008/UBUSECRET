using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
using Main;
using www.controls;

namespace www.details
{
    public partial class SecretDetails : System.Web.UI.Page
    {
        private DB db;
        private bool isOwner;
        private Secret secret;

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isLogged = Master.IsLogged();

            if (!isLogged)
                Response.Redirect("Error.aspx");

            // If logged.
            db = DB.GetInstance();

            // Get param and try int conversion.
            string secretId = Request.QueryString["id"];
            try
            {
                int id = int.Parse(secretId);

                // Check if user has permisison.
                User loggedUser = Master.GetUser();
                secret = GetSecret(id, loggedUser);
                if (secret == null) throw new Exception("You don't have permission to access this secret");

                // If user has permission => Load data.
                LoadData(loggedUser);
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        private void LoadData(User user)
        {
            SecretName.Text = secret.Title;
            SecretMessage.Text = secret.GetMesssage();
            SecretOwner.Text = secret.Owner.Name;

            if (isOwner)
            {
                // Add YOU to user name.
                SecretOwner.Text += " (YOU)";

                // Show consumers panel.
                SecretConsumers.Visible = true;

                if (secret.Consumers.Count > 0)
                {
                    NoConsumers.Visible = false;
                    ConsumerList.Visible = true;
                    ConsumerList.Controls.Clear();

                    foreach (User consumer in secret.Consumers)
                    {
                        void Reload(object sender, EventArgs e) { LoadData(user); };
                        ConsumerLabel control = (ConsumerLabel)Page.LoadControl("/controls/ConsumerLabel.ascx");
                        control.Consumer = consumer;
                        control.Secret = secret;
                        control.ID = consumer.Id.ToString();
                        control.OnRemove = new EventHandler(Reload);
                        ConsumerList.Controls.Add(control);
                    }
                }
                else
                {
                    NoConsumers.Visible = true;
                    ConsumerList.Visible = false;
                }
            }
            else
            {
                void DetachFromSecret(object sender, EventArgs e)
                {
                    secret.RemoveConsumer(user);
                    Response.Redirect("/default.aspx");
                }
                DetachContainer.Visible = true;
                DetachButton.ServerClick += new EventHandler(DetachFromSecret);
            }
        }

        private Secret GetSecret(int id, User user)
        {
            Secret secret = db.ReadSecret(id);
            if (secret == null) return null;

            // Check secret as user as owner or consumer.
            isOwner = secret.Owner.Equals(user);
            bool isConsumer = secret.Consumers.Contains(user);

            if (!isOwner && !isConsumer) return null;

            return secret;
        }

        private string SetEmailError(User user)
        {
            if (user == null) return "No user with given email";
            if (secret.Consumers.Contains(user)) return "User already added as a consumer";
            if (user == secret.Owner) return "You cannot invite yourself to your own secret";
            return "";
        }

        protected void RemoveSecret(object sender, EventArgs e)
        {
            db.DeleteSecret(secret);
            Response.Redirect("/default.aspx");
        }

        protected void AddConsumer(object sender, EventArgs e)
        {
            string email = Consumer_Input.Text;
            User newConsumer = db.ReadUser(email);
            ConsumerError.Text = SetEmailError(newConsumer);

            if (ConsumerError.Text == "")
            {
                secret.AddConsumer(newConsumer);
                Consumer_Input.Text = "";
                Master.ShowPopUp($"{newConsumer.Name} added successfully", PopUpType.SUCCESS);
                LoadData(Master.GetUser());
            }
        }
    }
}