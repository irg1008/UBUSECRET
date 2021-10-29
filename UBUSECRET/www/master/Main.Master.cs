using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
using System.Threading;
using System.Drawing;
using Main;

namespace www
{
    public enum PopUpType
    {
        SUCCESS,
        ERROR,
        WARNING,
        INFO
    }

    public partial class MainMaster : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // Reset Pop Up if postback.
            if (IsPostBack)
                PopUp.Visible = false;

            // If logged => Disable BG decoration. (Blur title and icons).
            if (IsLogged())
                BG_Decoration.Visible = false;
        }

        public void LogIn(User user)
        {
            Page.Session["is-logged"] = true;
            Page.Session["user"] = user;
            user.Activate();
            Response.Redirect("/default.aspx");
        }

        public void LogOut()
        {
            Page.Session["is-logged"] = false;
            User user = (User)Page.Session["user"];
            if (user != null)
                user.Unactivate();
            Response.Redirect("/default.aspx");
        }

        public bool IsLogged()
        {
            var loggedSession = Page.Session["is-logged"];
            if (loggedSession == null) LogOut();
            bool isLogged = (bool)Page.Session["is-logged"];
            return isLogged;
        }

        public void ShowPopUp(string text, PopUpType type)
        {
            switch (type)
            {
                case PopUpType.SUCCESS:
                    PopUp.BackColor = Color.LightGreen;
                    break;
                case PopUpType.INFO:
                    PopUp.BackColor = Color.LightSkyBlue;
                    break;
                case PopUpType.ERROR:
                    PopUp.BackColor = Color.LightCoral;
                    break;
                case PopUpType.WARNING:
                    PopUp.BackColor = Color.Gold;
                    break;
            }

            PopUp_Text.Text = text;
            PopUp.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "hidePopUp", "HideLabel();", true);
        }
    }
}