using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woof_Gang_Sales___Inventory.Database;
using Woof_Gang_Sales___Inventory.Models;

namespace Woof_Gang_Sales___Inventory.Data.Database
{
    public class ReportsRepository
    {

        public List<SalesReportItem> GetSalesReport(DateTime startDate, DateTime endDate, string mode)
        {
            var list = new List<SalesReportItem>();

            string formatString;
            switch (mode)
            {
                case "Monthly": formatString = "yyyy-MM"; break;
                case "Yearly": formatString = "yyyy"; break;
                default: formatString = "yyyy-MM-dd"; break;
            }

            // ✅ NEW QUERY: Calculates Cash vs. Digital separately
            string query = $@"
        SELECT 
            FORMAT(s.SaleDate, '{formatString}') as PeriodLabel,
            COUNT(DISTINCT s.SaleID) as TransCount,
            
            -- 1. Cash Sales
            ISNULL(SUM(CASE WHEN s.PaymentMethod = 'Cash' THEN s.TotalAmount ELSE 0 END), 0) as CashSales,

            -- 2. Digital Sales (GCash + Maya)
            ISNULL(SUM(CASE WHEN s.PaymentMethod IN ('GCash', 'Maya') THEN s.TotalAmount ELSE 0 END), 0) as DigitalSales,

            -- 3. Total Revenue (Sum of everything)
            ISNULL(SUM(s.TotalAmount), 0) as TotalRevenue,

            -- 4. Cost (Sum of item costs)
            ISNULL(SUM(sd.Quantity * p.CostPrice), 0) as TotalCost

        FROM Sales s
        JOIN SalesDetails sd ON s.SaleID = sd.SaleID
        JOIN Products p ON sd.ProductID = p.ProductID
        WHERE s.IsActive = 1 
        AND s.SaleDate BETWEEN @StartDate AND @EndDate
        GROUP BY FORMAT(s.SaleDate, '{formatString}')
        ORDER BY PeriodLabel DESC";

            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate.Date);
                        cmd.Parameters.AddWithValue("@EndDate", endDate.Date.AddDays(1).AddSeconds(-1));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new SalesReportItem();
                                string rawPeriod = reader["PeriodLabel"].ToString();

                                // Date Formatting
                                if (mode == "Monthly" && DateTime.TryParse(rawPeriod + "-01", out DateTime d))
                                    item.Period = d.ToString("MMMM yyyy");
                                else if (mode == "Daily" && DateTime.TryParse(rawPeriod, out DateTime d2))
                                    item.Period = d2.ToString("MMM dd (ddd)");
                                else
                                    item.Period = rawPeriod;

                                item.TransactionCount = Convert.ToInt32(reader["TransCount"]);

                                // Map New Columns
                                item.CashSales = Convert.ToDecimal(reader["CashSales"]);
                                item.DigitalSales = Convert.ToDecimal(reader["DigitalSales"]);
                                item.Revenue = Convert.ToDecimal(reader["TotalRevenue"]);
                                item.CostOfGoods = Convert.ToDecimal(reader["TotalCost"]);

                                // Calculate Profit
                                item.GrossProfit = item.Revenue - item.CostOfGoods;

                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("Report Error: " + ex.Message); }

            return list;
        }


        public List<InventoryReportItem> GetInventoryReport()
        {
            var list = new List<InventoryReportItem>();

            string query = @"
        SELECT 
            p.SKU, p.ProductName, p.Brand, 
            c.CategoryName, s.SupplierName,
            p.Quantity, p.CostPrice, p.ReorderLevel, -- ✅ Fetch ReorderLevel
            
            ISNULL((
                SELECT SUM(sd.Quantity) 
                FROM SalesDetails sd 
                JOIN Sales sa ON sd.SaleID = sa.SaleID 
                WHERE sd.ProductID = p.ProductID 
                AND sa.SaleDate >= DATEADD(day, -30, GETDATE())
                AND sa.IsActive = 1
            ), 0) as SoldLast30Days

        FROM Products p
        LEFT JOIN SubCategories sc ON p.SubCategoryID = sc.SubCategoryID
        LEFT JOIN Categories c ON sc.CategoryID = c.CategoryID
        LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID
        WHERE p.IsActive = 1
        ORDER BY SoldLast30Days DESC";

            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new InventoryReportItem();

                                item.ProductID = reader["SKU"] != DBNull.Value ? reader["SKU"].ToString() : "N/A";
                                item.ItemName = reader["ProductName"].ToString();
                                item.Brand = reader["Brand"] != DBNull.Value ? reader["Brand"].ToString() : "-";
                                item.Category = reader["CategoryName"] != DBNull.Value ? reader["CategoryName"].ToString() : "-";
                                item.Supplier = reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : "-";

                                item.CurrentStock = Convert.ToInt32(reader["Quantity"]);
                                item.UnitCost = Convert.ToDecimal(reader["CostPrice"]);
                                item.TotalAssetValue = item.CurrentStock * item.UnitCost;
                                item.SoldLast30Days = Convert.ToInt32(reader["SoldLast30Days"]);

                                // ✅ Get Reorder Level (Default to 5 if null)
                                int reorderLevel = reader["ReorderLevel"] != DBNull.Value ? Convert.ToInt32(reader["ReorderLevel"]) : 5;

                                // --- LOGIC: STOCK MOVEMENT ---
                                if (item.SoldLast30Days >= 50) item.StockMovement = "Fast Moving";
                                else if (item.SoldLast30Days > 0) item.StockMovement = "Slow Moving";
                                else item.StockMovement = "Non-Moving";

                                // --- LOGIC: STATUS (Updated) ---
                                if (item.CurrentStock == 0)
                                {
                                    item.Status = "Out of Stock";
                                }
                                else if (item.CurrentStock <= reorderLevel)
                                {
                                    item.Status = "Low Stock"; // Uses the product's specific reorder level
                                }
                                else
                                {
                                    item.Status = "Sufficient";
                                }

                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine("Inventory Report Error: " + ex.Message); }

            return list;
        }

    }
}
