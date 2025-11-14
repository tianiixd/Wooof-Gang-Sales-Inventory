using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Helpers;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmPurchaseOrderView : Sample
    {
        private readonly PurchaseOrderRepository _poRepo;

        private string[] statusFilter = {
            "All Active", // Default
            "Pending",
            "Received",
            "Cancelled",
            "Archived",
            "Show All"
        };

        public FrmPurchaseOrderView()
        {
            InitializeComponent();
            _poRepo = new PurchaseOrderRepository();

            // ✅ --- This now follows your FrmCategoryView pattern ---
            // 1. Load initial data (will use default "All Active")
            ReadPurchaseOrders();

            // 2. Wire up CellFormatting (just like FrmCategoryView)
            dgvPurchaseOrders.CellFormatting += dgvPurchaseOrders_CellFormatting;

            // 3. Wire up other events
            dgvPurchaseOrders.SelectionChanged += dgvPurchaseOrders_SelectionChanged;
            txtSearch.TextChanged += txtSearch_TextChanged;
            cmbStatusFilter.SelectedIndexChanged += FilterChanged;

            // 4. Wire up your button click events in the VS Designer
            // btnAdd.Click += btnAdd_Click;
            // btnEdit.Click += btnEdit_Click;
            // btnDelete.Click += btnDelete_Click;
            // btnReceiveStock.Click += btnReceiveStock_Click;
            // btnCancelOrder.Click += btnCancelOrder_Click;
        }

        private void FrmPurchaseOrderView_Load(object sender, EventArgs e)
        {
            // The Load event now ONLY sets up the filters.
            SetupFilters();
        }

        private void SetupFilters()
        {
            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.AddRange(statusFilter);
            cmbStatusFilter.SelectedIndex = 0; // "All Active"
            txtSearch.PlaceholderText = "Search by PO ID or Supplier Name...";
        }

        // ✅ --- NEW: CellFormatting event (like FrmCategoryView) ---
        private void dgvPurchaseOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format Status Column
            if (dgvPurchaseOrders.Columns[e.ColumnIndex].Name == "Status")
            {
                string value = e.Value?.ToString() ?? "";

                if (value.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(255, 128, 0); // Orange
                    e.CellStyle.SelectionForeColor = Color.FromArgb(255, 128, 0);
                    e.CellStyle.Font = new Font(dgvPurchaseOrders.Font, FontStyle.Bold);
                }
                else if (value.Equals("Received", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.SelectionForeColor = Color.Green;
                    e.CellStyle.Font = new Font(dgvPurchaseOrders.Font, FontStyle.Bold);
                }
                else if (value.Equals("Cancelled", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.SelectionForeColor = Color.Red;
                    e.CellStyle.Font = new Font(dgvPurchaseOrders.Font, FontStyle.Bold);
                }
            }

            // Format TotalCost Column
            if (dgvPurchaseOrders.Columns[e.ColumnIndex].Name == "TotalCost")
            {
                if (e.Value != null && e.Value is decimal cost)
                {
                    e.Value = cost.ToString("C"); // "C" for Currency
                    e.FormattingApplied = true;
                }
            }
        }


        private void ReadPurchaseOrders()
        {
            if (cmbStatusFilter.SelectedItem == null) return;

            string search = txtSearch.Text.Trim();
            string status = cmbStatusFilter.SelectedItem.ToString();

            var poList = _poRepo.GetPurchaseOrders(search, status);

            // ✅ --- PATTERN MATCH: Create DataTable ---
            DataTable dt = new DataTable();
            dt.Columns.Add("POID");
            dt.Columns.Add("SupplierName");
            dt.Columns.Add("PODate", typeof(DateTime));
            dt.Columns.Add("TotalCost", typeof(decimal));
            dt.Columns.Add("Status");
            dt.Columns.Add("IsActive", typeof(bool));

            foreach (var po in poList)
            {
                // ✅ --- PATTERN MATCH: Create DataRow ---
                var row = dt.NewRow();
                row["POID"] = po.POID;
                row["SupplierName"] = po.SupplierName;
                row["PODate"] = po.PODate;
                row["TotalCost"] = po.TotalCost ?? 0m;
                row["Status"] = po.Status;
                row["IsActive"] = po.IsActive;
                dt.Rows.Add(row);
            }

            // ✅ --- PATTERN MATCH: Set DataSource ---
            dgvPurchaseOrders.DataSource = dt;

            // ✅ --- PATTERN MATCH: Apply Styling After ---
            DataGridViewStyler.ApplyStyle(dgvPurchaseOrders, "POID");

            // Set column visibility and formatting
            dgvPurchaseOrders.Columns["POID"].HeaderText = "PO ID";
            dgvPurchaseOrders.Columns["SupplierName"].HeaderText = "Supplier";
            dgvPurchaseOrders.Columns["SupplierName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvPurchaseOrders.Columns["PODate"].HeaderText = "Order Date";
            dgvPurchaseOrders.Columns["PODate"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dgvPurchaseOrders.Columns["TotalCost"].HeaderText = "Total Cost";
            dgvPurchaseOrders.Columns["IsActive"].Visible = false;
        }

        private void dgvPurchaseOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPurchaseOrders.SelectedRows.Count == 0)
            {
                dgvOrderDetails.DataSource = null;
                lblDetails.Text = "Select an order to see details";
                return;
            }

            DataRowView drv = dgvPurchaseOrders.SelectedRows[0].DataBoundItem as DataRowView;
            if (drv == null) return;

            int selectedPOID = Convert.ToInt32(drv["POID"]);
            lblDetails.Text = $"Details for Order #{selectedPOID}";

            var detailsList = _poRepo.GetPurchaseOrderDetails(selectedPOID);

            // ✅ --- PATTERN MATCH: Create DataTable for Detail Grid ---
            DataTable dtDetails = new DataTable();
            dtDetails.Columns.Add("ProductName");
            dtDetails.Columns.Add("Quantity");
            dtDetails.Columns.Add("UnitCost", typeof(decimal));
            dtDetails.Columns.Add("Subtotal", typeof(decimal));

            foreach (var item in detailsList)
            {
                // ✅ --- PATTERN MATCH: Create DataRow ---
                dtDetails.Rows.Add(item.ProductName, item.Quantity, item.UnitCost, item.Subtotal);
            }

            // ✅ --- PATTERN MATCH: Set DataSource ---
            dgvOrderDetails.DataSource = dtDetails;

            // ✅ --- PATTERN MATCH: Apply Styling After ---
            DataGridViewStyler.ApplyStyle(dgvOrderDetails, "ProductName");

            dgvOrderDetails.Columns["ProductName"].HeaderText = "Product";
            dgvOrderDetails.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvOrderDetails.Columns["Quantity"].HeaderText = "Qty";
            dgvOrderDetails.Columns["UnitCost"].HeaderText = "Unit Cost";
            dgvOrderDetails.Columns["UnitCost"].DefaultCellStyle.Format = "N2";
            dgvOrderDetails.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
        }


        // --- Helper method to get data from the selected row ---
        private (int poID, string status, bool isActive) GetSelectedOrderInfo()
        {
            if (dgvPurchaseOrders.SelectedRows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Selection", "Please select an order first.", "warning");
                return (0, null, false);
            }

            DataRowView drv = dgvPurchaseOrders.SelectedRows[0].DataBoundItem as DataRowView;
            if (drv == null)
            {
                return (0, null, false);
            }

            int poID = Convert.ToInt32(drv["POID"]);
            string status = drv["Status"].ToString();
            bool isActive = Convert.ToBoolean(drv["IsActive"]);

            return (poID, status, isActive);
        }

        // --- Event Handlers ---

        private void FilterChanged(object sender, EventArgs e)
        {
            ReadPurchaseOrders();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ReadPurchaseOrders();
        }

        // ... (Your 5 button-click methods are all correct) ...
        // ... (btnAdd_Click, btnEdit_Click, btnDelete_Click, ...) ...
        // ... (btnReceiveStock_Click, btnCancelOrder_Click) ...

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // We will create this form in the next step
            // FrmCreateEditPurchaseOrder form = new FrmCreateEditPurchaseOrder();
            // form.IsEditMode = false;
            // if (form.ShowDialog() == DialogResult.OK)
            // {
            //     ReadPurchaseOrders();
            // }
            MessageBox.Show("This will open FrmCreateEditPurchaseOrder (IsEditMode = false)");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var (poID, status, isActive) = GetSelectedOrderInfo();
            if (poID == 0) return; // No selection

            if (status != "Pending")
            {
                DialogHelper.ShowCustomDialog("Action Denied", $"This order cannot be edited because its status is '{status}'.", "error");
                return;
            }

            // We will create this form in the next step
            // FrmCreateEditPurchaseOrder form = new FrmCreateEditPurchaseOrder(poID); // Pass ID
            // form.IsEditMode = true;
            // if (form.ShowDialog() == DialogResult.OK)
            // {
            //     ReadPurchaseOrders();
            // }
            MessageBox.Show($"This will open FrmCreateEditPurchaseOrder (IsEditMode = true) for POID {poID}");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var (poID, status, isActive) = GetSelectedOrderInfo();
            if (poID == 0) return; // No selection

            if (status == "Pending" || !isActive)
            {
                DialogHelper.ShowCustomDialog("Action Denied", "Only 'Received' or 'Cancelled' active orders can be archived.", "error");
                return;
            }

            var result = DialogHelper.ShowConfirmDialog("Archive Order", "Are you sure you want to archive this order? It will be hidden from the main list.", "warning");
            if (result == DialogResult.No) return;

            bool success = _poRepo.ArchivePurchaseOrder(poID);
            if (success)
            {
                ReadPurchaseOrders(); // Refresh the grid
            }
        }

        private void btnReceiveStock_Click(object sender, EventArgs e)
        {
            var (poID, status, isActive) = GetSelectedOrderInfo();
            if (poID == 0) return; // No selection

            if (status != "Pending")
            {
                DialogHelper.ShowCustomDialog("Action Denied", $"Stock cannot be received for this order. Its status is '{status}'.", "error");
                return;
            }

            // We will create this form in the next step
            // FrmReceivePO form = new FrmReceivePO(poID); // Pass POID
            // if (form.ShowDialog() == DialogResult.OK)
            // {
            //     ReadPurchaseOrders();
            // }
            MessageBox.Show($"This will open FrmReceivePO for POID {poID}");
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            var (poID, status, isActive) = GetSelectedOrderInfo();
            if (poID == 0) return; // No selection

            if (status != "Pending")
            {
                DialogHelper.ShowCustomDialog("Action Denied", $"This order can no longer be cancelled (Status: '{status}').", "error");
                return;
            }

            var result = DialogHelper.ShowConfirmDialog("Cancel Order", "Are you sure you want to cancel this purchase order?", "warning");
            if (result == DialogResult.No) return;

            bool success = _poRepo.UpdatePOStatus(poID, "Cancelled");
            if (success)
            {
                ReadPurchaseOrders(); // Refresh the grid
            }
        }
    }
}