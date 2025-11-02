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
        private string? _existingSku = null;

        // ✅ --- FIX 1: Add private fields to store selection ---
        private int _editCategoryID = 0;
        private int _editSubCategoryID = 0;
        private int _editSupplierID = 0;
        private string _editUnit = "";

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
                toggleProductStatus.Checked = true;
                toggleProductStatus.Enabled = false;

                lblProductStatus.Text = "";
               
            }
            else
            {
                // --- Edit Mode ---
                toggleProductStatus.Enabled = true;

                // ✅ --- FIX 3: Set ComboBox values AFTER they are loaded ---
                cmbCategory.SelectedValue = _editCategoryID;
                cmbSubCategory.SelectedValue = _editSubCategoryID;
                cmbSupplier.SelectedValue = _editSupplierID;
                cmbUnit.Text = _editUnit;
            }
        }

        private void LoadAllComboBoxData()
        {
            // 1. Load Suppliers
            var suppliers = supplierRepo.GetSuppliers("");
            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember = "SupplierID";
            cmbSupplier.DataSource = suppliers;
            cmbSupplier.SelectedIndex = -1; // No selection by default

            // 2. Load All SubCategories (for filtering)
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
            if (cmbCategory.SelectedValue == null)
            {
                cmbSubCategory.DataSource = null;
                cmbUnit.Items.Clear();
                return;
            }

            try
            {
                if (cmbCategory.SelectedValue is int selectedCategoryID)
                {
                    var filteredSubs = allSubCategories
                        .Where(s => s.CategoryID == selectedCategoryID)
                        .ToList();

                    cmbSubCategory.DataSource = filteredSubs;
                    cmbSubCategory.DisplayMember = "SubCategoryName";
                    cmbSubCategory.ValueMember = "SubCategoryID";

                    if (filteredSubs.Count == 0)
                    {
                        cmbSubCategory.Text = "No subcategories found";
                        cmbSubCategory.DataSource = null;
                    }

                    // --- Populate cmbUnit based on Category ---
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
                }
            }
            catch (Exception)
            {
                cmbSubCategory.DataSource = null;
            }
        }

        private Image LoadImageWithoutLock(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return null;

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
                return null;
            }
        }

        private void DisposeCurrentImage()
        {
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

        public void EditProduct(Product product)
        {
            this.lblTitle.Text = "Edit Product";
            this.lblID.Text = product.ProductID.ToString();
            this.btnSave.Text = "Save";

            // Set text fields
            txtProductName.Text = product.ProductName;
            txtBrand.Text = product.Brand; // ✅ Re-added this
            txtWeight.Text = product.Weight?.ToString(); // ✅ Re-added this
            txtSellingPrice.Text = product.SellingPrice.ToString("F2");

            numQuantity.Value = product.Quantity;
            numOrderLevel.Value = product.ReorderLevel ?? 5;

            // --- ✅ FIX 2: Store values instead of setting ComboBoxes ---
            _editCategoryID = product.CategoryID;
            _editSubCategoryID = product.SubCategoryID ?? 0;
            _editSupplierID = product.SupplierID ?? 0;
            _editUnit = product.Unit;

            // Set Status
            toggleProductStatus.Checked = product.IsActive;
            lblProductStatus.Text = product.IsActive ? "Active" : "Archived";
            lblProductStatus.ForeColor = product.IsActive ? Color.Green : Color.Red;

            // Set Image
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
                selectedImagePath = string.Empty;
                previousImagePath = string.Empty;
                imageChanged = false;
            }

            // Set internal IDs
            this.productID = product.ProductID;
            this._existingSku = product.SKU;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!VerifyInput()) return;

            // --- Step 1: Handle Image Saving ---
            string imagePath = previousImagePath;

            if (!IsEditMode || imageChanged)
            {
                if (picProductImage.Image != null && !string.IsNullOrEmpty(selectedImagePath))
                {
                    try
                    {
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        string productImagesDir = Path.Combine(desktopPath, "ProductImages");

                        if (!Directory.Exists(productImagesDir))
                            Directory.CreateDirectory(productImagesDir);

                        string safeProductName = txtProductName.Text.Trim().Replace(" ", "_");
                        string safeBrand = txtBrand.Text.Trim().Replace(" ", "_");
                        string imageExtension = Path.GetExtension(selectedImagePath) ?? ".jpg";

                        string imageFileName = $"{safeProductName}_{safeBrand}{imageExtension}";

                        imagePath = Path.Combine(productImagesDir, imageFileName);

                        int count = 1;
                        while (File.Exists(imagePath) && imagePath != previousImagePath)
                        {
                            imageFileName = $"{safeProductName}_{safeBrand}_{count}{imageExtension}";
                            imagePath = Path.Combine(productImagesDir, imageFileName);
                            count++;
                        }

                        using (var imageCopy = new Bitmap(picProductImage.Image))
                        {
                            imageCopy.Save(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }

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

                SKU = this._existingSku,

                Brand = string.IsNullOrWhiteSpace(txtBrand.Text) ? null : txtBrand.Text.Trim(),
                Unit = string.IsNullOrWhiteSpace(cmbUnit.Text) ? null : cmbUnit.Text,
                Weight = decimal.TryParse(txtWeight.Text, out decimal weight) ? (decimal?)weight : null,
                SellingPrice = decimal.Parse(txtSellingPrice.Text),
                Quantity = (int)numQuantity.Value,
                ReorderLevel = (int)numOrderLevel.Value < 5 ? 5 : (int)numOrderLevel.Value,
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
                if (!IsEditMode)
                {
                    ResetInput();
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
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
            if (cmbCategory.SelectedValue == null || (int)cmbCategory.SelectedValue <= 0)
            {
                DialogHelper.ShowCustomDialog("Missing Information", "Please select a Category.", "warning");
                return false;
            }
            if (cmbSubCategory.SelectedValue == null || (int)cmbSubCategory.SelectedValue <= 0)
            {
                DialogHelper.ShowCustomDialog("Missing Information", "Please select a SubCategory.", "warning");
                return false;
            }
            if (cmbSupplier.SelectedValue == null || (int)cmbSupplier.SelectedValue <= 0)
            {
                DialogHelper.ShowCustomDialog("Missing Information", "Please select a Supplier.", "warning");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cmbUnit.Text) || cmbUnit.SelectedIndex < 0)
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

            if (numOrderLevel.Value < 5 && numOrderLevel.Value != 0) // Allow 0, but not 1-4
            {
                DialogHelper.ShowCustomDialog("Invalid Input", "Reorder Level must be 0 or 5 or more.", "warning");
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
            txtBrand.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtSellingPrice.Text = string.Empty;

            numQuantity.Value = 0;
            numOrderLevel.Value = 5;

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

            _existingSku = null;
        }

        private void toggleProductStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleProductStatus.Checked)
            {
                lblProductStatus.Text = "Active";
                lblProductStatus.ForeColor = Color.Green;
            }
            else
            {
                lblProductStatus.Text = "Archived";
                lblProductStatus.ForeColor = Color.Red;
            }
        }
    }
}

