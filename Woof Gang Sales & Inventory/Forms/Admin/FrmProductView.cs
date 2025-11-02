using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Helpers;
using Woof_Gang_Sales___Inventory.Models; // ✅ Added Models
using Woof_Gang_Sales___Inventory.Util;   // ✅ Added Util

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmProductView : Sample
    {
        private ProductRepository productRepo = new ProductRepository();

        // ✅ Added CategoryRepo to load the filter
        private CategoryRepository categoryRepo = new CategoryRepository();

        // ✅ Changed to "Archived" for consistency
        private string[] statusFilter = { "Active Products", "Archived Products", "All Products" };

        public FrmProductView()
        {
            InitializeComponent();
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
                        e.CellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); // Matched your user form
                    }
                    else if (value.Equals("Archived", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.SelectionForeColor = Color.Red;
                        e.CellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); // Matched your user form
                    }
                }

                // --- Format the price column ---
                if (dgvProduct.Columns[e.ColumnIndex].Name == "SellingPrice")
                {
                    if (e.Value != null)
                    {
                        decimal price;
                        if (decimal.TryParse(e.Value.ToString(), out price))
                        {
                            // "C" formats as currency (e.g., ₱1,234.50)
                            e.Value = price.ToString("C");
                            e.FormattingApplied = true;
                        }
                    }
                }

                // --- ✅ NEW: Format StockLevel Column ---
                if (dgvProduct.Columns[e.ColumnIndex].Name == "StockLevel")
                {
                    string value = e.Value?.ToString() ?? "";
                    e.CellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

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
                // --- END NEW FORMATTING ---
            };

            // Call ReadProducts here AFTER InitializeComponent
            ReadProducts();
            cmbFilterStatus.SelectedIndexChanged += FilterChanged;
            cmbFilterCategory.SelectedIndexChanged += FilterChanged;
        }

        private void FrmProductView_Load(object sender, EventArgs e)
        {
            // Load Status Filter
            foreach (var status in statusFilter)
            {
                cmbFilterStatus.Items.Add(status);
            }
            cmbFilterStatus.SelectedIndex = 0; // Default to "Active Products"

            // Load Category Filter from Database
            LoadCategoryFilter();
        }

        /// <summary>
        /// Loads Categories from DB into the filter ComboBox
        /// </summary>
        private void LoadCategoryFilter()
        {
            try
            {
                // 1. Create a "dummy" category for "All Categories"
                var allCategories = new Category { CategoryID = 0, CategoryName = "All Categories" };

                // 2. Get the real categories from the DB
                var realCategories = categoryRepo.GetCategories(""); // Gets active categories

                // 3. Combine them into a new list
                var categoryDataSource = new List<Category>();
                categoryDataSource.Add(allCategories);
                categoryDataSource.AddRange(realCategories);

                // 4. Bind the list to the ComboBox
                cmbFilterCategory.DisplayMember = "CategoryName";
                cmbFilterCategory.ValueMember = "CategoryID";
                cmbFilterCategory.DataSource = categoryDataSource;
                cmbFilterCategory.SelectedIndex = 0; // Default to "All Categories"
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Use the pattern from your FrmCreateEditUser
            FrmCreateEditProduct form = new FrmCreateEditProduct();
            form.IsEditMode = false;

            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadProducts(); // Refresh the grid
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProduct.SelectedRows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Selection", "Please select a product first.", "warning");
                return;
            }

            var value = this.dgvProduct.SelectedRows[0].Cells["ProductID"].Value.ToString();

            if (string.IsNullOrEmpty(value)) return;

            if (!int.TryParse(value, out int productID))
            {
                DialogHelper.ShowCustomDialog("Invalid Selection", "The selected product ID is invalid.", "error");
                return;
            }

            var product = productRepo.GetProductById(productID);

            if (product == null)
            {
                DialogHelper.ShowCustomDialog("Not Found", "The selected product no longer exists.", "error");
                ReadProducts();
                return;
            }

            FrmCreateEditProduct form = new FrmCreateEditProduct();
            form.IsEditMode = true;
            form.EditProduct(product);

            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadProducts(); // Refresh the grid
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProduct.SelectedRows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Selection", "Please select a product first.", "warning");
                return;
            }

            var value = this.dgvProduct.SelectedRows[0].Cells["ProductID"].Value.ToString();
            if (string.IsNullOrEmpty(value)) return;

            int productID = int.Parse(value);

            DialogResult result = DialogHelper.ShowConfirmDialog("Archive Product", "Are you sure you want to archive this product?", "warning");

            if (result == DialogResult.No) return;

            bool success = productRepo.DeleteProduct(productID); // This is the soft-delete
            if (success)
                ReadProducts(); // Refresh the grid
        }

        // --- Search and Data Loading ---

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ReadProducts();
        }

        /// <summary>
        /// Main data loading and filtering logic
        /// </summary>
        private void ReadProducts()
        {
            string searchText = txtSearch.Text.Trim();
            string selectedStatus = cmbFilterStatus.SelectedItem?.ToString() ?? "Active Products";

            // Get the *ID* from the category combobox
            int selectedCategoryID = 0;
            if (cmbFilterCategory.SelectedValue != null)
            {
                int.TryParse(cmbFilterCategory.SelectedValue.ToString(), out selectedCategoryID);
            }

            List<Models.Product> products;

            // 1. Get List based on Status (and DB search if active)
            if (selectedStatus == "Active Products")
            {
                products = productRepo.GetProducts(searchText);
            }
            else if (selectedStatus == "Archived Products")
            {
                products = productRepo.GetAllInactiveProducts();
                // Filter archived list using LINQ (client-side) as per your FrmUserView pattern
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

            // 3. Build DataTable to bind (as per your pattern)
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductID");
            dt.Columns.Add("SKU");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("Brand");
            dt.Columns.Add("CategoryName");
            dt.Columns.Add("SubCategoryName");
            dt.Columns.Add("SupplierName");
            dt.Columns.Add("Weight");
            dt.Columns.Add("Unit");
            dt.Columns.Add("SellingPrice", typeof(decimal));
            dt.Columns.Add("Quantity");
            dt.Columns.Add("ReorderLevel");
            dt.Columns.Add("StockLevel"); // ✅ NEW Column
            dt.Columns.Add("Status");

            foreach (var p in products)
            {
                var row = dt.NewRow();
                row["ProductID"] = p.ProductID;
                row["SKU"] = p.SKU;
                row["ProductName"] = p.ProductName;
                row["Brand"] = p.Brand;
                row["CategoryName"] = p.CategoryName;
                row["SubCategoryName"] = p.SubCategoryName;
                row["SupplierName"] = p.SupplierName;
                row["Weight"] = p.Weight.HasValue ? p.Weight.Value.ToString() : null;
                row["Unit"] = p.Unit;
                row["SellingPrice"] = p.SellingPrice;
                row["Quantity"] = p.Quantity;
                row["ReorderLevel"] = p.ReorderLevel.HasValue ? p.ReorderLevel.Value.ToString() : null;
                row["Status"] = p.IsActive ? "Active" : "Archived";

                // ✅ --- NEW STOCK LEVEL LOGIC ---
                int quantity = p.Quantity;
                // Use 0 if reorder level is null
                int reorder = p.ReorderLevel ?? 0;

                if (quantity == 0)
                {
                    row["StockLevel"] = "Out of Stock";
                }
                // Only "Critical" if ReorderLevel is set (greater than 0)
                else if (reorder > 0 && quantity <= reorder)
                {
                    row["StockLevel"] = "Critical";
                }
                else
                {
                    row["StockLevel"] = "Sufficient";
                }
                // --- END NEW LOGIC ---

                dt.Rows.Add(row);
            }

            dgvProduct.DataSource = dt;

            // Hide the ID column (as per your pattern)
            if (dgvProduct.Columns.Contains("ProductID"))
                dgvProduct.Columns["ProductID"].Visible = false;

            // Apply your custom styler
            DataGridViewStyler.ApplyStyle(dgvProduct, "ProductID");

            // --- ✅ NEW: HIDE UNNECESSARY COLUMNS ---
            // We're replacing ReorderLevel with StockLevel, so hide it.
            if (dgvProduct.Columns.Contains("ReorderLevel"))
                dgvProduct.Columns["ReorderLevel"].Visible = false;

            // Optional, but good for space
            if (dgvProduct.Columns.Contains("Brand"))
                dgvProduct.Columns["Brand"].Visible = false;
            if (dgvProduct.Columns.Contains("SubCategoryName"))
                dgvProduct.Columns["SubCategoryName"].Visible = false;
            if (dgvProduct.Columns.Contains("SupplierName"))
                dgvProduct.Columns["SupplierName"].Visible = false;
            if (dgvProduct.Columns.Contains("Weight"))
                dgvProduct.Columns["Weight"].Visible = false;
            if (dgvProduct.Columns.Contains("Unit"))
                dgvProduct.Columns["Unit"].Visible = false;
            // --- END HIDE COLUMNS ---
        }
    }
}

