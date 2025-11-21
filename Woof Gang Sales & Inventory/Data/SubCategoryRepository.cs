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
    public class SubCategoryRepository
    {

        // ✅ Get all active subcategories (joined with CategoryName)
        public List<SubCategory> GetSubCategories(string search = "")
        {
            var subCategories = new List<SubCategory>();

            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    string query = @"
                SELECT sc.SubCategoryID, sc.SubCategoryName, sc.CategoryID,
                       c.CategoryName, sc.IsActive, sc.CreatedAt, sc.UpdatedAt
                FROM SubCategories sc
                INNER JOIN Categories c ON sc.CategoryID = c.CategoryID
                WHERE sc.IsActive = 1";

                    // ✅ Add search filter only if search has a value
                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        query += " AND (sc.SubCategoryName LIKE @search OR c.CategoryName LIKE @search)";
                    }

                    query += " ORDER BY c.CategoryName, sc.SubCategoryName ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(search))
                        {
                            cmd.Parameters.AddWithValue("@search", "%" + search.Trim() + "%");
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                subCategories.Add(MapToSubCategory(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Occurred", ex.Message, "error");
            }

            return subCategories;
        }

        public SubCategory? GetSubCategoryById(int subCategoryId)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"
                SELECT sc.SubCategoryID, sc.SubCategoryName, sc.CategoryID,
                       c.CategoryName, sc.IsActive, sc.CreatedAt, sc.UpdatedAt
                FROM SubCategories sc
                INNER JOIN Categories c ON sc.CategoryID = c.CategoryID
                WHERE sc.SubCategoryID = @SubCategoryID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SubCategoryID", subCategoryId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                                return MapToSubCategory(reader);
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



        // ✅ Get all inactive (archived) subcategories
        public List<SubCategory> GetAllInactiveSubCategories()
        {
            var subCategories = new List<SubCategory>();

            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT sc.SubCategoryID, sc.SubCategoryName, sc.CategoryID,
                               c.CategoryName, sc.IsActive, sc.CreatedAt, sc.UpdatedAt
                        FROM SubCategories sc
                        INNER JOIN Categories c ON sc.CategoryID = c.CategoryID
                        WHERE sc.IsActive = 0
                        ORDER BY c.CategoryName, sc.SubCategoryName ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            subCategories.Add(MapToSubCategory(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Occurred", ex.Message, "error");
            }

            return subCategories;
        }


        public void StatsCard(Label lblTotal, Label lblEmpty, Label lblTop)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    // 1. TOTAL SUB-CATEGORIES
                    // Simple count of all active categories
                    string queryTotal = "SELECT COUNT(*) FROM SubCategories WHERE IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(queryTotal, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        lblTotal.Text = count.ToString("N0");
                    }

                    // 2. EMPTY SUB-CATEGORIES
                    // Categories that have 0 products linked to them
                    string queryEmpty = @"
                        SELECT COUNT(*) 
                        FROM SubCategories s
                        WHERE s.IsActive = 1
                        AND NOT EXISTS (
                            SELECT 1 FROM Products p 
                            WHERE p.SubCategoryID = s.SubCategoryID 
                            AND p.IsActive = 1
                        )";
                    using (SqlCommand cmd = new SqlCommand(queryEmpty, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        lblEmpty.Text = count.ToString("N0");

                        // Logic: Turn RED if there are empty categories (Action needed)
                        if (count > 0)
                        {
                            lblEmpty.ForeColor = Color.Red;
                        }
                        else
                        {
                            lblEmpty.ForeColor = Color.Black;
                        }
                    }

                    // 3. TOP SUB-CATEGORY
                    // The category that contains the most products
                    string queryTop = @"
                        SELECT TOP 1 s.SubCategoryName 
                        FROM SubCategories s
                        JOIN Products p ON s.SubCategoryID = p.SubCategoryID
                        WHERE s.IsActive = 1 AND p.IsActive = 1
                        GROUP BY s.SubCategoryName
                        ORDER BY COUNT(p.ProductID) DESC";

                    using (SqlCommand cmd = new SqlCommand(queryTop, conn))
                    {
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            lblTop.Text = result.ToString();
                            lblTop.ForeColor = Color.Green;
                        }
                        else
                        {
                            lblTop.Text = "None";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Stats Error", ex.Message, "error");
            }
        }


        // ✅ Create new subcategory (duplicate check per Category)
        public bool CreateSubCategory(SubCategory subCategory)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    string checkQuery = @"
                        SELECT COUNT(*) 
                        FROM SubCategories 
                        WHERE CategoryID = @CategoryID 
                        AND LOWER(LTRIM(RTRIM(SubCategoryName))) = LOWER(LTRIM(RTRIM(@SubCategoryName)))";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@CategoryID", subCategory.CategoryID);
                        checkCmd.Parameters.AddWithValue("@SubCategoryName", subCategory.SubCategoryName.Trim());
                        int exists = (int)checkCmd.ExecuteScalar();

                        if (exists > 0)
                        {
                            DialogHelper.ShowCustomDialog("Duplicate",
                                $"Subcategory '{subCategory.SubCategoryName}' already exists in this category.", "warning");
                            return false;
                        }
                    }

                    string insertQuery = @"
                        INSERT INTO SubCategories (CategoryID, SubCategoryName, IsActive, CreatedAt)
                        VALUES (@CategoryID, @SubCategoryName, 1, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", subCategory.CategoryID);
                        cmd.Parameters.AddWithValue("@SubCategoryName", subCategory.SubCategoryName.Trim());
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            DialogHelper.ShowCustomDialog("Success",
                                $"Subcategory '{subCategory.SubCategoryName}' created successfully!", "success");
                            return true;
                        }
                        else
                        {
                            DialogHelper.ShowCustomDialog("Failed", "Subcategory could not be added.", "error");
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

        // ✅ Update existing subcategory (check for duplicates)
        public bool UpdateSubCategory(SubCategory subCategory)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    string checkQuery = @"
                    SELECT COUNT(*) 
                    FROM SubCategories 
                    WHERE CategoryID = @CategoryID 
                    AND LOWER(LTRIM(RTRIM(SubCategoryName))) = LOWER(LTRIM(RTRIM(@SubCategoryName)))
                    AND SubCategoryID <> @SubCategoryID";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@CategoryID", subCategory.CategoryID);
                        checkCmd.Parameters.AddWithValue("@SubCategoryName", subCategory.SubCategoryName.Trim());
                        checkCmd.Parameters.AddWithValue("@SubCategoryID", subCategory.SubCategoryID);
                        int exists = (int)checkCmd.ExecuteScalar();

                        if (exists > 0)
                        {
                            DialogHelper.ShowCustomDialog("Duplicate",
                            $"Subcategory '{subCategory.SubCategoryName}' already exists under '{subCategory.CategoryName}'.", "warning");
                            return false;
                        }
                    }

                    string updateQuery = @"
                        UPDATE SubCategories
                        SET CategoryID = @CategoryID,
                            SubCategoryName = @SubCategoryName,
                            IsActive = @IsActive,
                            UpdatedAt = GETDATE()
                        WHERE SubCategoryID = @SubCategoryID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", subCategory.CategoryID);
                        cmd.Parameters.AddWithValue("@SubCategoryName", subCategory.SubCategoryName.Trim());
                        cmd.Parameters.Add("@IsActive", System.Data.SqlDbType.Bit).Value = subCategory.IsActive;
                        cmd.Parameters.AddWithValue("@SubCategoryID", subCategory.SubCategoryID);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            DialogHelper.ShowCustomDialog("Success",
                                $"Subcategory '{subCategory.SubCategoryName}' updated successfully!", "success");
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

        // ✅ Soft delete / restore
        public bool DeleteSubCategory(int subCategoryId) =>
            SetActiveStatus(subCategoryId, false, "Subcategory archived successfully!");

        public bool RestoreSubCategory(int subCategoryId) =>
            SetActiveStatus(subCategoryId, true, "Subcategory restored successfully!");

        private bool SetActiveStatus(int subCategoryId, bool isActive, string message)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE SubCategories SET IsActive = @IsActive, UpdatedAt = GETDATE() WHERE SubCategoryID = @SubCategoryID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SubCategoryID", subCategoryId);
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

        // ⚠️ Permanent delete
        public bool DeleteSubCategoryPermanent(int subCategoryId)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM SubCategories WHERE SubCategoryID = @SubCategoryID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SubCategoryID", subCategoryId);
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            DialogHelper.ShowCustomDialog("Deleted", "Subcategory permanently removed.", "success");
                            return true;
                        }
                        else
                        {
                            DialogHelper.ShowCustomDialog("Failed", "Subcategory could not be deleted.", "error");
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

        // ✅ Map SQL row to SubCategory model
        private SubCategory MapToSubCategory(SqlDataReader reader)
        {
            return new SubCategory
            {
                SubCategoryID = Convert.ToInt32(reader["SubCategoryID"]),
                SubCategoryName = reader["SubCategoryName"].ToString() ?? "",
                CategoryID = Convert.ToInt32(reader["CategoryID"]),
                CategoryName = reader["CategoryName"].ToString() ?? "",
                IsActive = reader["IsActive"] != DBNull.Value && Convert.ToBoolean(reader["IsActive"]),
                CreatedAt = reader["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedAt"]) : DateTime.MinValue,
                UpdatedAt = reader["UpdatedAt"] as DateTime?
            };
        }
    }
}
