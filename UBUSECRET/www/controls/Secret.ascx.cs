using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Main;

namespace www.controls
{
    public partial class SecretControl : System.Web.UI.UserControl
    {
        private Secret secret;
        private User sharedBy;

        public Secret Secret { get => secret; set => secret = value; }
        public User SharedBy { get => sharedBy; set => sharedBy = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Secret != null)
            {
                SecretTitle.Text = Secret.Title;
                SecretMsg.Text = Secret.GetMesssage();

                if (SharedBy != null)
                {
                    SharedByContainer.Visible = true;
                    SharedUserName.Text = SharedBy.Name;
                }
            }
        }

        protected void GoToSecretDetails(object sender, EventArgs e)
        {
            Response.Redirect($"/details/Secret.aspx?id={Secret.Id}");
        }
    }
}