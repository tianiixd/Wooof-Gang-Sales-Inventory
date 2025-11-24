using Guna.Charts.WinForms;
using Microsoft.Data.SqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // For Charts
using Woof_Gang_Sales___Inventory.Database;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmDashboard : Form
    {
        TimeClockHelper time = new TimeClockHelper();
        public FrmDashboard()
        {
            InitializeComponent();
            this.Load += FrmDashboard_Load;
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            time.StartClock(lblTimeClock, lblDateClock);
            LoadDashboardStats();
            LoadSalesTrendChart();
            LoadGunaChart();
        }

        private void LoadDashboardStats()
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    // 1. Daily Sales (Today)
                    string queryDaily = "SELECT ISNULL(SUM(TotalAmount), 0) FROM Sales WHERE CAST(SaleDate AS DATE) = CAST(GETDATE() AS DATE) AND IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(queryDaily, conn))
                    {
                        decimal daily = Convert.ToDecimal(cmd.ExecuteScalar());
                        lblDailySales.Text = daily.ToString("C");
                    }

                    // Total Products
                    string queryProducts = "SELECT COUNT(*) From Products WHERE IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(queryProducts, conn))
                    {
                        decimal totalProducts = Convert.ToDecimal(cmd.ExecuteScalar());
                        lblTotalProducts.Text = totalProducts.ToString("N0");
                    }

                    // 3. Low Stock Items
                    string queryLowStock = "SELECT COUNT(*) FROM Products WHERE Quantity <= ReorderLevel AND IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(queryLowStock, conn))
                    {
                        int low = Convert.ToInt32(cmd.ExecuteScalar());
                        lblLowStock.Text = low.ToString("N0");
                        lblLowStock.ForeColor = low > 0 ? Color.Red : Color.Green;
                    }

                    // 4. Pending Orders
                    string queryPO = "SELECT COUNT(*) FROM PurchaseOrders WHERE Status = 'Pending' AND IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(queryPO, conn))
                    {
                        int pending = Convert.ToInt32(cmd.ExecuteScalar());
                        lblPendingPO.Text = pending.ToString("N0");
                        lblPendingPO.ForeColor = pending > 0 ? Color.Orange : Color.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Occured", ex.Message, "error");
            }
        }

        private void LoadGunaChart()
        {
            // 1. Create a Spline Area Dataset (This gives the smooth curve + fill)
            GunaSplineAreaDataset dataset = new GunaSplineAreaDataset();
            dataset.Label = "Qty Sold";

            // 2. Styling to match your image (Purple/Blue)
            // Fill Color (The area under the line)
            dataset.FillColor = Color.FromArgb(150, 100, 100, 255); // Semi-transparent Purple/Blue

            // Border Color (The line itself)
            dataset.BorderColor = Color.FromArgb(100, 80, 80, 255); // Solid Purple/Blue
            dataset.BorderWidth = 3;

            // Point Style (The circles on the line)
            dataset.PointStyle = PointStyle.Circle;
            dataset.PointRadius = 5;

            dataset.PointBorderColors.Add(Color.White);
            dataset.PointFillColors.Add(Color.FromArgb(100, 80, 80, 255));

            dataset.PointBorderWidth = 2;

            // 3. Fetch Data
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT TOP 5 p.ProductName, ISNULL(SUM(sd.Quantity),0) as QtySold
            FROM SalesDetails sd
            JOIN Products p ON sd.ProductID = p.ProductID
            JOIN Sales s ON sd.SaleID = s.SaleID
            WHERE s.IsActive = 1
            GROUP BY p.ProductName
            ORDER BY QtySold DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string prodName = reader["ProductName"].ToString();
                        double qty = Convert.ToDouble(reader["QtySold"]);

                        // 4. Add DataPoint (String Label, Double Value)
                        dataset.DataPoints.Add(prodName, qty);
                    }
                }
            }

            // 5. Apply to Chart
            // Configure the chart itself to look clean
            chartTopProducts.Datasets.Clear();
            chartTopProducts.Datasets.Add(dataset);

            // Optional: Adjust Chart Configuration
            chartTopProducts.Legend.Display = false; // Hide legend if you want a cleaner look
            chartTopProducts.XAxes.GridLines.Display = false; // Hide vertical grid lines
            chartTopProducts.YAxes.GridLines.Color = Color.FromArgb(240, 240, 240); // Light horizontal lines

            chartTopProducts.Update(); // Refresh
        }


        private void LoadSalesTrendChart()
        {
            // 1. Create Dataset (Purple Area Chart)
            GunaSplineAreaDataset dataset = new GunaSplineAreaDataset();
            dataset.Label = "Revenue";

            // Styling (Same Purple Theme as before)
            dataset.FillColor = Color.FromArgb(150, 100, 100, 255); // Semi-transparent Purple/Blue

            // Border Color (The line itself)
            dataset.BorderColor = Color.FromArgb(100, 80, 80, 255); // Solid Purple/Blue
            dataset.BorderWidth = 3;
            
            dataset.PointStyle = PointStyle.Circle;
            dataset.PointRadius = 5;
            dataset.PointFillColors.Add(Color.White);
            dataset.PointBorderColors.Add(Color.FromArgb(138, 43, 226));

            // 2. Fetch Data (Last 7 Days Revenue)
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT 
                FORMAT(SaleDate, 'MMM dd') as SaleDay, 
                SUM(TotalAmount) as TotalRevenue
            FROM Sales 
            WHERE IsActive = 1 
            AND SaleDate >= DATEADD(day, -7, GETDATE())
            GROUP BY FORMAT(SaleDate, 'MMM dd'), CAST(SaleDate as DATE)
            ORDER BY CAST(SaleDate as DATE)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    bool hasData = false;
                    while (reader.Read())
                    {
                        hasData = true;
                        string day = reader["SaleDay"].ToString();
                        double revenue = Convert.ToDouble(reader["TotalRevenue"]);

                        dataset.DataPoints.Add(day, revenue);
                    }

                    if (!hasData)
                    {
                        dataset.DataPoints.Add("No Data", 0);
                    }
                }
            }

            // 3. Apply to Chart (assuming you add a second chart control named 'chartSalesTrend')
            // If you don't have a second chart control, you can swap this into your existing one to test.
            chartSalesTrend.Datasets.Clear();
            chartSalesTrend.Datasets.Add(dataset);

            // Config
            chartSalesTrend.Legend.Display = false;
            chartSalesTrend.XAxes.GridLines.Display = false;
            chartSalesTrend.YAxes.GridLines.Color = Color.FromArgb(240, 240, 240);
            chartSalesTrend.Update();
        }

        /*
        private void LoadPaymentMethodChart()
        {
            // 1. Create Pie Dataset
            GunaPieDataset dataset = new GunaPieDataset();
            dataset.Label = "Payment Methods";

            // 2. Fetch Data
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT PaymentMethod, SUM(TotalAmount) as TotalValue
            FROM Sales
            WHERE IsActive = 1
            GROUP BY PaymentMethod";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string method = reader["PaymentMethod"].ToString();
                        double value = Convert.ToDouble(reader["TotalValue"]);

                        dataset.DataPoints.Add(method, value);
                    }
                }
            }

            // 3. Apply
            chartPaymentMethods.Datasets.Clear();
            chartPaymentMethods.Datasets.Add(dataset);

            // 4. Config
            chartPaymentMethods.Legend.Position = Guna.Charts.WinForms.LegendPosition.Right;
            chartPaymentMethods.Update();
        }
        */

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGunaChart();
            LoadDashboardStats();
            LoadSalesTrendChart();
        }
    }
}