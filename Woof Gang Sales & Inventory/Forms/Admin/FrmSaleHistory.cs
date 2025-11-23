using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Helpers;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmSalesHistory : Form
    {
        TimeClockHelper time = new TimeClockHelper();

        private readonly SalesRepository _salesRepo;

        // We keep the full list in memory to avoid hitting DB on every search keystroke
        private List<SaleViewModel> _allSales;

        public FrmSalesHistory()
        {
            InitializeComponent();
            _salesRepo = new SalesRepository();

            // ✅ STEP 1: Set Limits FIRST (Crucial to prevent crashes)
            // We use 1753 because that is the minimum value for SQL Server 'DATETIME' type.
            DateTime minDate = new DateTime(1753, 1, 1);
            DateTime maxDate = new DateTime(9998, 12, 31);

            // Reset Start Picker
            dtpStart.MinDate = minDate;
            dtpStart.MaxDate = maxDate;

            // Reset End Picker
            dtpEnd.MinDate = minDate;
            dtpEnd.MaxDate = maxDate;

            // ✅ STEP 2: Now it is safe to set the Default Values
            // Default: First day of this month
            dtpStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            // Default: Today (Now)
            dtpEnd.Value = DateTime.Now;

            
            this.VisibleChanged += FrmSalesHistory_VisibleChanged;

            txtSearch.TextChanged += txtSearch_TextChanged;
            dgvSales.SelectionChanged += dgvSales_SelectionChanged;

            // 4. Apply Styling
            DataGridViewStyler.ApplyStyle(dgvSales);
            DataGridViewStyler.ApplyStyle(dgvDetails);
        }

        private void FrmSaleHistory_Load(object sender, EventArgs e)
        {
            time.StartClock(lblTimeClock, lblDateClock);
            SetupGrids();
            LoadSalesHistory();
        }

        private void FrmSalesHistory_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadSalesHistory();
            }
        }

        private void SetupGrids()
        {
            // Prevents auto-generation mess
            dgvSales.AutoGenerateColumns = false;
            dgvDetails.AutoGenerateColumns = false;
        }

        // ============================================================
        // 1. LOAD DATA (Parent Grid)
        // ============================================================
        private void LoadSalesHistory()
        {
            DateTime start = dtpStart.Value.Date;
            DateTime end = dtpEnd.Value.Date.AddDays(1).AddSeconds(-1);
            string search = txtSearch.Text.Trim();

            // 1. Determine User Role & ID
            int? cashierFilter = null;

            if (SessionManager.CurrentUser.Role == "StoreClerk")
            {
                // If Clerk, ONLY show their sales
                cashierFilter = SessionManager.CurrentUser.UserID;
            }
            // If Admin, cashierFilter stays null (Show All)

            // 2. Fetch Data with Filter
            _allSales = _salesRepo.GetSalesHistory(start, end, search, cashierFilter);

            // 3. Bind Grid
            BindSalesGrid(_allSales);

            // 4. Update Stats Cards
            UpdateStatsCards(_allSales);
        }

        private void UpdateStatsCards(List<SaleViewModel> sales)
        {
            // Card 1: Total Revenue
            decimal totalRevenue = sales.Sum(s => s.TotalAmount);
            lblTotalRevenue.Text = totalRevenue.ToString("C");

            // Card 2: Total Transactions
            int count = sales.Count;
            lblCount.Text = count.ToString("N0");

            // Card 3: Average Sale Value
            decimal average = count > 0 ? (totalRevenue / count) : 0;
            lblAvg.Text = average.ToString("C");

            // Card 4: Highest Single Sale
            decimal highest = count > 0 ? sales.Max(s => s.TotalAmount) : 0;
            lblHigh.Text = highest.ToString("C");
        }

        private void BindSalesGrid(List<SaleViewModel> sales)
        {
            dgvSales.DataSource = sales;

            // Configure Columns (Create them programmatically to be safe)
            dgvSales.Columns.Clear();

            dgvSales.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "SaleID", HeaderText = "Ref #", DataPropertyName = "SaleID", Width = 80 });

            dgvSales.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "SaleDate", HeaderText = "Date", DataPropertyName = "SaleDate", DefaultCellStyle = { Format = "yyyy-MM-dd" } });

            // Optional: Combine Date & Time visually if you want
            // dgvSales.Columns.Add(new DataGridViewTextBoxColumn { Name = "SaleTime", ... });

            dgvSales.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "CustomerName", HeaderText = "Customer", DataPropertyName = "CustomerName", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            dgvSales.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "CashierName", HeaderText = "Cashier", DataPropertyName = "CashierName" });

            dgvSales.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "PaymentMethod", HeaderText = "Payment", DataPropertyName = "PaymentMethod" });

            dgvSales.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalAmount",
                HeaderText = "Total",
                DataPropertyName = "TotalAmount",
                DefaultCellStyle = { Format = "C", }
            });

            // Update Summary Labels
            decimal totalRevenue = sales.Sum(s => s.TotalAmount);
            
        }

        // ============================================================
        // 2. LOAD DETAILS (Child Grid)
        // ============================================================
        private void dgvSales_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSales.SelectedRows.Count > 0)
            {
                var selected = dgvSales.SelectedRows[0].DataBoundItem as SaleViewModel;
                if (selected != null)
                {
                    LoadSaleDetails(selected.SaleID);
                }
            }
            else
            {
                dgvDetails.DataSource = null;
            }
        }

        private void LoadSaleDetails(int saleID)
        {
            var details = _salesRepo.GetSaleDetails(saleID);
            dgvDetails.DataSource = details;

            dgvDetails.Columns.Clear();

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "ProductName", HeaderText = "Item", DataPropertyName = "ProductName", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "Brand", HeaderText = "Brand", DataPropertyName = "Brand" });

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "UnitPrice", HeaderText = "Price", DataPropertyName = "UnitPrice", DefaultCellStyle = { Format = "N2" } });

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "Quantity", HeaderText = "Qty", DataPropertyName = "Quantity" });

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "Subtotal", HeaderText = "Subtotal", DataPropertyName = "Subtotal", DefaultCellStyle = { Format = "N2" } });
        }

        // ============================================================
        // 3. FILTERS & SEARCH
        // ============================================================
        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadSalesHistory();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_allSales == null) return;

            string search = txtSearch.Text.Trim().ToLower();

            // Filter the in-memory list (Fast!)
            var filtered = _allSales.Where(s =>
                s.SaleID.ToString().Contains(search) ||
                s.CustomerName.ToLower().Contains(search) ||
                s.CashierName.ToLower().Contains(search)
            ).ToList();

            BindSalesGrid(filtered);
        }
    }
}