using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Data.Database;
using Woof_Gang_Sales___Inventory.Helpers;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmInventoryReports : Form
    {
        private readonly ReportsRepository _repo;
        private List<InventoryReportItem> _reportData;

        TimeClockHelper time = new TimeClockHelper();

        public FrmInventoryReports()
        {
            InitializeComponent();
            _repo = new ReportsRepository();

            this.VisibleChanged += FrmInventoryReports_VisibleChanged;

            DataGridViewStyler.ApplyStyle(dgvInventory);
        }

        private void FrmInventoryReports_Load(object sender, EventArgs e)
        {
            time.StartClock(lblTimeClock, lblDateClock);
            LoadReport();
        }

        private void FrmInventoryReports_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadReport();
            }
        }

        private void LoadReport()
        {
            // 1. Get Data
            _reportData = _repo.GetInventoryReport();

            // 2. Bind Grid
            dgvInventory.DataSource = _reportData;
            FormatGrid();

            // 3. Calculate Stats
            CalculateStats();
        }

        private void CalculateStats()
        {
            if (_reportData == null || _reportData.Count == 0)
            {
                // Reset labels if empty
                lblTotalValue.Text = "₱0.00";
                lblFastMoving.Text = "0";
                lblSlowMoving.Text = "0";
                lblNonMoving.Text = "0";
                return;
            }

            // Card 1: Total Asset Value
            decimal totalValue = _reportData.Sum(x => x.TotalAssetValue);
            lblTotalValue.Text = totalValue.ToString("C");

            // ✅ FIX: Ensure this logic matches the "Movement" column logic exactly

            // Card 2: Fast Moving (Sold > 50)
            // MAKE SURE this matches the string in your Repository logic ("Fast Moving")
            int fastCount = _reportData.Count(x => x.StockMovement == "Fast Moving");
            lblFastMoving.Text = fastCount.ToString("N0");

            // Card 3: Slow Moving (Sold 1-50)
            int slowCount = _reportData.Count(x => x.StockMovement == "Slow Moving");
            lblSlowMoving.Text = slowCount.ToString("N0");

            // Card 4: Non-Moving (Sold 0)
            int nonMovingCount = _reportData.Count(x => x.StockMovement == "Non-Moving");
            lblNonMoving.Text = nonMovingCount.ToString("N0");
        }

        private void FormatGrid()
        {
            if (dgvInventory.Columns["ProductID"] != null)
            {
                dgvInventory.Columns["ProductID"].HeaderText = "SKU";
                dgvInventory.Columns["ItemName"].HeaderText = "Item Name";
                dgvInventory.Columns["ItemName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgvInventory.Columns["Brand"].HeaderText = "Brand";
                dgvInventory.Columns["Category"].HeaderText = "Category";
                dgvInventory.Columns["Supplier"].HeaderText = "Supplier";

                dgvInventory.Columns["CurrentStock"].HeaderText = "Stock";
                dgvInventory.Columns["UnitCost"].HeaderText = "Unit Cost";
                dgvInventory.Columns["UnitCost"].DefaultCellStyle.Format = "C";

                dgvInventory.Columns["TotalAssetValue"].HeaderText = "Asset Value";
                dgvInventory.Columns["TotalAssetValue"].DefaultCellStyle.Format = "C";

                // The Analysis Columns
                dgvInventory.Columns["SoldLast30Days"].HeaderText = "Sold (30 Days)";

                dgvInventory.Columns["StockMovement"].HeaderText = "Movement";
                dgvInventory.Columns["Status"].HeaderText = "Status";

                dgvInventory.CellFormatting += (s, e) =>
                {
                    if (e.RowIndex >= 0 && dgvInventory.Columns[e.ColumnIndex].Name == "Status")
                    {
                        string val = e.Value?.ToString();
                        if (val == "Out of Stock")
                        {
                            e.CellStyle.ForeColor = Color.Red;
                            e.CellStyle.SelectionForeColor = Color.Red;
                        }
                        else if (val == "Low Stock")
                        {
                            e.CellStyle.ForeColor = Color.Orange;
                            e.CellStyle.SelectionForeColor = Color.Orange;
                        }
                        else
                        {
                            e.CellStyle.ForeColor = Color.Green;
                            e.CellStyle.SelectionForeColor = Color.Green;
                        }
                    }

                    // 2. MOVEMENT COLUMN (Text Color)
                    if (dgvInventory.Columns[e.ColumnIndex].Name == "StockMovement")
                    {
                        string val = e.Value?.ToString();
                        if (val == "Fast Moving")
                        {
                            e.CellStyle.ForeColor = Color.Green;
                            e.CellStyle.SelectionForeColor = Color.Green;
                        }
                        else if (val == "Slow Moving") 
                        {
                            e.CellStyle.ForeColor = Color.Orange;
                            e.CellStyle.SelectionForeColor = Color.Orange; 
                        }
                        
                        else
                        {
                            e.CellStyle.ForeColor = Color.Gray;
                            e.CellStyle.SelectionForeColor = Color.Gray;
                        }
                    }

                };
            }
        }

        // --- EXPORT ---
        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            User? loggedInUser = SessionManager.CurrentUser;

            if (loggedInUser == null)
            {
                DialogHelper.ShowCustomDialog("Login Error", "Could not find the logged-in user. Please log out and log back in.", "error");
                return;
            }

            string fullName = $"{loggedInUser.FirstName} {loggedInUser.MiddleName} {loggedInUser.LastName}";

            // Pass the 4th argument
            ExportHelper.ExportInventoryToPdf(dgvInventory, "Inventory_Report", fullName);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
           
            ExportHelper.SalesExportGridToCSV(dgvInventory, "Inventory Report");
        }
    }
}