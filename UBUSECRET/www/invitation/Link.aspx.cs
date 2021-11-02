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
            {
                ShowYouNeedToBeLogged();
            }
            else
            {
                db = DB.GetInstance();

                // Check invitation with id exist and is accessible.
                Guid id = new Guid(Request.QueryString["id"]);
                CheckValidId(id);
            }
        }

        protected void LogInAndRedirect(object sender, EventArgs e)
        {
            Response.Redirect("/auth/LogIn.aspx");
        }

        // Mad function.
        // TOREFACTOR.
        private void CheckValidId(Guid id)
        {
            InvitationLink link = db.ReadInvitation(id);
            if (link == null)
                IncorrectInvitation();

            // Check if invitation is accessed by owner.
            User loggedUser = Master.GetUser();
            bool userIsOwner = loggedUser == link.Secret.Owner;
            if (userIsOwner)
            {
                ShowUserIsOwner();
            }
            else
            {
                // Check if user already consumes the secret.
                bool userAlreadyconsumes = link.Secret.Consumers.Contains(loggedUser);
                if (userAlreadyconsumes)
                {
                    AlreadyHasAccess_Panel.Visible = true;
                }
                else
                {
                    // It's accessible if current date (Datetime.Now) is before the limit time.
                    bool isAccessible = link.IsAccessible();

                    if (isAccessible)
                    {
                        ShowValidLinkControls(link);
                    }
                    else
                    {
                        ShowInaccessible(link);
                    }
                }
            }
        }

        private void ShowUserIsOwner()
        {
            IsOwner_Panel.Visible = true;
        }

        private void ShowYouNeedToBeLogged()
        {
            NotLogged_Panel.Visible = true;
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
                Response.Redirect("/default.aspx");
            }

            ValidInvitation.Visible = true;
            ValidInvitationText.Text = $"Do you wanto to add the secret \"{link.Secret.Title}\" (by {link.Secret.Owner.Name}) to your library?";
            AcceptButton.ServerClick += new EventHandler(AddSecretToUser);
        }

        private Exception IncorrectInvitation()
        {
            throw new HttpException(404, "");
        }
    }
}