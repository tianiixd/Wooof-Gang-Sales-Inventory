using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data; // Updated Namespace
using Woof_Gang_Sales___Inventory.Data.Database;
using Woof_Gang_Sales___Inventory.Helpers;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util; // For DataGridViewStyler

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmSalesReports : Form
    {
        private readonly ReportsRepository _repo;
        private List<SalesReportItem> _currentReportData;
        TimeClockHelper time = new TimeClockHelper();

        public FrmSalesReports()
        {
            InitializeComponent();
            _repo = new ReportsRepository();

            // 1. Set Safe Dates
            DateTime minDate = new DateTime(2000, 1, 1);
            DateTime maxDate = new DateTime(9998, 12, 31);

            dtpStart.MinDate = minDate;
            dtpStart.MaxDate = maxDate;
            dtpEnd.MinDate = minDate;
            dtpEnd.MaxDate = maxDate;

            // 2. Set Defaults
            dtpStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEnd.Value = DateTime.Now;

            cmbReportType.Items.Clear();
            cmbReportType.Items.Add("Daily Report");
            cmbReportType.Items.Add("Monthly Report");
            cmbReportType.Items.Add("Yearly Report");
            cmbReportType.SelectedIndex = 0;
            cmbReportType.SelectedIndexChanged += (s, e) => btnGenerate_Click(null, null);

     
            DataGridViewStyler.ApplyStyle(dgvReport);

            // ✅ FIX: Trigger the load immediately!
            // We pass (null, null) because our method doesn't use the arguments.
            btnGenerate_Click(null, null);
            this.VisibleChanged += FrmSalesReports_VisibleChanged;
        }

        private void FrmSalesReports_Load(object sender, EventArgs e)
        {
            time.StartClock(lblTimeClock, lblDateClock);
        }


        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (cmbReportType.SelectedItem == null) return;

            string selected = cmbReportType.SelectedItem.ToString();
            string mode = "Daily";

            if (selected.Contains("Monthly")) mode = "Monthly";
            else if (selected.Contains("Yearly")) mode = "Yearly";

            // 1. Get Data
            _currentReportData = _repo.GetSalesReport(dtpStart.Value, dtpEnd.Value, mode);

            // 2. Bind to Grid
            dgvReport.DataSource = _currentReportData;
            FormatGrid();

            // 3. Calculate Totals for Stats Cards
            CalculateDashboardTotals();
        }

        private void CalculateDashboardTotals()
        {
            // 1. Safety Check
            if (_currentReportData == null || _currentReportData.Count == 0)
            {
                lblTotalRevenue.Text = "₱0.00";
                lblTotalCash.Text = "₱0.00";
                lblTotalDigital.Text = "₱0.00";
                lblTotalProfit.Text = "0.00%";
                return;
            }

            // 2. Sum up the totals
            decimal totalSales = _currentReportData.Sum(x => x.Revenue);
            decimal totalCash = _currentReportData.Sum(x => x.CashSales);
            decimal totalDigital = _currentReportData.Sum(x => x.DigitalSales);
            decimal totalProfit = _currentReportData.Sum(x => x.GrossProfit);

            // 3. Update Labels
            // Card 1: Total Revenue
            lblTotalRevenue.Text = totalSales.ToString("C");

            // Card 2: Total Cash (Make sure Label header says "Total Cash")
            lblTotalCash.Text = totalCash.ToString("C");

            // Card 3: Total Digital (Make sure Label header says "Gross Profit")
            lblTotalDigital.Text = totalDigital.ToString("C");
            lblTotalDigital.ForeColor = totalDigital >= 0 ? Color.Green : Color.Red;

            // Card 4: Total Profit (Make sure Label header says "Total Digital")
            lblTotalProfit.Text = totalProfit.ToString("C");
            lblTotalProfit.ForeColor = totalProfit >= 0 ? Color.Green : Color.Red; ; // Visual cue

        }

        private void FormatGrid()
        {
            if (dgvReport.Columns["Period"] != null)
            {
                dgvReport.Columns["Period"].HeaderText = "Date";
                dgvReport.Columns["Period"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgvReport.Columns["TransactionCount"].HeaderText = "Trans #";
                dgvReport.Columns["TransactionCount"].Width = 100;

                // ✅ NEW: Payment Breakdown Columns
                dgvReport.Columns["CashSales"].HeaderText = "Cash";
                dgvReport.Columns["CashSales"].DefaultCellStyle.Format = "N2";

                dgvReport.Columns["DigitalSales"].HeaderText = "GCash/Maya";
                dgvReport.Columns["DigitalSales"].DefaultCellStyle.Format = "N2";

                // Revenue
                dgvReport.Columns["Revenue"].HeaderText = "Total Revenue";
                dgvReport.Columns["Revenue"].DefaultCellStyle.Format = "C";
                

                // ✅ Retained: Cost & Profit
                dgvReport.Columns["CostOfGoods"].HeaderText = "Cost Of Goods";
                dgvReport.Columns["CostOfGoods"].DefaultCellStyle.Format = "N2";

                dgvReport.Columns["GrossProfit"].HeaderText = "Profit";
                dgvReport.Columns["GrossProfit"].DefaultCellStyle.Format = "N2";
                dgvReport.Columns["GrossProfit"].DefaultCellStyle.ForeColor = Color.DarkGreen;
            }
        }

        private void FrmSalesReports_VisibleChanged(object sender, EventArgs e)
        {
            // Only reload if the form is becoming visible
            if (this.Visible)
            {
                // Re-run the generation logic using current date pickers
                btnGenerate_Click(null, null);
            }
        }


        // --- EXPORT BUTTONS ---

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportHelper.SalesExportGridToCSV(dgvReport, "Sales_Report");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            string selectedType = cmbReportType.Text;

            // ✅ THE FIX: Insert the word "SALES" into the title
            // Logic: Replace "Report" with "Sales Report" -> "Daily Sales Report"
            string reportTitle = selectedType.Replace("Report", "Sales Report").ToUpper();

            // Get Current User Name (or default to Admin)
            User? loggedInUser = SessionManager.CurrentUser;

            if (loggedInUser == null)
            {
                DialogHelper.ShowCustomDialog("Login Error", "Could not find the logged-in user. Please log out and log back in.", "error");
                return;
            }

            string fullName = $"{loggedInUser.FirstName} {loggedInUser.MiddleName} {loggedInUser.LastName}";

            // Pass the 4th argument
            ExportHelper.SalesExportGridToPdf(dgvReport, "Sales_Report", reportTitle, fullName);
        }

    }
}