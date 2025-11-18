namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    partial class FrmPOS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlCart = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDiscount = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.btnPay = new Guna.UI2.WinForms.Guna2Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.pnlCartTop = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClearAll = new Guna.UI2.WinForms.Guna2Button();
            this.pnlCenter = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2VSeparator1 = new Guna.UI2.WinForms.Guna2VSeparator();
            this.flpProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlFilters = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.cmbSubCategoryFilter = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbCategoryFilter = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtSearchProduct = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.pnlCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.pnlCartTop.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCart
            // 
            this.pnlCart.Controls.Add(this.dgvCart);
            this.pnlCart.Controls.Add(this.guna2Panel1);
            this.pnlCart.Controls.Add(this.pnlCartTop);
            this.pnlCart.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCart.Location = new System.Drawing.Point(1324, 0);
            this.pnlCart.Name = "pnlCart";
            this.pnlCart.Size = new System.Drawing.Size(350, 1031);
            this.pnlCart.TabIndex = 0;
            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToResizeColumns = false;
            this.dgvCart.AllowUserToResizeRows = false;
            this.dgvCart.BackgroundColor = System.Drawing.Color.White;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Inter", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(19)))), ((int)(((byte)(26)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCart.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCart.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.dgvCart.Location = new System.Drawing.Point(3, 91);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.Size = new System.Drawing.Size(342, 711);
            this.dgvCart.TabIndex = 2;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.label2);
            this.guna2Panel1.Controls.Add(this.cmbDiscount);
            this.guna2Panel1.Controls.Add(this.lblPrice);
            this.guna2Panel1.Controls.Add(this.btnPay);
            this.guna2Panel1.Controls.Add(this.lblTotal);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 803);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(350, 228);
            this.guna2Panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Inter", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(44)))), ((int)(((byte)(73)))));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.label2.Location = new System.Drawing.Point(7, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 28);
            this.label2.TabIndex = 10;
            this.label2.Text = "Discount";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDiscount
            // 
            this.cmbDiscount.BackColor = System.Drawing.Color.Transparent;
            this.cmbDiscount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.cmbDiscount.BorderRadius = 8;
            this.cmbDiscount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbDiscount.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDiscount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiscount.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbDiscount.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbDiscount.Font = new System.Drawing.Font("Nunito", 12F);
            this.cmbDiscount.ForeColor = System.Drawing.Color.Black;
            this.cmbDiscount.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbDiscount.ItemHeight = 41;
            this.cmbDiscount.ItemsAppearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDiscount.ItemsAppearance.ForeColor = System.Drawing.Color.Black;
            this.cmbDiscount.Location = new System.Drawing.Point(11, 100);
            this.cmbDiscount.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.cmbDiscount.Name = "cmbDiscount";
            this.cmbDiscount.Size = new System.Drawing.Size(230, 47);
            this.cmbDiscount.TabIndex = 9;
            this.cmbDiscount.TextOffset = new System.Drawing.Point(3, 0);
            this.cmbDiscount.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Inter", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(44)))), ((int)(((byte)(73)))));
            this.lblPrice.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblPrice.Location = new System.Drawing.Point(164, 7);
            this.lblPrice.Margin = new System.Windows.Forms.Padding(5);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(44, 48);
            this.lblPrice.TabIndex = 5;
            this.lblPrice.Text = "₱";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPay
            // 
            this.btnPay.Animated = true;
            this.btnPay.BorderRadius = 8;
            this.btnPay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPay.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPay.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPay.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPay.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPay.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(177)))), ((int)(((byte)(66)))));
            this.btnPay.Font = new System.Drawing.Font("Inter", 16F);
            this.btnPay.ForeColor = System.Drawing.Color.White;
            this.btnPay.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(150)))), ((int)(((byte)(48)))));
            this.btnPay.Location = new System.Drawing.Point(5, 158);
            this.btnPay.Margin = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.btnPay.Name = "btnPay";
            this.btnPay.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(30)))));
            this.btnPay.PressedDepth = 40;
            this.btnPay.Size = new System.Drawing.Size(340, 65);
            this.btnPay.TabIndex = 3;
            this.btnPay.Text = "Pay";
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Inter", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(44)))), ((int)(((byte)(73)))));
            this.lblTotal.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblTotal.Location = new System.Drawing.Point(7, 22);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(5);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(59, 28);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCartTop
            // 
            this.pnlCartTop.Controls.Add(this.label1);
            this.pnlCartTop.Controls.Add(this.btnClearAll);
            this.pnlCartTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCartTop.Location = new System.Drawing.Point(0, 0);
            this.pnlCartTop.Name = "pnlCartTop";
            this.pnlCartTop.Size = new System.Drawing.Size(350, 88);
            this.pnlCartTop.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Inter", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(44)))), ((int)(((byte)(73)))));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.label1.Location = new System.Drawing.Point(7, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sales Summary";
            // 
            // btnClearAll
            // 
            this.btnClearAll.Animated = true;
            this.btnClearAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnClearAll.BorderRadius = 8;
            this.btnClearAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearAll.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClearAll.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClearAll.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClearAll.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClearAll.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnClearAll.Font = new System.Drawing.Font("Inter", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClearAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(85)))), ((int)(((byte)(118)))));
            this.btnClearAll.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(85)))), ((int)(((byte)(118)))));
            this.btnClearAll.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.btnClearAll.Location = new System.Drawing.Point(258, 33);
            this.btnClearAll.Margin = new System.Windows.Forms.Padding(5);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.ShadowDecoration.BorderRadius = 8;
            this.btnClearAll.Size = new System.Drawing.Size(87, 42);
            this.btnClearAll.TabIndex = 1;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // pnlCenter
            // 
            this.pnlCenter.Controls.Add(this.guna2VSeparator1);
            this.pnlCenter.Controls.Add(this.flpProducts);
            this.pnlCenter.Controls.Add(this.pnlFilters);
            this.pnlCenter.Location = new System.Drawing.Point(0, 0);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(1321, 1031);
            this.pnlCenter.TabIndex = 1;
            // 
            // guna2VSeparator1
            // 
            this.guna2VSeparator1.FillColor = System.Drawing.Color.Black;
            this.guna2VSeparator1.Location = new System.Drawing.Point(1313, 0);
            this.guna2VSeparator1.Name = "guna2VSeparator1";
            this.guna2VSeparator1.Size = new System.Drawing.Size(15, 1064);
            this.guna2VSeparator1.TabIndex = 0;
            // 
            // flpProducts
            // 
            this.flpProducts.AutoScroll = true;
            this.flpProducts.Location = new System.Drawing.Point(2, 136);
            this.flpProducts.Name = "flpProducts";
            this.flpProducts.Size = new System.Drawing.Size(1316, 892);
            this.flpProducts.TabIndex = 1;
            // 
            // pnlFilters
            // 
            this.pnlFilters.Controls.Add(this.lblDate);
            this.pnlFilters.Controls.Add(this.lblTime);
            this.pnlFilters.Controls.Add(this.guna2Separator1);
            this.pnlFilters.Controls.Add(this.cmbSubCategoryFilter);
            this.pnlFilters.Controls.Add(this.cmbCategoryFilter);
            this.pnlFilters.Controls.Add(this.txtSearchProduct);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Location = new System.Drawing.Point(0, 0);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Size = new System.Drawing.Size(1321, 135);
            this.pnlFilters.TabIndex = 0;
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.FillColor = System.Drawing.Color.Black;
            this.guna2Separator1.Location = new System.Drawing.Point(0, 125);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(1318, 10);
            this.guna2Separator1.TabIndex = 9;
            // 
            // cmbSubCategoryFilter
            // 
            this.cmbSubCategoryFilter.BackColor = System.Drawing.Color.Transparent;
            this.cmbSubCategoryFilter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.cmbSubCategoryFilter.BorderRadius = 8;
            this.cmbSubCategoryFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbSubCategoryFilter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSubCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubCategoryFilter.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbSubCategoryFilter.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbSubCategoryFilter.Font = new System.Drawing.Font("Nunito", 12F);
            this.cmbSubCategoryFilter.ForeColor = System.Drawing.Color.Black;
            this.cmbSubCategoryFilter.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbSubCategoryFilter.ItemHeight = 41;
            this.cmbSubCategoryFilter.ItemsAppearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSubCategoryFilter.ItemsAppearance.ForeColor = System.Drawing.Color.Black;
            this.cmbSubCategoryFilter.Location = new System.Drawing.Point(720, 78);
            this.cmbSubCategoryFilter.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.cmbSubCategoryFilter.Name = "cmbSubCategoryFilter";
            this.cmbSubCategoryFilter.Size = new System.Drawing.Size(230, 47);
            this.cmbSubCategoryFilter.TabIndex = 8;
            this.cmbSubCategoryFilter.TextOffset = new System.Drawing.Point(3, 0);
            this.cmbSubCategoryFilter.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // cmbCategoryFilter
            // 
            this.cmbCategoryFilter.BackColor = System.Drawing.Color.Transparent;
            this.cmbCategoryFilter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.cmbCategoryFilter.BorderRadius = 8;
            this.cmbCategoryFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbCategoryFilter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoryFilter.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCategoryFilter.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCategoryFilter.Font = new System.Drawing.Font("Nunito", 12F);
            this.cmbCategoryFilter.ForeColor = System.Drawing.Color.Black;
            this.cmbCategoryFilter.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCategoryFilter.ItemHeight = 41;
            this.cmbCategoryFilter.ItemsAppearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategoryFilter.ItemsAppearance.ForeColor = System.Drawing.Color.Black;
            this.cmbCategoryFilter.Location = new System.Drawing.Point(470, 78);
            this.cmbCategoryFilter.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.cmbCategoryFilter.Name = "cmbCategoryFilter";
            this.cmbCategoryFilter.Size = new System.Drawing.Size(230, 47);
            this.cmbCategoryFilter.TabIndex = 7;
            this.cmbCategoryFilter.TextOffset = new System.Drawing.Point(3, 0);
            this.cmbCategoryFilter.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.cmbCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.cmbCategoryFilter_SelectedIndexChanged);
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchProduct.Animated = true;
            this.txtSearchProduct.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.txtSearchProduct.BorderRadius = 20;
            this.txtSearchProduct.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearchProduct.DefaultText = "";
            this.txtSearchProduct.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSearchProduct.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSearchProduct.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearchProduct.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearchProduct.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearchProduct.Font = new System.Drawing.Font("Nunito", 14F);
            this.txtSearchProduct.ForeColor = System.Drawing.Color.Black;
            this.txtSearchProduct.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearchProduct.IconLeft = global::Woof_Gang_Sales___Inventory.Properties.Resources.search;
            this.txtSearchProduct.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.txtSearchProduct.Location = new System.Drawing.Point(10, 69);
            this.txtSearchProduct.Margin = new System.Windows.Forms.Padding(10);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.PlaceholderText = "Search by SKU, Product Name...";
            this.txtSearchProduct.SelectedText = "";
            this.txtSearchProduct.Size = new System.Drawing.Size(440, 56);
            this.txtSearchProduct.TabIndex = 2;
            this.txtSearchProduct.TextOffset = new System.Drawing.Point(10, 0);
            this.txtSearchProduct.TextChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Inter", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(923, 38);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(395, 37);
            this.lblDate.TabIndex = 11;
            this.lblDate.Text = "Tuesday, November 18, 2025";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("Inter", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(1129, 4);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(189, 37);
            this.lblTime.TabIndex = 10;
            this.lblTime.Text = "9:38 PM";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1674, 1031);
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlCart);
            this.Name = "FrmPOS";
            this.Text = "FrmPOS";
            this.Load += new System.EventHandler(this.FrmPOS_Load);
            this.pnlCart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.pnlCartTop.ResumeLayout(false);
            this.pnlCenter.ResumeLayout(false);
            this.pnlFilters.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlCart;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btnClearAll;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Label lblTotal;
        private Guna.UI2.WinForms.Guna2Button btnPay;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label lblPrice;
        private Guna.UI2.WinForms.Guna2Panel pnlCartTop;
        private Guna.UI2.WinForms.Guna2Panel pnlCenter;
        private Guna.UI2.WinForms.Guna2Panel pnlFilters;
        public Guna.UI2.WinForms.Guna2TextBox txtSearchProduct;
        public Guna.UI2.WinForms.Guna2ComboBox cmbCategoryFilter;
        private System.Windows.Forms.FlowLayoutPanel flpProducts;
        public Guna.UI2.WinForms.Guna2ComboBox cmbSubCategoryFilter;
        public Guna.UI2.WinForms.Guna2ComboBox cmbDiscount;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2VSeparator guna2VSeparator1;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTime;
    }
}