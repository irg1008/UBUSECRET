using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace www
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isLogged = Master.IsLogged();

            // If logged show dashboard..
            if (isLogged)
            {
                Dashboard.Visible = true;
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

        protected void LogOut(object sender, EventArgs e)
        {
            Master.LogOut();
        }
    }
}