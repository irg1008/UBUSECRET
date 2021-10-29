using System;
using Data;
using Main;

namespace www
{
    public partial class LogIn : System.Web.UI.Page
    {
        private DB db;
        private User userToLogIn;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = DB.GetInstance();
        }

        private string EmailErrorMsg(string email)
        {
            User user = db.ReadUser(email);
            if (user == null) return "User does not exists";
            else userToLogIn = user;
            return "";
        }

        private string PasswordErrorMsg(string password)
        {
            bool correctPassword = userToLogIn.CheckPasword(password);
            if (!correctPassword) return "Password is incorrect";
            return "";
        }

        private bool Check_Email()
        {
            string error = EmailErrorMsg(Email_Input.Text);
            EmailError.Text = error;
            return error == "";
        }

        private bool Check_Password()
        {
            string error = PasswordErrorMsg(Password_Input.Text);
            PasswordError.Text = error;
            return error == "";
        }

        private void Show(String error, PopUpType type)
        {
            Master.Master.ShowPopUp(error, type);
        }

        private void DoLogIn()
        {

            State userState = userToLogIn.State;

            switch (userState)
            {
                case State.BANNED:
                    Show("You are banned. Contact us for more information", PopUpType.ERROR);
                    break;

                case State.PREFETCHED:
                    Show("Some error ocurred. Ask admins for an authorization request", PopUpType.ERROR);
                    break;

                case State.REQUESTED:
                    Show("You need to be authorized to enter", PopUpType.INFO);
                    break;

                case State.ACTIVE:
                // Re login if already active.
                // We do this in the case user manually deleted session data.
                case State.INACTIVE:
                    Master.Master.LogIn(userToLogIn);
                    break;

                case State.AUTHORIZED:
                    // Set new password to user.
                    Response.Redirect($"/auth/NewPassword.aspx?email={userToLogIn.Email}");
                    break;

            }
        }

        protected void Submit_Form(object sender, EventArgs e)
        {
            bool emailIsCorrect = Check_Email();

            // Check first email, then password.
            if (emailIsCorrect)
            {
                bool passIsCorrect = Check_Password();

                if (passIsCorrect)
                    DoLogIn();
            }
        }
    }
}