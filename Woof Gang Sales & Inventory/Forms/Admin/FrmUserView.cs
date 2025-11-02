using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Admin;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Helpers;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmUserView : frmView
    {
        private UserRepository userRepo = new UserRepository();
        private string[] statusUsers = {"Active Users", "Archived Users", "All Users"};
        private string[] roleUsers = {"All Roles", "Admin", "StoreClerk"};
        public FrmUserView()
        {
            InitializeComponent();
            dgvUser.CellFormatting += (s, e) =>
            {
                if (dgvUser.Columns[e.ColumnIndex].Name == "Status")
                {
                    string value = e.Value?.ToString() ?? "";

                    if (value == "Yes" || value.Equals("Activated", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.SelectionForeColor = Color.Green; // ✅ keeps color when selected
                        e.CellStyle.Font = new Font("Segoe UI", 10F); // no bold
                    }
                    else if (value == "No" || value.Equals("Archived", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.SelectionForeColor = Color.Red; // ✅ keeps color when selected
                        e.CellStyle.Font = new Font("Segoe UI", 10F);
                    }
                }
            };
            ReadUsers();
            cmbFilterStatus.SelectedIndexChanged += FilterChanged;
            cmbFilterRole.SelectedIndexChanged += FilterChanged;
            
        }

        private void FrmUserView_Load(object sender, EventArgs e)
        {
            foreach (var status in statusUsers)
            {
                this.cmbFilterStatus.Items.Add(status);
                

            }
            this.cmbFilterStatus.SelectedIndex = 0;
          

            foreach (var role in roleUsers)
            {
                this.cmbFilterRole.Items.Add(role);
            }
            this.cmbFilterRole.SelectedIndex = 0;
            

        }

        public override void btnAdd_Click(object sender, EventArgs e)
        {
            FrmCreateEditUser form = new FrmCreateEditUser();
            form.IsEditMode = false;

            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadUsers();
            }

        }

        public override void btnEdit_Click(object sender, EventArgs e)
        {

            if (dgvUser.SelectedRows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Selection", "Please select a user first.", "warning");
                return;
            }

            var value = this.dgvUser.SelectedRows[0].Cells[0].Value.ToString();

            if (string.IsNullOrEmpty(value)) return;

            if (!int.TryParse(value, out int userID))
            {
                DialogHelper.ShowCustomDialog("Invalid Selection", "The selected category ID is invalid.", "error");
                return;
            }

            var user = userRepo.GetUserById(userID);

            if (user == null)
            {
                DialogHelper.ShowCustomDialog("Not Found", "The selected user no longer exists.", "error");
                ReadUsers();
                return;
            }
            var dashboard = FrmAdminDashboard.GetInstance();
            bool isEditingSelf = dashboard.LoggedInUser?.UserID == userID;

            string originalRole = user.Role;
            bool wasActive = user.IsActive;

            FrmCreateEditUser form = new FrmCreateEditUser();
            form.IsEditMode = true;
            form.EditUser(user);

            if (form.ShowDialog() == DialogResult.OK)
            {
                // Refresh the grid
                ReadUsers();

                // ✅ If user edited themselves, update sidebar
                if (isEditingSelf)
                {
                    // Get fresh user data from database
                    var updatedUser = userRepo.GetUserById(userID);
                    if (updatedUser != null)
                    {
                        bool roleChanged = originalRole != updatedUser.Role;
                        bool deactivated = wasActive && !updatedUser.IsActive;
                        if (roleChanged || deactivated)
                        {
                            string reason = roleChanged ? "Your role has been changed." : "Your account has been deactivated.";

                            DialogHelper.ShowCustomDialog("Account Changed", $"{reason} You will be logged out for security reasons.","warning");

                            FrmAdminDashboard.ResetInstance();

                            
                            frmLogin loginForm = new frmLogin();
                            loginForm.Show();
                            return;
                        }
                        else
                        {
                            dashboard.LoggedInUser = updatedUser;
                            dashboard.RefreshProfile(updatedUser);
                        }
                    }
                }
            }
        }

        public override void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvUser.SelectedRows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Selection", "Please select a user first.", "warning");
                return;
            }

            var value = this.dgvUser.SelectedRows[0].Cells[0].Value.ToString();

            var username = this.dgvUser.SelectedRows[0].Cells["Username"].Value.ToString();

            if (string.IsNullOrEmpty(value)) return;

            int userID = int.Parse(value);

            DialogResult result = DialogHelper.ShowConfirmDialog("Archive User","Are you sure you want to archive this user?","warning");

            if (result == DialogResult.No) return;
           
            bool success = userRepo.DeleteUser(userID, username);
            if (success)
                ReadUsers();

        }


        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ReadUsers();
        }


        private void ReadUsers()
        {
            string searchText = txtSearch.Text.Trim();
            var users = userRepo.GetUsers(searchText);

            // Dito combobox Filter
            string selectedStatus = cmbFilterStatus.SelectedItem?.ToString() ?? "All Users";
            string selectedRole = cmbFilterRole.SelectedItem?.ToString() ?? "All roles";

            // Filter sa status ng user
            if (selectedStatus == "Active Users")
                users = users.Where(u => u.IsActive).ToList();
            else if (selectedStatus == "Archived Users")
                users = users.Where(u => !u.IsActive).ToList();

            // Filter sa roles
            if (selectedRole != "All Roles")
                users = users.Where(u => u.Role.Equals(selectedRole, StringComparison.OrdinalIgnoreCase)).ToList();

            // Eto yung pag build ng DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("UserID");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("MiddleName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("Username");
            dt.Columns.Add("Role");
            dt.Columns.Add("Status");

            foreach (var user in users)
            {
                var row = dt.NewRow();
                row["UserID"] = user.UserID;
                row["FirstName"] = user.FirstName;
                row["MiddleName"] = user.MiddleName;
                row["LastName"] = user.LastName;
                row["Username"] = user.Username;
                row["Role"] = user.Role;
                row["Status"] = user.IsActive ? "Activated" : "Archived";
                dt.Rows.Add(row);
            }

            dgvUser.DataSource = dt;
            DataGridViewStyler.ApplyStyle(dgvUser, "UserID");

            //dgvUser.DefaultCellStyle.SelectionForeColor = dgvUser.DefaultCellStyle.ForeColor;

            foreach (DataGridViewRow row in dgvUser.Rows)
            {
                string status = row.Cells["Status"].Value?.ToString() ?? "";
                if (status == "Activated")
                {
                    row.Cells["Status"].Style.ForeColor = Color.Green;
                    row.Cells["Status"].Style.Font = new Font(dgvUser.Font, FontStyle.Bold);
                }
                else if (status == "Archived")
                {
                    row.Cells["Status"].Style.ForeColor = Color.Red;
                    row.Cells["Status"].Style.Font = new Font(dgvUser.Font, FontStyle.Bold);
                }
            }

        }


        private void FilterChanged(object sender, EventArgs e)
        {
            ReadUsers();
        }

    }
}
