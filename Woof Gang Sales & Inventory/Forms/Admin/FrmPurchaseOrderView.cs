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
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;
using Woof_Gang_Sales___Inventory.Helpers;
using Guna.UI2.WinForms;
using System.Drawing.Drawing2D;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmPurchaseOrderView : Form
    {
        private readonly PurchaseOrderRepository _poRepo;
        // We need ProductRepo for Auto-Restock
        private readonly ProductRepository _prodRepo;

        private string[] statusFilter = {
            "All Active", // Default
            "Pending",
            "Received",
            "Cancelled",
            "Archived",
            "Show All"
        };

        // ✅ --- ICONS ---
        // MAKE SURE you add a 'truck' or 'delivery' icon to your Resources!
        private Image editIcon = Properties.Resources.edit;
        private Image deleteIcon = Properties.Resources.delete;
        private Image receiveIcon = Properties.Resources.truck_solid_full; // 🚚 New Icon!

        // ✅ --- LAYOUT CONSTANTS ---
        // Increased width to fit 3 buttons
        private int btnWidth = 45;
        private int btnHeight = 35;
        private int btnSpacing = 5;
        private int iconSize = 20;

        public FrmPurchaseOrderView()
        {
            InitializeComponent();
            _poRepo = new PurchaseOrderRepository();
            _prodRepo = new ProductRepository();

            // 1. Load initial data
            ReadPurchaseOrders();
            ReadOrderDetails(0);

            // 2. Wire up Formatting
            dgvPurchaseOrders.CellFormatting += dgvPurchaseOrders_CellFormatting;
            dgvOrderDetails.CellFormatting += dgvOrderDetails_CellFormatting;

            // 3. Wire up Smart Actions
            dgvPurchaseOrders.CellPainting += dgvPurchaseOrders_CellPainting;
            dgvPurchaseOrders.CellMouseClick += dgvPurchaseOrders_CellMouseClick;
            dgvPurchaseOrders.CellMouseMove += dgvPurchaseOrders_CellMouseMove;
            dgvPurchaseOrders.CellMouseLeave += dgvPurchaseOrders_CellMouseLeave;

            dgvPurchaseOrders.SelectionChanged += dgvPurchaseOrders_SelectionChanged;
            txtSearch.TextChanged += txtSearch_TextChanged;
            cmbStatusFilter.SelectedIndexChanged += FilterChanged;

            // 4. Wire up Top Buttons (Manual & Auto)
            // btnCreatePO.Click += btnCreatePO_Click;
            // btnAutoRestock.Click += btnAutoRestock_Click;
        }

        private void FrmPurchaseOrderView_Load(object sender, EventArgs e)
        {
            SetupFilters();

            // ✅ --- ADD ACTIONS COLUMN ---
            DataGridViewButtonColumn actionCol = new DataGridViewButtonColumn();
            actionCol.Name = "Actions";
            actionCol.HeaderText = "Actions";
            actionCol.Text = "";
            actionCol.UseColumnTextForButtonValue = false;
            actionCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            actionCol.Resizable = DataGridViewTriState.False;

            // Width = (35 * 3) + (5 * 2) + padding ≈ 130-140
            actionCol.Width = 140;

            dgvPurchaseOrders.Columns.Add(actionCol);
        }

        private void SetupFilters()
        {
            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.AddRange(statusFilter);
            cmbStatusFilter.SelectedIndex = 0;
            txtSearch.PlaceholderText = "Search by PO ID or Supplier Name...";
        }

        // ✅ --- SMART PAINTING LOGIC ---
        private void dgvPurchaseOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvPurchaseOrders.Columns[e.ColumnIndex].Name == "Actions")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);

                // Get Status to decide what to draw
                string status = dgvPurchaseOrders.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                bool isPending = status == "Pending";

                // Calculate Center for 3 buttons
                int totalWidth = (btnWidth * 3) + (btnSpacing * 2);
                int startX = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int startY = e.CellBounds.Y + (e.CellBounds.Height - btnHeight) / 2;

                // --- SLOT 1: RECEIVE (Green) ---
                // Only show if Pending
                if (isPending)
                {
                    Rectangle receiveRect = new Rectangle(startX, startY, btnWidth, btnHeight);
                    DrawRoundedButton(e.Graphics, receiveRect, Color.FromArgb(25, 135, 84), receiveIcon); // Green
                }

                // --- SLOT 2: EDIT (Blue) ---
                // Only show if Pending
                if (isPending)
                {
                    Rectangle editRect = new Rectangle(startX + btnWidth + btnSpacing, startY, btnWidth, btnHeight);
                    DrawRoundedButton(e.Graphics, editRect, Color.FromArgb(94, 148, 255), editIcon); // Blue
                }

                // --- SLOT 3: CANCEL / ARCHIVE (Red) ---
                // Always visible, but acts differently
                Rectangle deleteRect = new Rectangle(startX + (btnWidth * 2) + (btnSpacing * 2), startY, btnWidth, btnHeight);
                DrawRoundedButton(e.Graphics, deleteRect, Color.FromArgb(220, 53, 69), deleteIcon); // Red

                e.Handled = true;
            }
        }

        private void DrawRoundedButton(Graphics g, Rectangle rect, Color color, Image icon)
        {
            using (GraphicsPath path = GetRoundedPath(rect, 6))
            using (SolidBrush brush = new SolidBrush(color))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillPath(brush, path);
            }
            if (icon != null)
            {
                g.DrawImage(icon, new Rectangle(
                    rect.X + (btnWidth - iconSize) / 2,
                    rect.Y + (btnHeight - iconSize) / 2,
                    iconSize, iconSize));
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        // ✅ --- SMART CLICK LOGIC ---
        private void dgvPurchaseOrders_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvPurchaseOrders.Columns[e.ColumnIndex].Name == "Actions")
            {
                int w = dgvPurchaseOrders.Columns[e.ColumnIndex].Width;
                int totalWidth = (btnWidth * 3) + (btnSpacing * 2);
                int startX = (w - totalWidth) / 2;

                int poID = Convert.ToInt32(dgvPurchaseOrders.Rows[e.RowIndex].Cells["POID"].Value);
                string status = dgvPurchaseOrders.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                bool isPending = status == "Pending";

                // --- CLICK: RECEIVE (Slot 1) ---
                if (isPending && e.X >= startX && e.X <= startX + btnWidth)
                {
                    // Open FrmReceivePO
                    // FrmReceivePO form = new FrmReceivePO(poID);
                    // if (form.ShowDialog() == DialogResult.OK) ReadPurchaseOrders();
                    MessageBox.Show($"Opening Receive PO for #{poID}");
                }

                // --- CLICK: EDIT (Slot 2) ---
                else if (isPending && e.X >= startX + btnWidth + btnSpacing && e.X <= startX + (btnWidth * 2) + btnSpacing)
                {
                    // Open Edit Form                                               // poID argument
                    FrmCreateEditPurchaseOrder form = new FrmCreateEditPurchaseOrder();
                    form.IsEditMode = true;
                    if (form.ShowDialog() == DialogResult.OK) ReadPurchaseOrders();
                }

                // --- CLICK: DELETE/ARCHIVE (Slot 3) ---
                else if (e.X >= startX + (btnWidth * 2) + (btnSpacing * 2) && e.X <= startX + (btnWidth * 3) + (btnSpacing * 2))
                {
                    if (status == "Pending")
                    {
                        // CANCEL
                        var result = DialogHelper.ShowConfirmDialog("Cancel Order", "Cancel this pending order?", "warning");
                        if (result == DialogResult.No) return;
                        if (_poRepo.UpdatePOStatus(poID, "Cancelled")) ReadPurchaseOrders();
                    }
                    else
                    {
                        // ARCHIVE
                        var result = DialogHelper.ShowConfirmDialog("Archive Order", "Archive this order? It will be hidden.", "warning");
                        if (result == DialogResult.No) return;
                        if (_poRepo.ArchivePurchaseOrder(poID)) ReadPurchaseOrders();
                    }
                }
            }
        }

        // ✅ --- SMART CURSOR LOGIC ---
        private void dgvPurchaseOrders_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvPurchaseOrders.Columns[e.ColumnIndex].Name == "Actions")
            {
                int w = dgvPurchaseOrders.Columns[e.ColumnIndex].Width;
                int h = dgvPurchaseOrders.Rows[e.RowIndex].Height;
                int totalWidth = (btnWidth * 3) + (btnSpacing * 2);
                int startX = (w - totalWidth) / 2;
                int startY = (h - btnHeight) / 2;

                // Check status so we know if buttons 1 & 2 exist
                string status = dgvPurchaseOrders.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                bool isPending = status == "Pending";

                bool overReceive = isPending && (e.X >= startX && e.X <= startX + btnWidth) && (e.Y >= startY && e.Y <= startY + btnHeight);
                bool overEdit = isPending && (e.X >= startX + btnWidth + btnSpacing && e.X <= startX + (btnWidth * 2) + btnSpacing) && (e.Y >= startY && e.Y <= startY + btnHeight);
                bool overDelete = (e.X >= startX + (btnWidth * 2) + (btnSpacing * 2) && e.X <= startX + (btnWidth * 3) + (btnSpacing * 2)) && (e.Y >= startY && e.Y <= startY + btnHeight);

                dgvPurchaseOrders.Cursor = (overReceive || overEdit || overDelete) ? Cursors.Hand : Cursors.Default;
            }
            else
            {
                dgvPurchaseOrders.Cursor = Cursors.Default;
            }
        }

        private void dgvPurchaseOrders_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvPurchaseOrders.Cursor = Cursors.Default;
        }


        // --- NEW TOP BUTTONS ---

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 1. Manual Creation
            FrmCreateEditPurchaseOrder form = new FrmCreateEditPurchaseOrder();
            // Default constructor = empty cart

            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadPurchaseOrders();
            }
        }

        private void btnAutoRestock_Click(object sender, EventArgs e)
        {
            // 2. Auto-Restock Logic
            // Check if any products are low stock first
            // (You need to add GetLowStockProducts to ProductRepository)

            /*
            var lowStockItems = _prodRepo.GetLowStockProducts(); 
            if (lowStockItems.Count == 0)
            {
                DialogHelper.ShowCustomDialog("Stock OK", "No products are currently low on stock.", "info");
                return;
            }

            // Pass these items to the form
            FrmCreateEditPurchaseOrder form = new FrmCreateEditPurchaseOrder();
            form.LoadAutoRestock(lowStockItems); // You'll need to create this method on the form
            
            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadPurchaseOrders();
            }
            */
            MessageBox.Show("Auto-restock feature coming soon! (Need to update ProductRepo)");
        }


        // --- Data Loading (Same as before) ---

        private void ReadPurchaseOrders()
        {
            if (cmbStatusFilter.SelectedItem == null) return;

            string search = txtSearch.Text.Trim();
            string status = cmbStatusFilter.SelectedItem.ToString();
            var poList = _poRepo.GetPurchaseOrders(search, status);

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
                dt.Rows.Add(po.POID, po.SupplierName, po.PODate, po.ReceivedDate.HasValue ? (object)po.ReceivedDate.Value : DBNull.Value, po.TotalCost ?? 0m, po.Status, po.Remarks, po.IsActive);
            }

            dgvPurchaseOrders.DataSource = dt;
            DataGridViewStyler.ApplyStyle(dgvPurchaseOrders, "POID");

            dgvPurchaseOrders.Columns["POID"].HeaderText = "PO ID";
            dgvPurchaseOrders.Columns["SupplierName"].HeaderText = "Supplier";
            dgvPurchaseOrders.Columns["SupplierName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvPurchaseOrders.Columns["PODate"].HeaderText = "Order Date";
            dgvPurchaseOrders.Columns["PODate"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dgvPurchaseOrders.Columns["ReceivedDate"].HeaderText = "Received On";
            dgvPurchaseOrders.Columns["ReceivedDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dgvPurchaseOrders.Columns["TotalCost"].HeaderText = "Total Cost";
            dgvPurchaseOrders.Columns["TotalCost"].DefaultCellStyle.Format = "C";
            dgvPurchaseOrders.Columns["Remarks"].Visible = false;
            dgvPurchaseOrders.Columns["IsActive"].Visible = false;
        }

        private void ReadOrderDetails(int poID)
        {
            DataTable dtDetails = new DataTable();
            dtDetails.Columns.Add("ProductName");
            dtDetails.Columns.Add("Quantity");
            dtDetails.Columns.Add("UnitCost", typeof(decimal));
            dtDetails.Columns.Add("Subtotal", typeof(decimal));

            if (poID > 0)
            {
                var detailsList = _poRepo.GetPurchaseOrderDetails(poID);
                foreach (var item in detailsList)
                {
                    dtDetails.Rows.Add(item.ProductName, item.Quantity, item.UnitCost, item.Subtotal);
                }
            }
            dgvOrderDetails.DataSource = dtDetails;
            DataGridViewStyler.ApplyStyle(dgvOrderDetails, "ProductName");
            dgvOrderDetails.Columns["ProductName"].HeaderText = "Product";
            dgvOrderDetails.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvOrderDetails.Columns["Quantity"].HeaderText = "Qty";
            dgvOrderDetails.Columns["UnitCost"].HeaderText = "Unit Cost";
            dgvOrderDetails.Columns["UnitCost"].DefaultCellStyle.Format = "N2";
            dgvOrderDetails.Columns["Subtotal"].HeaderText = "Subtotal";
            dgvOrderDetails.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
        }

        private void dgvPurchaseOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPurchaseOrders.SelectedRows.Count == 0)
            {
                ReadOrderDetails(0);
                lblDetails.Text = "Select an order to see details";
                txtRemarksView.Text = "";
                return;
            }

            DataRowView drv = dgvPurchaseOrders.SelectedRows[0].DataBoundItem as DataRowView;
            if (drv == null) return;

            int selectedPOID = Convert.ToInt32(drv["POID"]);
            lblDetails.Text = $"Details for Order #{selectedPOID}";
            txtRemarksView.Text = drv["Remarks"].ToString();

            ReadOrderDetails(selectedPOID);
        }

        // --- DataGrid Formatting ---
        private void dgvPurchaseOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvPurchaseOrders.Columns[e.ColumnIndex].Name == "Status")
            {
                string value = e.Value?.ToString() ?? "";
                if (value.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(255, 128, 0);
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
                if (e.Value == null || e.Value == DBNull.Value) e.Value = "N/A";
            }
        }

        private void dgvOrderDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Formatting logic for details
        }

        private void FilterChanged(object sender, EventArgs e) => ReadPurchaseOrders();
        private void txtSearch_TextChanged(object sender, EventArgs e) => ReadPurchaseOrders();
    }
}