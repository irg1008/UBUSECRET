using System;
using System.Web.UI;
using Data;
using Log;
using System.Drawing;
using Main;
using Utils;

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
        DB db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = DB.GetInstance();
            DB.LoadSampleData();

            // Reset Pop Up if postback.
            if (IsPostBack)
                PopUp.Visible = false;

            // If logged => Disable BG decoration. (Blur title and icons).
            if (IsLogged())
            {
                Navbar.Visible = true;
                BG_Decoration.Visible = false;

                // Get user.
                User user = (User)Page.Session["user"];

                // Set user name.
                User_Name.Text = $"{user.Name} (Last online: {user.LastSeen})";

                // Check if is admin
                if (user.IsAdmin)
                {
                    AdminPanelLink.Visible = true;
                }
            }

        }

        public void LogIn(User user)
        {
            // Set seesion variables.
            Page.Session["is-logged"] = true;
            Page.Session["user"] = user;

            // Activate user.
            user.Activate();

            // Update log.
            AppLogs.LogIn(user);

            // Redirect to home.
            Response.Redirect("/default.aspx");
        }

        public void LogOut()
        {
            Page.Session["is-logged"] = false;
            Page.Session["user"] = null;
            User user = GetUser();
            if (user != null)
            {
                // Update log.
                AppLogs.LogOut(user);

                user.Unactivate();
                user.LastSeen = DateTime.Now;
            }
            Response.Redirect("/auth/LogIn.aspx");
        }

        public User GetUser()
        {
            User user = (User)Page.Session["user"];
            return user;
        }

        protected void LogOut(object sender, EventArgs e)
        {
            LogOut();
        }

        protected void GoToAdminPanel(object sender, EventArgs e)
        {
            Response.Redirect("/admin/Users.aspx");
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