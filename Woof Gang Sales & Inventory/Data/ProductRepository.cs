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
    public class ProductRepository
    {
        // Base query with all JOINs needed for display and searching
        private const string BASE_SELECT_QUERY = @"
            SELECT 
                p.ProductID, p.SKU, p.Brand, p.ProductName, p.Weight, p.Unit,
                p.SupplierID, p.SubCategoryID, p.SellingPrice, p.Quantity,
                p.ReorderLevel, p.LastSoldDate, p.TotalSold, p.ImagePath,
                p.IsActive, p.CreatedAt, p.UpdatedAt,
                ISNULL(s.SupplierName, 'N/A') AS SupplierName,
                ISNULL(sc.SubCategoryName, 'N/A') AS SubCategoryName,
                ISNULL(c.CategoryName, 'N/A') AS CategoryName,
                ISNULL(c.CategoryID, 0) AS CategoryID
            FROM Products p
            LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID
            LEFT JOIN SubCategories sc ON p.SubCategoryID = sc.SubCategoryID
            LEFT JOIN Categories c ON sc.CategoryID = c.CategoryID";

        /// <summary>
        /// Gets all ACTIVE products, with searching across multiple fields.
        /// </summary>
        public List<Product> GetProducts(string search = "")
        {
            var products = new List<Product>();
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = BASE_SELECT_QUERY + " WHERE p.IsActive = 1";

                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        query += @" AND (p.ProductName LIKE @search OR p.Brand LIKE @search 
                                        OR p.SKU LIKE @search OR s.SupplierName LIKE @search 
                                        OR sc.SubCategoryName LIKE @search OR c.CategoryName LIKE @search)";
                    }
                    query += " ORDER BY p.ProductName ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(search))
                        {
                            cmd.Parameters.AddWithValue("@search", "%" + search.Trim() + "%");
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(MapToProduct(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Occurred", ex.Message, "error");
            }
            return products;
        }

        /// <summary>
        /// Gets all INACTIVE (archived) products.
        /// (Search will be handled in the form using LINQ, as per your pattern)
        /// </summary>
        public List<Product> GetAllInactiveProducts()
        {
            var products = new List<Product>();
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = BASE_SELECT_QUERY + " WHERE p.IsActive = 0 ORDER BY p.ProductName ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(MapToProduct(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Occurred", ex.Message, "error");
            }
            return products;
        }

        /// <summary>
        /// Gets a single product by its ID, with all JOINed display names.
        /// </summary>
        public Product? GetProductById(int productId)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = BASE_SELECT_QUERY + " WHERE p.ProductID = @ProductID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", productId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapToProduct(reader);
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

        /// <summary>
        /// Creates a new product after checking for duplicates.
        /// </summary>
        public bool CreateProduct(Product product)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    // ✅ --- GUARANTEED UNIQUE SKU LOGIC ---
                    if (string.IsNullOrWhiteSpace(product.SKU))
                    {
                        product.SKU = GenerateUniqueSKU(conn);
                    }
                    // --- END OF SKU LOGIC ---

                    // Duplicate check (ProductName and Brand are a good unique combo)
                    string checkQuery = @"SELECT COUNT(*) FROM Products 
                                          WHERE LOWER(LTRIM(RTRIM(ProductName))) = LOWER(LTRIM(RTRIM(@ProductName)))
                                          AND LOWER(LTRIM(RTRIM(Brand))) = LOWER(LTRIM(RTRIM(@Brand)))";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@ProductName", product.ProductName.Trim());
                        checkCmd.Parameters.AddWithValue("@Brand", (object?)product.Brand ?? DBNull.Value);

                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            DialogHelper.ShowCustomDialog("Duplicate", $"A product named '{product.ProductName}' from brand '{product.Brand}' already exists.", "warning");
                            return false;
                        }
                    }

                    string insertQuery = @"
                        INSERT INTO Products (
                            SKU, Brand, ProductName, Weight, Unit, SupplierID, SubCategoryID,
                            SellingPrice, Quantity, ReorderLevel, ImagePath, IsActive, CreatedAt
                        ) VALUES (
                            @SKU, @Brand, @ProductName, @Weight, @Unit, @SupplierID, @SubCategoryID,
                            @SellingPrice, @Quantity, @ReorderLevel, @ImagePath, @IsActive, GETDATE()
                        )";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        AddParameters(cmd, product);
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            DialogHelper.ShowCustomDialog("Success", $"Product '{product.ProductName}' created successfully!", "success");
                            return true;
                        }
                        else
                        {
                            DialogHelper.ShowCustomDialog("Failed", "Product could not be added. Please try again.", "error");
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

        /// <summary>
        /// Updates an existing product after checking for duplicates.
        /// </summary>
        public bool UpdateProduct(Product product)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    // ✅ --- FIX: ADDED SKU GENERATION LOGIC TO UPDATE METHOD ---
                    // This will fix any old products that have a NULL SKU
                    if (string.IsNullOrWhiteSpace(product.SKU))
                    {
                        product.SKU = GenerateUniqueSKU(conn);
                    }
                    // --- END OF FIX ---

                    // Duplicate check (same as create, but ignoring its own ID)
                    string checkQuery = @"SELECT COUNT(*) FROM Products 
                                          WHERE LOWER(LTRIM(RTRIM(ProductName))) = LOWER(LTRIM(RTRIM(@ProductName)))
                                          AND LOWER(LTRIM(RTRIM(Brand))) = LOWER(LTRIM(RTRIM(@Brand)))
                                          AND ProductID <> @ProductID";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@ProductName", product.ProductName.Trim());
                        checkCmd.Parameters.AddWithValue("@Brand", (object?)product.Brand ?? DBNull.Value);
                        checkCmd.Parameters.AddWithValue("@ProductID", product.ProductID);

                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            DialogHelper.ShowCustomDialog("Duplicate", $"Another product named '{product.ProductName}' from brand '{product.Brand}' already exists.", "warning");
                            return false;
                        }
                    }

                    string updateQuery = @"
                        UPDATE Products SET
                            SKU = @SKU,
                            Brand = @Brand,
                            ProductName = @ProductName,
                            Weight = @Weight,
                            Unit = @Unit,
                            SupplierID = @SupplierID,
                            SubCategoryID = @SubCategoryID,
                            SellingPrice = @SellingPrice,
                            Quantity = @Quantity,
                            ReorderLevel = @ReorderLevel,
                            ImagePath = @ImagePath,
                            IsActive = @IsActive,
                            UpdatedAt = GETDATE()
                        WHERE ProductID = @ProductID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        AddParameters(cmd, product);
                        cmd.Parameters.AddWithValue("@ProductID", product.ProductID);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            DialogHelper.ShowCustomDialog("Success", $"Product '{product.ProductName}' updated successfully!", "success");
                            return true;
                        }
                        else
                        {
                            DialogHelper.ShowCustomDialog("Failed", "No changes were made to the product.", "warning");
                            return false; // No rows affected, but not a system error
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

        // --- Soft Delete / Restore ---

        public bool DeleteProduct(int productId) =>
            SetProductActiveStatus(productId, false, "Product archived successfully!");

        public bool RestoreProduct(int productId) =>
            SetProductActiveStatus(productId, true, "Product restored successfully!");

        private bool SetProductActiveStatus(int productId, bool isActive, string message)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Products SET IsActive = @IsActive, UpdatedAt = GETDATE() WHERE ProductID = @ProductID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", productId);
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

        // --- Helper Methods ---

        /// <summary>
        /// Helper method to generate a guaranteed unique SKU.
        /// </summary>
        private string GenerateUniqueSKU(SqlConnection conn)
        {
            string newSku;
            int attempts = 0;
            const int maxAttempts = 10; // Safety break to prevent infinite loop
            var rand = new Random(); // Use one Random instance

            do
            {
                if (attempts >= maxAttempts)
                {
                    // This is extremely unlikely (1 in 90 million * 10)
                    throw new Exception("Failed to generate a unique SKU after 10 attempts. Please try again.");
                }

                // 1. Generate a new random SKU
                newSku = $"WG-{rand.Next(10000000, 99999999)}";

                // 2. Check if this SKU already exists
                string checkQuery = "SELECT COUNT(*) FROM Products WHERE SKU = @SKU";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@SKU", newSku);
                    int exists = (int)checkCmd.ExecuteScalar();
                    if (exists == 0)
                    {
                        // 3. It's unique. Return it.
                        return newSku;
                    }
                }

                // 4. It's not unique. Loop and try again.
                attempts++;

            } while (true);
        }

        private void AddParameters(SqlCommand cmd, Product product)
        {
            cmd.Parameters.AddWithValue("@SKU", (object?)product.SKU ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Brand", (object?)product.Brand ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ProductName", product.ProductName.Trim());
            cmd.Parameters.AddWithValue("@Weight", (object?)product.Weight ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Unit", (object?)product.Unit ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SupplierID", (object?)product.SupplierID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SubCategoryID", (object?)product.SubCategoryID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SellingPrice", product.SellingPrice);
            cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
            cmd.Parameters.AddWithValue("@ReorderLevel", (object?)product.ReorderLevel ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ImagePath", (object?)product.ImagePath ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IsActive", product.IsActive);
        }

        private Product MapToProduct(SqlDataReader reader)
        {
            return new Product
            {
                ProductID = Convert.ToInt32(reader["ProductID"]),
                SKU = reader["SKU"] != DBNull.Value ? reader["SKU"].ToString() : null,
                Brand = reader["Brand"] != DBNull.Value ? reader["Brand"].ToString() : null,
                ProductName = reader["ProductName"].ToString() ?? "",
                Weight = reader["Weight"] != DBNull.Value ? Convert.ToDecimal(reader["Weight"]) : (decimal?)null,
                Unit = reader["Unit"] != DBNull.Value ? reader["Unit"].ToString() : null,
                SupplierID = reader["SupplierID"] != DBNull.Value ? Convert.ToInt32(reader["SupplierID"]) : (int?)null,
                SubCategoryID = reader["SubCategoryID"] != DBNull.Value ? Convert.ToInt32(reader["SubCategoryID"]) : (int?)null,
                SellingPrice = Convert.ToDecimal(reader["SellingPrice"]),
                Quantity = Convert.ToInt32(reader["Quantity"]),
                ReorderLevel = reader["ReorderLevel"] != DBNull.Value ? Convert.ToInt32(reader["ReorderLevel"]) : (int?)null,
                LastSoldDate = reader["LastSoldDate"] as DateTime?,
                TotalSold = Convert.ToInt32(reader["TotalSold"]),
                ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : null,
                IsActive = reader["IsActive"] != DBNull.Value && Convert.ToBoolean(reader["IsActive"]),
                CreatedAt = reader["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedAt"]) : DateTime.MinValue,
                UpdatedAt = reader["UpdatedAt"] as DateTime?,

                // From JOINs
                SupplierName = reader["SupplierName"].ToString() ?? "N/A",
                SubCategoryName = reader["SubCategoryName"].ToString() ?? "N/A",
                CategoryName = reader["CategoryName"].ToString() ?? "N/A",
                CategoryID = Convert.ToInt32(reader["CategoryID"])
            };
        }
    }
}

