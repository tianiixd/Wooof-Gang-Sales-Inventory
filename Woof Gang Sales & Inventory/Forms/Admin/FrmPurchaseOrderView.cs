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
    public partial class FrmPurchaseOrderView : Form
    {
        private readonly PurchaseOrderRepository _poRepo;

        private string[] statusFilter = {
            "Pending", // Default
            "Received",
            "Cancelled",
            "All Active",
            "All Archived",
            "Show All"
        };

        public FrmPurchaseOrderView()
        {
            InitializeComponent();
            _poRepo = new PurchaseOrderRepository();

            // ✅ --- This now follows your FrmCategoryView pattern ---
            // 1. Load initial data for both grids
            ReadPurchaseOrders();
            ReadOrderDetails(0); // Call with 0 to load empty headers

            // 2. Wire up CellFormatting (just like FrmCategoryView)
            dgvPurchaseOrders.CellFormatting += dgvPurchaseOrders_CellFormatting;
            dgvOrderDetails.CellFormatting += dgvOrderDetails_CellFormatting;

            // 3. Wire up other events
            dgvPurchaseOrders.SelectionChanged += dgvPurchaseOrders_SelectionChanged;
            txtSearch.TextChanged += txtSearch_TextChanged;
            cmbStatusFilter.SelectedIndexChanged += FilterChanged;

            // 4. Wire up your button click events in the VS Designer
            // (btnAdd, btnEdit, btnDelete, btnReceiveStock, btnCancelOrder)
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
            cmbStatusFilter.SelectedIndex = 0; 
            txtSearch.PlaceholderText = "Search by PO ID or Supplier Name...";
        }

        // ❌ --- The SetupGrids() method is GONE (it's now inside the Read... methods) ---


        #region DataGrid Formatting

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

            if (dgvPurchaseOrders.Columns[e.ColumnIndex].Name == "ReceivedDate")
            {
                if (e.Value == null || e.Value == DBNull.Value)
                {
                    e.Value = "N/A";
                    e.FormattingApplied = true;
                }
            }


        }

        private void dgvOrderDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format Cost Columns
            if (dgvOrderDetails.Columns[e.ColumnIndex].Name == "UnitCost" ||
                dgvOrderDetails.Columns[e.ColumnIndex].Name == "Subtotal")
            {
                if (e.Value != null && e.Value is decimal cost)
                {
                    e.Value = cost.ToString("N2"); // "N2" for 2 decimal places
                    e.FormattingApplied = true;
                }
            }
        }

        #endregion

        #region Data Loading

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
            dt.Columns.Add("ReceivedDate", typeof(DateTime));
            dt.Columns.Add("TotalCost", typeof(decimal));
            dt.Columns.Add("Status");
            dt.Columns.Add("Remarks");
            dt.Columns.Add("IsActive", typeof(bool));

            foreach (var po in poList)
            {
                dt.Rows.Add(
                    po.POID,
                    po.SupplierName,
                    po.PODate,
                    po.ReceivedDate.HasValue ? (object)po.ReceivedDate.Value : DBNull.Value,
                    po.TotalCost ?? 0m,
                    po.Status,
                    po.Remarks,
                    po.IsActive
                );
            }

            dgvPurchaseOrders.DataSource = dt;

            // ✅ --- PATTERN MATCH: Apply Styling After ---
            DataGridViewStyler.ApplyStyle(dgvPurchaseOrders, "POID");

            dgvPurchaseOrders.Columns["POID"].HeaderText = "PO ID";
            dgvPurchaseOrders.Columns["SupplierName"].HeaderText = "Supplier";
            dgvPurchaseOrders.Columns["SupplierName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Fill is good here
            dgvPurchaseOrders.Columns["PODate"].HeaderText = "Order Date";
            dgvPurchaseOrders.Columns["PODate"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dgvPurchaseOrders.Columns["ReceivedDate"].HeaderText = "Received On";
            dgvPurchaseOrders.Columns["ReceivedDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dgvPurchaseOrders.Columns["TotalCost"].HeaderText = "Total Cost";
            dgvPurchaseOrders.Columns["TotalCost"].DefaultCellStyle.Format = "C"; // Format as Currency
            dgvPurchaseOrders.Columns["Remarks"].Visible = false;
            dgvPurchaseOrders.Columns["IsActive"].Visible = false;
        }

        // ✅ --- NEW METHOD: As you suggested ---
        private void ReadOrderDetails(int poID)
        {
            DataTable dtDetails = new DataTable();
            dtDetails.Columns.Add("ProductName");
            dtDetails.Columns.Add("Quantity");
            dtDetails.Columns.Add("UnitCost", typeof(decimal));
            dtDetails.Columns.Add("Subtotal", typeof(decimal));

            if (poID > 0)
            {
                // Only fetch details if we have a real POID
                var detailsList = _poRepo.GetPurchaseOrderDetails(poID);
                foreach (var item in detailsList)
                {
                    dtDetails.Rows.Add(item.ProductName, item.Quantity, item.UnitCost, item.Subtotal);
                }
            }
            // If poID is 0, this creates an EMPTY table with just headers (which is what you wanted)

            dgvOrderDetails.DataSource = dtDetails;

            // Apply styling
            DataGridViewStyler.ApplyStyle(dgvOrderDetails, "ProductName");
            dgvOrderDetails.Columns["ProductName"].HeaderText = "Product";
            dgvOrderDetails.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvOrderDetails.Columns["Quantity"].HeaderText = "Qty";
            dgvOrderDetails.Columns["UnitCost"].HeaderText = "Unit Cost";
            dgvOrderDetails.Columns["Subtotal"].HeaderText = "Subtotal";
        }


        private void dgvPurchaseOrders_SelectionChanged(object sender, EventArgs e)
        {
            // ✅ --- This method is now much simpler! ---

            if (dgvPurchaseOrders.SelectedRows.Count == 0)
            {
                ReadOrderDetails(0); // Load empty grid
                lblDetails.Text = "Select an order to see details";
                txtRemarksView.Text = "";
                return;
            }

            DataRowView drv = dgvPurchaseOrders.SelectedRows[0].DataBoundItem as DataRowView;
            if (drv == null) return;

            int selectedPOID = Convert.ToInt32(drv["POID"]);
            lblDetails.Text = $"Details for Order #{selectedPOID}";

            txtRemarksView.Text = drv["Remarks"].ToString();

            // Just call our new helper method
            ReadOrderDetails(selectedPOID);
        }

        #endregion

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
            ReadOrderDetails(0); // Clear details when filter changes
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ReadPurchaseOrders();
            ReadOrderDetails(0); // Clear details when search changes
        }

        // --- Button Click Methods ---
        // (These are all correct from the previous version)

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            FrmCreateEditPurchaseOrder form = new FrmCreateEditPurchaseOrder();
            form.IsEditMode = false;
            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadPurchaseOrders();
            }
            
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