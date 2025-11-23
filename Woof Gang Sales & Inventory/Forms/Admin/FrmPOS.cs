using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web.SessionState;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Forms.Controls;
using Woof_Gang_Sales___Inventory.Helpers;
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

        private int btnHeight = 28;
        // We don't need btnWidth for the Remove button, we hardcode it to 30 like in the PO form

        // Hover State Variables
        private int hoveredRowIndex = -1;
        private bool isHoveringButton = false;


        public FrmPOS()
        {
            InitializeComponent();

            // Wire up event handlers HERE (in the constructor)
            cmbCategoryFilter.SelectedIndexChanged += cmbCategoryFilter_SelectedIndexChanged;
            cmbSubCategoryFilter.SelectedIndexChanged += Filter_Changed;
            txtSearchProduct.TextChanged += Filter_Changed;
            //cmbDiscount.SelectedIndexChanged += (s, ev) => RefreshCartGrid();

            dgvCart.CellFormatting += dgvCart_CellFormatting;
            dgvCart.CellValueChanged += dgvCart_CellValueChanged;
            dgvCart.CellPainting += dgvCart_CellPainting;
            dgvCart.CellMouseMove += dgvCart_CellMouseMove;
            dgvCart.CellMouseLeave += dgvCart_CellMouseLeave;

            // Note: We changed ContentClick to MouseClick for better hit-testing
            dgvCart.CellMouseClick += dgvCart_CellMouseClick;
            dgvCart.CellValueChanged += dgvCart_CellValueChanged;

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
            //LoadDiscountFilter();

            // Manually trigger the category change event
            cmbCategoryFilter_SelectedIndexChanged(null, null);

            // Now this will work, because both ComboBoxes are full.
            LoadProductsToPanel();

            SetupCartGrid();
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
                Width = 200
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "Qty",
                DataPropertyName = "Quantity",
                ReadOnly = false, // <-- This allows editing
                Width = 80
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Subtotal",
                HeaderText = "Cost",
                DataPropertyName = "Subtotal",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = { Format = "N2" }
            });

            dgvCart.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Remove",
                HeaderText = "Action",
                Text = "", // The text on the button
                UseColumnTextForButtonValue = false, // This makes the "X" appear
                Width = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Resizable = DataGridViewTriState.False,
            });

            DataGridViewStyler.ApplyStyle(dgvCart);
        }

        private void dgvCart_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Check the Column NAME (which is "ProductName"), not the HeaderText "Item"
            if (dgvCart.Columns[e.ColumnIndex].Name == "ProductName" && e.RowIndex >= 0)
            {
                // 2. Cast to CartItem (NOT Product) because shoppingCart is List<CartItem>
                var item = dgvCart.Rows[e.RowIndex].DataBoundItem as CartItem;

                if (item != null)
                {
                    // 3. Use the properties from CartItem
                    string brand = item.ProductBrand ?? "";
                    string name = item.ProductName ?? "";

                    // Combine them
                    e.Value = string.IsNullOrWhiteSpace(brand) ? name : $"{brand} {name}";
                    e.FormattingApplied = true;
                }
            }
        }

        private void DrawRoundedButton(Graphics g, Rectangle rect, Color color, string text)
        {
            using (GraphicsPath path = GetRoundedPath(rect, 4)) // 4 is the corner radius
            using (SolidBrush brush = new SolidBrush(color))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillPath(brush, path);
            }
            TextRenderer.DrawText(g, text, this.Font, rect, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
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

        // 1. PAINTING (Draws the Red Button)
        private void dgvCart_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvCart.Columns[e.ColumnIndex].Name == "Remove")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);

                // Dimensions (Same as PO Form)
                int smallWidth = 30;
                int startX = e.CellBounds.X + (e.CellBounds.Width - smallWidth) / 2;
                int startY = e.CellBounds.Y + (e.CellBounds.Height - btnHeight) / 2;
                Rectangle btnRect = new Rectangle(startX, startY, smallWidth, btnHeight);

                // Hover Logic (Dark Red vs Light Red)
                Color btnColor = (e.RowIndex == hoveredRowIndex && isHoveringButton)
                    ? Color.FromArgb(255, 100, 100) // Lighter Red (Hover)
                    : Color.FromArgb(220, 53, 69);  // Standard Red

                DrawRoundedButton(e.Graphics, btnRect, btnColor, "X");
                e.Handled = true;
            }
        }

        // 2. MOUSE MOVE (Detects Hover)
        private void dgvCart_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvCart.Columns[e.ColumnIndex].Name == "Remove")
            {
                int w = dgvCart.Columns[e.ColumnIndex].Width;
                int h = dgvCart.Rows[e.RowIndex].Height;
                int startX = (w - 30) / 2;
                int startY = (h - btnHeight) / 2;

                // Check if mouse is exactly over the button rectangle
                bool isOver = (e.X >= startX && e.X <= startX + 30) &&
                              (e.Y >= startY && e.Y <= startY + btnHeight);

                dgvCart.Cursor = isOver ? Cursors.Hand : Cursors.Default;

                // Only repaint if the state changed (Optimization)
                if (hoveredRowIndex != e.RowIndex || isHoveringButton != isOver)
                {
                    hoveredRowIndex = e.RowIndex;
                    isHoveringButton = isOver;
                    dgvCart.InvalidateCell(e.ColumnIndex, e.RowIndex);
                }
            }
            else
            {
                dgvCart.Cursor = Cursors.Default;
            }
        }

        // 3. MOUSE LEAVE (Resets Hover)
        private void dgvCart_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvCart.Cursor = Cursors.Default;
            hoveredRowIndex = -1;
            isHoveringButton = false;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                dgvCart.InvalidateCell(e.ColumnIndex, e.RowIndex);
        }

        // 4. MOUSE CLICK (Performs the Delete)
        private void dgvCart_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvCart.Columns[e.ColumnIndex].Name == "Remove")
            {
                // Hit Test: Did they click the button, or just the white space?
                int w = dgvCart.Columns[e.ColumnIndex].Width;
                int startX = (w - 30) / 2;

                if (e.X >= startX && e.X <= startX + 30)
                {
                    if (dgvCart.Rows[e.RowIndex].DataBoundItem is CartItem itemToRemove)
                    {
                        shoppingCart.Remove(itemToRemove);

                        // Refresh Grid
                        this.BeginInvoke(new Action(() => { RefreshCartGrid(); }));
                    }
                }
            }
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

        /*
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
        */


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
            var bindingList = new BindingList<CartItem>(shoppingCart);
            dgvCart.DataSource = bindingList;

            if (shoppingCart.Count == 0)
            {
                dgvCart.DataSource = null;
            }
            // --- End of Fix ---

            // ✅ LOGIC SIMPLIFIED: Just sum the items. No discounts.
            decimal total = shoppingCart.Sum(item => item.Subtotal);

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

            int? discountId = null;

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
                    shoppingCart.Clear();
                    RefreshCartGrid();
                    LoadProductsToPanel();
                }
            }
        }

        #endregion

    }
}