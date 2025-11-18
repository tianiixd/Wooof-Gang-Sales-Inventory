using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Admin;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Helpers;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmUserView : frmView
    {
        private UserRepository userRepo = new UserRepository();

        TimeClockHelper time = new TimeClockHelper();

        private string[] statusUsers = {"Active Users", "Archived Users", "All Users"};
        private string[] roleUsers = {"All Roles", "Admin", "StoreClerk"};

        // Icons
        private Image editIcon = Properties.Resources.edit2;
        private Image deleteIcon = Properties.Resources.delete2;

        // ✅ --- CONSTANTS FOR LAYOUT ---
        // We define these here so Paint, Click, and MouseMove all align perfectly.
        private int btnWidth = 40;
        private int btnHeight = 35;
        private int btnSpacing = 10;
        private int iconSize = 20;

        private int hoveredRowIndex = -1;
        private string hoveredButton = ""; // Values: "Edit", "Delete", or ""
        public FrmUserView()
        {
            InitializeComponent();
            dgvUser.CellMouseClick += dgvUser_CellMouseClick;
            dgvUser.CellPainting += dgvUser_CellPainting;

            // ✅ --- NEW: Smart Cursor Logic ---
            dgvUser.CellMouseMove += dgvUser_CellMouseMove;
            dgvUser.CellMouseLeave += dgvUser_CellMouseLeave;
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
            time.StartClock(lblTime, lblDate);

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


            DataGridViewButtonColumn actionCol = new DataGridViewButtonColumn();
            actionCol.Name = "Actions";
            actionCol.HeaderText = "Actions";
            actionCol.Text = "";
            actionCol.UseColumnTextForButtonValue = false;

            // ✅ --- FIX: STRICT WIDTH CONTROL ---
            actionCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            actionCol.Resizable = DataGridViewTriState.False; // Prevent resizing
            actionCol.Width = 150; // Fixed width

            dgvUser.Columns.Add(actionCol);

        }

        private void dgvUser_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvUser.Columns[e.ColumnIndex].Name == "Actions")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);

                // Calculate Center
                int totalWidth = (btnWidth * 2) + btnSpacing;
                int startX = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int startY = e.CellBounds.Y + (e.CellBounds.Height - btnHeight) / 2;

                Color editColor = Color.FromArgb(47, 128, 237);    // Normal Blue
                Color deleteColor = Color.FromArgb(235, 87, 87);

                if (e.RowIndex == hoveredRowIndex)
                {
                    if (hoveredButton == "Edit")
                        editColor = Color.FromArgb(87, 158, 255); // Lighter Blue

                    if (hoveredButton == "Delete")
                        deleteColor = Color.FromArgb(255, 117, 117); // Lighter Red
                }

                // Draw Edit (Blue)
                Rectangle editRect = new Rectangle(startX, startY, btnWidth, btnHeight);
                DrawRoundedButton(e.Graphics, editRect, editColor, editIcon);

                // Draw Delete (Red)
                Rectangle deleteRect = new Rectangle(startX + btnWidth + btnSpacing, startY, btnWidth, btnHeight);
                DrawRoundedButton(e.Graphics, deleteRect, deleteColor, deleteIcon);

                e.Handled = true;
            }
        }

        private void DrawRoundedButton(Graphics g, Rectangle rect, Color color, Image icon)
        {
            using (GraphicsPath path = GetRoundedPath(rect, 6))
            using (SolidBrush brush = new SolidBrush(color))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillPath(brush, path);
            }

            if (icon != null)
            {
                g.DrawImage(icon, new Rectangle(
                    rect.X + (btnWidth - iconSize) / 2,
                    rect.Y + (btnHeight - iconSize) / 2,
                    iconSize, iconSize));
            }
        }

        // ✅ --- CLICK LOGIC ---
        private void dgvUser_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvUser.Columns[e.ColumnIndex].Name == "Actions")
            {
                // Calculate positions
                int w = dgvUser.Columns[e.ColumnIndex].Width;
                int totalWidth = (btnWidth * 2) + btnSpacing;
                int startX = (w - totalWidth) / 2;

                int userID = Convert.ToInt32(dgvUser.Rows[e.RowIndex].Cells["UserID"].Value);

                // Check Edit Click (Blue Area)
                if (e.X >= startX && e.X <= startX + btnWidth)
                {
                    var user = userRepo.GetUserById(userID);
                    if (user == null) return;

                    bool isEditingSelf = SessionManager.CurrentUser?.UserID == userID;
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

                                    DialogHelper.ShowCustomDialog("Account Changed", $"{reason} You will be logged out for security reasons.", "warning");

                                    FrmAdminDashboard.ResetInstance();

                                    SessionManager.CurrentUser = null;

                                    frmLogin loginForm = new frmLogin();
                                    loginForm.Show();
                                    return;
                                }
                                else
                                {
                                    SessionManager.CurrentUser = updatedUser;
                                    // Get the dashboard instance to refresh its UI
                                    var dashboard = FrmAdminDashboard.GetInstance();
                                    dashboard.RefreshProfile(updatedUser);
                                }
                            }
                        }
                    }
                }
                // Check Delete Click (Red Area)
                else if (e.X >= startX + btnWidth + btnSpacing && e.X <= startX + (btnWidth * 2) + btnSpacing)
                {
                    if (SessionManager.CurrentUser?.UserID == userID)
                    {
                        DialogHelper.ShowCustomDialog("Action Denied", "You cannot archive your own account while you are logged in.", "error");
                        return;
                    }

                    // 4. FIXED: Retrieve the username for the Repo method
                    string username = dgvUser.Rows[e.RowIndex].Cells["Username"].Value.ToString();

                    DialogResult result = DialogHelper.ShowConfirmDialog("Archive User", "Are you sure you want to archive this user?", "warning");
                    if (result == DialogResult.No) return;

                    bool success = userRepo.DeleteUser(userID, username);
                    if (success)
                        ReadUsers();
                }
            }
        }

        // ✅ --- SMART CURSOR LOGIC ---
        private void dgvUser_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvUser.Columns[e.ColumnIndex].Name == "Actions")
            {
                // Calculate positions
                int w = dgvUser.Columns[e.ColumnIndex].Width;
                int totalWidth = (btnWidth * 2) + btnSpacing;
                int startX = (w - totalWidth) / 2;

                // Identify which button is being hovered
                string newHoveredButton = "";

                if (e.X >= startX && e.X <= startX + btnWidth)
                {
                    newHoveredButton = "Edit";
                    dgvUser.Cursor = Cursors.Hand;
                }
                else if (e.X >= startX + btnWidth + btnSpacing && e.X <= startX + (btnWidth * 2) + btnSpacing)
                {
                    newHoveredButton = "Delete";
                    dgvUser.Cursor = Cursors.Hand;
                }
                else
                {
                    dgvUser.Cursor = Cursors.Default;
                }

                // ✅ OPTIMIZATION: Only repaint if the state has actually changed
                // This prevents the grid from flickering wildly while moving the mouse
                if (hoveredRowIndex != e.RowIndex || hoveredButton != newHoveredButton)
                {
                    hoveredRowIndex = e.RowIndex;
                    hoveredButton = newHoveredButton;
                    dgvUser.InvalidateCell(e.ColumnIndex, e.RowIndex); // Trigger CellPainting
                }
            }
            else
            {
                dgvUser.Cursor = Cursors.Default;
                // Reset if we moved to a different column
                if (hoveredRowIndex != -1)
                {
                    hoveredRowIndex = -1;
                    hoveredButton = "";
                    dgvUser.InvalidateRow(e.RowIndex);
                }
            }
        }

        private void dgvUser_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvUser.Cursor = Cursors.Default;

            if (hoveredRowIndex != -1)
            {
                // Clear the hover state and redraw the row to remove the glow
                int rowToInvalidate = hoveredRowIndex;
                hoveredRowIndex = -1;
                hoveredButton = "";
                if (rowToInvalidate >= 0 && rowToInvalidate < dgvUser.Rows.Count)
                    dgvUser.InvalidateRow(rowToInvalidate);
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
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
                DialogHelper.ShowCustomDialog("Invalid Selection", "The selected userID is invalid.", "error");
                return;
            }

            var user = userRepo.GetUserById(userID);

            if (user == null)
            {
                DialogHelper.ShowCustomDialog("Not Found", "The selected user no longer exists.", "error");
                ReadUsers();
                return;
            }

            bool isEditingSelf = SessionManager.CurrentUser?.UserID == userID;

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

                            SessionManager.CurrentUser = null;

                            frmLogin loginForm = new frmLogin();
                            loginForm.Show();
                            return;
                        }
                        else
                        {
                            SessionManager.CurrentUser = updatedUser;
                            // Get the dashboard instance to refresh its UI
                            var dashboard = FrmAdminDashboard.GetInstance();
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

            if (int.TryParse(value, out int parsedUserID) && SessionManager.CurrentUser?.UserID == parsedUserID)
            {
                DialogHelper.ShowCustomDialog("Action Denied", "You cannot archive your own account while you are logged in.", "error");
                return;
            }

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


        public void ReadUsers()
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

            if (selectedStatus == "Active Users" || selectedStatus == "Archived Users")
            {
                dgvUser.Columns["Status"].Visible = false;
            }
            else
            {
                dgvUser.Columns["Status"].Visible = true;
            }


        }


        private void FilterChanged(object sender, EventArgs e)
        {
            ReadUsers();
        }

    }
}
