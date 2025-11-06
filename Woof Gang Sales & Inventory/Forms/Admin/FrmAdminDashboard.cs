using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Forms.Admin;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Woof_Gang_Sales___Inventory.Admin
{
    public partial class FrmAdminDashboard : Sample
    {

        static FrmAdminDashboard formAdmin;
        public User? LoggedInUser { get; set; }

        private FrmUserView currentUserView;
        private FrmProductView productView;
        private FrmCategoryView categoryView;
        private FrmSubCategoryView subCategoryView;
        private FrmSupplierView supplierView;
        private FrmPOS posView;
        public static FrmAdminDashboard GetInstance(User? user = null)
        {
            if (formAdmin == null || formAdmin.IsDisposed)
            {
                formAdmin = new FrmAdminDashboard(user);
            }
            else if (user != null)
            {
                // ✅ Update LoggedInUser if instance already exists
                formAdmin.LoggedInUser = user;
            }
            return formAdmin;
        }

        public static void ResetInstance()
        {
            if (formAdmin != null)
            {
                // ✅ Just set to null, don't close/dispose yet
                // The form will be disposed naturally when it closes
                var temp = formAdmin;
                formAdmin = null;

                // Only dispose if not already disposed
                if (!temp.IsDisposed)
                {
                    temp.Dispose();
                }
            }
        }

        public FrmAdminDashboard(User? user = null)
        {
            InitializeComponent();
            LoggedInUser = user;
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            guna2MessageDialog1.Parent = this;
            btnMaximize.PerformClick();

            RefreshProfile(LoggedInUser);
        }

        public void UpdateLoggedInUser(User updatedUser)
        {
            LoggedInUser = updatedUser;
            RefreshProfile(updatedUser);
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

        public void AddControls(Form form)
        {
            this.CenterPanel.Controls.Clear();
            form.Dock = DockStyle.Fill; // ✅ ensures it fills the entire panel
            form.TopLevel = false;      // ✅ needed for embedding a Form
            form.FormBorderStyle = FormBorderStyle.None; // ✅ removes window borders
            CenterPanel.Controls.Add(form);
            form.Show();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            // ✅ Reuse existing instance or create new one
            if (currentUserView == null || currentUserView.IsDisposed)
            {
                currentUserView = new FrmUserView();
            }

            AddControls(currentUserView);
        }

        private void btnLogOut_Click(object sender, EventArgs e)
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
                FrmAdminDashboard.ResetInstance();

                frmLogin form = new frmLogin();
                form.Show();
            }
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            if (productView == null || productView.IsDisposed)
            {
                productView = new FrmProductView();
            }

            AddControls(productView);
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            if (categoryView == null || categoryView.IsDisposed)
            {
                categoryView = new FrmCategoryView();
            }

            AddControls(categoryView);
        }

        private void btnSubCategory_Click(object sender, EventArgs e)
        {
            if (subCategoryView == null || categoryView.IsDisposed)
            {
                subCategoryView = new FrmSubCategoryView();
            }

            AddControls(subCategoryView);
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            if (supplierView == null || supplierView.IsDisposed)
            {
                supplierView = new FrmSupplierView();
            }

            AddControls(supplierView);
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            if (posView == null || posView.IsDisposed)
            {
                posView = new FrmPOS();
            }

            AddControls(posView);   
        }
    }
}
