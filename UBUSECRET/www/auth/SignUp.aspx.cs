using System;
using Val = Utils.Validate;
using Data;
using Main;

namespace www.auth
{
    public partial class SignUp : System.Web.UI.Page
    {
        private DB db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = DB.GetInstance();
        }

        private string EmailErrorMsg(string email)
        {
            bool isEmpty = email == "";
            if (isEmpty) return "Email cannot be empty";

            bool emailIsCorrect = Val.Email(email);
            if (!emailIsCorrect) return "Incorrect email format";

            bool userExists = db.ContainsUser(email);
            if (userExists) return "User already exists";

            return "";
        }

        private string PasswordErrorMsg(string password)
        {
            bool isEmpty = password == "";
            if (isEmpty) return "Password cannot be empty";

            bool passwordIsCorrect = Val.Password(password);
            if (!passwordIsCorrect) return "Incorrect password format";

            return "";
        }

        private string NameErrorMsg(string name)
        {
            bool isEmpty = name == "";
            if (isEmpty) return "Name cannot be empty";

            return "";
        }

        private string ConfirmPasswordErrorMsg(string first, string second)
        {
            bool isEmpty = second == "";
            if (isEmpty) return "This field cannot be empty";

            if (first != second) return "The passwords do not match";

            return "";
        }

        private bool Check_Name()
        {
            string error = NameErrorMsg(Name_Input.Text);
            NameError.Text = error;
            return error == "";
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

        private bool Check_ConfirmPassword()
        {
            string error = ConfirmPasswordErrorMsg(Password_Input.Text, ConfirmPassword_Input.Text);
            ConfirmPasswordError.Text = error;
            return error == "";
        }

        private void CreateAccount()
        {
            User newUser = new User(Name_Input.Text, Email_Input.Text, Password_Input.Text);

            // Request authorization.
            newUser.Request();

            db.InsertUser(newUser);

            Email_Input.Text = "";
            Name_Input.Text = "";

            Master.Master.ShowPopUp("User created successfully. You will be authorized soon.", PopUpType.SUCCESS);
        }

        protected void Submit_Form(object sender, EventArgs e)
        {
            bool nameIsCorrect = Check_Name();
            bool emailIsCorrect = Check_Email();
            bool passIsCorrect = Check_Password();
            bool checkIsCorrect = Check_ConfirmPassword();

            if (nameIsCorrect && emailIsCorrect && passIsCorrect && checkIsCorrect)
                CreateAccount();
        }
    }
}