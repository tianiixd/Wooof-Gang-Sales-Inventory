using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using Woof_Gang_Sales___Inventory.Database;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Data
{
    public class PurchaseOrderRepository
    {
        // We will need this to update product stock when an order is received
        private readonly ProductRepository _productRepo;

        public PurchaseOrderRepository()
        {
            _productRepo = new ProductRepository();
        }

        /// <summary>
        /// Gets all Purchase Orders for the main (master) grid.
        /// </summary>
        public List<PurchaseOrder> GetPurchaseOrders(string search, string status)
        {
            var poList = new List<PurchaseOrder>();

            string query = @"
                SELECT 
                    po.POID, po.SupplierID, po.OrderedBy, po.PODate, 
                    po.Status, po.Remarks, po.IsActive,
                    po.ReceivedDate, -- ✅ --- FIX: Added ReceivedDate ---
                    s.SupplierName,
                    ISNULL(SUM(pod.Subtotal), 0) AS CalculatedTotalCost
                FROM PurchaseOrders po
                JOIN Suppliers s ON po.SupplierID = s.SupplierID
                LEFT JOIN PurchaseOrderDetails pod ON po.POID = pod.POID
                WHERE 1=1";

            // --- Apply Filters ---
            if (!string.IsNullOrWhiteSpace(search))
            {
                if (int.TryParse(search, out int poID))
                {
                    query += " AND po.POID = @SearchID";
                }
                else
                {
                    query += " AND s.SupplierName LIKE @SearchText";
                }
            }

            // --- Apply Status Filter ---
            switch (status)
            {
      
                case "Pending":
                    query += " AND po.IsActive = 1 AND po.Status = 'Pending'";
                    break;
                case "Received":
                    query += " AND po.IsActive = 1 AND po.Status = 'Received'";
                    break;
                case "Cancelled":
                    query += " AND po.IsActive = 1 AND po.Status = 'Cancelled'";
                    break;
                case "All Active":
                    query += " AND po.IsActive = 1";
                    break;
                case "All Archived":
                    query += " AND po.IsActive = 0";
                    break;
                case "Show All":
                    // No extra filter needed
                    break;
            }

            // ✅ --- FIX: Added ReceivedDate to GROUP BY ---
            query += @"
                GROUP BY 
                    po.POID, po.SupplierID, po.OrderedBy, po.PODate, 
                    po.Status, po.Remarks, po.IsActive, s.SupplierName, po.ReceivedDate";

            query += " ORDER BY po.PODate DESC";

            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(search))
                        {
                            if (int.TryParse(search, out int poID))
                            {
                                cmd.Parameters.AddWithValue("@SearchID", poID);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@SearchText", $"%{search}%");
                            }
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                poList.Add(MapToPurchaseOrder(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Loading POs", $"An error occurred: {ex.Message}", "error");
            }
            return poList;
        }


        /// <summary>
        /// Gets the line items (products) for a single selected Purchase Order.
        /// </summary>
        public List<PurchaseOrderDetailView> GetPurchaseOrderDetails(int poID)
        {
            var detailList = new List<PurchaseOrderDetailView>();
            string query = @"
            SELECT 
            pod.PODetailID, pod.ProductID, pod.Quantity, 
            pod.UnitCost, pod.Subtotal,
            
            -- ✅ FIX: Concatenate Brand and Name here in SQL
            CASE 
                WHEN p.Brand IS NULL OR p.Brand = '' THEN p.ProductName 
                ELSE p.Brand + ' ' + p.ProductName 
            END AS ProductName

            FROM PurchaseOrderDetails pod
            JOIN Products p ON pod.ProductID = p.ProductID
            WHERE pod.POID = @POID
            ORDER BY p.ProductName";

            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@POID", poID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                detailList.Add(MapToPODetailView(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Loading PO Details", $"Failed to load order details: {ex.Message}", "error");
            }
            return detailList;
        }


        /// <summary>
        /// Creates a new Purchase Order and its detail lines in a single transaction.
        /// </summary>
        public int CreatePurchaseOrder(PurchaseOrder po, List<PurchaseOrderDetail> details)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Step 1: Insert the main PurchaseOrder record
                        string poQuery = @"
                            INSERT INTO PurchaseOrders (SupplierID, OrderedBy, PODate, Status, Remarks, IsActive)
                            OUTPUT INSERTED.POID
                            VALUES (@SupplierID, @OrderedBy, @PODate, @Status, @Remarks, 1)";

                        int newPOID;
                        using (SqlCommand poCmd = new SqlCommand(poQuery, conn, transaction))
                        {
                            poCmd.Parameters.AddWithValue("@SupplierID", po.SupplierID);
                            poCmd.Parameters.AddWithValue("@OrderedBy", (object?)po.OrderedBy ?? DBNull.Value);
                            poCmd.Parameters.AddWithValue("@PODate", po.PODate);
                            poCmd.Parameters.AddWithValue("@Status", "Pending"); // New orders are always Pending
                            poCmd.Parameters.AddWithValue("@Remarks", (object?)po.Remarks ?? DBNull.Value);

                            newPOID = (int)poCmd.ExecuteScalar();
                        }

                        // Step 2: Loop and insert all detail lines
                        foreach (var item in details)
                        {
                            string detailQuery = @"
                                INSERT INTO PurchaseOrderDetails (POID, ProductID, Quantity, UnitCost)
                                VALUES (@POID, @ProductID, @Quantity, @UnitCost)";

                            using (SqlCommand detailCmd = new SqlCommand(detailQuery, conn, transaction))
                            {
                                detailCmd.Parameters.AddWithValue("@POID", newPOID);
                                detailCmd.Parameters.AddWithValue("@ProductID", item.ProductID);
                                detailCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                                detailCmd.Parameters.AddWithValue("@UnitCost", item.UnitCost);
                                detailCmd.ExecuteNonQuery();
                            }
                        }

                        // Step 3: Update the TotalCost on the main PO
                        UpdateTotalCost(newPOID, conn, transaction);

                        // Step 4: Commit
                        transaction.Commit();
                        DialogHelper.ShowCustomDialog("Success", $"Purchase Order #{newPOID} created successfully.", "success");
                        return newPOID;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        DialogHelper.ShowCustomDialog("Error", $"Failed to create Purchase Order: {ex.Message}", "error");
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// Updates an existing Purchase Order and its detail lines.
        /// </summary>
        public bool UpdatePurchaseOrder(PurchaseOrder po, List<PurchaseOrderDetail> details)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Step 1: Update the main PO record
                        string poQuery = @"
                            UPDATE PurchaseOrders SET
                                SupplierID = @SupplierID,
                                OrderedBy = @OrderedBy,
                                PODate = @PODate,
                                Remarks = @Remarks,
                                UpdatedAt = GETDATE()
                            WHERE POID = @POID";

                        using (SqlCommand poCmd = new SqlCommand(poQuery, conn, transaction))
                        {
                            poCmd.Parameters.AddWithValue("@SupplierID", po.SupplierID);
                            poCmd.Parameters.AddWithValue("@OrderedBy", (object?)po.OrderedBy ?? DBNull.Value);
                            poCmd.Parameters.AddWithValue("@PODate", po.PODate);
                            poCmd.Parameters.AddWithValue("@Remarks", (object?)po.Remarks ?? DBNull.Value);
                            poCmd.Parameters.AddWithValue("@POID", po.POID);
                            poCmd.ExecuteNonQuery();
                        }

                        // Step 2: Delete all OLD detail lines
                        string deleteQuery = "DELETE FROM PurchaseOrderDetails WHERE POID = @POID";
                        using (SqlCommand delCmd = new SqlCommand(deleteQuery, conn, transaction))
                        {
                            delCmd.Parameters.AddWithValue("@POID", po.POID);
                            delCmd.ExecuteNonQuery();
                        }

                        // Step 3: Insert all NEW detail lines
                        foreach (var item in details)
                        {
                            string detailQuery = @"
                                INSERT INTO PurchaseOrderDetails (POID, ProductID, Quantity, UnitCost)
                                VALUES (@POID, @ProductID, @Quantity, @UnitCost)";

                            using (SqlCommand detailCmd = new SqlCommand(detailQuery, conn, transaction))
                            {
                                detailCmd.Parameters.AddWithValue("@POID", po.POID);
                                detailCmd.Parameters.AddWithValue("@ProductID", item.ProductID);
                                detailCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                                detailCmd.Parameters.AddWithValue("@UnitCost", item.UnitCost);
                                detailCmd.ExecuteNonQuery();
                            }
                        }

                        // Step 4: Update the TotalCost
                        UpdateTotalCost(po.POID, conn, transaction);

                        // Step 5: Commit
                        transaction.Commit();
                        DialogHelper.ShowCustomDialog("Success", $"Purchase Order #{po.POID} updated successfully.", "success");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        DialogHelper.ShowCustomDialog("Error", $"Failed to update Purchase Order: {ex.Message}", "error");
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// This method calculates the sum of details and updates the main PO's TotalCost.
        /// </summary>
        private void UpdateTotalCost(int poID, SqlConnection conn, SqlTransaction transaction)
        {
            string query = @"
                UPDATE PurchaseOrders
                SET TotalCost = (SELECT SUM(Subtotal) FROM PurchaseOrderDetails WHERE POID = @POID)
                WHERE POID = @POID";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@POID", poID);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Gets all data needed to populate the "Edit" form.
        /// </summary>
        public (PurchaseOrder, List<PurchaseOrderDetailView>) GetPurchaseOrderForEdit(int poID)
        {
            PurchaseOrder po = null;
            List<PurchaseOrderDetailView> details = new List<PurchaseOrderDetailView>();

            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();

                // Step 1: Get the main PurchaseOrder
                string poQuery = @"
                    SELECT 
                        po.POID, po.SupplierID, po.OrderedBy, po.PODate, 
                        po.Status, po.Remarks, po.IsActive,
                        po.ReceivedDate, -- ✅ --- FIX: Added ReceivedDate ---
                        s.SupplierName,
                        ISNULL(SUM(pod.Subtotal), 0) AS CalculatedTotalCost
                    FROM PurchaseOrders po
                    JOIN Suppliers s ON po.SupplierID = s.SupplierID
                    LEFT JOIN PurchaseOrderDetails pod ON po.POID = pod.POID
                    WHERE po.POID = @POID
                    GROUP BY 
                        po.POID, po.SupplierID, po.OrderedBy, po.PODate, 
                        po.Status, po.Remarks, po.IsActive, s.SupplierName, po.ReceivedDate"; // ✅ --- FIX: Added ReceivedDate ---

                using (SqlCommand poCmd = new SqlCommand(poQuery, conn))
                {
                    poCmd.Parameters.AddWithValue("@POID", poID);
                    using (SqlDataReader reader = poCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            po = MapToPurchaseOrder(reader);
                        }
                    }
                }

                if (po == null)
                {
                    return (null, null);
                }

                // Step 2: Get the details for the bottom grid
                details = GetPurchaseOrderDetails(poID);
            }

            return (po, details);
        }

        /// <summary>
        /// Updates only the Status of a PO. Used for "Cancelling".
        /// </summary>
        public bool UpdatePOStatus(int poID, string newStatus)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE PurchaseOrders SET Status = @Status, UpdatedAt = GETDATE() WHERE POID = @POID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", newStatus);
                        cmd.Parameters.AddWithValue("@POID", poID);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            DialogHelper.ShowCustomDialog("Success", $"Order #{poID} has been successfully {newStatus.ToLower()}.", "success");
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error", $"Could not update order status: {ex.Message}", "error");
            }
            return false;
        }

        /// <summary>
        /// Archives a PO by setting IsActive = 0.
        /// </summary>
        public bool ArchivePurchaseOrder(int poID)
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE PurchaseOrders SET IsActive = 0, UpdatedAt = GETDATE() WHERE POID = @POID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@POID", poID);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            DialogHelper.ShowCustomDialog("Success", $"Order #{poID} has been successfully archived.", "success");
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error", $"Could not archive order: {ex.Message}", "error");
            }
            return false;
        }


        public bool ReceivePurchaseOrder(int poID, List<PurchaseOrderDetail> receivedItems)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Update PO Status to 'Received' and set ReceivedDate
                        string updatePO = @"UPDATE PurchaseOrders 
                                    SET Status = 'Received', 
                                        ReceivedDate = GETDATE(),
                                        UpdatedAt = GETDATE() 
                                    WHERE POID = @POID";

                        using (SqlCommand cmd = new SqlCommand(updatePO, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@POID", poID);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Loop through items and update Product Stock
                        foreach (var item in receivedItems)
                        {
                            // Call the ProductRepository logic we made earlier
                            // We pass the transaction so it's all part of the same safety net
                            // Note: expirationDate is passed inside the item object in this logic
                            _productRepo.ReceiveStock(item.ProductID, item.Quantity, item.ExpirationDate, conn, transaction);
                        }

                        // 3. Commit Transaction
                        transaction.Commit();
                        DialogHelper.ShowCustomDialog("Success", "Stocks have been added to inventory successfully!", "success");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        DialogHelper.ShowCustomDialog("Transaction Error", $"Failed to receive order: {ex.Message}", "error");
                        return false;
                    }
                }
            }
        }


        // --- Mapping Helpers ---

        private PurchaseOrder MapToPurchaseOrder(SqlDataReader reader)
        {
            return new PurchaseOrder
            {
                POID = Convert.ToInt32(reader["POID"]),
                SupplierID = Convert.ToInt32(reader["SupplierID"]),
                OrderedBy = reader["OrderedBy"] as int?,
                PODate = Convert.ToDateTime(reader["PODate"]),

                // ✅ --- FIX: Read the new column ---
                ReceivedDate = reader["ReceivedDate"] as DateTime?,

                Status = reader["Status"].ToString() ?? "N/A",
                TotalCost = reader["CalculatedTotalCost"] as decimal?,
                Remarks = reader["Remarks"] != DBNull.Value ? reader["Remarks"].ToString() : null,
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                SupplierName = reader["SupplierName"].ToString() ?? "N/A"
            };
        }

        private PurchaseOrderDetailView MapToPODetailView(SqlDataReader reader)
        {
            return new PurchaseOrderDetailView
            {
                PODetailID = Convert.ToInt32(reader["PODetailID"]),
                ProductID = Convert.ToInt32(reader["ProductID"]),
                Quantity = Convert.ToInt32(reader["Quantity"]),
                UnitCost = Convert.ToDecimal(reader["UnitCost"]),
                Subtotal = Convert.ToDecimal(reader["Subtotal"]),
                ProductName = reader["ProductName"].ToString() ?? "N/A"
            };
        }
    }
}