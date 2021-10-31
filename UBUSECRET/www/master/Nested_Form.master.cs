using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace www
{
    public partial class Nested_Form : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If logged in => Redirect to home.
            bool isLogged = Master.IsLogged();
            if (isLogged) Response.Redirect("Error.aspx");
        }
    }
}