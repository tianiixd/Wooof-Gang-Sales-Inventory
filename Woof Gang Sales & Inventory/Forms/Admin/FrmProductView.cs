using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Database;
using Woof_Gang_Sales___Inventory.Helpers;
using Woof_Gang_Sales___Inventory.Models; // ✅ Added Models
using Woof_Gang_Sales___Inventory.Util;   // ✅ Added Util

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    // ✅ This inherits from Sample, which is correct for your pattern
    public partial class FrmProductView : Sample
    {
        private ProductRepository productRepo = new ProductRepository();

        private CategoryRepository categoryRepo = new CategoryRepository();

        TimeClockHelper time = new TimeClockHelper();

        private string[] statusFilter = { "Active Products", "Archived Products", "All Products" };

        private Image editIcon = Properties.Resources.edit2;
        private Image deleteIcon = Properties.Resources.delete2;


        // ✅ --- CONSTANTS FOR LAYOUT ---
        // We define these here so Paint, Click, and MouseMove all align perfectly.
        private int btnWidth = 45;
        private int btnHeight = 35;
        private int btnSpacing = 10;
        private int iconSize = 20;

        private int hoveredRowIndex = -1;
        private string hoveredButton = ""; // Values: "Edit", "Delete", or ""


        public FrmProductView()
        {
            InitializeComponent();

 

            dgvProduct.CellMouseClick += dgvProduct_CellMouseClick;
            dgvProduct.CellPainting += dgvProduct_CellPainting;

            // ✅ --- NEW: Smart Cursor Logic ---
            dgvProduct.CellMouseMove += dgvProduct_CellMouseMove;
            dgvProduct.CellMouseLeave += dgvProduct_CellMouseLeave;
            dgvProduct.CellFormatting += (s, e) =>
            {
                // --- Format Status Column ---
                if (dgvProduct.Columns[e.ColumnIndex].Name == "Status")
                {
                    string value = e.Value?.ToString() ?? "";

                    if (value.Equals("Active", StringComparison.OrdinalIgnoreCase))
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

                // --- Format the price column ---
                if (dgvProduct.Columns[e.ColumnIndex].Name == "Selling Price")
                {
                    if (e.Value != null)
                    {
                        decimal price;
                        if (Decimal.TryParse(e.Value.ToString(), out price))
                        {
                            e.Value = price.ToString("C");
                            e.FormattingApplied = true;
                        }
                    }
                }

                // --- Format StockLevel Column ---
                if (dgvProduct.Columns[e.ColumnIndex].Name == "Stock Level")
                {
                    string value = e.Value?.ToString() ?? "";
                    e.CellStyle.Font = new Font("Segoe UI", 10F);

                    if (value.Equals("Out of Stock", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.SelectionForeColor = Color.Red;
                    }
                    else if (value.Equals("Critical", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(255, 128, 0); // Orange
                        e.CellStyle.SelectionForeColor = Color.FromArgb(255, 128, 0);
                    }
                    else // "Sufficient"
                    {
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.SelectionForeColor = Color.Green;
                    }
                }

                // ✅ --- ADD THIS NEW BLOCK ---
                // --- Format ExpirationDate Column ---
                if (dgvProduct.Columns[e.ColumnIndex].Name == "Expiration Date")
                {
                    // If the value is null or empty, show "N/A"
                    if (e.Value == null || e.Value == DBNull.Value)
                    {
                        e.Value = "N/A";
                        e.FormattingApplied = true;
                    }
                    else if (e.Value is DateTime date)
                    {
                        // Format the date as "yyyy-MM-dd"
                        e.Value = date.ToString("yyyy-MM-dd");
                        e.FormattingApplied = true;

                        // Color-code if expiring soon (e.g., within 10 days)
                        if (date < DateTime.Today.AddDays(10))
                        {
                            e.CellStyle.ForeColor = Color.Red;
                            e.CellStyle.SelectionForeColor = Color.Red;
                            e.CellStyle.Font = new Font("Segoe UI", 10F);
                        }
                    }
                }

            };

            // ✅ --- BUG FIX 1: REMOVED ReadProducts() from here. ---
            // It was causing a NullReferenceException because filters weren't loaded.

            cmbFilterStatus.SelectedIndexChanged += FilterChanged;
            cmbFilterCategory.SelectedIndexChanged += FilterChanged;
        }

        private void FrmProductView_Load(object sender, EventArgs e)
        {
          
            time.StartClock(lblTime, lblDate);

            // Load Status Filter
            foreach (var status in statusFilter)
            {
                cmbFilterStatus.Items.Add(status);
            }
            cmbFilterStatus.SelectedIndex = 0; // Default to "Active Products"

            DataGridViewButtonColumn actionCol = new DataGridViewButtonColumn();
            actionCol.Name = "Actions";
            actionCol.HeaderText = "Actions";
            actionCol.Text = "";
            actionCol.UseColumnTextForButtonValue = false;

            // ✅ --- FIX: STRICT WIDTH CONTROL ---
            actionCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            actionCol.Resizable = DataGridViewTriState.False; // Prevent resizing
            actionCol.Width = 150; // Fixed width

            dgvProduct.Columns.Add(actionCol);

            // Load Category Filter from Database
            LoadCategoryFilter();

            // ✅ --- BUG FIX 2: Call ReadProducts() here. ---
            // This runs AFTER the ComboBoxes are loaded and ready.
            ReadProducts();
            productRepo.StatsCard(lblTotalProducts, lblTotalStocks, lblLowStocks, lblOutofStock, lblNearExpiry);
        }


        private void LoadCategoryFilter()
        {
            try
            {
                var allCategories = new Category { CategoryID = 0, CategoryName = "All Categories" };
                var realCategories = categoryRepo.GetCategories("");
                var categoryDataSource = new List<Category>();
                categoryDataSource.Add(allCategories);
                categoryDataSource.AddRange(realCategories);

                cmbFilterCategory.DisplayMember = "CategoryName";
                cmbFilterCategory.ValueMember = "CategoryID";
                cmbFilterCategory.DataSource = categoryDataSource;
                cmbFilterCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Loading Categories", ex.Message, "error");
            }
        }


        private void FilterChanged(object sender, EventArgs e)
        {
            ReadProducts();
        }

        // --- CRUD Button Clicks ---
        // (These are 'private' which is correct as they are wired up
        // in your FrmProductView.Designer.cs file)

        private void dgvProduct_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvProduct.Columns[e.ColumnIndex].Name == "Actions")
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
        private void dgvProduct_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvProduct.Columns[e.ColumnIndex].Name == "Actions")
            {
                // Calculate positions
                int w = dgvProduct.Columns[e.ColumnIndex].Width;
                int totalWidth = (btnWidth * 2) + btnSpacing;
                int startX = (w - totalWidth) / 2;

                int productID = Convert.ToInt32(dgvProduct.Rows[e.RowIndex].Cells["ProductID"].Value);

                // Check Edit Click (Blue Area)
                if (e.X >= startX && e.X <= startX + btnWidth)
                {
                    var product = productRepo.GetProductById(productID);
                    if (product == null) return;

                    FrmCreateEditProduct form = new FrmCreateEditProduct();
                    form.IsEditMode = true;
                    form.EditProduct(product);
                    if (form.ShowDialog() == DialogResult.OK) ReadProducts(); 
                    productRepo.StatsCard(lblTotalProducts, lblTotalStocks, lblLowStocks, lblOutofStock, lblNearExpiry);
                }
                // Check Delete Click (Red Area)
                else if (e.X >= startX + btnWidth + btnSpacing && e.X <= startX + (btnWidth * 2) + btnSpacing)
                {
                    DialogResult result = DialogHelper.ShowConfirmDialog("Archive Product", "Are you sure you want to archive this product?", "warning");
                    if (result == DialogResult.No) return;

                    bool success = productRepo.DeleteProduct(productID);
                    if (success) ReadProducts();
                    productRepo.StatsCard(lblTotalProducts, lblTotalStocks, lblLowStocks, lblOutofStock, lblNearExpiry);
                }
            }
        }

        // ✅ --- SMART CURSOR LOGIC ---
        private void dgvProduct_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvProduct.Columns[e.ColumnIndex].Name == "Actions")
            {
                // Recalculate positions relative to the cell
                int w = dgvProduct.Columns[e.ColumnIndex].Width;
                int totalWidth = (btnWidth * 2) + btnSpacing;
                int startX = (w - totalWidth) / 2;

                string newHoveredButton = "";

                // Check if mouse is over Edit OR Delete button
                if (e.X >= startX && e.X <= startX + btnWidth)
                {
                    newHoveredButton = "Edit";
                    dgvProduct.Cursor = Cursors.Hand;
                }
                else if (e.X >= startX + btnWidth + btnSpacing && e.X <= startX + (btnWidth * 2) + btnSpacing)
                {
                    newHoveredButton = "Delete";
                    dgvProduct.Cursor = Cursors.Hand;
                }
                else
                {
                    dgvProduct.Cursor = Cursors.Default;
                }

                // ✅ OPTIMIZATION: Only repaint if the state has actually changed
                // This prevents the grid from flickering wildly while moving the mouse
                if (hoveredRowIndex != e.RowIndex || hoveredButton != newHoveredButton)
                {
                    hoveredRowIndex = e.RowIndex;
                    hoveredButton = newHoveredButton;
                    dgvProduct.InvalidateCell(e.ColumnIndex, e.RowIndex); // Trigger CellPainting
                }
            }
            else
            {
                dgvProduct.Cursor = Cursors.Default;
                // Reset if we moved to a different column
                if (hoveredRowIndex != -1)
                {
                    hoveredRowIndex = -1;
                    hoveredButton = "";
                    dgvProduct.InvalidateRow(e.RowIndex);
                }
            }
        }
        private void dgvProduct_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvProduct.Cursor = Cursors.Default;

            if (hoveredRowIndex != -1)
            {
                // Clear the hover state and redraw the row to remove the glow
                int rowToInvalidate = hoveredRowIndex;
                hoveredRowIndex = -1;
                hoveredButton = "";
                if (rowToInvalidate >= 0 && rowToInvalidate < dgvProduct.Rows.Count)
                    dgvProduct.InvalidateRow(rowToInvalidate);
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
            FrmCreateEditProduct form = new FrmCreateEditProduct();
            form.IsEditMode = false;

            if (form.ShowDialog() == DialogResult.OK)
            {
                //Refresh All
                ReadProducts();
                productRepo.StatsCard(lblTotalProducts, lblTotalStocks, lblLowStocks, lblOutofStock, lblNearExpiry);
            }
        }

        // --- Search and Data Loading ---

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ReadProducts();
        }

        /// <summary>
        /// Main data loading and filtering logic
        /// </summary>
        public void ReadProducts()
        {
            // ✅ --- BUG FIX 3: Add a safety check ---
            // This prevents ReadProducts() from running before the filters are ready
            if (cmbFilterStatus.SelectedItem == null || cmbFilterCategory.SelectedValue == null)
            {
                return; // Exit the method; it will be called again when filters are set.
            }

            string searchText = txtSearch.Text.Trim();
            string selectedStatus = cmbFilterStatus.SelectedItem.ToString();

            int selectedCategoryID = 0;
            int.TryParse(cmbFilterCategory.SelectedValue.ToString(), out selectedCategoryID);

            List<Models.Product> products;

            // 1. Get List based on Status
            if (selectedStatus == "Active Products")
            {
                products = productRepo.GetProducts(searchText);
            }
            else if (selectedStatus == "Archived Products")
            {
                products = productRepo.GetAllInactiveProducts();
                // Filter archived list using LINQ
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    string searchLower = searchText.ToLower();
                    products = products.Where(p =>
                        p.ProductName.ToLower().Contains(searchLower) ||
                        (p.Brand != null && p.Brand.ToLower().Contains(searchLower)) ||
                        (p.CategoryName != null && p.CategoryName.ToLower().Contains(searchLower)) ||
                        (p.SubCategoryName != null && p.SubCategoryName.ToLower().Contains(searchLower)) ||
                        (p.SKU != null && p.SKU.ToLower().Contains(searchLower))
                    ).ToList();
                }
            }
            else // "All Products"
            {
                var active = productRepo.GetProducts(searchText);
                var inactive = productRepo.GetAllInactiveProducts();
                // Filter archived list before combining
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    string searchLower = searchText.ToLower();
                    inactive = inactive.Where(p =>
                        p.ProductName.ToLower().Contains(searchLower) ||
                        (p.Brand != null && p.Brand.ToLower().Contains(searchLower)) ||
                        (p.CategoryName != null && p.CategoryName.ToLower().Contains(searchLower)) ||
                        (p.SubCategoryName != null && p.SubCategoryName.ToLower().Contains(searchLower)) ||
                        (p.SKU != null && p.SKU.ToLower().Contains(searchLower))
                    ).ToList();
                }
                products = active.Concat(inactive).OrderBy(p => p.ProductName).ToList();
            }

            // 2. Filter list by Category (client-side)
            if (selectedCategoryID != 0) // 0 is "All Categories"
            {
                products = products.Where(p => p.CategoryID == selectedCategoryID).ToList();
            }

            // 3. Build DataTable to bind
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductID");
            dt.Columns.Add("SKU");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Brand");

            // ✅ --- BUG FIX 4: Removed the stray word "Services" ---
            dt.Columns.Add("Category Name");
            dt.Columns.Add("SubCategory Name");
            dt.Columns.Add("Supplier Name");
            dt.Columns.Add("Weight");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Selling Price", typeof(decimal));
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Reorder Level");
            dt.Columns.Add("Stock Level");
            dt.Columns.Add("Expiration Date", typeof(DateTime));
            dt.Columns.Add("Status");

            foreach (var p in products)
            {
                var row = dt.NewRow();
                row["ProductID"] = p.ProductID;
                row["SKU"] = p.SKU ?? "";
                row["Product Name"] = $"{p.Brand} {p.ProductName}";
                row["Brand"] = p.Brand;
                row["Category Name"] = p.CategoryName;
                row["SubCategory Name"] = p.SubCategoryName;
                row["Supplier Name"] = p.SupplierName;
                row["Weight"] = p.Weight.HasValue ? p.Weight.Value.ToString() : null;
                row["Unit"] = p.Unit;
                row["Selling Price"] = p.SellingPrice;
                row["Quantity"] = p.Quantity;
                row["Reorder Level"] = p.ReorderLevel.HasValue ? p.ReorderLevel.Value.ToString() : null;
                row["Status"] = p.IsActive ? "Active" : "Archived";

                if (p.ExpirationDate.HasValue)
                {
                    row["Expiration Date"] = p.ExpirationDate.Value;
                }
                else
                {
                    row["Expiration Date"] = DBNull.Value; // Use DBNull for null dates
                }

                int quantity = p.Quantity;
                int reorder = p.ReorderLevel ?? 0;

                if (quantity == 0)
                {
                    row["Stock Level"] = "Out of Stock";
                }
                else if (reorder > 0 && quantity <= reorder)
                {
                    row["Stock Level"] = "Critical";
                }
                else
                {
                    row["Stock Level"] = "Sufficient";
                }

                dt.Rows.Add(row);
            }

            dgvProduct.DataSource = dt;

            if (dgvProduct.Columns.Contains("Actions"))
            {
                dgvProduct.Columns["Actions"].DisplayIndex = dgvProduct.Columns.Count - 1;
            }

            // Hide the ID column
            if (dgvProduct.Columns.Contains("ProductID"))
                dgvProduct.Columns["ProductID"].Visible = false;

            // Apply your custom styler
            DataGridViewStyler.ApplyStyle(dgvProduct, "SKU");

            // --- HIDE UNNECESSARY COLUMNS ---

            dgvProduct.Columns["Reorder Level"].Visible = false;
            dgvProduct.Columns["Brand"].Visible = false;
            dgvProduct.Columns["Category Name"].Visible = false;
            dgvProduct.Columns["SubCategory Name"].Visible = false;
            dgvProduct.Columns["Supplier Name"].Visible = true;
            dgvProduct.Columns["Weight"].Visible = false;
            dgvProduct.Columns["Unit"].Visible = false;
            if (dgvProduct.Columns.Contains("Expiration Date"))
            {
                dgvProduct.Columns["Expiration Date"].HeaderText = "Expiration Date";
                dgvProduct.Columns["Expiration Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            if (selectedStatus == "Active Products" || selectedStatus == "Archived Products")
            {
                dgvProduct.Columns["Status"].Visible = false;
            }
            else
            {
                dgvProduct.Columns["Status"].Visible = true;
            }
            
        }

        private void lblTotalProducts_Click(object sender, EventArgs e)
        {

        }
    }
}

