#nullable enable
using Guna.UI2.WinForms;
using BCrypt;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Database;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Admin;
using Woof_Gang_Sales___Inventory.Util;
using Woof_Gang_Sales___Inventory.Forms.Admin;
namespace Woof_Gang_Sales___Inventory
{
    public partial class frmLogin : Sample
    {
        private readonly UserRepository userRepo = new UserRepository();

        private bool passwordVisible = false;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public frmLogin()
        {
            InitializeComponent();
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (!verifyInput(username, password)) return;

            try
            {
                // Dummy hash to prevent timing attacks
                string dummyHash = "$2a$10$7EqJtq98hPqEX7fNZaFWoOhi5g1f1f1f1f1f1f1f1f1f1f1f1f1f"; // bcrypt dummy
                bool passwordMatch = false;

                User? user = userRepo.GetUserByName(username);

                if (user != null)
                {
                    passwordMatch = PasswordHelper.VerifyPassword(password, user.PasswordHash);
                }
                else
                {
                    // Dummy check to equalize timing
                    PasswordHelper.VerifyPassword(password, dummyHash);
                }

                // Unified error message to prevent user enumeration
                if (!passwordMatch)
                {
                    DialogHelper.ShowCustomDialog("Login Failed", "Invalid username or password.", "error");
                    return;
                }

                SessionManager.CurrentUser = user;

                // Successful login
                string fullName = $"{user.FirstName} {user.LastName}";
                DialogHelper.ShowCustomDialog("Login Successful", $"Welcome {fullName}! You are logged in as {user.Role}.", "success");

                this.Hide();

                switch (user.Role)
                {
                    case "Admin":
                        FrmAdminDashboard.GetInstance(user).Show();
                        break;
                    case "StoreClerk":
                        new FrmProductView().Show();
                        break;
                    default:
                        DialogHelper.ShowCustomDialog("Access Denied", "Your role is not authorized to access this application.", "error");
                        break;
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Occurred", "An unexpected error occurred. Please contact support.", "error");
                // Optional: Log ex.Message internally for diagnostics
            }
        }



        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            
            txtPassword.IconRightClick += TxtPassword_IconRightClick;
            EnableDrag(this); // entire form
            EnableDrag(guna2Panel1);
        }

        private void EnableDrag(Control control)
        {
            control.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
                }
            };
        }

        private void TxtPassword_IconRightClick(object sender, EventArgs e)
        {
            passwordVisible = !passwordVisible;

           
            txtPassword.UseSystemPasswordChar = !passwordVisible; 
                                                                  

            
            txtPassword.IconRight = passwordVisible ? Properties.Resources.eye_closed : Properties.Resources.eye;

            
            txtPassword.SelectionStart = txtPassword.Text?.Length ?? 0;
            txtPassword.SelectionLength = 0;
        }



        private bool verifyInput(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                SystemSounds.Hand.Play();
                DialogHelper.ShowCustomDialog("Login Failed", "Please enter username and password", "warning");
                return false;
            }
            return true;
                
        }

        private void guna2VSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }
    }
}
