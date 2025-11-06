using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using Woof_Gang_Sales___Inventory.Database;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Data
{
    /// <summary>
    /// A simple repository to READ discounts.
    /// Management is done directly in the database (SSMS) per instructor.
    /// </summary>
    public class DiscountRepository
    {
        public List<Discount> GetActiveDiscounts()
        {
            var discounts = new List<Discount>();
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Discounts WHERE IsActive = 1 ORDER BY DiscountName";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // ✅ --- FIX: Added DBNull.Value checks ---
                            // This makes the code safe, even if your INSERT
                            // query had a typo and left a column NULL.
                            discounts.Add(new Discount
                            {
                                DiscountID = Convert.ToInt32(reader["DiscountID"]),
                                DiscountName = reader["DiscountName"] != DBNull.Value ? reader["DiscountName"].ToString() : "N/A",
                                DiscountType = reader["DiscountType"] != DBNull.Value ? reader["DiscountType"].ToString() : "Fixed",
                                Value = reader["DiscountValue"] != DBNull.Value ? Convert.ToDecimal(reader["DiscountValue"]) : 0m,
                                IsActive = reader["IsActive"] != DBNull.Value && Convert.ToBoolean(reader["IsActive"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Loading Discounts", ex.Message, "error");
            }
            return discounts;
        }
    }
}

