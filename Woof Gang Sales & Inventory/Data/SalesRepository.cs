using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using Woof_Gang_Sales___Inventory.Database;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Data
{
    public class SalesRepository
    {
        // We need these repositories to perform the "Four Things"
        private readonly ProductRepository _productRepo;
        private readonly StockTransactionRepository _stockRepo;

        public SalesRepository()
        {
            _productRepo = new ProductRepository();
            _stockRepo = new StockTransactionRepository();
        }


        // Update the signature to accept 'int? cashierID = null'
        public List<SaleViewModel> GetSalesHistory(DateTime startDate, DateTime endDate, string search = "", int? cashierID = null)
        {
            var list = new List<SaleViewModel>();

            string query = @"
        SELECT 
            s.SaleID, s.SaleDate, s.SaleTime, 
            s.CustomerName, s.TotalAmount, s.PaymentMethod,
            u.Username, u.FirstName, u.LastName
        FROM Sales s
        LEFT JOIN Users u ON s.CashierID = u.UserID
        WHERE s.SaleDate BETWEEN @StartDate AND @EndDate 
        AND s.IsActive = 1";

            // ✅ NEW LOGIC: Filter by Cashier if provided
            if (cashierID.HasValue)
            {
                query += " AND s.CashierID = @CashierID";
            }

            // Search Logic
            if (!string.IsNullOrWhiteSpace(search))
            {
                query += " AND (CAST(s.SaleID AS VARCHAR) LIKE @Search OR s.CustomerName LIKE @Search)";
            }

            query += " ORDER BY s.SaleDate DESC, s.SaleTime DESC";

            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate.Date);
                        cmd.Parameters.AddWithValue("@EndDate", endDate.Date);

                        // ✅ Add Parameter
                        if (cashierID.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@CashierID", cashierID.Value);
                        }

                        if (!string.IsNullOrWhiteSpace(search))
                        {
                            cmd.Parameters.AddWithValue("@Search", $"%{search}%");
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // ... (Your existing mapping code remains the same) ...
                                string fname = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : "";
                                string lname = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : "";
                                string cashier = $"{fname} {lname}".Trim();
                                if (string.IsNullOrEmpty(cashier)) cashier = reader["Username"].ToString();

                                list.Add(new SaleViewModel
                                {
                                    SaleID = Convert.ToInt32(reader["SaleID"]),
                                    SaleDate = Convert.ToDateTime(reader["SaleDate"]),
                                    SaleTime = (TimeSpan)reader["SaleTime"],
                                    CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : "Walk-in",
                                    CashierName = cashier,
                                    PaymentMethod = reader["PaymentMethod"].ToString(),
                                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sales History Error: " + ex.Message);
            }

            return list;
        }

        /// <summary>
        /// Gets the specific items inside one Sale.
        /// </summary>
        public List<SalesDetailView> GetSaleDetails(int saleID)
        {
            var list = new List<SalesDetailView>();

            string query = @"
                SELECT 
                    sd.ProductID, sd.Quantity, sd.UnitPrice, sd.Subtotal,
                    p.ProductName, p.Brand
                FROM SalesDetails sd
                JOIN Products p ON sd.ProductID = p.ProductID
                WHERE sd.SaleID = @SaleID";

            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SaleID", saleID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new SalesDetailView
                                {
                                    ProductID = Convert.ToInt32(reader["ProductID"]),
                                    // Combine Brand + Name immediately for display
                                    Brand = reader["Brand"] != DBNull.Value ? reader["Brand"].ToString() : "",
                                    ProductName = reader["ProductName"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                    Subtotal = Convert.ToDecimal(reader["Subtotal"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sale Details Error: " + ex.Message);
            }

            return list;
        }

        public int CreateNewSale(Sale sale) // <-- 1. CHANGED from 'bool' to 'int'
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // --- THING 1: Save the main Sale record ---
                        string saleQuery = @"
                            INSERT INTO Sales (SaleDate, SaleTime, CashierID, DiscountID, CustomerName, TotalAmount, PaymentMethod, PaymentRef, IsActive)
                            OUTPUT INSERTED.SaleID
                            VALUES (@SaleDate, @SaleTime, @CashierID, @DiscountID, @CustomerName, @TotalAmount, @PaymentMethod, @PaymentRef, 1)";

                        int saleID;
                        using (SqlCommand saleCmd = new SqlCommand(saleQuery, conn, transaction))
                        {
                            saleCmd.Parameters.AddWithValue("@SaleDate", sale.SaleDate);
                            saleCmd.Parameters.AddWithValue("@SaleTime", sale.SaleTime);
                            saleCmd.Parameters.AddWithValue("@CashierID", sale.CashierID);
                            saleCmd.Parameters.AddWithValue("@DiscountID", (object?)sale.DiscountID ?? DBNull.Value);
                            string customerName = string.IsNullOrWhiteSpace(sale.CustomerName) ? "Walk-in Customer" : sale.CustomerName;
                            saleCmd.Parameters.AddWithValue("@CustomerName", customerName);
                            saleCmd.Parameters.AddWithValue("@TotalAmount", sale.TotalAmount);
                            saleCmd.Parameters.AddWithValue("@PaymentMethod", sale.PaymentMethod);
                            saleCmd.Parameters.AddWithValue("@PaymentRef", (object?)sale.PaymentRef ?? DBNull.Value);

                            saleID = (int)saleCmd.ExecuteScalar();
                        }

                        // --- THING 2: Loop and save all SaleDetails ---
                        foreach (var detail in sale.Details)
                        {
                            string detailQuery = @"
                                INSERT INTO SalesDetails (SaleID, ProductID, Quantity, Subtotal, IsActive)
                                VALUES (@SaleID, @ProductID, @Quantity, @Subtotal, 1)";

                            using (SqlCommand detailCmd = new SqlCommand(detailQuery, conn, transaction))
                            {
                                detailCmd.Parameters.AddWithValue("@SaleID", saleID); // <-- Uses new SaleID
                                // ... (rest of detail parameters)
                                detailCmd.Parameters.AddWithValue("@ProductID", detail.ProductID);
                                detailCmd.Parameters.AddWithValue("@Quantity", detail.Quantity);
                                detailCmd.Parameters.AddWithValue("@Subtotal", detail.Subtotal);
                                detailCmd.ExecuteNonQuery();
                            }

                            // --- THING 3: Log the Stock Transaction ---
                            _stockRepo.LogTransaction(conn, transaction,
                                detail.ProductID,
                                sale.CashierID,
                                -detail.Quantity,
                                "Sale",
                                saleID, // <-- Uses new SaleID
                                $"Sold {detail.Quantity} items");

                            // --- THING 4: Update the Product's Quantity ---
                            _productRepo.UpdateStock(conn, transaction,
                                detail.ProductID,
                                -detail.Quantity);
                        }

                        transaction.Commit();
                        DialogHelper.ShowCustomDialog("Success", "Sale completed successfully!", "success");

                        return saleID; // <-- 2. CHANGED from 'return true;'

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        DialogHelper.ShowCustomDialog("Transaction Failed", $"The sale could not be completed. Error: {ex.Message}", "error");

                        return 0; // <-- 3. CHANGED from 'return false;'
                    }
                }
            }
        }
    }
}

