using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmCreateEditProduct : Sample
    {
        // --- Repositories ---
        private readonly ProductRepository productRepo = new ProductRepository();
        private readonly CategoryRepository categoryRepo = new CategoryRepository();
        private readonly SubCategoryRepository subCategoryRepo = new SubCategoryRepository();
        private readonly SupplierRepository supplierRepo = new SupplierRepository();

        // --- Form State ---
        public bool IsEditMode { get; set; } = false;
        private int productID = 0;

        // --- Image Handling State (from FrmCreateEditUser) ---
        private string selectedImagePath = string.Empty;
        private string previousImagePath = string.Empty;
        private bool imageChanged = false;

        // --- Data for ComboBoxes ---
        private List<SubCategory> allSubCategories = new List<SubCategory>();


        public FrmCreateEditProduct()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        private void FrmCreateEditProduct_Load(object sender, EventArgs e)
        {
            // Load all data into ComboBoxes
            LoadAllComboBoxData();

            if (!IsEditMode)
            {
                // --- Create Mode ---
                this.lblTitle.Text = "Add New Product";
                this.btnSave.Text = "Add";
                // picProductImage.Image = Properties.Resources.placeholder; // Set default image
                toggleProductStatus.Checked = false; // ✅ Default to Active
                toggleProductStatus.Enabled = false;
                lblProductStatus.Text = "";
            }
            else
            {
                // --- Edit Mode ---
                toggleProductStatus.Enabled = true;
            }
        }

        #region ComboBox Loading Logic

        private void LoadAllComboBoxData()
        {
            // 1. Load Suppliers
            var suppliers = supplierRepo.GetSuppliers("");
            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember = "SupplierID";
            cmbSupplier.DataSource = suppliers;
            cmbSupplier.SelectedIndex = -1; // No selection by default

            // 2. Load All SubCategories (for filtering)
            // We load this *once* to prevent hitting the database on every click
            allSubCategories = subCategoryRepo.GetSubCategories(""); // Gets all active subcategories

            // 3. Load Categories
            var categories = categoryRepo.GetCategories(""); // Gets all active categories
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
            cmbCategory.DataSource = categories;
            cmbCategory.SelectedIndex = -1; // No selection by default

            // 4. Attach the event handler for cascading
            cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // --- This is the Cascading Dropdown Logic ---

            if (cmbCategory.SelectedValue == null)
            {
                cmbSubCategory.DataSource = null;
                cmbUnit.DataSource = null; // Also clear units
                return;
            }

            try
            {
                int selectedCategoryID = (int)cmbCategory.SelectedValue;

                // Filter the in-memory list we loaded earlier
                var filteredSubs = allSubCategories
                    .Where(s => s.CategoryID == selectedCategoryID)
                    .ToList();

                cmbSubCategory.DataSource = filteredSubs;
                cmbSubCategory.DisplayMember = "SubCategoryName";
                cmbSubCategory.ValueMember = "SubCategoryID";

                if (filteredSubs.Count == 0)
                {
                    cmbSubCategory.Text = "No subcategories found";
                }

                // ✅ --- ADDED: Populate cmbUnit based on Category ---
                cmbUnit.Items.Clear();
                string selectedCategoryName = cmbCategory.Text;

                switch (selectedCategoryName)
                {
                    case "Dog Food":
                    case "Cat Food":
                        cmbUnit.Items.AddRange(new string[] { "Bag", "Can", "Pouch", "Box", "Kg", "g" });
                        break;
                    case "Pet Treats":
                        cmbUnit.Items.AddRange(new string[] { "Pack", "Box", "Piece", "g" });
                        break;
                    case "Pet Grooming":
                        cmbUnit.Items.AddRange(new string[] { "Bottle", "mL", "Piece" });
                        break;
                    case "Pet Accessories":
                        cmbUnit.Items.AddRange(new string[] { "Piece", "Set" });
                        break;
                    case "Pet Health":
                        cmbUnit.Items.AddRange(new string[] { "Bottle", "Box", "mL", "g", "Tablet" });
                        break;
                    case "Pet Toys":
                        cmbUnit.Items.AddRange(new string[] { "Piece", "Pack" });
                        break;
                    default:
                        cmbUnit.Items.AddRange(new string[] { "Piece", "Pack", "Box", "Kg", "g", "mL", "L" });
                        break;
                }
                if (cmbUnit.Items.Count > 0)
                    cmbUnit.SelectedIndex = 0;
                // ✅ --- END OF ADDED SECTION ---

            }
            catch (Exception)
            {
                // Handle case where SelectedValue might not be an int (e.g., during load)
                cmbSubCategory.DataSource = null;
            }
        }

        #endregion

        #region Image Handling Logic (Copied from FrmCreateEditUser)

        private Image LoadImageWithoutLock(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return null; // Or return placeholder image

            try
            {
                using (var tempImage = Image.FromFile(path))
                {
                    return new Bitmap(tempImage); // Create a copy
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading image: " + ex.Message);
                return null; // Or return placeholder
            }
        }

        private void DisposeCurrentImage()
        {
            // if (picProductImage.Image != null && picProductImage.Image != Properties.Resources.placeholder)
            if (picProductImage.Image != null)
            {
                var img = picProductImage.Image;
                picProductImage.Image = null;
                img.Dispose();
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Choose Product Image";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DisposeCurrentImage();
                        picProductImage.Image = LoadImageWithoutLock(ofd.FileName);
                        picProductImage.SizeMode = PictureBoxSizeMode.Zoom;
                        selectedImagePath = ofd.FileName;
                        imageChanged = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error selecting image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DisposeCurrentImage();
            base.OnFormClosing(e);
        }

        #endregion

        #region Save, Edit, and Validation Logic

        public void EditProduct(Product product)
        {
            this.lblTitle.Text = "Edit Product";
            this.lblID.Text = product.ProductID.ToString();
            this.btnSave.Text = "Save";

            // Set text fields
            txtProductName.Text = product.ProductName;
            // txtSKU.Text = product.SKU; // REMOVED
            txtBrand.Text = product.Brand;
            txtWeight.Text = product.Weight?.ToString();
            txtSellingPrice.Text = product.SellingPrice.ToString("F2");

            // ✅ CHANGED: Set Guna2NumericUpDown values
            numQuantity.Value = product.Quantity;
            numOrderLevel.Value = product.ReorderLevel ?? 0; // Default to 0 if null

            // --- Set ComboBoxes (Handle cascading) ---

            // Set Category first. This will auto-trigger the cmbCategory_SelectedIndexChanged event
            // which will populate the SubCategory and Unit lists.
            cmbCategory.SelectedValue = product.CategoryID;

            // NOW we can safely set the SubCategory, Supplier, and Unit
            cmbSubCategory.SelectedValue = product.SubCategoryID;
            cmbSupplier.SelectedValue = product.SupplierID;
            cmbUnit.Text = product.Unit; // Set Unit by text

            // Set Status
            toggleProductStatus.Checked = product.IsActive;
            lblStatus.Text = product.IsActive ? "Active" : "Archived";
            lblStatus.ForeColor = product.IsActive ? Color.Green : Color.Red;

            // Set Image (Copied from FrmCreateEditUser)
            if (!string.IsNullOrEmpty(product.ImagePath) && File.Exists(product.ImagePath))
            {
                DisposeCurrentImage();
                picProductImage.Image = LoadImageWithoutLock(product.ImagePath);
                picProductImage.SizeMode = PictureBoxSizeMode.Zoom;
                selectedImagePath = product.ImagePath;
                previousImagePath = product.ImagePath;
                imageChanged = false;
            }
            else
            {
                // picProductImage.Image = Properties.Resources.placeholder;
                selectedImagePath = string.Empty;
                previousImagePath = string.Empty;
                imageChanged = false;
            }

            // Set internal ID
            this.productID = product.ProductID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!VerifyInput()) return;

            // --- Step 1: Handle Image Saving (Copied from FrmCreateEditUser) ---
            string imagePath = previousImagePath; // Default: keep existing image

            if (!IsEditMode || imageChanged)
            {
                if (picProductImage.Image != null && !string.IsNullOrEmpty(selectedImagePath))
                {
                    try
                    {
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        // ✅ CHANGED: Save to "ProductImages" folder
                        string productImagesDir = Path.Combine(desktopPath, "ProductImages");

                        if (!Directory.Exists(productImagesDir))
                            Directory.CreateDirectory(productImagesDir);

                        // ✅ CHANGED: Build filename from ProductName
                        string safeProductName = txtProductName.Text.Trim().Replace(" ", "_");
                        // string safeSku = txtSKU.Text.Trim(); // REMOVED
                        string imageExtension = Path.GetExtension(selectedImagePath) ?? ".jpg";
                        string imageFileName = $"{safeProductName}{imageExtension}"; // REMOVED SKU

                        imagePath = Path.Combine(productImagesDir, imageFileName);

                        // Avoid overwriting (simplified)
                        if (File.Exists(imagePath) && imagePath != previousImagePath)
                        {
                            imageFileName = $"{safeProductName}_{DateTime.Now.Ticks}{imageExtension}"; // REMOVED SKU
                            imagePath = Path.Combine(productImagesDir, imageFileName);
                        }

                        using (var imageCopy = new Bitmap(picProductImage.Image))
                        {
                            imageCopy.Save(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }

                        // Delete old image if it changed
                        if (IsEditMode && !string.IsNullOrEmpty(previousImagePath) && File.Exists(previousImagePath) && previousImagePath != imagePath)
                        {
                            try
                            {
                                DisposeCurrentImage();
                                System.Threading.Thread.Sleep(50);
                                File.Delete(previousImagePath);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Could not delete old image: {ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    imagePath = null; // No image was selected
                }
            }


            // --- Step 2: Build Product Object ---
            Product product = new Product
            {
                ProductID = this.productID,
                ProductName = txtProductName.Text.Trim(),
                SKU = null, // ✅ CHANGED: Set to null since there is no textbox
                Brand = string.IsNullOrWhiteSpace(txtBrand.Text) ? null : txtBrand.Text.Trim(),

                // ✅ CHANGED: Read from cmbUnit
                Unit = string.IsNullOrWhiteSpace(cmbUnit.Text) ? null : cmbUnit.Text,

                // Handle numeric conversions
                Weight = decimal.TryParse(txtWeight.Text, out decimal weight) ? (decimal?)weight : null,
                SellingPrice = decimal.Parse(txtSellingPrice.Text), // Verified in VerifyInput

                // ✅ CHANGED: Read from Guna2NumericUpDown
                Quantity = (int)numQuantity.Value,
                ReorderLevel = (int)numOrderLevel.Value == 0 ? (int?)null : (int)numOrderLevel.Value, // Handle 0 as null

                // Get IDs from ComboBoxes
                CategoryID = (int)cmbCategory.SelectedValue,
                SubCategoryID = (int)cmbSubCategory.SelectedValue,
                SupplierID = (int)cmbSupplier.SelectedValue,

                IsActive = toggleProductStatus.Checked,
                ImagePath = imagePath
            };

            // --- Step 3: Save to Database ---
            bool success = false;
            if (IsEditMode)
            {
                success = productRepo.UpdateProduct(product);
            }
            else
            {
                success = productRepo.CreateProduct(product);
            }

            if (success)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            // Errors are handled by the repository's DialogHelper
        }

        private bool VerifyInput()
        {
            // 1. Check required text fields
            if (string.IsNullOrWhiteSpace(txtProductName.Text) ||
                string.IsNullOrWhiteSpace(txtSellingPrice.Text))
            {
                DialogHelper.ShowCustomDialog("Missing Information", "Product Name and Selling Price are required.", "warning");
                return false;
            }

            // 2. Check ComboBoxes
            if (cmbCategory.SelectedValue == null || (int)cmbCategory.SelectedValue == 0)
            {
                DialogHelper.ShowCustomDialog("Missing Information", "Please select a Category.", "warning");
                return false;
            }
            if (cmbSubCategory.SelectedValue == null || (int)cmbSubCategory.SelectedValue == 0)
            {
                DialogHelper.ShowCustomDialog("Missing Information", "Please select a SubCategory.", "warning");
                return false;
            }
            if (cmbSupplier.SelectedValue == null || (int)cmbSupplier.SelectedValue == 0)
            {
                DialogHelper.ShowCustomDialog("Missing Information", "Please select a Supplier.", "warning");
                return false;
            }
            // ✅ ADDED: Check cmbUnit
            if (string.IsNullOrWhiteSpace(cmbUnit.Text))
            {
                DialogHelper.ShowCustomDialog("Missing Information", "Please select a Unit (e.g., Bag, Kg, Piece).", "warning");
                return false;
            }

            // 3. Regex and Numeric Validation
            if (!Regex.IsMatch(txtSellingPrice.Text, @"^\d+(\.\d{1,2})?$") || decimal.Parse(txtSellingPrice.Text) <= 0)
            {
                DialogHelper.ShowCustomDialog("Invalid Input", "Selling Price must be a positive number (e.g., 100 or 100.50).", "warning");
                return false;
            }

            // ✅ CHANGED: No regex needed for Guna2NumericUpDown, just check value
            if (numQuantity.Value < 0)
            {
                DialogHelper.ShowCustomDialog("Invalid Input", "Quantity cannot be a negative number.", "warning");
                return false;
            }
            if (numOrderLevel.Value < 0)
            {
                DialogHelper.ShowCustomDialog("Invalid Input", "Reorder Level cannot be a negative number.", "warning");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtWeight.Text) && !Regex.IsMatch(txtWeight.Text, @"^\d+(\.\d{1,2})?$"))
            {
                DialogHelper.ShowCustomDialog("Invalid Input", "Weight must be a positive number (e.g., 1.5 or 10).", "warning");
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ResetInput()
        {
            txtProductName.Text = string.Empty;
            // txtSKU.Text = string.Empty; // REMOVED
            txtBrand.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtSellingPrice.Text = string.Empty;

            // ✅ CHANGED: Reset Guna2NumericUpDown and cmbUnit
            numQuantity.Value = 0;
            numOrderLevel.Value = 0;
            cmbUnit.DataSource = null;
            cmbUnit.Items.Clear();

            cmbCategory.SelectedIndex = -1;
            cmbSubCategory.DataSource = null;
            cmbSupplier.SelectedIndex = -1;

            toggleProductStatus.Checked = false;

            DisposeCurrentImage();
            // picProductImage.Image = Properties.Resources.placeholder;
            selectedImagePath = string.Empty;
            previousImagePath = string.Empty;
            imageChanged = false;
        }

        #endregion
    }
}


