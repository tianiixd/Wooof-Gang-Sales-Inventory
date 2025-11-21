namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    partial class FrmCreateEditProduct
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCreateEditProduct));
            this.numOrderLevel = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numQuantity = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSellingPrice = new Guna.UI2.WinForms.Guna2TextBox();
            this.cmbUnit = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbCategory = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.toggleProductStatus = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.txtWeight = new Guna.UI2.WinForms.Guna2TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBrand = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtProductName = new Guna.UI2.WinForms.Guna2TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.cmbSubCategory = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnChoose = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.cmbSupplier = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblProductStatus = new System.Windows.Forms.Label();
            this.picProductImage = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.chkHasExpiration = new Guna.UI2.WinForms.Guna2CheckBox();
            this.dtpExpirationDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.guna2Separator2 = new Guna.UI2.WinForms.Guna2Separator();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.guna2Separator3 = new Guna.UI2.WinForms.Guna2Separator();
            this.txtCostPrice = new Guna.UI2.WinForms.Guna2TextBox();
            this.label17 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).BeginInit();
            this.SuspendLayout();
            // 
            // numOrderLevel
            // 
            this.numOrderLevel.BackColor = System.Drawing.Color.Transparent;
            this.numOrderLevel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.numOrderLevel.BorderRadius = 10;
            this.numOrderLevel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numOrderLevel.Font = new System.Drawing.Font("Nunito", 12F);
            this.numOrderLevel.Location = new System.Drawing.Point(290, 884);
            this.numOrderLevel.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.numOrderLevel.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numOrderLevel.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numOrderLevel.Name = "numOrderLevel";
            this.numOrderLevel.Size = new System.Drawing.Size(320, 41);
            this.numOrderLevel.TabIndex = 45;
            this.numOrderLevel.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Inter", 13F);
            this.label10.Location = new System.Drawing.Point(97, 884);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(133, 41);
            this.label10.TabIndex = 44;
            this.label10.Text = "Reorder Level";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numQuantity
            // 
            this.numQuantity.BackColor = System.Drawing.Color.Transparent;
            this.numQuantity.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.numQuantity.BorderRadius = 10;
            this.numQuantity.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numQuantity.Font = new System.Drawing.Font("Nunito", 12F);
            this.numQuantity.Location = new System.Drawing.Point(290, 823);
            this.numQuantity.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(320, 41);
            this.numQuantity.TabIndex = 43;
            this.numQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Inter", 13F);
            this.label9.Location = new System.Drawing.Point(97, 823);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 41);
            this.label9.TabIndex = 42;
            this.label9.Text = "Quantity";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSellingPrice
            // 
            this.txtSellingPrice.Animated = true;
            this.txtSellingPrice.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.txtSellingPrice.BorderRadius = 10;
            this.txtSellingPrice.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSellingPrice.DefaultText = "";
            this.txtSellingPrice.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSellingPrice.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSellingPrice.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSellingPrice.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSellingPrice.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSellingPrice.Font = new System.Drawing.Font("Nunito", 12F);
            this.txtSellingPrice.ForeColor = System.Drawing.Color.Black;
            this.txtSellingPrice.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSellingPrice.Location = new System.Drawing.Point(290, 701);
            this.txtSellingPrice.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.PlaceholderText = "Enter Selling Price";
            this.txtSellingPrice.SelectedText = "";
            this.txtSellingPrice.Size = new System.Drawing.Size(320, 41);
            this.txtSellingPrice.TabIndex = 41;
            // 
            // cmbUnit
            // 
            this.cmbUnit.BackColor = System.Drawing.Color.Transparent;
            this.cmbUnit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.cmbUnit.BorderRadius = 10;
            this.cmbUnit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbUnit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbUnit.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbUnit.Font = new System.Drawing.Font("Nunito", 12F);
            this.cmbUnit.ForeColor = System.Drawing.Color.Black;
            this.cmbUnit.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbUnit.ItemHeight = 36;
            this.cmbUnit.Location = new System.Drawing.Point(290, 578);
            this.cmbUnit.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(320, 42);
            this.cmbUnit.TabIndex = 40;
            this.cmbUnit.TextOffset = new System.Drawing.Point(3, 0);
            this.cmbUnit.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // cmbCategory
            // 
            this.cmbCategory.BackColor = System.Drawing.Color.Transparent;
            this.cmbCategory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.cmbCategory.BorderRadius = 10;
            this.cmbCategory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCategory.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCategory.Font = new System.Drawing.Font("Nunito", 12F);
            this.cmbCategory.ForeColor = System.Drawing.Color.Black;
            this.cmbCategory.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCategory.ItemHeight = 36;
            this.cmbCategory.Location = new System.Drawing.Point(290, 411);
            this.cmbCategory.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(320, 42);
            this.cmbCategory.TabIndex = 39;
            this.cmbCategory.TextOffset = new System.Drawing.Point(3, 0);
            this.cmbCategory.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(286, 933);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 22);
            this.lblStatus.TabIndex = 38;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Inter", 13F);
            this.label8.Location = new System.Drawing.Point(683, 745);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(144, 26);
            this.label8.TabIndex = 37;
            this.label8.Text = "Product Status:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggleProductStatus
            // 
            this.toggleProductStatus.Animated = true;
            this.toggleProductStatus.BackColor = System.Drawing.Color.Transparent;
            this.toggleProductStatus.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.toggleProductStatus.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.toggleProductStatus.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleProductStatus.CheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleProductStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toggleProductStatus.Location = new System.Drawing.Point(833, 751);
            this.toggleProductStatus.Name = "toggleProductStatus";
            this.toggleProductStatus.Size = new System.Drawing.Size(35, 20);
            this.toggleProductStatus.TabIndex = 36;
            this.toggleProductStatus.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.toggleProductStatus.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.toggleProductStatus.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleProductStatus.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleProductStatus.CheckedChanged += new System.EventHandler(this.toggleProductStatus_CheckedChanged);
            // 
            // txtWeight
            // 
            this.txtWeight.Animated = true;
            this.txtWeight.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.txtWeight.BorderRadius = 10;
            this.txtWeight.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtWeight.DefaultText = "";
            this.txtWeight.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtWeight.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtWeight.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtWeight.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtWeight.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtWeight.Font = new System.Drawing.Font("Nunito", 12F);
            this.txtWeight.ForeColor = System.Drawing.Color.Black;
            this.txtWeight.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtWeight.Location = new System.Drawing.Point(290, 640);
            this.txtWeight.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.PlaceholderText = "Enter Weight/Size";
            this.txtWeight.SelectedText = "";
            this.txtWeight.Size = new System.Drawing.Size(320, 41);
            this.txtWeight.TabIndex = 33;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Inter", 13F);
            this.label7.Location = new System.Drawing.Point(97, 640);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 41);
            this.label7.TabIndex = 35;
            this.label7.Text = "Weight/Size";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBrand
            // 
            this.txtBrand.Animated = true;
            this.txtBrand.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.txtBrand.BorderRadius = 10;
            this.txtBrand.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBrand.DefaultText = "";
            this.txtBrand.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtBrand.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtBrand.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBrand.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBrand.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBrand.Font = new System.Drawing.Font("Nunito", 12F);
            this.txtBrand.ForeColor = System.Drawing.Color.Black;
            this.txtBrand.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBrand.Location = new System.Drawing.Point(290, 288);
            this.txtBrand.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.PlaceholderText = "Enter Brand";
            this.txtBrand.SelectedText = "";
            this.txtBrand.Size = new System.Drawing.Size(320, 41);
            this.txtBrand.TabIndex = 30;
            // 
            // txtProductName
            // 
            this.txtProductName.Animated = true;
            this.txtProductName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.txtProductName.BorderRadius = 10;
            this.txtProductName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtProductName.DefaultText = "";
            this.txtProductName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtProductName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtProductName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtProductName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtProductName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtProductName.Font = new System.Drawing.Font("Nunito", 12F);
            this.txtProductName.ForeColor = System.Drawing.Color.Black;
            this.txtProductName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtProductName.Location = new System.Drawing.Point(290, 227);
            this.txtProductName.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.PlaceholderText = "Enter Product Name";
            this.txtProductName.SelectedText = "";
            this.txtProductName.Size = new System.Drawing.Size(320, 41);
            this.txtProductName.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Inter", 13F);
            this.label6.Location = new System.Drawing.Point(97, 701);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 41);
            this.label6.TabIndex = 28;
            this.label6.Text = "Selling Price";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Inter", 13F);
            this.label5.Location = new System.Drawing.Point(97, 578);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 42);
            this.label5.TabIndex = 27;
            this.label5.Text = "Unit";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Inter", 13F);
            this.label4.Location = new System.Drawing.Point(97, 411);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 42);
            this.label4.TabIndex = 26;
            this.label4.Text = "Category";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Inter", 13F);
            this.label3.Location = new System.Drawing.Point(97, 288);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 41);
            this.label3.TabIndex = 25;
            this.label3.Text = "Brand";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Inter", 13F);
            this.label2.Location = new System.Drawing.Point(97, 227);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 41);
            this.label2.TabIndex = 24;
            this.label2.Text = "Product Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblID
            // 
            this.lblID.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(295, 173);
            this.lblID.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(244, 41);
            this.lblID.TabIndex = 23;
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Inter", 13F);
            this.label1.Location = new System.Drawing.Point(97, 172);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 41);
            this.label1.TabIndex = 22;
            this.label1.Text = "Product ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(41)))), ((int)(((byte)(70)))));
            this.guna2Panel1.Controls.Add(this.lblTitle);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1200, 120);
            this.guna2Panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(41)))), ((int)(((byte)(70)))));
            this.lblTitle.Font = new System.Drawing.Font("Poppins", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1200, 120);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Add Product";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 30;
            this.guna2Elipse1.TargetControl = this;
            // 
            // cmbSubCategory
            // 
            this.cmbSubCategory.BackColor = System.Drawing.Color.Transparent;
            this.cmbSubCategory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.cmbSubCategory.BorderRadius = 10;
            this.cmbSubCategory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbSubCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSubCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubCategory.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbSubCategory.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbSubCategory.Font = new System.Drawing.Font("Nunito", 12F);
            this.cmbSubCategory.ForeColor = System.Drawing.Color.Black;
            this.cmbSubCategory.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbSubCategory.ItemHeight = 36;
            this.cmbSubCategory.Location = new System.Drawing.Point(290, 473);
            this.cmbSubCategory.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.cmbSubCategory.Name = "cmbSubCategory";
            this.cmbSubCategory.Size = new System.Drawing.Size(320, 42);
            this.cmbSubCategory.TabIndex = 51;
            this.cmbSubCategory.TextOffset = new System.Drawing.Point(3, 0);
            this.cmbSubCategory.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Inter", 13F);
            this.label11.Location = new System.Drawing.Point(97, 473);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(133, 42);
            this.label11.TabIndex = 50;
            this.label11.Text = "Sub Category";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnChoose
            // 
            this.btnChoose.Animated = true;
            this.btnChoose.BackColor = System.Drawing.Color.Transparent;
            this.btnChoose.BorderRadius = 10;
            this.btnChoose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChoose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnChoose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnChoose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnChoose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnChoose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnChoose.Font = new System.Drawing.Font("Inter", 12F);
            this.btnChoose.ForeColor = System.Drawing.Color.White;
            this.btnChoose.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnChoose.Image = global::Woof_Gang_Sales___Inventory.Properties.Resources.photo;
            this.btnChoose.ImageOffset = new System.Drawing.Point(-2, 0);
            this.btnChoose.ImageSize = new System.Drawing.Size(30, 30);
            this.btnChoose.Location = new System.Drawing.Point(811, 496);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.ShadowDecoration.BorderRadius = 8;
            this.btnChoose.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnChoose.Size = new System.Drawing.Size(180, 50);
            this.btnChoose.TabIndex = 49;
            this.btnChoose.Text = "Upload Image";
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Animated = true;
            this.btnCancel.BorderRadius = 10;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnCancel.Font = new System.Drawing.Font("Inter", 12F);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.Image = global::Woof_Gang_Sales___Inventory.Properties.Resources.close1;
            this.btnCancel.ImageOffset = new System.Drawing.Point(-5, 0);
            this.btnCancel.Location = new System.Drawing.Point(895, 807);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 51);
            this.btnCancel.TabIndex = 47;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Animated = true;
            this.btnSave.BorderRadius = 10;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(74)))), ((int)(((byte)(145)))));
            this.btnSave.Font = new System.Drawing.Font("Inter", 12F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(59)))), ((int)(((byte)(120)))));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageOffset = new System.Drawing.Point(-8, 0);
            this.btnSave.ImageSize = new System.Drawing.Size(35, 35);
            this.btnSave.Location = new System.Drawing.Point(688, 807);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 51);
            this.btnSave.TabIndex = 46;
            this.btnSave.Text = "Add";
            this.btnSave.TextOffset = new System.Drawing.Point(-5, 0);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.BackColor = System.Drawing.Color.Transparent;
            this.cmbSupplier.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.cmbSupplier.BorderRadius = 10;
            this.cmbSupplier.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbSupplier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbSupplier.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbSupplier.Font = new System.Drawing.Font("Nunito", 12F);
            this.cmbSupplier.ForeColor = System.Drawing.Color.Black;
            this.cmbSupplier.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbSupplier.ItemHeight = 36;
            this.cmbSupplier.Location = new System.Drawing.Point(290, 349);
            this.cmbSupplier.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(320, 42);
            this.cmbSupplier.TabIndex = 53;
            this.cmbSupplier.TextOffset = new System.Drawing.Point(3, 0);
            this.cmbSupplier.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Inter", 13F);
            this.label12.Location = new System.Drawing.Point(97, 349);
            this.label12.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(133, 42);
            this.label12.TabIndex = 52;
            this.label12.Text = "Supplier";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProductStatus
            // 
            this.lblProductStatus.Location = new System.Drawing.Point(874, 736);
            this.lblProductStatus.Name = "lblProductStatus";
            this.lblProductStatus.Size = new System.Drawing.Size(235, 47);
            this.lblProductStatus.TabIndex = 54;
            this.lblProductStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picProductImage
            // 
            this.picProductImage.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.picProductImage.ImageRotate = 0F;
            this.picProductImage.Location = new System.Drawing.Point(749, 183);
            this.picProductImage.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.picProductImage.Name = "picProductImage";
            this.picProductImage.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.picProductImage.Size = new System.Drawing.Size(300, 300);
            this.picProductImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProductImage.TabIndex = 48;
            this.picProductImage.TabStop = false;
            // 
            // chkHasExpiration
            // 
            this.chkHasExpiration.AutoSize = true;
            this.chkHasExpiration.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkHasExpiration.CheckedState.BorderRadius = 3;
            this.chkHasExpiration.CheckedState.BorderThickness = 0;
            this.chkHasExpiration.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkHasExpiration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkHasExpiration.Font = new System.Drawing.Font("Inter", 11.25F);
            this.chkHasExpiration.Location = new System.Drawing.Point(688, 694);
            this.chkHasExpiration.Name = "chkHasExpiration";
            this.chkHasExpiration.Size = new System.Drawing.Size(177, 26);
            this.chkHasExpiration.TabIndex = 55;
            this.chkHasExpiration.Text = "This product expires\r\n";
            this.chkHasExpiration.UncheckedState.BorderColor = System.Drawing.Color.Black;
            this.chkHasExpiration.UncheckedState.BorderRadius = 3;
            this.chkHasExpiration.UncheckedState.BorderThickness = 1;
            this.chkHasExpiration.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            // 
            // dtpExpirationDate
            // 
            this.dtpExpirationDate.Animated = true;
            this.dtpExpirationDate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.dtpExpirationDate.BorderRadius = 10;
            this.dtpExpirationDate.BorderThickness = 1;
            this.dtpExpirationDate.Checked = true;
            this.dtpExpirationDate.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.dtpExpirationDate.CheckedState.FillColor = System.Drawing.Color.White;
            this.dtpExpirationDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpExpirationDate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.dtpExpirationDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpExpirationDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(19)))), ((int)(((byte)(26)))));
            this.dtpExpirationDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpExpirationDate.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.dtpExpirationDate.Location = new System.Drawing.Point(688, 640);
            this.dtpExpirationDate.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.dtpExpirationDate.MaxDate = new System.DateTime(2025, 11, 6, 0, 0, 0, 0);
            this.dtpExpirationDate.MinDate = new System.DateTime(2025, 11, 6, 0, 0, 0, 0);
            this.dtpExpirationDate.Name = "dtpExpirationDate";
            this.dtpExpirationDate.Size = new System.Drawing.Size(320, 41);
            this.dtpExpirationDate.TabIndex = 56;
            this.dtpExpirationDate.Value = new System.DateTime(2025, 11, 6, 0, 0, 0, 0);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Inter", 13F);
            this.label13.Location = new System.Drawing.Point(683, 612);
            this.label13.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(141, 26);
            this.label13.TabIndex = 57;
            this.label13.Text = "Expiration Date";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.Location = new System.Drawing.Point(102, 159);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(508, 10);
            this.guna2Separator1.TabIndex = 59;
            // 
            // guna2Separator2
            // 
            this.guna2Separator2.Location = new System.Drawing.Point(102, 560);
            this.guna2Separator2.Name = "guna2Separator2";
            this.guna2Separator2.Size = new System.Drawing.Size(508, 10);
            this.guna2Separator2.TabIndex = 61;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Poppins", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(96, 133);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(232, 30);
            this.label14.TabIndex = 62;
            this.label14.Text = "Product Details";
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Poppins", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(96, 534);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(253, 30);
            this.label15.TabIndex = 63;
            this.label15.Text = "Pricing && Inventory";
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Poppins", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(682, 563);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(253, 30);
            this.label16.TabIndex = 65;
            this.label16.Text = "Expiration && Status";
            // 
            // guna2Separator3
            // 
            this.guna2Separator3.Location = new System.Drawing.Point(688, 589);
            this.guna2Separator3.Name = "guna2Separator3";
            this.guna2Separator3.Size = new System.Drawing.Size(426, 10);
            this.guna2Separator3.TabIndex = 64;
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.Animated = true;
            this.txtCostPrice.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.txtCostPrice.BorderRadius = 10;
            this.txtCostPrice.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCostPrice.DefaultText = "";
            this.txtCostPrice.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCostPrice.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCostPrice.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCostPrice.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCostPrice.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCostPrice.Font = new System.Drawing.Font("Nunito", 12F);
            this.txtCostPrice.ForeColor = System.Drawing.Color.Black;
            this.txtCostPrice.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCostPrice.Location = new System.Drawing.Point(290, 762);
            this.txtCostPrice.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.PlaceholderText = "Enter Cost Price";
            this.txtCostPrice.SelectedText = "";
            this.txtCostPrice.Size = new System.Drawing.Size(320, 41);
            this.txtCostPrice.TabIndex = 67;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Inter", 13F);
            this.label17.Location = new System.Drawing.Point(97, 762);
            this.label17.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(133, 41);
            this.label17.TabIndex = 66;
            this.label17.Text = "Cost Price";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmCreateEditProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 950);
            this.Controls.Add(this.txtCostPrice);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.guna2Separator3);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.guna2Separator2);
            this.Controls.Add(this.guna2Separator1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.dtpExpirationDate);
            this.Controls.Add(this.chkHasExpiration);
            this.Controls.Add(this.lblProductStatus);
            this.Controls.Add(this.cmbSupplier);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmbSubCategory);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.picProductImage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.numOrderLevel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtSellingPrice);
            this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.toggleProductStatus);
            this.Controls.Add(this.txtWeight);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtBrand);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "FrmCreateEditProduct";
            this.Text = "FrmCreateEditProduct";
            this.Load += new System.EventHandler(this.FrmCreateEditProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numOrderLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2ToggleSwitch toggleProductStatus;
        private Guna.UI2.WinForms.Guna2TextBox txtWeight;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2TextBox txtBrand;
        private Guna.UI2.WinForms.Guna2TextBox txtProductName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox cmbCategory;
        private Guna.UI2.WinForms.Guna2ComboBox cmbUnit;
        private Guna.UI2.WinForms.Guna2NumericUpDown numQuantity;
        private System.Windows.Forms.Label label9;
        private Guna.UI2.WinForms.Guna2TextBox txtSellingPrice;
        private Guna.UI2.WinForms.Guna2NumericUpDown numOrderLevel;
        private System.Windows.Forms.Label label10;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnChoose;
        private Guna.UI2.WinForms.Guna2DataGridViewStyler guna2DataGridViewStyler1;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSubCategory;
        private System.Windows.Forms.Label label11;
        private Guna.UI2.WinForms.Guna2ComboBox cmbSupplier;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblProductStatus;
        private Guna.UI2.WinForms.Guna2CirclePictureBox picProductImage;
        private Guna.UI2.WinForms.Guna2CheckBox chkHasExpiration;
        private System.Windows.Forms.Label label13;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpExpirationDate;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator3;
        private Guna.UI2.WinForms.Guna2TextBox txtCostPrice;
        private System.Windows.Forms.Label label17;
    }
}