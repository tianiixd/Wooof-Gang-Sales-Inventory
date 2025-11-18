using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Helpers;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmSubCategoryView : Form
    {
        private readonly SubCategoryRepository subCategoryRepo = new SubCategoryRepository();

        TimeClockHelper time = new TimeClockHelper();

        private string[] subCategoryStatus = { "Active Subcategories", "Archived Subcategories", "All Subcategories" };

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

        public FrmSubCategoryView()
        {
            InitializeComponent();

            dgvSubCategory.CellMouseClick += dgvSubCategory_CellMouseClick;
            dgvSubCategory.CellPainting += dgvSubCategory_CellPainting;

            // ✅ --- NEW: Smart Cursor Logic ---
            dgvSubCategory.CellMouseMove += dgvSubCategory_CellMouseMove;
            dgvSubCategory.CellMouseLeave += dgvSubCategory_CellMouseLeave;

            // ✅ Apply same visual style logic as FrmCategoryView
            dgvSubCategory.CellFormatting += (s, e) =>
            {
                if (dgvSubCategory.Columns[e.ColumnIndex].Name == "Status")
                {
                    string value = e.Value?.ToString() ?? "";

                    if (value.Equals("Activated", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.SelectionForeColor = Color.Green;
                        e.CellStyle.Font = new Font("Segoe UI", 10F);
                    }
                    else if (value.Equals("Archived", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.SelectionForeColor = Color.Red;
                        e.CellStyle.Font = new Font("Segoe UI", 10F);
                    }
                }
            };

            ReadSubCategories();
            cmbFilterStatus.SelectedIndexChanged += FilterChanged;
        }

        private void FrmSubCategoryView_Load(object sender, EventArgs e)
        {
            time.StartClock(lblTime, lblDate);
            foreach (var status in subCategoryStatus)
                cmbFilterStatus.Items.Add(status);

            cmbFilterStatus.SelectedIndex = 0;

            // ✅ --- ADD ACTIONS COLUMN ---
            DataGridViewButtonColumn actionCol = new DataGridViewButtonColumn();
            actionCol.Name = "Actions";
            actionCol.HeaderText = "Actions";
            actionCol.Text = "";
            actionCol.UseColumnTextForButtonValue = false;

            // ✅ --- FIX: STRICT WIDTH CONTROL ---
            actionCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            actionCol.Resizable = DataGridViewTriState.False; // Prevent resizing
            actionCol.Width = 150; // Fixed width

            dgvSubCategory.Columns.Add(actionCol);
        }

        // ✅ --- PAINTING ---
        private void dgvSubCategory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvSubCategory.Columns[e.ColumnIndex].Name == "Actions")
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
        private void dgvSubCategory_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvSubCategory.Columns[e.ColumnIndex].Name == "Actions")
            {
                // Calculate positions
                int w = dgvSubCategory.Columns[e.ColumnIndex].Width;
                int totalWidth = (btnWidth * 2) + btnSpacing;
                int startX = (w - totalWidth) / 2;

                int subCategoryID = Convert.ToInt32(dgvSubCategory.Rows[e.RowIndex].Cells["SubCategoryID"].Value);

                // Check Edit Click (Blue Area)
                if (e.X >= startX && e.X <= startX + btnWidth)
                {
                    var subCategory = subCategoryRepo.GetSubCategoryById(subCategoryID);
                    if (subCategory == null) return;

                    FrmCreateEditSubCategory form = new FrmCreateEditSubCategory();
                    form.IsEditMode = true;
                    form.EditSubCategory(subCategory);
                    if (form.ShowDialog() == DialogResult.OK) ReadSubCategories();
                }
                // Check Delete Click (Red Area)
                else if (e.X >= startX + btnWidth + btnSpacing && e.X <= startX + (btnWidth * 2) + btnSpacing)
                {
                    DialogResult result = DialogHelper.ShowConfirmDialog("Archive Subcategory", "Are you sure you want to archive this subcategory?", "warning");
                    if (result == DialogResult.No) return;

                    bool success = subCategoryRepo.DeleteSubCategory(subCategoryID);
                    if (success) ReadSubCategories();
                }
            }
        }

        // ✅ --- SMART CURSOR LOGIC ---
        private void dgvSubCategory_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvSubCategory.Columns[e.ColumnIndex].Name == "Actions")
            {
                // Recalculate positions relative to the cell
                int w = dgvSubCategory.Columns[e.ColumnIndex].Width;
                int totalWidth = (btnWidth * 2) + btnSpacing;
                int startX = (w - totalWidth) / 2;

                string newHoveredButton = "";

                // Check if mouse is over Edit OR Delete button
                if (e.X >= startX && e.X <= startX + btnWidth)
                {
                    newHoveredButton = "Edit";
                    dgvSubCategory.Cursor = Cursors.Hand;
                }
                else if (e.X >= startX + btnWidth + btnSpacing && e.X <= startX + (btnWidth * 2) + btnSpacing)
                {
                    newHoveredButton = "Delete";
                    dgvSubCategory.Cursor = Cursors.Hand;
                }
                else
                {
                    dgvSubCategory.Cursor = Cursors.Default;
                }

                // ✅ OPTIMIZATION: Only repaint if the state has actually changed
                // This prevents the grid from flickering wildly while moving the mouse
                if (hoveredRowIndex != e.RowIndex || hoveredButton != newHoveredButton)
                {
                    hoveredRowIndex = e.RowIndex;
                    hoveredButton = newHoveredButton;
                    dgvSubCategory.InvalidateCell(e.ColumnIndex, e.RowIndex); // Trigger CellPainting
                }
            }
            else
            {
                dgvSubCategory.Cursor = Cursors.Default;
                // Reset if we moved to a different column
                if (hoveredRowIndex != -1)
                {
                    hoveredRowIndex = -1;
                    hoveredButton = "";
                    dgvSubCategory.InvalidateRow(e.RowIndex);
                }
            }
        }
        private void dgvSubCategory_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvSubCategory.Cursor = Cursors.Default;

            if (hoveredRowIndex != -1)
            {
                // Clear the hover state and redraw the row to remove the glow
                int rowToInvalidate = hoveredRowIndex;
                hoveredRowIndex = -1;
                hoveredButton = "";
                if (rowToInvalidate >= 0 && rowToInvalidate < dgvSubCategory.Rows.Count)
                    dgvSubCategory.InvalidateRow(rowToInvalidate);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmCreateEditSubCategory form = new FrmCreateEditSubCategory();
            form.IsEditMode = false;

            if (form.ShowDialog() == DialogResult.OK)
                ReadSubCategories();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSubCategory.SelectedRows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Selection", "Please select a subcategory first.", "warning");
                return;
            }

            var value = dgvSubCategory.SelectedRows[0].Cells["SubCategoryID"].Value?.ToString();
            if (string.IsNullOrEmpty(value)) return;

            if (!int.TryParse(value, out int subCategoryID))
            {
                DialogHelper.ShowCustomDialog("Invalid Selection", "The selected subcategory ID is invalid.", "error");
                return;
            }

            var subCategory = subCategoryRepo.GetSubCategoryById(subCategoryID);
            if (subCategory == null)
            {
                DialogHelper.ShowCustomDialog("Not Found", "The selected subcategory no longer exists.", "error");
                ReadSubCategories();
                return;
            }

            FrmCreateEditSubCategory form = new FrmCreateEditSubCategory
            {
                IsEditMode = true
            };
            form.EditSubCategory(subCategory);

            if (form.ShowDialog() == DialogResult.OK)
                ReadSubCategories();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSubCategory.SelectedRows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Selection", "Please select a subcategory first.", "warning");
                return;
            }

            var value = dgvSubCategory.SelectedRows[0].Cells["SubCategoryID"].Value?.ToString();
            if (string.IsNullOrEmpty(value)) return;

            int subCategoryID = int.Parse(value);

            DialogResult result = DialogHelper.ShowConfirmDialog("Archive Subcategory", "Are you sure you want to archive this subcategory?", "warning");
            if (result == DialogResult.No) return;

            bool success = subCategoryRepo.DeleteSubCategory(subCategoryID);
            if (success)
                ReadSubCategories();
        }

        public void ReadSubCategories()
        {
            string searchText = txtSearch.Text.Trim();
            string selectedStatus = cmbFilterStatus.SelectedItem?.ToString() ?? "All Subcategories";

            List<SubCategory> subCategories;

            if (selectedStatus == "Active Subcategories")
            {
                subCategories = subCategoryRepo.GetSubCategories(searchText);
            }
            else if (selectedStatus == "Archived Subcategories")
            {
                subCategories = subCategoryRepo.GetAllInactiveSubCategories();

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    string searchLower = searchText.ToLower();
                    subCategories = subCategories
                        .Where(sc => sc.SubCategoryName.ToLower().Contains(searchLower)
                                  || sc.CategoryName.ToLower().Contains(searchLower))
                        .ToList();
                }
            }
            else
            {
                var active = subCategoryRepo.GetSubCategories(searchText);
                var inactive = subCategoryRepo.GetAllInactiveSubCategories();

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    string searchLower = searchText.ToLower();
                    inactive = inactive
                        .Where(sc => sc.SubCategoryName.ToLower().Contains(searchLower)
                                  || sc.CategoryName.ToLower().Contains(searchLower))
                        .ToList();
                }

                subCategories = active.Concat(inactive).ToList();
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("SubCategoryID");
            dt.Columns.Add("CategoryID");
            dt.Columns.Add("SubCategoryName");
            dt.Columns.Add("CategoryName");
            dt.Columns.Add("Status");

            foreach (var sc in subCategories)
            {
                var row = dt.NewRow();
                row["SubCategoryID"] = sc.SubCategoryID;
                row["CategoryID"] = sc.CategoryID;
                row["SubCategoryName"] = sc.SubCategoryName;
                row["CategoryName"] = sc.CategoryName;
                row["Status"] = sc.IsActive ? "Activated" : "Archived";
                dt.Rows.Add(row);
            }
            
            dgvSubCategory.DataSource = dt;
            dgvSubCategory.Columns["CategoryID"].Visible = false;
            dgvSubCategory.Columns["SubCategoryID"].Visible = false;
            DataGridViewStyler.ApplyStyle(dgvSubCategory, "SubCategoryID");

            foreach (DataGridViewRow row in dgvSubCategory.Rows)
            {
                string status = row.Cells["Status"].Value?.ToString() ?? "";
                if (status == "Activated")
                {
                    row.Cells["Status"].Style.ForeColor = Color.Green;
                    row.Cells["Status"].Style.Font = new Font(dgvSubCategory.Font, FontStyle.Bold);
                }
                else if (status == "Archived")
                {
                    row.Cells["Status"].Style.ForeColor = Color.Red;
                    row.Cells["Status"].Style.Font = new Font(dgvSubCategory.Font, FontStyle.Bold);
                }
            }
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            ReadSubCategories();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ReadSubCategories();
        }
    }
}
