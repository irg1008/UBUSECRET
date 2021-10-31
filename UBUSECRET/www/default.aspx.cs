using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace www
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isLogged = Master.IsLogged();

            // If logged show dashboard..
            if (isLogged)
            {
                Dashboard.Visible = true;
                SecretList.Owner = Master.GetUser();
            }
            // If not, show home page.
            else
            {
                Home.Visible = true;
            }
        }

        protected void SignUp(object sender, EventArgs e)
        {
            Response.Redirect("/auth/SignUp.aspx");
        }

        protected void LogIn(object sender, EventArgs e)
        {
            Response.Redirect("/auth/LogIn.aspx");
        }
    }
}