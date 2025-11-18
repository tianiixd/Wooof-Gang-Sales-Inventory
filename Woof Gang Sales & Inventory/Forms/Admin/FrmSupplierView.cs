using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
// Make sure to add these using statements to match your sample
using Woof_Gang_Sales___Inventory.Helpers;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmSupplierView : Form
    {
        private SupplierRepository supplierRepo = new SupplierRepository();

        TimeClockHelper time = new TimeClockHelper(); 

        // Changed to use the supplier status array
        private string[] supplierStatus = { "Active Suppliers", "Archived Suppliers", "All Suppliers" };

        // Icons
        private Image editIcon = Properties.Resources.edit2;
        private Image deleteIcon = Properties.Resources.delete2;


        // ✅ --- CONSTANTS FOR LAYOUT ---
        // We define these here so Paint, Click, and MouseMove all align perfectly.
        private int btnWidth = 40;
        private int btnHeight = 35;
        private int btnSpacing = 10;
        private int iconSize = 20;

        private int hoveredRowIndex = -1;
        private string hoveredButton = ""; // Values: "Edit", "Delete", or ""
        public FrmSupplierView()
        {
            InitializeComponent();

            // Renamed dgvCategory to dgvSupplier (assuming this is your control's name)
            dgvSupplier.CellMouseClick += dgvSupplier_CellMouseClick;
            dgvSupplier.CellPainting += dgvSupplier_CellPainting;

            // ✅ --- NEW: Smart Cursor Logic ---
            dgvSupplier.CellMouseMove += dgvSupplier_CellMouseMove;
            dgvSupplier.CellMouseLeave += dgvSupplier_CellMouseLeave;
            dgvSupplier.CellFormatting += (s, e) =>
            {
                // Renamed dgvCategory to dgvSupplier
                if (dgvSupplier.Columns[e.ColumnIndex].Name == "Status")
                {
                    string value = e.Value?.ToString() ?? "";

                    if (value == "Yes" || value.Equals("Activated", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.SelectionForeColor = Color.Green;
                        e.CellStyle.Font = new Font("Segoe UI", 10F);
                    }
                    else if (value == "No" || value.Equals("Archived", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.SelectionForeColor = Color.Red;
                        e.CellStyle.Font = new Font("Segoe UI", 10F);
                    }
                }
            };
            // Changed to call ReadSupplier()
            ReadSupplier();
            cmbFilterStatus.SelectedIndexChanged += FilterChanged;

            // Added this based on your sample
            txtSearch.TextChanged += txtSearch_TextChanged;
        }

        // This is the FrmSupplierView_Load event, based on your sample
        private void FrmSupplierView_Load(object sender, EventArgs e)
        {
            time.StartClock(lblTime, lblDate);

            foreach (var status in supplierStatus)
            {
                cmbFilterStatus.Items.Add(status);
            }
            cmbFilterStatus.SelectedIndex = 0;

            DataGridViewButtonColumn actionCol = new DataGridViewButtonColumn();
            actionCol.Name = "Actions";
            actionCol.HeaderText = "Actions";
            actionCol.Text = "";
            actionCol.UseColumnTextForButtonValue = false;

            // ✅ --- FIX: STRICT WIDTH CONTROL ---
            actionCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            actionCol.Resizable = DataGridViewTriState.False; // Prevent resizing
            actionCol.Width = 150; // Fixed width

            dgvSupplier.Columns.Add(actionCol);

        }

        private void dgvSupplier_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvSupplier.Columns[e.ColumnIndex].Name == "Actions")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);

                // Calculate Center
                int totalWidth = (btnWidth * 2) + btnSpacing;
                int startX = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int startY = e.CellBounds.Y + (e.CellBounds.Height - btnHeight) / 2;

                Color editColor = Color.FromArgb(47, 128, 237);    // Normal Blue
                Color deleteColor = Color.FromArgb(235, 87, 87);

                if (e.RowIndex == hoveredRowIndex)
                {
                    if (hoveredButton == "Edit")
                        editColor = Color.FromArgb(87, 158, 255); // Lighter Blue

                    if (hoveredButton == "Delete")
                        deleteColor = Color.FromArgb(255, 117, 117); // Lighter Red
                }

                // Draw Edit (Blue)
                Rectangle editRect = new Rectangle(startX, startY, btnWidth, btnHeight);
                DrawRoundedButton(e.Graphics, editRect, editColor, editIcon);

                // Draw Delete (Red)
                Rectangle deleteRect = new Rectangle(startX + btnWidth + btnSpacing, startY, btnWidth, btnHeight);
                DrawRoundedButton(e.Graphics, deleteRect, deleteColor, deleteIcon);

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

        // ✅ --- CLICK LOGIC ---
        private void dgvSupplier_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvSupplier.Columns[e.ColumnIndex].Name == "Actions")
            {
                // Calculate positions
                int w = dgvSupplier.Columns[e.ColumnIndex].Width;
                int totalWidth = (btnWidth * 2) + btnSpacing;
                int startX = (w - totalWidth) / 2;

                int supplierID = Convert.ToInt32(dgvSupplier.Rows[e.RowIndex].Cells["SupplierID"].Value);

                // Check Edit Click (Blue Area)
                if (e.X >= startX && e.X <= startX + btnWidth)
                {
                    var supplier = supplierRepo.GetSupplierById(supplierID);
                    if (supplier == null) return;

                    FrmCreateEditSupplier form = new FrmCreateEditSupplier();
                    form.IsEditMode = true;
                    form.EditSupplier(supplier);
                    if (form.ShowDialog() == DialogResult.OK) ReadSupplier();
                }
                // Check Delete Click (Red Area)
                else if (e.X >= startX + btnWidth + btnSpacing && e.X <= startX + (btnWidth * 2) + btnSpacing)
                {
                    DialogResult result = DialogHelper.ShowConfirmDialog("Archive Supplier", "Are you sure you want to archive this supplier?", "warning");
                    if (result == DialogResult.No) return;

                    bool success = supplierRepo.DeleteSupplier(supplierID);
                    if (success) ReadSupplier();
                }
            }
        }

        // ✅ --- SMART CURSOR LOGIC ---
        private void dgvSupplier_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvSupplier.Columns[e.ColumnIndex].Name == "Actions")
            {
                // Recalculate positions relative to the cell
                int w = dgvSupplier.Columns[e.ColumnIndex].Width;
                int totalWidth = (btnWidth * 2) + btnSpacing;
                int startX = (w - totalWidth) / 2;

                string newHoveredButton = "";

                // Check if mouse is over Edit OR Delete button
                if (e.X >= startX && e.X <= startX + btnWidth)
                {
                    newHoveredButton = "Edit";
                    dgvSupplier.Cursor = Cursors.Hand;
                }
                else if (e.X >= startX + btnWidth + btnSpacing && e.X <= startX + (btnWidth * 2) + btnSpacing)
                {
                    newHoveredButton = "Delete";
                    dgvSupplier.Cursor = Cursors.Hand;
                }
                else
                {
                    dgvSupplier.Cursor = Cursors.Default;
                }

                // ✅ OPTIMIZATION: Only repaint if the state has actually changed
                // This prevents the grid from flickering wildly while moving the mouse
                if (hoveredRowIndex != e.RowIndex || hoveredButton != newHoveredButton)
                {
                    hoveredRowIndex = e.RowIndex;
                    hoveredButton = newHoveredButton;
                    dgvSupplier.InvalidateCell(e.ColumnIndex, e.RowIndex); // Trigger CellPainting
                }
            }
            else
            {
                dgvSupplier.Cursor = Cursors.Default;
                // Reset if we moved to a different column
                if (hoveredRowIndex != -1)
                {
                    hoveredRowIndex = -1;
                    hoveredButton = "";
                    dgvSupplier.InvalidateRow(e.RowIndex);
                }
            }
        }
        private void dgvSupplier_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvSupplier.Cursor = Cursors.Default;

            if (hoveredRowIndex != -1)
            {
                // Clear the hover state and redraw the row to remove the glow
                int rowToInvalidate = hoveredRowIndex;
                hoveredRowIndex = -1;
                hoveredButton = "";
                if (rowToInvalidate >= 0 && rowToInvalidate < dgvSupplier.Rows.Count)
                    dgvSupplier.InvalidateRow(rowToInvalidate);
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

        // Added btnAdd_Click based on your sample
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Assumes you have a FrmCreateEditSupplier form
            FrmCreateEditSupplier form = new FrmCreateEditSupplier();
            form.IsEditMode = false;

            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadSupplier();
            }
        }

        // Added btnEdit_Click based on your sample
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSupplier.SelectedRows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Selection", "Please select a supplier first.", "warning");
                return;
            }

            var value = this.dgvSupplier.SelectedRows[0].Cells[0].Value.ToString();

            if (string.IsNullOrEmpty(value)) return;

            if (!int.TryParse(value, out int supplierID))
            {
                DialogHelper.ShowCustomDialog("Invalid Selection", "The selected supplier ID is invalid.", "error");
                return;
            }

            // Use the Supplier repository
            var supplier = supplierRepo.GetSupplierById(supplierID);

            if (supplier == null)
            {
                DialogHelper.ShowCustomDialog("Not Found", "The selected supplier no longer exists.", "error");
                ReadSupplier();
                return;
            }

            // Assumes you have a FrmCreateEditSupplier form
            FrmCreateEditSupplier form = new FrmCreateEditSupplier();
            form.IsEditMode = true;
            // Assumes your edit form has this method
            form.EditSupplier(supplier);

            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadSupplier();
            }
        }

        // Added btnDelete_Click based on your sample
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSupplier.SelectedRows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Selection", "Please select a supplier first.", "warning");
                return;
            }

            var value = this.dgvSupplier.SelectedRows[0].Cells[0].Value.ToString();

            if (string.IsNullOrEmpty(value)) return;

            int supplierID = int.Parse(value);

            // Updated confirmation dialog text
            DialogResult result = DialogHelper.ShowConfirmDialog("Archive Supplier", "Are you sure you want to archive this supplier?", "warning");

            if (result == DialogResult.No) return;

            // Use the Supplier repository
            bool success = supplierRepo.DeleteSupplier(supplierID);
            if (success)
                ReadSupplier();
        }


        // This is the completed ReadSupplier method, based on your ReadCategory
        public void ReadSupplier()
        {
            string searchText = txtSearch.Text.Trim();
            string selectedStatus = cmbFilterStatus.SelectedItem?.ToString() ?? "All Suppliers";

            List<Supplier> suppliers;

            if (selectedStatus == "Active Suppliers")
            {
                // This method already filters by search in the DB
                suppliers = supplierRepo.GetSuppliers(searchText);
            }
            else if (selectedStatus == "Archived Suppliers")
            {
                // This method gets all inactive, so we filter in C# (LINQ)
                suppliers = supplierRepo.GetAllInactiveSuppliers();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    string searchLower = searchText.ToLower();
                    // Match the search logic from GetActiveSuppliers (Name, Contact, Email)
                    suppliers = suppliers
                        .Where(s => (s.SupplierName != null && s.SupplierName.ToLower().Contains(searchLower)) ||
                                    (s.ContactPerson != null && s.ContactPerson.ToLower().Contains(searchLower)) ||
                                    (s.Email != null && s.Email.ToLower().Contains(searchLower)))
                        .ToList();
                }
            }
            else // "All Suppliers"
            {
                // Active list is already filtered by search (from DB)
                var active = supplierRepo.GetSuppliers(searchText);

                // Inactive list is not, so we filter in C# (LINQ)
                var inactive = supplierRepo.GetAllInactiveSuppliers();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    string searchLower = searchText.ToLower();
                    inactive = inactive
                         .Where(s => (s.SupplierName != null && s.SupplierName.ToLower().Contains(searchLower)) ||
                                     (s.ContactPerson != null && s.ContactPerson.ToLower().Contains(searchLower)) ||
                                     (s.Email != null && s.Email.ToLower().Contains(searchLower)))
                        .ToList();
                }

                suppliers = active.Concat(inactive).ToList();
            }

            DataTable dt = new DataTable();
            // Add Supplier columns to the DataTable
            dt.Columns.Add("SupplierID");
            dt.Columns.Add("SupplierName");
            dt.Columns.Add("ContactPerson");
            dt.Columns.Add("PhoneNumber");
            dt.Columns.Add("Email");
            dt.Columns.Add("Address"); // Added Address
            dt.Columns.Add("Status");

            foreach (var supplier in suppliers)
            {
                var row = dt.NewRow();
                row["SupplierID"] = supplier.SupplierID;
                row["SupplierName"] = supplier.SupplierName;
                row["ContactPerson"] = supplier.ContactPerson;
                row["PhoneNumber"] = supplier.PhoneNumber;
                row["Email"] = supplier.Email;
                row["Address"] = supplier.Address;
                row["Status"] = supplier.IsActive ? "Activated" : "Archived";
                dt.Rows.Add(row);
            }

            dgvSupplier.DataSource = dt;
            // Assumes you have a DataGridViewStyler class
            DataGridViewStyler.ApplyStyle(dgvSupplier, "SupplierID");

            dgvSupplier.Columns["SupplierID"].Visible = false;
            // This second loop for styling also matches your sample
            foreach (DataGridViewRow row in dgvSupplier.Rows)
            {
                string status = row.Cells["Status"].Value?.ToString() ?? "";
                if (status == "Activated")
                {
                    row.Cells["Status"].Style.ForeColor = Color.Green;
                    row.Cells["Status"].Style.Font = new Font(dgvSupplier.Font, FontStyle.Bold);
                }
                else if (status == "Archived")
                {
                    row.Cells["Status"].Style.ForeColor = Color.Red;
                    row.Cells["Status"].Style.Font = new Font(dgvSupplier.Font, FontStyle.Bold);
                }
            }

            if (selectedStatus == "Active Suppliers" || selectedStatus == "Archived Suppliers")
            {
                dgvSupplier.Columns["Status"].Visible = false;
            }
            else
            {
                dgvSupplier.Columns["Status"].Visible = true;
            }
        }

        // Event handler for the filter ComboBox
        private void FilterChanged(object sender, EventArgs e)
        {
            ReadSupplier();
        }

        // Event handler for the search TextBox
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ReadSupplier();
        }
    }
}
