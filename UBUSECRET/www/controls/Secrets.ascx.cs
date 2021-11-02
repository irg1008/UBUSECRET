using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Main;
using Data;
using www.controls;

namespace www.controls
{
    public partial class SecretsControl : System.Web.UI.UserControl
    {
        private DB db;
        private User owner;

        public User Owner { get => owner; set => owner = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            db = DB.GetInstance();

            // If owner is passed.
            if (owner != null)
            {
                DisplaySecrets();
            }
        }

        private void LoadSecretsToControl(IEnumerable<Secret> secrets, Panel secretsPanel, bool useSharedBy)
        {
            secretsPanel.CssClass = "flex gap-8 flex-wrap w-full";

            foreach (Secret secret in secrets)
            {
                SecretControl control = (SecretControl)Page.LoadControl("/controls/Secret.ascx");
                control.Secret = secret;
                control.ID = secret.Id.ToString();

                if (useSharedBy)
                    control.SharedBy = secret.Owner;

                secretsPanel.Controls.Add(control);
            }
        }

        private void DisplaySecrets()
        {
            // Get secrets in wich user is onwer and secrets of wich is invited.
            IEnumerable<Secret> ownedSecrets = db.GetUserSecrets(Owner);
            IEnumerable<Secret> invitedSecrets = db.GetInvitedSecrets(Owner);

            if (ownedSecrets.Count() > 0)
            {
                // Display owned secrets.
                LoadSecretsToControl(ownedSecrets, OwnedSecrets, false);
            }

            if (invitedSecrets.Count() > 0)
            {
                SharedEmpty.Visible = false;
                // Display shared secrets.
                LoadSecretsToControl(invitedSecrets, InvitedSecrets, true);
            }
        }

        protected void GoToAddSecret(object sender, EventArgs e)
        {
            Response.Redirect("/add/Secret.aspx");
        }
    }
}