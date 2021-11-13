using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Main;
using Data;

namespace www.auth
{
    public partial class RecoverPassword : System.Web.UI.Page
    {
        private DB db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = DB.GetInstance();
        }

        private string EmailErrorMsg(string email)
        {
            User user = db.ReadUser(email);
            if (user == null) return "User does not exists";
            return "";
        }

        private bool Check_Email()
        {
            string error = EmailErrorMsg(Email_Input.Text);
            EmailError.Text = error;
            return error == "";
        }

        protected void Submit_Form(object sender, EventArgs e)
        {
            bool emailIsCorrect = Check_Email();

            if (emailIsCorrect)
            {
                Response.Redirect($"/auth/NewPassword.aspx?email={Email_Input.Text}");
            }
        }
    }
}