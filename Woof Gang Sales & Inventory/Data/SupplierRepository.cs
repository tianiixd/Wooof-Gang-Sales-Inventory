#nullable enable
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Database;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Data
{
    public class SupplierRepository
    {
        
        public List<Supplier> GetSuppliers(string search = "")
        {
            var suppliers = new List<Supplier>();
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT * FROM Suppliers
                        WHERE IsActive = 1";

                    // Add search filter if provided
                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        query += " AND (SupplierName LIKE @search OR ContactPerson LIKE @search OR Email LIKE @search)";
                    }

                    query += " ORDER BY SupplierName ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(search))
                        {
                            cmd.Parameters.AddWithValue("@search", "%" + search.Trim() + "%");
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                suppliers.Add(MapToSupplier(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Occurred", ex.Message, "error");
            }
            return suppliers;
        }

        
        public Supplier? GetSupplierById(int supplierId)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Suppliers WHERE SupplierID = @SupplierID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SupplierID", supplierId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                                return MapToSupplier(reader);
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

        public List<Supplier> GetAllInactiveSuppliers()
        {
            var suppliers = new List<Supplier>();
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT * FROM Suppliers
                        WHERE IsActive = 0
                        ORDER BY SupplierName ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            suppliers.Add(MapToSupplier(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Occurred", ex.Message, "error");
            }
            return suppliers;
        }


        public void StatsCard(Label lblTotalSuppliers, Label lblUnusedSuppliers, Label lblTopSupplier)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    
                    string queryTotal = "SELECT COUNT(*) FROM Suppliers WHERE IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(queryTotal, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        lblTotalSuppliers.Text = count.ToString("N0");
                    }

                   
                    string queryUnused = @"
                SELECT COUNT(*) 
                FROM Suppliers s
                WHERE s.IsActive = 1
                AND NOT EXISTS (
                    SELECT 1 FROM Products p 
                    WHERE p.SupplierID = s.SupplierID 
                    AND p.IsActive = 1
                )";
                    using (SqlCommand cmd = new SqlCommand(queryUnused, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        lblUnusedSuppliers.Text = count.ToString("N0");

                        
                        if (count > 0)
                        {
                            lblUnusedSuppliers.ForeColor = Color.Red;
                        }
                        else
                        {
                            lblUnusedSuppliers.ForeColor = Color.Black;
                        }
                    }

                   
                    string queryTop = @"
                SELECT TOP 1 s.SupplierName 
                FROM Suppliers s
                JOIN Products p ON s.SupplierID = p.SupplierID
                WHERE s.IsActive = 1 AND p.IsActive = 1
                GROUP BY s.SupplierName
                ORDER BY COUNT(p.ProductID) DESC";

                    using (SqlCommand cmd = new SqlCommand(queryTop, conn))
                    {
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            lblTopSupplier.Text = result.ToString();
                            lblTopSupplier.ForeColor = Color.Green; // Highlight the winner
                        }
                        else
                        {
                            lblTopSupplier.Text = "None";
                            lblTopSupplier.ForeColor = Color.FromArgb(19, 19, 26);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Stats Error", ex.Message, "error");
            }
        }


        public bool CreateSupplier(Supplier supplier)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    // 1. Check for duplicate supplier name
                    string checkQuery = @"
                        SELECT COUNT(*) 
                        FROM Suppliers 
                        WHERE LOWER(LTRIM(RTRIM(SupplierName))) = LOWER(LTRIM(RTRIM(@SupplierName)))";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@SupplierName", supplier.SupplierName.Trim());
                        int exists = (int)checkCmd.ExecuteScalar();

                        if (exists > 0)
                        {
                            DialogHelper.ShowCustomDialog("Duplicate", $"Supplier '{supplier.SupplierName}' already exists.", "warning");
                            return false;
                        }
                    }

                    // 2. Insert new supplier
                    string insertQuery = @"
                        INSERT INTO Suppliers (SupplierName, ContactPerson, PhoneNumber, Email, Address, IsActive, CreatedAt)
                        VALUES (@SupplierName, @ContactPerson, @PhoneNumber, @Email, @Address, 1, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        // Add parameters manually to match your pattern
                        cmd.Parameters.AddWithValue("@SupplierName", supplier.SupplierName.Trim());
                        cmd.Parameters.AddWithValue("@ContactPerson", (object?)supplier.ContactPerson ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PhoneNumber", (object?)supplier.PhoneNumber ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", (object?)supplier.Email ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Address", (object?)supplier.Address ?? DBNull.Value);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            DialogHelper.ShowCustomDialog("Success", $"Supplier '{supplier.SupplierName}' created successfully!", "success");
                            return true;
                        }
                        else
                        {
                            DialogHelper.ShowCustomDialog("Failed", "Supplier could not be added. Please try again.", "error");
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

        public bool UpdateSupplier(Supplier supplier)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    // 1. Check for duplicate name (excluding itself)
                    string checkQuery = @"
                        SELECT COUNT(*) 
                        FROM Suppliers 
                        WHERE LOWER(LTRIM(RTRIM(SupplierName))) = LOWER(LTRIM(RTRIM(@SupplierName))) 
                        AND SupplierID <> @SupplierID";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@SupplierName", supplier.SupplierName.Trim());
                        checkCmd.Parameters.AddWithValue("@SupplierID", supplier.SupplierID);
                        int exists = (int)checkCmd.ExecuteScalar();

                        if (exists > 0)
                        {
                            DialogHelper.ShowCustomDialog("Duplicate", $"Supplier name '{supplier.SupplierName}' already exists.", "warning");
                            return false;
                        }
                    }

                    // 2. Update the supplier
                    string updateQuery = @"
                        UPDATE Suppliers
                        SET SupplierName = @SupplierName,
                            ContactPerson = @ContactPerson,
                            PhoneNumber = @PhoneNumber,
                            Email = @Email,
                            Address = @Address,
                            IsActive = @IsActive,
                            UpdatedAt = GETDATE()
                        WHERE SupplierID = @SupplierID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        // Add parameters manually
                        cmd.Parameters.AddWithValue("@SupplierName", supplier.SupplierName.Trim());
                        cmd.Parameters.AddWithValue("@ContactPerson", (object?)supplier.ContactPerson ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PhoneNumber", (object?)supplier.PhoneNumber ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", (object?)supplier.Email ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Address", (object?)supplier.Address ?? DBNull.Value);
                        cmd.Parameters.Add("@IsActive", System.Data.SqlDbType.Bit).Value = supplier.IsActive;
                        cmd.Parameters.AddWithValue("@SupplierID", supplier.SupplierID);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            DialogHelper.ShowCustomDialog("Success", $"Supplier '{supplier.SupplierName}' updated successfully!", "success");
                            return true;
                        }
                        else
                        {
                            DialogHelper.ShowCustomDialog("Failed", "No changes were made.", "warning");
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

        public bool DeleteSupplier(int supplierId) =>
            SetSupplierActiveStatus(supplierId, false, "Supplier archived successfully!");

        public bool RestoreSupplier(int supplierId) =>
            SetSupplierActiveStatus(supplierId, true, "Supplier restored successfully!");

      
        private bool SetSupplierActiveStatus(int supplierId, bool isActive, string message)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Suppliers SET IsActive = @IsActive, UpdatedAt = GETDATE() WHERE SupplierID = @SupplierID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SupplierID", supplierId);
                        cmd.Parameters.AddWithValue("@IsActive", isActive);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            DialogHelper.ShowCustomDialog("Success", message, "success");
                            return true;
                        }
                        else
                        {
                            DialogHelper.ShowCustomDialog("Failed", "Operation could not be completed.", "error");
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

        
        private Supplier MapToSupplier(SqlDataReader reader)
        {
            return new Supplier
            {
                SupplierID = Convert.ToInt32(reader["SupplierID"]),
                SupplierName = reader["SupplierName"].ToString() ?? "",
                ContactPerson = reader["ContactPerson"].ToString() ?? "",
                PhoneNumber = reader["PhoneNumber"].ToString() ?? "",
                Email = reader["Email"].ToString() ?? "",
                Address = reader["Address"].ToString() ?? "",
                IsActive = reader["IsActive"] != DBNull.Value && Convert.ToBoolean(reader["IsActive"]),
                CreatedAt = reader["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedAt"]) : DateTime.MinValue,
                UpdatedAt = reader["UpdatedAt"] as DateTime?
            };
        }
    }
}

