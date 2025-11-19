using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.SessionState;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Forms.Controls;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmPOS : Sample
    {
        private readonly ProductRepository productRepo = new ProductRepository();
        private readonly CategoryRepository categoryRepo = new CategoryRepository();
        private readonly SubCategoryRepository subCategoryRepo = new SubCategoryRepository();
        private readonly DiscountRepository discountRepo = new DiscountRepository();

        TimeClockHelper time = new TimeClockHelper();

        // This form no longer needs the SalesRepository. FrmPayment handles it.
        // private readonly SalesRepository salesRepo = new SalesRepository();

        private List<CartItem> shoppingCart = new List<CartItem>();
        private List<SubCategory> allSubCategories = new List<SubCategory>();
        private List<Discount> allDiscounts = new List<Discount>();

        public FrmPOS()
        {
            InitializeComponent();

            // Wire up event handlers HERE (in the constructor)
            cmbCategoryFilter.SelectedIndexChanged += cmbCategoryFilter_SelectedIndexChanged;
            cmbSubCategoryFilter.SelectedIndexChanged += Filter_Changed;
            txtSearchProduct.TextChanged += Filter_Changed;
            cmbDiscount.SelectedIndexChanged += (s, ev) => RefreshCartGrid();

            dgvCart.CellValueChanged += dgvCart_CellValueChanged;
            dgvCart.CellContentClick += dgvCart_CellContentClick;

            dgvCart.CellMouseEnter += (s, e) =>
            {
                if (e.ColumnIndex >= 0 && dgvCart.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    dgvCart.Cursor = Cursors.Hand;
                }
            };

            dgvCart.CellMouseLeave += (s, e) =>
            {
                dgvCart.Cursor = Cursors.Default;
            };

        }

        private void FrmPOS_Load(object sender, EventArgs e)
        {
            time.StartClock(lblTime, lblDate);

            LoadCategoryFilter();
            LoadAllSubCategories();
            LoadDiscountFilter();

            // Manually trigger the category change event
            cmbCategoryFilter_SelectedIndexChanged(null, null);

            // Now this will work, because both ComboBoxes are full.
            LoadProductsToPanel();

            SetupCartGrid();
        }

        #region Load Products & Categories

        private void LoadCategoryFilter()
        {
            try
            {
                var allCategories = new Category { CategoryID = 0, CategoryName = "All Categories" };
                var realCategories = categoryRepo.GetCategories("");
                var categoryDataSource = new List<Category>();
                categoryDataSource.Add(allCategories);
                categoryDataSource.AddRange(realCategories);

                cmbCategoryFilter.DisplayMember = "CategoryName";
                cmbCategoryFilter.ValueMember = "CategoryID";
                cmbCategoryFilter.DataSource = categoryDataSource;
                cmbCategoryFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Loading Categories", ex.Message, "error");
            }
        }

        private void LoadAllSubCategories()
        {
            allSubCategories = subCategoryRepo.GetSubCategories("");
        }

        private void LoadDiscountFilter()
        {
            try
            {
                var noDiscount = new Discount { DiscountID = 0, DiscountName = "No Discount" };
                // ✅ --- FIX: Corrected typo ---
                allDiscounts = new List<Discount>();
                allDiscounts = discountRepo.GetActiveDiscounts();

                var discountDataSource = new List<Discount>();
                discountDataSource.Add(noDiscount);
                discountDataSource.AddRange(allDiscounts);

                cmbDiscount.DisplayMember = "DiscountName";
                cmbDiscount.ValueMember = "DiscountID";
                cmbDiscount.DataSource = discountDataSource;
                cmbDiscount.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Error Loading Discounts", ex.Message, "error");
            }
        }


        private void cmbCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategoryFilter.SelectedValue == null) return;

            int selectedCategoryID = (int)cmbCategoryFilter.SelectedValue;

            var allSubs = new SubCategory { SubCategoryID = 0, SubCategoryName = "All Subcategories" };

            List<SubCategory> filteredSubs;
            if (selectedCategoryID == 0)
            {
                filteredSubs = allSubCategories;
            }
            else
            {
                filteredSubs = allSubCategories
                    .Where(s => s.CategoryID == selectedCategoryID)
                    .ToList();
            }

            var subCategoryDataSource = new List<SubCategory>();
            subCategoryDataSource.Add(allSubs);
            subCategoryDataSource.AddRange(filteredSubs);

            cmbSubCategoryFilter.DisplayMember = "SubCategoryName";
            cmbSubCategoryFilter.ValueMember = "SubCategoryID";
            cmbSubCategoryFilter.DataSource = subCategoryDataSource;
            cmbSubCategoryFilter.SelectedIndex = 0;
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            LoadProductsToPanel();
        }

        public void LoadProductsToPanel()
        {
            if (cmbCategoryFilter.SelectedValue == null || cmbSubCategoryFilter.SelectedValue == null) return;

            flpProducts.Controls.Clear();

            string search = txtSearchProduct.Text;
            int categoryId = (int)cmbCategoryFilter.SelectedValue;
            int subCategoryId = (int)cmbSubCategoryFilter.SelectedValue;

            List<Product> productsToShow;
            productsToShow = productRepo.GetProducts(search);

            if (categoryId != 0)
            {
                productsToShow = productsToShow
                    .Where(p => p.CategoryID == categoryId)
                    .ToList();
            }

            if (subCategoryId != 0)
            {
                productsToShow = productsToShow
                    .Where(p => p.SubCategoryID == subCategoryId)
                    .ToList();
            }

            foreach (Product p in productsToShow)
            {
                ProductCard card = new ProductCard();
                card.ProductID = p.ProductID;
                card.ProductBrand = p.Brand;
                card.ProductTitle = p.ProductName;
                card.ProductImage = LoadImageWithoutLock(p.ImagePath);
                card.UnitPrice = p.SellingPrice;

                // ✅ --- PASS ALL DATA TO THE CARD ---
                card.ProductStock = p.Quantity;
                card.ExpirationDate = p.ExpirationDate; // Pass the expiration date

                flpProducts.Controls.Add(card);
                card.CardClicked += ProductCard_Clicked;
            }
        }

        #endregion

        #region Shopping Cart Logic

        private void ProductCard_Clicked(object sender, EventArgs e)
        {
            ProductCard clickedCard = (ProductCard)sender;

            // ✅ --- ADD THIS CHECK ---
            // Only add to cart if the card says it's sellable
            if (clickedCard.IsSellable)
            {
                AddToCart(clickedCard.ProductID, clickedCard.ProductBrand, clickedCard.ProductTitle, clickedCard.UnitPrice);
            }
            else
            {
                // Optional: Play a "beep" sound or flash the card
                System.Media.SystemSounds.Exclamation.Play();
            }
        }

        private void AddToCart(int productID, string brand, string name, decimal price)
        {
            int currentStock = productRepo.GetStock(productID);

            CartItem existingItem = shoppingCart.FirstOrDefault(item => item.ProductID == productID);

            if (existingItem != null)
            {
                if (existingItem.Quantity >= currentStock)
                {
                    DialogHelper.ShowCustomDialog("Stock Limit", $"No more stock available for '{name}'. You only have {currentStock}.", "warning");
                    return;
                }
                existingItem.Quantity++;
            }
            else
            {
                // This check is still good as a fallback, but the
                // IsSellable flag on the card should prevent this.
                if (currentStock <= 0)
                {
                    DialogHelper.ShowCustomDialog("Out of Stock", $"'{name}' is out of stock.", "warning");
                    return;
                }
                shoppingCart.Add(new CartItem
                {
                    ProductID = productID,
                    ProductBrand = brand,
                    ProductName = name,
                    UnitPrice = price,
                    Quantity = 1
                });
            }

            // Use BeginInvoke to safely refresh the grid
            this.BeginInvoke(new Action(() => { RefreshCartGrid(); }));
        }

        private void SetupCartGrid()
        {
            // ✅ --- ADD THIS LINE ---
            dgvCart.AllowUserToAddRows = false; // Remove the blank "new row"
            dgvCart.AutoGenerateColumns = false;
            dgvCart.Columns.Clear();

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                HeaderText = "Item",
                DataPropertyName = "ProductName",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "Qty",
                DataPropertyName = "Quantity",
                ReadOnly = false, // <-- This allows editing
                Width = 40
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Subtotal",
                HeaderText = "Cost",
                DataPropertyName = "Subtotal",
                ReadOnly = true,
                Width = 70,
                DefaultCellStyle = { Format = "N2" }
            });

            dgvCart.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Remove",
                HeaderText = "",
                Text = "X", // The text on the button
                UseColumnTextForButtonValue = true, // This makes the "X" appear
                Width = 30,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = { BackColor = Color.White, ForeColor = Color.Red, }
            });

            dgvCart.CellPainting += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == dgvCart.Columns["Remove"].Index)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentBackground);
                    e.Handled = true;
                }
            };
        }

        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if it's the "Remove" button column
            if (e.RowIndex >= 0 && dgvCart.Columns[e.ColumnIndex].Name == "Remove")
            {
                if (dgvCart.Rows[e.RowIndex].DataBoundItem is CartItem itemToRemove)
                {
                    shoppingCart.Remove(itemToRemove);
                }
                // Use BeginInvoke to safely refresh the grid
                this.BeginInvoke(new Action(() => { RefreshCartGrid(); }));
            }
        }

        private void dgvCart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Check if it's the "Quantity" column and the row is valid
            if (e.RowIndex >= 0 && dgvCart.Columns[e.ColumnIndex].Name == "Quantity")
            {
                if (!int.TryParse(dgvCart.Rows[e.RowIndex].Cells["Quantity"].Value.ToString(), out int newQty))
                {
                    newQty = 1;
                }

                if (dgvCart.Rows[e.RowIndex].DataBoundItem is CartItem item)
                {
                    int maxStock = productRepo.GetStock(item.ProductID);

                    if (newQty <= 0)
                    {
                        shoppingCart.Remove(item);
                    }
                    else if (newQty > maxStock)
                    {
                        DialogHelper.ShowCustomDialog("Stock Limit", $"You only have {maxStock} of this item.", "warning");
                        item.Quantity = maxStock;
                    }
                    else
                    {
                        item.Quantity = newQty;
                    }
                }
                // ✅ --- FIX FOR REENTRANT CALL ---
                // Safely refresh the grid *after* the event is finished.
                this.BeginInvoke(new Action(() => { RefreshCartGrid(); }));
            }
        }


        private void RefreshCartGrid()
        {
            // --- FIX for Reentrant Call ---
            // Use a temporary BindingList to prevent grid errors
            var bindingList = new BindingList<CartItem>(shoppingCart);
            dgvCart.DataSource = bindingList;

            if (shoppingCart.Count == 0)
            {
                dgvCart.DataSource = null;
            }
            // --- End of Fix ---


            if (cmbDiscount == null) return;

            decimal subtotal = shoppingCart.Sum(item => item.Subtotal);

            int discountID = 0;
            if (cmbDiscount.SelectedValue != null)
            {
                int.TryParse(cmbDiscount.SelectedValue.ToString(), out discountID);
            }

            decimal total = subtotal;
            decimal discountAmount = 0;

            if (discountID > 0)
            {
                Discount? selectedDiscount = allDiscounts.FirstOrDefault(d => d.DiscountID == discountID);
                if (selectedDiscount != null)
                {
                    if (selectedDiscount.DiscountType == "Percentage")
                    {
                        decimal percent = selectedDiscount.Value / 100m;
                        discountAmount = subtotal * percent;
                        total = subtotal - discountAmount;
                    }
                    else // "Fixed"
                    {
                        discountAmount = selectedDiscount.Value;
                        total = subtotal - discountAmount;
                    }
                }
            }

            lblPrice.Text = total.ToString("C");
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (shoppingCart.Count > 0)
            {
                var result = DialogHelper.ShowConfirmDialog("Clear Cart", "Are you sure you want to clear the current order?", "warning");
                if (result == DialogResult.Yes)
                {
                    shoppingCart.Clear();
                    RefreshCartGrid();
                }
            }
        }


        #endregion

        #region Image Helpers (Copied from FrmCreateEditUser)

        private Image LoadImageWithoutLock(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return null;

            try
            {
                using (var tempImage = Image.FromFile(path))
                {
                    return new Bitmap(tempImage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading image: " + ex.Message);
                return null;
            }
        }

        #endregion

        #region Checkout Logic


        private void btnPay_Click(object sender, EventArgs e)
        {
            if (shoppingCart.Count == 0)
            {
                DialogHelper.ShowCustomDialog("Empty Cart", "Please add items to the cart before processing payment.", "warning");
                return;
            }

            // --- 1. Get Total & Discount Info from the UI ---

            decimal.TryParse(lblPrice.Text, System.Globalization.NumberStyles.Currency,
                             System.Globalization.CultureInfo.CurrentCulture, out decimal totalAmount);

            int? discountId = (int)cmbDiscount.SelectedValue > 0 ? (int)cmbDiscount.SelectedValue : (int?)null;

            // ✅ --- Get the user from the global session ---
            User? loggedInUser = SessionManager.CurrentUser;

            if (loggedInUser == null)
            {
                DialogHelper.ShowCustomDialog("Login Error", "Could not find the logged-in user. Please log out and log back in.", "error");
                return;
            }

            // --- 2. Open the "Smart" Payment Form ---
            using (FrmPayment paymentForm = new FrmPayment())
            {
                // 3. Pass all the data to the payment form
                paymentForm.LoadPaymentDetails(totalAmount, discountId, shoppingCart, loggedInUser);

                // 4. Show the form
                var result = paymentForm.ShowDialog();

                // 5. Check if it was successful
                if (result == DialogResult.OK)
                {
                    // Success! The payment form already showed the success message
                    // and saved everything to the database.
                    // We just need to clear the cart in FrmPOS.
                    shoppingCart.Clear();
                    cmbDiscount.SelectedIndex = 0;
                    // txtCustomerName.Text = ""; // Clear customer if you have one
                    RefreshCartGrid();
                    LoadProductsToPanel();
                }
                // else
                // {
                //   // User clicked "Cancel" on the payment form.
                //   // Do nothing. The cart remains as-is.
                // }
            }
        }

        #endregion

    }
}