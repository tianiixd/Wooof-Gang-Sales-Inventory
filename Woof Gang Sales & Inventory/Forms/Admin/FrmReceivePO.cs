using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Helpers;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmReceivePO : Form
    {
        private int _poID;
        private PurchaseOrderRepository _poRepo;
        private ProductRepository _productRepo;
        private List<PurchaseOrderDetailView> _originalOrderDetails;

        // ✅ THE GUNA DATE PICKER
        private Guna2DateTimePicker dtpGrid;

        // ✅ REMOVED dtpGrid from arguments. We create it internally.
        public FrmReceivePO(int poID)
        {
            InitializeComponent();
            _poID = poID;
            _poRepo = new PurchaseOrderRepository();
            _productRepo = new ProductRepository();

            // Initialize the Guna picker
            InitDateTimePicker();
        }

        private void FrmReceivePO_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadData();
        }

        private void InitDateTimePicker()
        {
            dtpGrid = new Guna2DateTimePicker();

            // Styling
            dtpGrid.BorderRadius = 4;
            dtpGrid.FillColor = Color.White;
            dtpGrid.ForeColor = Color.Black;
            dtpGrid.BorderColor = Color.Silver;
            dtpGrid.BorderThickness = 1;
            dtpGrid.Format = DateTimePickerFormat.Short;
            dtpGrid.Cursor = Cursors.Hand;

            dtpGrid.MinDate = DateTime.Today;
            dtpGrid.MaxDate = DateTime.Today.AddYears(100);

            // Initial Visibility
            dtpGrid.Visible = false;
            dtpGrid.Width = 150;

            // ✅ FIX 1: Use a helper method to update the grid
            // We call this from multiple events to ensure it always writes back
            void CommitDateToGrid()
            {
                if (dgvReceive.CurrentCell != null && dtpGrid.Visible)
                {
                    dgvReceive.CurrentCell.Value = dtpGrid.Value.ToString("yyyy-MM-dd");
                    // Optional: Hide immediately after selection
                    // dtpGrid.Visible = false; 
                }
            }

            // Event: ValueChanged (Handles scrolling months/years)
            dtpGrid.ValueChanged += (s, e) => CommitDateToGrid();

            // ✅ FIX 2: CloseUp (Handles clicking the SAME date again)
            // This fires when the calendar dropdown closes.
            dtpGrid.CloseUp += (s, e) =>
            {
                CommitDateToGrid();
                dtpGrid.Visible = false; // Hide after selection
            };

            // Event: Lost Focus (Hide if user clicks away)
            dtpGrid.Leave += (s, e) => { dtpGrid.Visible = false; };

            // Event: KeyDown (Allow Enter key to confirm)
            dtpGrid.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    CommitDateToGrid();
                    dtpGrid.Visible = false;
                    e.SuppressKeyPress = true;
                }
            };

            dgvReceive.Controls.Add(dtpGrid);
        }
        private void SetupGrid()
        {
            dgvReceive.AutoGenerateColumns = false;
            dgvReceive.Columns.Clear();

            // 1. Product Name
            dgvReceive.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                HeaderText = "Product Name",
                DataPropertyName = "ProductName",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // 2. Ordered Qty
            dgvReceive.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OrderedQty",
                HeaderText = "Ordered",
                DataPropertyName = "Quantity",
                ReadOnly = true,
                Width = 100
            });

            // 3. Received Qty
            dgvReceive.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ReceivedQty",
                HeaderText = "Received",
                DataPropertyName = "Quantity",
                ReadOnly = false,
                Width = 100
            });

            // 4. Expiration Date
            dgvReceive.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ExpirationDate",
                HeaderText = "Expiry (Click to set)",
                Width = 200, // Made wider for the Guna Picker
                ReadOnly = true
            });

            DataGridViewStyler.ApplyStyle(dgvReceive);

            // Wire up events
            dgvReceive.CellClick += dgvReceive_CellClick;
            dgvReceive.Scroll += (s, e) => dtpGrid.Visible = false;
        }

        private void LoadData()
        {
            var (po, details) = _poRepo.GetPurchaseOrderForEdit(_poID);

            if (po == null) { this.Close(); return; }

            lblTitle.Text = $"Receiving Order #{po.POID}";
            lblSupplier.Text = $"Supplier: {po.SupplierName}";

            _originalOrderDetails = details;
            dgvReceive.DataSource = details;
        }

        // --- THE GUNA DATE PICKER LOGIC ---
        private void dgvReceive_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // If clicking the Expiration Date Column
            if (dgvReceive.Columns[e.ColumnIndex].Name == "ExpirationDate")
            {
                // Calculate Position
                Rectangle rect = dgvReceive.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                // Set Size and Location
                dtpGrid.Size = new Size(rect.Width, rect.Height);
                dtpGrid.Location = new Point(rect.X, rect.Y);

                // Set Value
                if (DateTime.TryParse(dgvReceive.CurrentCell.Value?.ToString(), out DateTime existingDate))
                {
                    dtpGrid.Value = existingDate;
                }

                // Show it
                dtpGrid.Visible = true;
                dtpGrid.BringToFront();
                dtpGrid.Focus();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            List<PurchaseOrderDetail> itemsToReceive = new List<PurchaseOrderDetail>();
            bool hasWarnings = false; // Track if we showed a warning

            foreach (DataGridViewRow row in dgvReceive.Rows)
            {
                var viewItem = row.DataBoundItem as PurchaseOrderDetailView;
                if (viewItem == null) continue;

                // --- 1. QUANTITY VALIDATION ---
                int orderedQty = Convert.ToInt32(row.Cells["OrderedQty"].Value);
                int receivedQty = 0;

                if (row.Cells["ReceivedQty"].Value != null)
                {
                    int.TryParse(row.Cells["ReceivedQty"].Value.ToString(), out receivedQty);
                }

                // Error: Negative
                if (receivedQty <= 0)
                {
                    DialogHelper.ShowCustomDialog("Invalid Quantity", $"Item '{viewItem.ProductName}' cannot have zero or negative quantity.", "error");
                    return;
                }

                // Warning: Over-Delivery (Received > Ordered)
                // We allow this (e.g., bonus items), but we should warn the user in case of a typo.
                if (receivedQty > orderedQty && !hasWarnings)
                {
                    var res = DialogHelper.ShowConfirmDialog("Over-Delivery Warning",
                        $"You are receiving {receivedQty} of '{viewItem.ProductName}', but you only ordered {orderedQty}.\n\nIs this correct?", "warning");

                    if (res == DialogResult.No) return;
                    hasWarnings = true; // Don't spam warnings for every line
                }

                // --- 2. DATE VALIDATION ---
                DateTime? expiry = null;
                if (row.Cells["ExpirationDate"].Value != null &&
                    DateTime.TryParse(row.Cells["ExpirationDate"].Value.ToString(), out DateTime parsedDate))
                {
                    expiry = parsedDate;

                    // Error: Date is in the Past
                    if (expiry < DateTime.Today)
                    {
                        DialogHelper.ShowCustomDialog("Invalid Expiration",
                            $"The item '{viewItem.ProductName}' has an expiration date in the past ({expiry:yyyy-MM-dd}).\nYou cannot receive expired goods.", "error");
                        return;
                    }
                }
                else
                {
                    
                    DialogHelper.ShowCustomDialog("Missing Date", $"Please set an expiration date for '{viewItem.ProductName}'.", "warning");
                    return;
                    
                }

                itemsToReceive.Add(new PurchaseOrderDetail
                {
                    ProductID = viewItem.ProductID,
                    Quantity = receivedQty,
                    ExpirationDate = expiry
                });
            }

            // Final Confirmation
            var result = DialogHelper.ShowConfirmDialog("Confirm Receive", "This will update your inventory stock. Proceed?", "question");
            if (result == DialogResult.Yes)
            {
                bool success = _poRepo.ReceivePurchaseOrder(_poID, itemsToReceive);
                if (success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}