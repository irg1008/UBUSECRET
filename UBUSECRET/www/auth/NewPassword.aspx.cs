using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data;
using Main;
using Val = Utils.Validate;

namespace www.auth
{
    public partial class NewPassword : System.Web.UI.Page
    {
        private DB db;
        private User userToChangePass;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = DB.GetInstance();

            if (!IsPostBack)
                Master.Master.ShowPopUp("You need to set up a new password to activate your account. Don't worry, you only have to do this once.", PopUpType.INFO);

            // If user email is not correct.
            string userEmail = Request.QueryString["email"];
            bool isCorrectEmail = IsCorrectUserEmail(userEmail);

            if (!isCorrectEmail)
                throw new HttpException(404, "");
        }

        private bool IsCorrectUserEmail(string email)
        {
            // If emprty param.
            if (email == "") return false;

            // If user does not exist.
            userToChangePass = db.ReadUser(email);
            if (userToChangePass == null) return false;

            return true;
        }

        private string OldPasswordErrorMsg(string password)
        {
            bool correctPassword = userToChangePass.CheckPasword(password);
            if (!correctPassword) return "Password is incorrect";
            return "";
        }

        private string NewPasswordErrorMsg(string password)
        {
            bool isEmpty = password == "";
            if (isEmpty) return "New Password cannot be empty";

            bool passwordIsCorrect = Val.Password(password);
            if (!passwordIsCorrect) return "Incorrect new password format";

            // Check if different from previous.
            bool isSameAsPrevious = userToChangePass.CheckPasword(password);
            if (isSameAsPrevious) return "Please do not use the same password";

            return "";
        }

        private string ConfirmNewPasswordErrorMsg(string first, string second)
        {
            bool isEmpty = second == "";
            if (isEmpty) return "The confirm new password field cannot be empty";

            if (first != second) return "The new passwords do not match";

            return "";
        }

        private bool Check_OldPassword()
        {
            string error = OldPasswordErrorMsg(OldPassword_Input.Text);
            OldPasswordError.Text = error;
            return error == "";
        }

        private bool Check_NewPassword()
        {
            string error = NewPasswordErrorMsg(NewPassword_Input.Text);
            NewPasswordError.Text = error;
            return error == "";
        }

        private bool Check_ConfirmNewPassword()
        {
            string error = ConfirmNewPasswordErrorMsg(NewPassword_Input.Text, ConfirmNewPassword_Input.Text);
            ConfirmNewPasswordError.Text = error;
            return error == "";
        }

        private void ConfirmNewPassword()
        {
            // Change password.
            string oldPassword = OldPassword_Input.Text;
            string newPassword = NewPassword_Input.Text;
            userToChangePass.ChangePassword(oldPassword, newPassword);

            // Log in.
            Master.Master.LogIn(userToChangePass);
        }

        protected void Submit_Form(object sender, EventArgs e)
        {
            bool oldPassIsCorrect = Check_OldPassword();
            bool passIsCorrect = Check_NewPassword();
            bool checkIsCorrect = Check_ConfirmNewPassword();

            if (oldPassIsCorrect && passIsCorrect && checkIsCorrect)
                ConfirmNewPassword();
        }
    }
}