using System;
using System.Web;
using Data;
using Invitation;
using Main;

namespace www.invitation
{
    public partial class Link : System.Web.UI.Page
    {
        private DB db;

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isLogged = Master.IsLogged();

            if (!isLogged)
                IncorrectInvitation();


            db = DB.GetInstance();

            // Check invitation with id exist and is accessible.
            Guid id = new Guid(Request.QueryString["id"]);
            CheckValidId(id);
        }

        private void CheckValidId(Guid id)
        {
            InvitationLink link = db.ReadInvitation(id);
            if (link == null)
                IncorrectInvitation();


            // If exists => We check is accessible.
            // It's accessible if current date (Datetime.Now) is before the limit time.
            bool isAccessible = link.IsAccessible();

            if (isAccessible)
                ShowValidLinkControls(link);
            else
                ShowInaccessible(link);
        }

        private void ShowYouNeedToBeLogged()
        {
            // TODO everything here.
            // And then a list in every secret.
        }

        private void ShowInaccessible(InvitationLink link)
        {
            Inaccessible.Visible = true;
            ExpiredTime.Text = link.Limit.ToString();
        }

        private void ShowValidLinkControls(InvitationLink link)
        {
            void AddSecretToUser(object sender, EventArgs e)
            {
                User loggedUser = Master.GetUser();
                link.Secret.AddConsumer(loggedUser);
            }

            ValidInvitation.Visible = true;
            SecretTitle.Text = link.Secret.Title;
            AcceptButton.ServerClick += new EventHandler(AddSecretToUser);
        }

        private Exception IncorrectInvitation()
        {
            throw new HttpException(404, "");
        }
    }
}