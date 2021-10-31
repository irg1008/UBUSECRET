using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Main;
using Data;

namespace www.admin
{
    public partial class Users : System.Web.UI.Page
    {
        private DB db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = DB.GetInstance();

            // Check logged user exists and is admin.
            User loggedUser = Master.GetUser();

            if (loggedUser == null || !loggedUser.IsAdmin)
                Response.Redirect("Error.aspx");

            // Load all users.
            LoadRequestedUsers();
        }

        private void LoadRequestedUsers()
        {
            IList<User> users = db.UserList();

            // Get only requested users.
            IEnumerable<User> requestedUsers = users.Where(user => user.State == State.REQUESTED);

            if (requestedUsers.Count() == 0)
            {
                NoUsers.Visible = true;
                UsersTable.Visible = false;
            }

            // Show data for every user to be authorized.
            foreach (User user in requestedUsers)
            {
                TableRow newRow = NewTableRow(user);
                UsersTable.Rows.Add(newRow);
            }
        }

        private TableRow NewTableRow(User user)
        {
            TableRow row = new TableRow();
            TableCell cell;

            // 5 Cells:
            //  * Name.
            //  * Email.
            //  * Role.
            //  * Last seen.
            //  * Authorize button.

            // Name.
            cell = new TableCell { Text = user.Name };
            row.Cells.Add(cell);

            // Email.
            cell = new TableCell { Text = user.Email };
            row.Cells.Add(cell);

            // Role.
            cell = new TableCell { Text = user.IsAdmin ? "Admin" : "User" };
            row.Cells.Add(cell);

            // Last Seen.
            cell = new TableCell { Text = user.LastSeen.ToString() };
            row.Cells.Add(cell);

            // Authorize button.
            Button btn = new Button { Text = "Authorize" };
            void AuthorizeUser(Object sender, EventArgs e)
            {
                user.Authorize();
                LoadRequestedUsers();
            }
            btn.Click += new EventHandler(AuthorizeUser);
            cell = new TableCell();
            cell.Controls.Add(btn);
            row.Cells.Add(cell);

            return row;
        }
    }
}