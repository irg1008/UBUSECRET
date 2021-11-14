using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Log;
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
                throw new HttpException(404, "");

            LoadRequestedUsers();
            LoadLog();
        }

        private void CleanLogTable()
        {
            // Clean table.
            while (LogTable.Rows.Count > 1) LogTable.Rows.RemoveAt(1);
        }

        private void LoadLog()
        {
            List<LogEntry> logs = db.LogList();

            if (logs.Count() == 0)
            {
                NoLog.Visible = true;
                LogTable.Visible = false;
                ClearLogButton.Visible = false;
            }
            else
            {
                // Clean table.
                CleanLogTable();

                foreach (LogEntry log in logs)
                {
                    TableCell type = new TableCell { Text = log.Entry };
                    TableCell message = new TableCell { Text = log.Message };
                    TableCell date = new TableCell { Text = log.Date.ToString() };

                    TableRow newRow = new TableRow();
                    newRow.Cells.Add(type);
                    newRow.Cells.Add(message);
                    newRow.Cells.Add(date);

                    LogTable.Rows.Add(newRow);
                };
            }
        }

        protected void ClearLog(object sender, EventArgs e)
        {
            db.ClearLogs();
            CleanLogTable();
            LoadLog();
        }

        private void LoadRequestedUsers()
        {
            // Get only requested users.
            List<User> requestedUsers = db.GetRequestedUsers();

            if (requestedUsers.Count() == 0)
            {
                NoUsers.Visible = true;
                UsersTable.Visible = false;
            }
            else
            {
                // Clean table.
                while (UsersTable.Rows.Count > 1) UsersTable.Rows.RemoveAt(1);

                // Show data for every user to be authorized.
                foreach (User user in requestedUsers)
                {
                    TableRow newRow = NewTableRow(user);
                    UsersTable.Rows.Add(newRow);
                }
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
            Button btn = new Button { Text = "Authorize", ID = user.Id.ToString() };
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