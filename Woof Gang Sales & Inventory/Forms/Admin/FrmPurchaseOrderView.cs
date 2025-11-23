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

        TimeClockHelper time = new TimeClockHelper();

        private string[] statusFilter = {
            "Pending",
            "Received",
            "Cancelled",
            "All Archived",
            "All Active",
            "Show All"
        };

        // ✅ --- ICONS ---
        // MAKE SURE you add a 'truck' or 'delivery' icon to your Resources!
        private Image editIcon = Properties.Resources.edit;
        private Image deleteIcon = Properties.Resources.delete;
        private Image archiveIcon = Properties.Resources.close1;
        private Image receiveIcon = Properties.Resources.truck_solid_full; // 🚚 New Icon!

        // ✅ --- LAYOUT CONSTANTS ---
        // Increased width to fit 3 buttons
        private int btnWidth = 45;
        private int btnHeight = 35;
        private int btnSpacing = 5;
        private int iconSize = 20;

        private int hoveredRowIndex = -1;
        private int hoveredButtonIndex = 0;

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
            time.StartClock(lblTime, lblDateClock);
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
            actionCol.Width = 150;

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

                string status = dgvPurchaseOrders.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                bool isPending = status == "Pending";

                // ✅ LOGIC CHANGE: Calculate width based on visible buttons
                int buttonsToDraw = isPending ? 3 : 1;

                // Calculate the total width of the "Content"
                int totalContentWidth = (btnWidth * buttonsToDraw) + (btnSpacing * (buttonsToDraw - 1));

                // Calculate StartX to center the content in the current column width
                int startX = e.CellBounds.X + (e.CellBounds.Width - totalContentWidth) / 2;
                int startY = e.CellBounds.Y + (e.CellBounds.Height - btnHeight) / 2;

                bool isRowHovered = (e.RowIndex == hoveredRowIndex);

                // --- PENDING STATUS: Draw 3 Buttons ---
                if (isPending)
                {
                    // 1. Receive
                    Rectangle receiveRect = new Rectangle(startX, startY, btnWidth, btnHeight);
                    Color recColor = (isRowHovered && hoveredButtonIndex == 1) ? Color.FromArgb(35, 160, 100) : Color.FromArgb(25, 135, 84);
                    DrawRoundedButton(e.Graphics, receiveRect, recColor, receiveIcon);

                    // 2. Edit (Shift X by 1 button + spacing)
                    Rectangle editRect = new Rectangle(startX + btnWidth + btnSpacing, startY, btnWidth, btnHeight);
                    Color editColor = (isRowHovered && hoveredButtonIndex == 2) ? Color.FromArgb(130, 170, 255) : Color.FromArgb(94, 148, 255);
                    DrawRoundedButton(e.Graphics, editRect, editColor, editIcon);

                    // 3. Cancel (Shift X by 2 buttons + spacing) 
                    Rectangle deleteRect = new Rectangle(startX + (btnWidth * 2) + (btnSpacing * 2), startY, btnWidth, btnHeight);
                    Color delColor = (isRowHovered && hoveredButtonIndex == 3) ? Color.FromArgb(255, 100, 100) : Color.FromArgb(230, 230, 230);
                    DrawRoundedButton(e.Graphics, deleteRect, delColor, archiveIcon);
                }
                // --- OTHER STATUS: Draw 1 Button Centered ---
                else
                {
                    // 1. Delete/Archive (Centered because startX is calculated for 1 button)
                    Rectangle deleteRect = new Rectangle(startX, startY, btnWidth, btnHeight);
                    Color delColor = (isRowHovered && hoveredButtonIndex == 3) ? Color.FromArgb(255, 100, 100) : Color.FromArgb(230, 230, 230);
                    DrawRoundedButton(e.Graphics, deleteRect, delColor, deleteIcon);
                }

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
                // 1. Get Data
                int poID = Convert.ToInt32(dgvPurchaseOrders.Rows[e.RowIndex].Cells["POID"].Value);
                string status = dgvPurchaseOrders.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                bool isPending = status == "Pending";

                // 2. Calculate Geometry
                // We must calculate StartX exactly the same way we draw it!
                int buttonsToDraw = isPending ? 3 : 1;
                int totalContentWidth = (btnWidth * buttonsToDraw) + (btnSpacing * (buttonsToDraw - 1));

                int w = dgvPurchaseOrders.Columns[e.ColumnIndex].Width;
                int startX = (w - totalContentWidth) / 2;

                // 3. PENDING LOGIC (3 Buttons)
                if (isPending)
                {
                    // Slot 1: Receive
                    if (e.X >= startX && e.X <= startX + btnWidth)
                    {
                        FrmReceivePO form = new FrmReceivePO(poID);
                        if (form.ShowDialog() == DialogResult.OK) ReadPurchaseOrders();
                        return;
                    }

                    // Slot 2: Edit
                    int editX = startX + btnWidth + btnSpacing;
                    if (e.X >= editX && e.X <= editX + btnWidth)
                    {
                        FrmCreateEditPurchaseOrder form = new FrmCreateEditPurchaseOrder(poID);
                        form.IsEditMode = true;
                        if (form.ShowDialog() == DialogResult.OK) ReadPurchaseOrders();
                        return;
                    }

                    // Slot 3: Cancel
                    int deleteX = startX + (btnWidth * 2) + (btnSpacing * 2);
                    if (e.X >= deleteX && e.X <= deleteX + btnWidth)
                    {
                        if (DialogHelper.ShowConfirmDialog("Cancel Order", "Cancel this pending order?", "warning") == DialogResult.Yes)
                            if (_poRepo.UpdatePOStatus(poID, "Cancelled")) ReadPurchaseOrders();
                        return;
                    }
                }
                // 4. RECEIVED / ARCHIVED LOGIC (1 Button Centered)
                else
                {
                    // ✅ FIX: The button is at 'startX' (Center), NOT calculated as Slot 3
                    if (e.X >= startX && e.X <= startX + btnWidth)
                    {
                        // ARCHIVE (Since it's already processed)
                        if (DialogHelper.ShowConfirmDialog("Archive Order", "Archive this order? It will be hidden from the active list.", "warning") == DialogResult.Yes)
                        {
                            if (_poRepo.ArchivePurchaseOrder(poID)) ReadPurchaseOrders();
                        }
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
                string status = dgvPurchaseOrders.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                bool isPending = status == "Pending";

                // ✅ MATCH THE PAINTING MATH EXACTLY
                int buttonsToDraw = isPending ? 3 : 1;
                int totalContentWidth = (btnWidth * buttonsToDraw) + (btnSpacing * (buttonsToDraw - 1));
                int startX = (dgvPurchaseOrders.Columns[e.ColumnIndex].Width - totalContentWidth) / 2;
                int startY = (dgvPurchaseOrders.Rows[e.RowIndex].Height - btnHeight) / 2;

                int newHoveredButton = 0;

                if (isPending)
                {
                    // Slot 1: Receive
                    if ((e.X >= startX && e.X <= startX + btnWidth) && (e.Y >= startY && e.Y <= startY + btnHeight))
                        newHoveredButton = 1;
                    // Slot 2: Edit
                    else if ((e.X >= startX + btnWidth + btnSpacing && e.X <= startX + (btnWidth * 2) + btnSpacing) && (e.Y >= startY && e.Y <= startY + btnHeight))
                        newHoveredButton = 2;
                    // Slot 3: Delete
                    else if ((e.X >= startX + (btnWidth * 2) + (btnSpacing * 2) && e.X <= startX + (btnWidth * 3) + (btnSpacing * 2)) && (e.Y >= startY && e.Y <= startY + btnHeight))
                        newHoveredButton = 3;
                }
                else
                {
                    // Only 1 Slot: Delete (Centered)
                    if ((e.X >= startX && e.X <= startX + btnWidth) && (e.Y >= startY && e.Y <= startY + btnHeight))
                        newHoveredButton = 3; // We keep ID 3 for consistency with the click logic
                }

                dgvPurchaseOrders.Cursor = (newHoveredButton > 0) ? Cursors.Hand : Cursors.Default;

                if (hoveredRowIndex != e.RowIndex || hoveredButtonIndex != newHoveredButton)
                {
                    hoveredRowIndex = e.RowIndex;
                    hoveredButtonIndex = newHoveredButton;
                    dgvPurchaseOrders.InvalidateCell(e.ColumnIndex, e.RowIndex);
                }
            }
            else
            {
                dgvPurchaseOrders.Cursor = Cursors.Default;
            }
        }

        private void dgvPurchaseOrders_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvPurchaseOrders.Cursor = Cursors.Default;

            // Reset hover state
            hoveredRowIndex = -1;
            hoveredButtonIndex = 0;

            // Repaint the cell to remove the effect
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgvPurchaseOrders.InvalidateCell(e.ColumnIndex, e.RowIndex);
            }
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
            var lowStockItems = _prodRepo.GetLowStockProducts();
            if (lowStockItems.Count == 0)
            {
                DialogHelper.ShowCustomDialog("Stock OK", "No products are currently below reorder level.", "info");
                return;
            }
            FrmCreateEditPurchaseOrder form = new FrmCreateEditPurchaseOrder();
            form.LoadAutoRestockItems(lowStockItems);
            if (form.ShowDialog() == DialogResult.OK) ReadPurchaseOrders();
        }


        // --- Data Loading (Same as before) ---

        private void ReadPurchaseOrders()
        {
            if (cmbStatusFilter.SelectedItem == null) return;

            string search = txtSearch.Text.Trim();
            string status = cmbStatusFilter.SelectedItem.ToString();



            bool needsWideColumn = (status == "Pending" || status == "All Active" || status == "Show All");

            // 2. Apply the width
            if (dgvPurchaseOrders.Columns.Contains("Actions"))
            {
                // 160 for 3 buttons, 70 for 1 button
                dgvPurchaseOrders.Columns["Actions"].Width = needsWideColumn ? 160 : 100;
            }



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
            dgvPurchaseOrders.RowTemplate.Height = 45;
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
            dgvOrderDetails.Columns["UnitCost"].DefaultCellStyle.Format = "C";
            dgvOrderDetails.Columns["Subtotal"].HeaderText = "Subtotal";
            dgvOrderDetails.Columns["Subtotal"].DefaultCellStyle.Format = "C";
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

            // ✅ LOGIC FOR STATUS COLUMN
            if (dgvPurchaseOrders.Columns[e.ColumnIndex].Name == "Status")
            {
                // 1. Get the DataRow to check "IsActive" (which might be hidden)
                DataRowView row = dgvPurchaseOrders.Rows[e.RowIndex].DataBoundItem as DataRowView;

                if (row != null)
                {
                    bool isActive = Convert.ToBoolean(row["IsActive"]);
                    string currentStatus = e.Value?.ToString() ?? "";

                    // 2. IF ARCHIVED (IsActive == 0)
                    if (!isActive)
                    {
                        // Change Text: "Archived - Cancelled" or "Archived - Received"
                        e.Value = $"Archived - {currentStatus}";

                        // Change Color: Gray (Visual cue that it's inactive)
                        e.CellStyle.ForeColor = Color.Gray;
                        e.CellStyle.SelectionForeColor = Color.Gray;
                        e.FormattingApplied = true; // Tell Grid we handled it
                        return;
                    }

                    // 3. IF ACTIVE (Normal Colors)
                    if (currentStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(255, 128, 0); // Orange
                        e.CellStyle.SelectionForeColor = Color.FromArgb(255, 128, 0);
                    }
                    else if (currentStatus.Equals("Received", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.SelectionForeColor = Color.Green;
                    }
                    else if (currentStatus.Equals("Cancelled", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.SelectionForeColor = Color.Red;
                    }
                }
            }

            // ✅ LOGIC FOR DATE COLUMN
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