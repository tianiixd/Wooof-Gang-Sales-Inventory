using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Helpers;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;
using Guna.UI2.WinForms;
using System.Drawing.Drawing2D;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmCreateEditPurchaseOrder : Form
    {
        // ... (Repositories and State variables remain the same) ...
        private readonly PurchaseOrderRepository _poRepo;
        private readonly ProductRepository _productRepo;
        private readonly SupplierRepository _supplierRepo;

        public bool IsEditMode { get; set; } = false;
        private int _poID = 0;
        private int _lastSelectedProductId = -1;
        private BindingList<PurchaseOrderDetail> _cartItems;
        private List<Product> _availableProducts;

        // UI Constants
        private int btnWidth = 45;
        private int btnHeight = 28;
        private int iconSize = 16;

        private int hoveredRowIndex_Left = -1;
        private int hoveredRowIndex_Right = -1;
        private bool isHoveringButton_Left = false;
        private bool isHoveringButton_Right = false;

        public FrmCreateEditPurchaseOrder(int poID = 0)
        {
            InitializeComponent();
            _poRepo = new PurchaseOrderRepository();
            _productRepo = new ProductRepository();
            _supplierRepo = new SupplierRepository();

            _poID = poID;
            _cartItems = new BindingList<PurchaseOrderDetail>();

            // Wire up events
            cmbSupplier.SelectedIndexChanged += cmbSupplier_SelectedIndexChanged;
            
            // Left Grid eto
            dgvAvailableProducts.CellPainting += dgvAvailableProducts_CellPainting;
            dgvAvailableProducts.CellMouseClick += dgvAvailableProducts_CellMouseClick;
            dgvAvailableProducts.CellMouseMove += dgvAvailableProducts_CellMouseMove;
            dgvAvailableProducts.CellMouseLeave += dgvAvailableProducts_CellMouseLeave;

            // Right Grid eto
            dgvOrderItems.CellPainting += dgvOrderItems_CellPainting;
            dgvOrderItems.CellMouseClick += dgvOrderItems_CellMouseClick;
            dgvOrderItems.CellMouseMove += dgvOrderItems_CellMouseMove;
            dgvOrderItems.CellMouseLeave += dgvOrderItems_CellMouseLeave;
            dgvOrderItems.CellValueChanged += dgvOrderItems_CellValueChanged;

            dtpOrderDate.MaxDate = DateTime.Today.AddYears(100);
            dtpOrderDate.MinDate = DateTime.Today;
        }

        private void FrmCreateEditPurchaseOrder_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
            SetupGrids();

            // ✅ --- FIX 1: Reset Date Constraints ---
            // Ensure the control accepts any date before we try to set it.
            // This fixes the "Value not valid" error.
            

            if (_poID > 0)
            {
                IsEditMode = true;
                lblTitle.Text = $"Edit Purchase Order #{_poID}";
                LoadOrderData(_poID);
            }
            else
            {
                IsEditMode = false;
                lblTitle.Text = "Create New Purchase Order";

                // Now safe to set to Today
                dtpOrderDate.Value = DateTime.Today;
            }
        }

        // ... (LoadSuppliers and SetupGrids remain the same) ...
        private void LoadSuppliers()
        {
            var suppliers = _supplierRepo.GetSuppliers("");
            cmbSupplier.DataSource = suppliers;
            cmbSupplier.DisplayMember = "SupplierName";
            cmbSupplier.ValueMember = "SupplierID";
            cmbSupplier.SelectedIndex = -1;
        }

        private void SetupGrids()
        {
            // Left Grid
            dgvAvailableProducts.AutoGenerateColumns = false;
            dgvAvailableProducts.Columns.Clear();

            dgvAvailableProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductID", DataPropertyName = "ProductID", Visible = false });
            dgvAvailableProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "Product", DataPropertyName = "ProductName", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true});
            dgvAvailableProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "CurrentStock", HeaderText = "Stock", DataPropertyName = "Quantity", Width = 80 , ReadOnly = true } );
            var costCol = new DataGridViewTextBoxColumn { Name = "Cost", HeaderText = "Cost", DataPropertyName = "CostPrice", Width = 100, ReadOnly = true };
            costCol.DefaultCellStyle.Format = "N2";
            dgvAvailableProducts.Columns.Add(costCol);

            DataGridViewButtonColumn addBtn = new DataGridViewButtonColumn();
            addBtn.Name = "Add";
            addBtn.HeaderText = "Action";
            addBtn.Text = "";
            addBtn.UseColumnTextForButtonValue = false;
            addBtn.Width = 80;
            addBtn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            addBtn.Resizable = DataGridViewTriState.False;
            dgvAvailableProducts.Columns.Add(addBtn);

            // Right Grid
            dgvOrderItems.AutoGenerateColumns = false;
            dgvOrderItems.Columns.Clear();
            dgvOrderItems.DataSource = _cartItems;

            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductID", DataPropertyName = "ProductID", Visible = false });
            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "Product", DataPropertyName = "ProductName", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Qty", DataPropertyName = "Quantity", Width = 80, ReadOnly = false });
            var unitCostCol = new DataGridViewTextBoxColumn { Name = "UnitCost", HeaderText = "Cost", DataPropertyName = "UnitCost", Width = 100, ReadOnly = true };
            unitCostCol.DefaultCellStyle.Format = "N2";
            dgvOrderItems.Columns.Add(unitCostCol);
            var subtotalCol = new DataGridViewTextBoxColumn { Name = "Subtotal", HeaderText = "Subtotal", Width = 100, ReadOnly = true };
            subtotalCol.DefaultCellStyle.Format = "N2";
            dgvOrderItems.Columns.Add(subtotalCol);

            DataGridViewButtonColumn delBtn = new DataGridViewButtonColumn();
            delBtn.Name = "Remove";
            delBtn.HeaderText = "Action";
            delBtn.Text = "";
            delBtn.UseColumnTextForButtonValue = false;
            delBtn.Width = 80;
            delBtn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            delBtn.Resizable = DataGridViewTriState.False;
            dgvOrderItems.Columns.Add(delBtn);
            DataGridViewStyler.ApplyStyle(dgvAvailableProducts);
            DataGridViewStyler.ApplyStyle(dgvOrderItems);
            dgvAvailableProducts.CellFormatting += dgvAvailableProducts_CellFormatting;   
        }

        // ✅ --- FIX 2: Added ColumnIndex Check ---
        private void dgvAvailableProducts_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Must check BOTH RowIndex and ColumnIndex to avoid crashes on headers
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvAvailableProducts.Columns[e.ColumnIndex].Name == "Add")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);

                int startX = e.CellBounds.X + (e.CellBounds.Width - btnWidth) / 2;
                int startY = e.CellBounds.Y + (e.CellBounds.Height - btnHeight) / 2;
                Rectangle btnRect = new Rectangle(startX, startY, btnWidth, btnHeight);

                Color btnColor = (e.RowIndex == hoveredRowIndex_Left && isHoveringButton_Left)
                    ? Color.FromArgb(35, 160, 100) // Lighter Green
                    : Color.FromArgb(25, 135, 84);

                DrawRoundedButton(e.Graphics, btnRect, btnColor, "Add");

                e.Handled = true;
            }
        }

        private void dgvAvailableProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if we are formatting the "ProductName" column and the row is valid
            if (dgvAvailableProducts.Columns[e.ColumnIndex].Name == "ProductName" && e.RowIndex >= 0)
            {
                // Get the actual Product object
                var product = dgvAvailableProducts.Rows[e.RowIndex].DataBoundItem as Product;

                if (product != null)
                {
                    // Combine Brand and Name
                    string brand = product.Brand ?? "";
                    string name = product.ProductName ?? "";

                    // If Brand exists, put it in front. Otherwise just show Name.
                    e.Value = string.IsNullOrWhiteSpace(brand) ? name : $"{brand} {name}";
                    e.FormattingApplied = true;
                }
            }
        }

        // ✅ --- FIX 2: Added ColumnIndex Check ---
        private void dgvOrderItems_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Must check BOTH RowIndex and ColumnIndex
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvOrderItems.Columns[e.ColumnIndex].Name == "Remove")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);


                int smallWidth = 30;
                int startX = e.CellBounds.X + (e.CellBounds.Width - smallWidth) / 2;
                int startY = e.CellBounds.Y + (e.CellBounds.Height - btnHeight) / 2;
                Rectangle btnRect = new Rectangle(startX, startY, smallWidth, btnHeight);

                Color btnColor = (e.RowIndex == hoveredRowIndex_Right && isHoveringButton_Right)
                    ? Color.FromArgb(255, 100, 100) // Lighter Red
                    : Color.FromArgb(220, 53, 69);


                DrawRoundedButton(e.Graphics, btnRect, btnColor, "X");

                e.Handled = true;
            }
        }

        // ... (The rest of the methods: DrawRoundedButton, GetRoundedPath, Click Logic, LoadOrderData, etc. remain exactly the same) ...
        private void DrawRoundedButton(Graphics g, Rectangle rect, Color color, string text)
        {
            using (GraphicsPath path = GetRoundedPath(rect, 4))
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

        private void dgvAvailableProducts_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var product = (Product)dgvAvailableProducts.Rows[e.RowIndex].DataBoundItem;

            // 1. Auto-fill the Cost Price TextBox (As discussed)

            if (product.ProductID != _lastSelectedProductId)
            {
                if (txtCostPrice != null)
                {
                    txtCostPrice.Text = product.CostPrice.ToString("N2");
                }
                // Update our tracker
                _lastSelectedProductId = product.ProductID;
            }


            // 2. Add Button Logic
            if (dgvAvailableProducts.Columns[e.ColumnIndex].Name == "Add")
            {
                // Create the Full Name String
                string fullName = string.IsNullOrWhiteSpace(product.Brand)
                                  ? product.ProductName
                                  : $"{product.Brand} {product.ProductName}";


                decimal finalCost = product.CostPrice; // Default fallback

                // Try to parse what you typed (e.g., "500")
                if (txtCostPrice != null && decimal.TryParse(txtCostPrice.Text, out decimal customCost))
                {
                    finalCost = customCost; // Use 500
                }


                // Check if item exists in cart
                var existing = _cartItems.FirstOrDefault(x => x.ProductID == product.ProductID);

                if (existing != null)
                {
                    existing.Quantity++;
                    existing.UnitCost = finalCost;
                    _cartItems.ResetBindings();
                }
                else
                {
                    
                    _cartItems.Add(new PurchaseOrderDetail
                    {
                        ProductID = product.ProductID,
                        ProductName = fullName, // Uses Brand + Name
                        Quantity = 1,
                        UnitCost = finalCost    // Uses the price from the Textbox
                    });
                }
                CalculateTotals();
            }
        }

        private void dgvOrderItems_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvOrderItems.Columns[e.ColumnIndex].Name == "Remove")
            {
                int w = dgvOrderItems.Columns[e.ColumnIndex].Width;
                int startX = (w - 30) / 2;
                if (e.X >= startX && e.X <= startX + 30)
                {
                    _cartItems.RemoveAt(e.RowIndex);
                    CalculateTotals();

                    if (txtCostPrice != null)
                    {
                        txtCostPrice.Text = ""; // or "0.00"
                        txtCostPrice.Focus();   // Bring focus back to it if needed
                    }

                }
            }
        }

        private void dgvAvailableProducts_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvAvailableProducts.Columns[e.ColumnIndex].Name == "Add")
            {
                int w = dgvAvailableProducts.Columns[e.ColumnIndex].Width;
                int h = dgvAvailableProducts.Rows[e.RowIndex].Height;
                int startX = (w - btnWidth) / 2;
                int startY = (h - btnHeight) / 2;

                bool isOver = (e.X >= startX && e.X <= startX + btnWidth) &&
                              (e.Y >= startY && e.Y <= startY + btnHeight);

                dgvAvailableProducts.Cursor = isOver ? Cursors.Hand : Cursors.Default;

                // Redraw only if state changed
                if (hoveredRowIndex_Left != e.RowIndex || isHoveringButton_Left != isOver)
                {
                    hoveredRowIndex_Left = e.RowIndex;
                    isHoveringButton_Left = isOver;
                    dgvAvailableProducts.InvalidateCell(e.ColumnIndex, e.RowIndex);
                }
            }
            else 
            {
                dgvAvailableProducts.Cursor = Cursors.Default;
            }
        }

        private void dgvAvailableProducts_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvAvailableProducts.Cursor = Cursors.Default;
            hoveredRowIndex_Left = -1;
            isHoveringButton_Left = false;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            dgvAvailableProducts.InvalidateCell(e.ColumnIndex, e.RowIndex);
        }
        private void dgvOrderItems_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvOrderItems.Columns[e.ColumnIndex].Name == "Remove")
            {
                int w = dgvOrderItems.Columns[e.ColumnIndex].Width;
                int h = dgvOrderItems.Rows[e.RowIndex].Height;
                int startX = (w - 30) / 2; // Width is 30 here
                int startY = (h - btnHeight) / 2;

                bool isOver = (e.X >= startX && e.X <= startX + 30) &&
                              (e.Y >= startY && e.Y <= startY + btnHeight);

                dgvOrderItems.Cursor = isOver ? Cursors.Hand : Cursors.Default;

                if (hoveredRowIndex_Right != e.RowIndex || isHoveringButton_Right != isOver)
                {
                    hoveredRowIndex_Right = e.RowIndex;
                    isHoveringButton_Right = isOver;
                    dgvOrderItems.InvalidateCell(e.ColumnIndex, e.RowIndex);
                }
            }
            else
            {
                dgvOrderItems.Cursor = Cursors.Default;
            }
        }
        private void dgvOrderItems_CellMouseLeave(object sender, DataGridViewCellEventArgs e) 
        {
            dgvOrderItems.Cursor = Cursors.Default;
            hoveredRowIndex_Right = -1;
            isHoveringButton_Right = false;
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            dgvOrderItems.InvalidateCell(e.ColumnIndex, e.RowIndex);

        }

        private void LoadOrderData(int poID)
        {
            var (po, details) = _poRepo.GetPurchaseOrderForEdit(poID);
            if (po == null) { this.Close(); return; }
            cmbSupplier.SelectedValue = po.SupplierID;
            dtpOrderDate.Value = po.PODate;
            txtRemarks.Text = po.Remarks;
            foreach (var d in details)
            {
                _cartItems.Add(new PurchaseOrderDetail { ProductID = d.ProductID, ProductName = d.ProductName, Quantity = d.Quantity, UnitCost = d.UnitCost });
            }
            CalculateTotals();
        }

        public void LoadAutoRestockItems(List<Product> itemsToRestock)
        {
            if (itemsToRestock == null || itemsToRestock.Count == 0) return;
            int supplierId = itemsToRestock[0].SupplierID ?? 0;
            if (supplierId > 0)
            {
                cmbSupplier.SelectedValue = supplierId;
                cmbSupplier.Enabled = false;
            }
            foreach (var product in itemsToRestock)
            {
                int targetStock = (product.ReorderLevel ?? 5) + 20;
                int qtyToOrder = targetStock - product.Quantity;
                if (qtyToOrder <= 0) qtyToOrder = 10;

                _cartItems.Add(new PurchaseOrderDetail
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    Quantity = qtyToOrder,
                    UnitCost = product.SellingPrice
                });
            }
            CalculateTotals();
            lblTitle.Text = "Auto-Restock Purchase Order";
        }

        private void LoadProductsForSupplier(int supplierId)
        {
            var allProducts = _productRepo.GetProducts("");
            _availableProducts = allProducts.Where(p => p.SupplierID == supplierId).ToList();
            dgvAvailableProducts.DataSource = _availableProducts;
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSupplier.SelectedValue != null && int.TryParse(cmbSupplier.SelectedValue.ToString(), out int supplierId))
            {
                if (_cartItems.Count > 0)
                {
                    var result = DialogHelper.ShowConfirmDialog("Change Supplier?", "Changing supplier will clear your current cart. Continue?", "warning");
                    if (result == DialogResult.No) { return; }
                    _cartItems.Clear();
                    CalculateTotals();
                }
                LoadProductsForSupplier(supplierId);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_availableProducts == null) return;
            string search = txtSearch.Text.ToLower();

            var filtered = _availableProducts.Where(p =>
                p.ProductName.ToLower().Contains(search) ||
                (p.Brand != null && p.Brand.ToLower().Contains(search))).ToList();

            dgvAvailableProducts.DataSource = filtered;
        }

        private void dgvOrderItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            CalculateTotals();
        }

        private void CalculateTotals()
        {
            decimal grandTotal = 0;
            foreach (DataGridViewRow row in dgvOrderItems.Rows)
            {
                if (row.DataBoundItem is PurchaseOrderDetail item)
                {
                    decimal subtotal = item.Quantity * item.UnitCost;
                    row.Cells["Subtotal"].Value = subtotal;
                    grandTotal += subtotal;
                }
            }
            if (lblTotal != null) lblTotal.Text = grandTotal.ToString("C");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbSupplier.SelectedValue == null) { DialogHelper.ShowCustomDialog("Error", "Please select a supplier.", "error"); return; }
            if (_cartItems.Count == 0) { DialogHelper.ShowCustomDialog("Error", "Please add items to the order.", "error"); return; }

            PurchaseOrder po = new PurchaseOrder
            {
                POID = _poID,
                SupplierID = (int)cmbSupplier.SelectedValue,
                OrderedBy = SessionManager.CurrentUser?.UserID,
                PODate = dtpOrderDate.Value,
                Remarks = txtRemarks.Text.Trim()
            };

            bool success = false;
            if (IsEditMode){
                success = _poRepo.UpdatePurchaseOrder(po, _cartItems.ToList());
            }
            else
            {
                int newId = _poRepo.CreatePurchaseOrder(po, _cartItems.ToList());
                success = newId > 0;
            }

            if (success)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}