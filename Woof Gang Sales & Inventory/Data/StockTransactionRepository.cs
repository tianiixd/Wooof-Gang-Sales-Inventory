using Microsoft.Data.SqlClient;
using System;
using Woof_Gang_Sales___Inventory.Database;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Data
{
    public class StockTransactionRepository
    {
        /// <summary>
        /// Logs a stock transaction.
        /// ✅ --- FIX: This method now requires the 'transaction' object ---
        /// </summary>
        public bool LogTransaction(SqlConnection conn, SqlTransaction transaction, int productID, int userID, int changeQty, string transType, int? refID, string remarks)
        {
            // Note: This method does NOT open or close its own connection.
            // It uses the one passed from SalesRepository.
            try
            {
                // 1. Get the stock before the change
                int stockBefore = 0;
                string getStockQuery = "SELECT Quantity FROM Products WHERE ProductID = @ProductID";
                using (SqlCommand getStockCmd = new SqlCommand(getStockQuery, conn, transaction))
                {
                    getStockCmd.Parameters.AddWithValue("@ProductID", productID);
                    object result = getStockCmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        stockBefore = Convert.ToInt32(result);
                    }
                }

                // 2. Calculate stock after
                int stockAfter = stockBefore + changeQty;

                // 3. Log the transaction
                string logQuery = @"
                    INSERT INTO StockTransactions (ProductID, UserID, StockBefore, StockAfter, ChangeQty, TransactionType, ReferenceID, Remarks, TransDate)
                    VALUES (@ProductID, @UserID, @StockBefore, @StockAfter, @ChangeQty, @TransactionType, @ReferenceID, @Remarks, GETDATE())";

                using (SqlCommand logCmd = new SqlCommand(logQuery, conn, transaction))
                {
                    logCmd.Parameters.AddWithValue("@ProductID", productID);
                    logCmd.Parameters.AddWithValue("@UserID", userID);
                    logCmd.Parameters.AddWithValue("@StockBefore", stockBefore);
                    logCmd.Parameters.AddWithValue("@StockAfter", stockAfter);
                    logCmd.Parameters.AddWithValue("@ChangeQty", changeQty);
                    logCmd.Parameters.AddWithValue("@TransactionType", transType);
                    logCmd.Parameters.AddWithValue("@ReferenceID", (object?)refID ?? DBNull.Value);
                    logCmd.Parameters.AddWithValue("@Remarks", (object?)remarks ?? DBNull.Value);

                    int rows = logCmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                // Don't show a dialog here, just re-throw the exception
                // This will cause the main SalesRepository transaction to roll back
                throw new Exception($"StockTransaction log failed: {ex.Message}");
            }
        }
    }
}

