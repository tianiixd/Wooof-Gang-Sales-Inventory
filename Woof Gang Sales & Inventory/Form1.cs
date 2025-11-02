using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Database;
using Woof_Gang_Sales___Inventory.Util;
using Guna.UI2.WinForms;
namespace Woof_Gang_Sales___Inventory
{
    public partial class Form1 : Sample
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
            string firstName = txtFName.Text.Trim();
            string lastName = txtLName.Text.Trim();
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cmbRole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                DialogHelper.ShowCustomDialog("Validation", "Please fill out all required fields", "warning");
                return;
            }

            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    
                    string checkUser = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                    SqlCommand checkCmd = new SqlCommand(checkUser, conn);
                    checkCmd.Parameters.AddWithValue("@username", username);
                    int userExists = (int)checkCmd.ExecuteScalar();

                    if (userExists > 0)
                    {
                        DialogHelper.ShowCustomDialog("Duplicate", "Username already exists please choose a different one", "warning");
                        return;
                    }

                    
                    string hashedPassword = PasswordHelper.HashPassword(password);

                    string query = @"INSERT INTO Users (FirstName, LastName, Username, PasswordHash, Role, IsActive)
                             VALUES (@first, @last, @username, @password, @role, 1)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@first", firstName);
                    cmd.Parameters.AddWithValue("@last", lastName);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);
                    cmd.Parameters.AddWithValue("@role", role);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] roles = {"Admin", "StoreClerk"};
            foreach (string role in roles)
            {
                cmbRole.Items.Add(role);
            }
            
            cmbRole.SelectedIndex = 0;

        }
    }
}
