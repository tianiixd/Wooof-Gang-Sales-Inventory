#nullable enable
using Guna.UI2.WinForms;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Database;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace Woof_Gang_Sales___Inventory.Data
{
    public class UserRepository
    {
        public User? GetUserByName(string username) // this for checking username pag nag login sila
        {
            try
            {

                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    if (conn == null) return null;
                    conn.Open();
                    string query = "SELECT UserID, FirstName, MiddleName, LastName, Username, PasswordHash, Role, IsActive, ProfileImagePath FROM Users WHERE Username=@username AND IsActive=1";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                return new User
                                {
                                    UserID = (int)reader["UserID"],
                                    FirstName = reader["FirstName"].ToString(),
                                    MiddleName = reader["MiddleName"].ToString(), // Sama parin natin incase na meron middlename
                                    LastName = reader["LastName"].ToString(),
                                    Username = reader["Username"].ToString(),
                                    PasswordHash = reader["PasswordHash"].ToString(),
                                    Role = reader["Role"].ToString(),
                                    IsActive = (bool)reader["IsActive"],
                                    ProfileImagePath = reader["ProfileImagePath"] == DBNull.Value ? null : reader["ProfileImagePath"].ToString(),
                                };

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Occured", ex.Message, "error");
            }
        
            return null;
        }

        public List<User> GetAllUsers() // pang display to ng data sa datagridview or table didisplay lahat ng users
        {
            List<User> users = new List<User>();

            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT UserID, FirstName, MiddleName, LastName, Username, Role, IsActive, ProfileImagePath FROM Users ORDER BY UserID DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                bool isActive = (bool)reader["IsActive"];
                                users.Add(new User
                                {
                                    UserID = (int)reader["UserID"],
                                    FirstName = reader["FirstName"].ToString(),
                                    MiddleName = reader["MiddleName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Username = reader["Username"].ToString(),
                                    Role = reader["Role"].ToString(),
                                    IsActive = isActive,
                                    ProfileImagePath = reader["ProfileImagePath"] == DBNull.Value ? null : reader["ProfileImagePath"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error loading user list: ", ex.Message, "error");
            }

            return users;
        }

       
        public List<User> GetUsers(string search = null)
        {
            List<User> users = new List<User>();

            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    // Base query
                    string query = @"
                SELECT 
                    UserID,
                    FirstName,
                    MiddleName,
                    LastName,
                    Username,
                    Role,
                    IsActive,
                    ProfileImagePath
                FROM Users
            ";

                    // Add WHERE only if search provided
                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        query += @"
                    WHERE FirstName LIKE @search
                       OR MiddleName LIKE @search
                       OR LastName  LIKE @search
                       OR Username LIKE @search
                       OR Username  LIKE @search
                       OR Role      LIKE @search
                ";
                    }

                    query += " ORDER BY UserID DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(search))
                        {
                            // Use parameterized query, put wildcards in the parameter value
                            cmd.Parameters.Add(new SqlParameter("@search", SqlDbType.NVarChar, 250)
                            {
                                Value = "%" + search.Trim() + "%"
                            });
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                bool isActive = reader["IsActive"] != DBNull.Value && (bool)reader["IsActive"];

                                users.Add(new User
                                {
                                    UserID = (int)reader["UserID"],
                                    FirstName = reader["FirstName"].ToString(),
                                    MiddleName = reader["MiddleName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Username = reader["Username"].ToString(),
                                    Role = reader["Role"].ToString(),
                                    IsActive = isActive,
                                    ProfileImagePath = reader["ProfileImagePath"] == DBNull.Value ? null : reader["ProfileImagePath"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error loading user list: ", ex.Message, "error");
            }

            return users;
        }


        public User? GetUserById(int id) // for getting user id pang check
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT UserID, FirstName, MiddleName, LastName, Username, Role, IsActive, ProfileImagePath FROM Users WHERE UserID=@id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    UserID = (int)reader["UserID"],
                                    FirstName = reader["FirstName"].ToString(),
                                    MiddleName = reader["MiddleName"].ToString(), 
                                    LastName = reader["LastName"].ToString(),
                                    Username = reader["Username"].ToString(),
                                    Role = reader["Role"].ToString(),
                                    IsActive = (bool)reader["IsActive"],
                                    ProfileImagePath = reader["ProfileImagePath"] == DBNull.Value ? null : reader["ProfileImagePath"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Occurred", ex.Message, "error");
            }

            return null;
        }

        public void StatsCard(Label lblTotal, Label lblAdmins, Label lblStoreClerks, Label lblInactive)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    string queryUsers = @"SELECT COUNT(*) FROM Users WHERE IsActive = 1";

                    using (SqlCommand cmd = new SqlCommand(queryUsers,conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        lblTotal.Text = count.ToString("N0");
                    }

                    string queryAdmin = @"SELECT COUNT(*) FROM Users WHERE IsActive = 1 AND Role = 'Admin'";
                    using (SqlCommand cmd = new SqlCommand(queryAdmin, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        lblAdmins.Text = count.ToString("N0");
                        lblAdmins.ForeColor = Color.Purple;
                    }

                    string queryStoreClerk = @"SELECT COUNT(*) FROM Users WHERE IsActive = 1 AND Role = 'StoreClerk'";
                    using (SqlCommand cmd = new SqlCommand(queryStoreClerk, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        lblStoreClerks.Text = count.ToString("N0");
                        lblStoreClerks.ForeColor = Color.Teal;
                    }

                    string queryInactive = @"SELECT COUNT(*) FROM Users WHERE IsActive = 0";
                    using (SqlCommand cmd = new SqlCommand(queryInactive, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        lblInactive.Text = count.ToString("N0");
                        // Logic: Turn RED if there are inactive accounts (Prompting cleanup)
                        if (count > 0)
                        {
                            lblInactive.ForeColor = Color.Red;
                        }
                        else
                        {
                            lblInactive.ForeColor = Color.FromArgb(19, 19, 26);
                        }
                    }

                }
            }catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Stats Error", ex.Message, "error");
            }
        }


        public bool CreateUser(User user)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    string checkUser = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                    using (SqlCommand checkCmd = new SqlCommand(checkUser, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@username", user.Username.Trim());
                        int userExists = (int)checkCmd.ExecuteScalar();

                        if (userExists > 0)
                        {
                            DialogHelper.ShowCustomDialog("Duplicate", "Username already exists. Please choose a different one.", "warning");
                            return false; // ❗ Return false nag fail
                        }
                    }

                    string hashedPassword = PasswordHelper.HashPassword(user.PasswordHash);

                    string insertQuery = @"
                INSERT INTO Users (FirstName, MiddleName, LastName, Username, PasswordHash, Role, ProfileImagePath)
                VALUES (@firstName, @middleName, @lastName, @username, @passwordHash, @role, @profileImagePath)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@middleName", (object)user.MiddleName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@lastName", user.LastName);
                        cmd.Parameters.AddWithValue("@username", user.Username);
                        cmd.Parameters.AddWithValue("@passwordHash", hashedPassword);
                        cmd.Parameters.AddWithValue("@role", user.Role);
                        cmd.Parameters.AddWithValue("@profileImagePath", (object?)user.ProfileImagePath ?? DBNull.Value);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            DialogHelper.ShowCustomDialog("Success", $"User '{user.Username}' created successfully!", "success");
                            return true; 
                        }
                        else
                        {
                            DialogHelper.ShowCustomDialog("Failed", "User could not be created. Please try again.", "error");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Occurred", ex.Message, "error");
                return false;
            }
        }

      

        public bool UpdateUser(User user, string? newPlainPassword = null)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    // Check if username already exists for another user
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username AND UserID != @id";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@username", user.Username.Trim());
                        checkCmd.Parameters.AddWithValue("@id", user.UserID);

                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            DialogHelper.ShowCustomDialog("Duplicate", "Username already exists. Please choose another.", "warning");
                            return false;
                        }
                    }

                    string updateQuery;
                    int rows = 0;

                    if (!string.IsNullOrWhiteSpace(newPlainPassword))
                    {
                        string newHashedPassword = PasswordHelper.HashPassword(newPlainPassword);
                        updateQuery = @"
                    UPDATE Users
                    SET FirstName = @firstName,
                        MiddleName = @middleName,
                        LastName = @lastName,
                        Username = @username,
                        Role = @role,
                        PasswordHash = @passwordHash,
                        IsActive = @IsActive,
                        ProfileImagePath = @profileImagePath,
                        UpdatedAt = GETDATE()
                    WHERE UserID = @id";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                            cmd.Parameters.AddWithValue("@middleName", (object)user.MiddleName ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@lastName", user.LastName);
                            cmd.Parameters.AddWithValue("@username", user.Username);
                            cmd.Parameters.AddWithValue("@role", user.Role);
                            cmd.Parameters.AddWithValue("@passwordHash", newHashedPassword);
                            cmd.Parameters.Add("@IsActive", System.Data.SqlDbType.Bit).Value = user.IsActive; // <-- add this
                            cmd.Parameters.AddWithValue("@id", user.UserID);
                            cmd.Parameters.AddWithValue("@profileImagePath", (object?)user.ProfileImagePath ?? DBNull.Value);
                            rows = cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        updateQuery = @"
                    UPDATE Users
                    SET FirstName = @firstName,
                        MiddleName = @middleName,
                        LastName = @lastName,
                        Username = @username,
                        Role = @role,
                        IsActive = @IsActive,
                        ProfileImagePath = @profileImagePath,
                        UpdatedAt = GETDATE()
                    WHERE UserID = @id";

                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                            cmd.Parameters.AddWithValue("@middleName", (object)user.MiddleName ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@lastName", user.LastName);
                            cmd.Parameters.AddWithValue("@username", user.Username);
                            cmd.Parameters.AddWithValue("@role", user.Role);
                            cmd.Parameters.Add("@IsActive", System.Data.SqlDbType.Bit).Value = user.IsActive; // <-- add this
                            cmd.Parameters.AddWithValue("@id", user.UserID);
                            cmd.Parameters.AddWithValue("@profileImagePath", (object?)user.ProfileImagePath ?? DBNull.Value);

                            rows = cmd.ExecuteNonQuery();
                        }
                    }

                    if (rows > 0)
                    {
                        DialogHelper.ShowCustomDialog("Success", $"User '{user.Username}' updated successfully!", "success");
                        return true;
                    }
                    else
                    {
                        DialogHelper.ShowCustomDialog("Failed", "No record was updated.", "error");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Occurred", ex.Message, "error");
                return false;
            }
        }



        public bool DeleteUser(int userId, string username)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    string updateQuery = @"
                UPDATE Users
                SET IsActive = 0
                WHERE UserID = @id";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", userId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            DialogHelper.ShowCustomDialog(
                                "User Deactivated",
                                $"User '{username}' has been archived successfully.",
                                "success"
                            );
                            return true;
                        }
                        else
                        {
                            DialogHelper.ShowCustomDialog(
                                "Not Found",
                                "No user was deactivated — the user might not exist.",
                                "warning"
                            );
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Occurred", ex.Message, "error");
                return false;
            }
        }



    }
}
