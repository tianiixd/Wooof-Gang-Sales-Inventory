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
using Guna.UI2.WinForms;
using System.Drawing.Drawing2D; // Required for GraphicsPath

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmCategoryView : Form
    {
        private CategoryRepository categoryRepo = new CategoryRepository();

        TimeClockHelper time = new TimeClockHelper();

        private string[] categoryStatus = { "Active Categories", "Archived Categories", "All Categories" };

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

        public FrmCategoryView()
        {
            InitializeComponent();

            // Wire up events
            dgvCategory.CellFormatting += dgvCategory_CellFormatting;
            dgvCategory.CellMouseClick += dgvCategory_CellMouseClick;
            dgvCategory.CellPainting += dgvCategory_CellPainting;

            // ✅ --- NEW: Smart Cursor Logic ---
            dgvCategory.CellMouseMove += dgvCategory_CellMouseMove;
            dgvCategory.CellMouseLeave += dgvCategory_CellMouseLeave;

            ReadCategory();
            cmbFilterStatus.SelectedIndexChanged += FilterChanged;
        }

        private void FrmCategoryView_Load(object sender, EventArgs e)
        {
            time.StartClock(lblTime, lblDate);

            foreach (var category in categoryStatus)
            {
                cmbFilterStatus.Items.Add(category);
            }
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

            dgvCategory.Columns.Add(actionCol);
        }


        // ✅ --- PAINTING ---
        private void dgvCategory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvCategory.Columns[e.ColumnIndex].Name == "Actions")
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
        private void dgvCategory_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvCategory.Columns[e.ColumnIndex].Name == "Actions")
            {
                // Calculate positions
                int w = dgvCategory.Columns[e.ColumnIndex].Width;
                int totalWidth = (btnWidth * 2) + btnSpacing;
                int startX = (w - totalWidth) / 2;

                int categoryID = Convert.ToInt32(dgvCategory.Rows[e.RowIndex].Cells["CategoryID"].Value);

                // Check Edit Click (Blue Area)
                if (e.X >= startX && e.X <= startX + btnWidth)
                {
                    var category = categoryRepo.GetCategoryById(categoryID);
                    if (category == null) return;

                    FrmCreateEditCategory form = new FrmCreateEditCategory();
                    form.IsEditMode = true;
                    form.EditCategory(category);
                    if (form.ShowDialog() == DialogResult.OK) ReadCategory();
                }
                // Check Delete Click (Red Area)
                else if (e.X >= startX + btnWidth + btnSpacing && e.X <= startX + (btnWidth * 2) + btnSpacing)
                {
                    DialogResult result = DialogHelper.ShowConfirmDialog("Archive Category", "Are you sure you want to archive this category?", "warning");
                    if (result == DialogResult.No) return;

                    bool success = categoryRepo.DeleteCategory(categoryID);
                    if (success) ReadCategory();
                }
            }
        }

        // ✅ --- SMART CURSOR LOGIC ---
        private void dgvCategory_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvCategory.Columns[e.ColumnIndex].Name == "Actions")
            {
                // Recalculate positions relative to the cell
                int w = dgvCategory.Columns[e.ColumnIndex].Width;
                int totalWidth = (btnWidth * 2) + btnSpacing;
                int startX = (w - totalWidth) / 2;

                string newHoveredButton = "";

                // Check if mouse is over Edit OR Delete button
                if (e.X >= startX && e.X <= startX + btnWidth)
                {
                    newHoveredButton = "Edit";
                    dgvCategory.Cursor = Cursors.Hand;
                }
                else if (e.X >= startX + btnWidth + btnSpacing && e.X <= startX + (btnWidth * 2) + btnSpacing)
                {
                    newHoveredButton = "Delete";
                    dgvCategory.Cursor = Cursors.Hand;
                }
                else
                {
                    dgvCategory.Cursor = Cursors.Default;
                }

                // ✅ OPTIMIZATION: Only repaint if the state has actually changed
                // This prevents the grid from flickering wildly while moving the mouse
                if (hoveredRowIndex != e.RowIndex || hoveredButton != newHoveredButton)
                {
                    hoveredRowIndex = e.RowIndex;
                    hoveredButton = newHoveredButton;
                    dgvCategory.InvalidateCell(e.ColumnIndex, e.RowIndex); // Trigger CellPainting
                }
            }
            else
            {
                dgvCategory.Cursor = Cursors.Default;
                // Reset if we moved to a different column
                if (hoveredRowIndex != -1)
                {
                    hoveredRowIndex = -1;
                    hoveredButton = "";
                    dgvCategory.InvalidateRow(e.RowIndex);
                }
            }
        }
        private void dgvCategory_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvCategory.Cursor = Cursors.Default;

            if (hoveredRowIndex != -1)
            {
                // Clear the hover state and redraw the row to remove the glow
                int rowToInvalidate = hoveredRowIndex;
                hoveredRowIndex = -1;
                hoveredButton = "";
                if (rowToInvalidate >= 0 && rowToInvalidate < dgvCategory.Rows.Count)
                    dgvCategory.InvalidateRow(rowToInvalidate);
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
            FrmCreateEditCategory form = new FrmCreateEditCategory();
            form.IsEditMode = false;
            if (form.ShowDialog() == DialogResult.OK) ReadCategory();
        }

        public void ReadCategory()
        {
            string searchText = txtSearch.Text.Trim();
            string selectedStatus = cmbFilterStatus.SelectedItem?.ToString() ?? "All Categories";
            List<Category> categories;

            if (selectedStatus == "Active Categories")
            {
                categories = categoryRepo.GetCategories(searchText);
            }
            else if (selectedStatus == "Archived Categories")
            {
                categories = categoryRepo.GetAllInactiveCategories();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    string searchLower = searchText.ToLower();
                    categories = categories.Where(c => c.CategoryName.ToLower().Contains(searchLower)).ToList();
                }
            }
            else
            {
                var active = categoryRepo.GetCategories(searchText);
                var inactive = categoryRepo.GetAllInactiveCategories();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    string searchLower = searchText.ToLower();
                    inactive = inactive.Where(c => c.CategoryName.ToLower().Contains(searchLower)).ToList();
                }
                categories = active.Concat(inactive).ToList();
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("CategoryID");
            dt.Columns.Add("CategoryName");
            dt.Columns.Add("Status");

            foreach (var category in categories)
            {
                var row = dt.NewRow();
                row["CategoryID"] = category.CategoryID;
                row["CategoryName"] = category.CategoryName;
                row["Status"] = category.IsActive ? "Activated" : "Archived";
                dt.Rows.Add(row);
            }
            dgvCategory.DataSource = dt;
            DataGridViewStyler.ApplyStyle(dgvCategory, "CategoryID");

            dgvCategory.Columns["CategoryID"].Visible = false;
            dgvCategory.Columns["CategoryName"].HeaderText = "Category Name";
            dgvCategory.Columns["CategoryName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCategory.Columns["Status"].HeaderText = "Status";
        }

        private void dgvCategory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvCategory.Columns[e.ColumnIndex].Name == "Status")
            {
                string value = e.Value?.ToString() ?? "";
                if (value.Equals("Activated", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.SelectionForeColor = Color.Green;
                }
                else if (value.Equals("Archived", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.SelectionForeColor = Color.Red;
                }
            }
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            ReadCategory();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ReadCategory();
        }
    }
}