using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace www
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string error = "Not found";

            string errorPath = Request.QueryString["aspxerrorpath"];

            if (errorPath != null)
                error += $": {errorPath}";

            Page.Title = error;
            Path.Text = error;
        }
    }
}