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

        /// <summary>
        /// This is the main checkout method. It does all "Four Things"
        /// inside a single database transaction.
        /// </summary>
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
                            saleCmd.Parameters.AddWithValue("@CustomerName", (object?)sale.CustomerName ?? DBNull.Value);
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

