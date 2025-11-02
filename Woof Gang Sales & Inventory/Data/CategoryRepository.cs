#nullable enable
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using Woof_Gang_Sales___Inventory.Database;
using Woof_Gang_Sales___Inventory.Helpers;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Data
{
    public class CategoryRepository
    {


        // ✅ Better: One method that handles both
        public List<Category> GetCategories(string search = "")
        {
            var categories = new List<Category>();

            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"
                SELECT * FROM Categories
                WHERE IsActive = 1";

                    // Add search filter if provided
                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        query += " AND CategoryName LIKE @search";
                    }

                    query += " ORDER BY CategoryName ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(search))
                        {
                            cmd.Parameters.AddWithValue("@search", "%" + search.Trim() + "%");
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                categories.Add(MapToCategory(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Occurred", ex.Message, "error");
            }

            return categories;
        }

        // ✅ Get single category by ID
        public Category? GetCategoryById(int categoryId)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Categories WHERE CategoryID = @CategoryID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                                return MapToCategory(reader);
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

        // ✅ Get all inactive categories
        public List<Category> GetAllInactiveCategories()
        {
            var categories = new List<Category>();

            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT * FROM Categories
                        WHERE IsActive = 0
                        ORDER BY CategoryName ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            categories.Add(MapToCategory(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Occurred", ex.Message, "error");
            }

            return categories;
        }

        

        // ✅ Create new category
        public bool CreateCategory(Category category)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    // 🔍 Check duplicate
                    string checkQuery = @"
                        SELECT COUNT(*) 
                        FROM Categories 
                        WHERE LOWER(LTRIM(RTRIM(CategoryName))) = LOWER(LTRIM(RTRIM(@CategoryName)))";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@CategoryName", category.CategoryName.Trim());
                        int exists = (int)checkCmd.ExecuteScalar();

                        if (exists > 0)
                        {
                            DialogHelper.ShowCustomDialog("Duplicate", $"Category '{category.CategoryName}' already exists.", "warning");
                            return false;
                        }
                    }

                    string insertQuery = @"
                        INSERT INTO Categories (CategoryName, IsActive, CreatedAt)
                        VALUES (@CategoryName, 1, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName.Trim());
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            DialogHelper.ShowCustomDialog("Success", $"Category '{category.CategoryName}' created successfully!", "success");
                            return true;
                        }
                        else
                        {
                            DialogHelper.ShowCustomDialog("Failed", "Category could not be added. Please try again.", "error");
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

        // ✅ Update category
        public bool UpdateCategory(Category category)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Categories WHERE LOWER(LTRIM(RTRIM(CategoryName))) = LOWER(LTRIM(RTRIM(@CategoryName))) AND CategoryID <> @CategoryID";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@CategoryName", category.CategoryName.Trim());
                        checkCmd.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                        int exists = (int)checkCmd.ExecuteScalar();

                        if (exists > 0)
                        {
                            DialogHelper.ShowCustomDialog("Duplicate", $"Category '{category.CategoryName}' already exists.", "warning");
                            return false;
                        }
                    }

                    string updateQuery = @"
                        UPDATE Categories
                        SET CategoryName = @CategoryName,
                            IsActive = @IsActive,
                            UpdatedAt = GETDATE()
                        WHERE CategoryID = @CategoryID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName.Trim());
                        cmd.Parameters.Add("@IsActive", System.Data.SqlDbType.Bit).Value = category.IsActive;
                        cmd.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                        

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            DialogHelper.ShowCustomDialog("Success", $"Category '{category.CategoryName}' updated successfully!", "success");
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

        // ✅ Soft delete & restore
        public bool DeleteCategory(int categoryId) =>
            SetActiveStatus(categoryId, false, "Category archived successfully!");

        public bool RestoreCategory(int categoryId) =>
            SetActiveStatus(categoryId, true, "Category restored successfully!");

        private bool SetActiveStatus(int categoryId, bool isActive, string message)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Categories SET IsActive = @IsActive, UpdatedAt = GETDATE() WHERE CategoryID = @CategoryID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);
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
        public bool DeleteCategoryPermanent(int categoryId)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM Categories WHERE CategoryID = @CategoryID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            DialogHelper.ShowCustomDialog("Deleted", "Category permanently removed from the database.", "success");
                            return true;
                        }
                        else
                        {
                            DialogHelper.ShowCustomDialog("Failed", "Category could not be deleted.", "error");
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

        // ✅ Map SQL row to Category object
        private Category MapToCategory(SqlDataReader reader)
        {
            return new Category
            {
                CategoryID = Convert.ToInt32(reader["CategoryID"]),
                CategoryName = reader["CategoryName"].ToString() ?? "",
                IsActive = reader["IsActive"] != DBNull.Value && Convert.ToBoolean(reader["IsActive"]),
                CreatedAt = reader["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedAt"]) : DateTime.MinValue,
                UpdatedAt = reader["UpdatedAt"] as DateTime?
            };
        }
    }
}
