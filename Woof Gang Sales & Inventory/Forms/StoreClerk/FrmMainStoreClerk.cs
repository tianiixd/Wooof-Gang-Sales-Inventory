using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Forms.Admin; // We reuse Admin forms
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.StoreClerk
{
    public partial class FrmMainStoreClerk : Form
    {
        private User _loggedInUser;
        public User? LoggedInUser { get; set; }

        // Reuse the same form instances
        private FrmPOS posView;
        private FrmSalesHistory saleView;
        private FrmDashboard dashboardView; // Optional for Clerk

        public FrmMainStoreClerk(User? user = null)
        {
            InitializeComponent();

            // 1. Set the Property (which RefreshProfile uses)
            this.LoggedInUser = user;
            // Also set the field if used elsewhere, though typically one is enough.
            _loggedInUser = user;

            // 2. Safety Check
            if (user != null)
            {
                // Optional: You can set it here immediately
                lblUser.Text = $"{user.FirstName} {user.LastName}";
                lblRole.Text = user.Role;
            }

            // Default View
            btnPOS.PerformClick();
        }

        private void FrmMainStoreClerk_Load(object sender, EventArgs e)
        {
            RefreshProfile(LoggedInUser);
        }

        // --- NAVIGATION LOGIC (Reusing Existing Forms) ---

        private void AddControls(Form form)
        {
            this.CenterPanel.Controls.Clear();
            form.Dock = DockStyle.Fill;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            CenterPanel.Controls.Add(form);
            form.Show();
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            if (posView == null || posView.IsDisposed) posView = new FrmPOS();
            AddControls(posView);
        }

        private void LoadProfileImage(string imagePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    // Dispose existing image first to prevent memory lock
                    if (picProfileImage.Image != null && picProfileImage.Image != Properties.Resources.user)
                    {
                        var oldImage = picProfileImage.Image;
                        picProfileImage.Image = null;
                        oldImage.Dispose();
                    }

                    // Load image without locking the source file
                    using (var tempImage = Image.FromFile(imagePath))
                    {
                        picProfileImage.Image = new Bitmap(tempImage);
                    }

                    picProfileImage.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    // If no image or file missing → use default
                    picProfileImage.Image = Properties.Resources.user;
                    picProfileImage.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading profile image: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                picProfileImage.Image = Properties.Resources.user;
                picProfileImage.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        public void RefreshProfile(User? user)
        {
            User displayUser = user ?? LoggedInUser;

            if (displayUser != null)
            {
                string fullName = $"{displayUser.FirstName} {displayUser.LastName}";

                // ✅ Update the controls
                lblUser.Text = fullName;
                lblRole.Text = $"{displayUser.Role}";

                // ✅ Load profile image
                LoadProfileImage(displayUser.ProfileImagePath);

                // ✅ Force the UI to repaint immediately
                lblUser.Invalidate();
                lblUser.Update();
                lblRole.Invalidate();
                lblRole.Update();
                picProfileImage.Invalidate();
                picProfileImage.Update();
            }
            else
            {
                lblUser.Text = "Unknown User";
                lblRole.Text = "";
                picProfileImage.Image = Properties.Resources.user;
            }
        }

        private void btnSalesHistory_Click(object sender, EventArgs e)
        {
            if (saleView == null || saleView.IsDisposed) saleView = new FrmSalesHistory();

            // The FrmSalesHistory already has logic:
            // "if (Session.User.Role == 'StoreClerk') { Filter by ID }"
            // So it automatically restricts data for this user!
            AddControls(saleView);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // Optional: If you want Clerks to see the dashboard stats
            if (dashboardView == null || dashboardView.IsDisposed) dashboardView = new FrmDashboard();
            AddControls(dashboardView);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogHelper.ShowConfirmDialog(
            "Logout Confirmation",
             "Are you sure you want to logout?",
            "warning"
            );

            if (result == DialogResult.No)
            {
                return;
            }
            else
            {


                this.Hide();
                new frmLogin().Show();
            }
        }

        // --- WINDOW CONTROLS ---
        private void btnExit_Click(object sender, EventArgs e) => Application.Exit();

        
    }
}