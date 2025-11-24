namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    partial class FrmSalesHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSalesHistory));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.dtpEnd = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtpStart = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblDateClock = new System.Windows.Forms.Label();
            this.lblTimeClock = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFilter = new Guna.UI2.WinForms.Guna2Button();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvSales = new System.Windows.Forms.DataGridView();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.totalRevenuePanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.guna2Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.totalTransactionPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.guna2Elipse4 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.avgSalePanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2PictureBox3 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAvg = new System.Windows.Forms.Label();
            this.highestSalePanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2PictureBox4 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblHigh = new System.Windows.Forms.Label();
            this.guna2Elipse5 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Elipse6 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.totalRevenuePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.totalTransactionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).BeginInit();
            this.avgSalePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).BeginInit();
            this.highestSalePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(41)))), ((int)(((byte)(70)))));
            this.guna2Panel1.Controls.Add(this.dtpEnd);
            this.guna2Panel1.Controls.Add(this.dtpStart);
            this.guna2Panel1.Controls.Add(this.lblDateClock);
            this.guna2Panel1.Controls.Add(this.lblTimeClock);
            this.guna2Panel1.Controls.Add(this.label3);
            this.guna2Panel1.Controls.Add(this.btnFilter);
            this.guna2Panel1.Controls.Add(this.txtSearch);
            this.guna2Panel1.Controls.Add(this.lblTitle);
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(10);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1674, 156);
            this.guna2Panel1.TabIndex = 10;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Animated = true;
            this.dtpEnd.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.dtpEnd.BorderRadius = 10;
            this.dtpEnd.BorderThickness = 1;
            this.dtpEnd.Checked = true;
            this.dtpEnd.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.dtpEnd.CheckedState.FillColor = System.Drawing.Color.White;
            this.dtpEnd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpEnd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.dtpEnd.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpEnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(19)))), ((int)(((byte)(26)))));
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpEnd.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.dtpEnd.Location = new System.Drawing.Point(1232, 105);
            this.dtpEnd.Margin = new System.Windows.Forms.Padding(10);
            this.dtpEnd.MaxDate = new System.DateTime(2025, 11, 6, 0, 0, 0, 0);
            this.dtpEnd.MinDate = new System.DateTime(2025, 11, 6, 0, 0, 0, 0);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(255, 41);
            this.dtpEnd.TabIndex = 42;
            this.dtpEnd.Value = new System.DateTime(2025, 11, 6, 0, 0, 0, 0);
            // 
            // dtpStart
            // 
            this.dtpStart.Animated = true;
            this.dtpStart.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.dtpStart.BorderRadius = 10;
            this.dtpStart.BorderThickness = 1;
            this.dtpStart.Checked = true;
            this.dtpStart.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.dtpStart.CheckedState.FillColor = System.Drawing.Color.White;
            this.dtpStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpStart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.dtpStart.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(19)))), ((int)(((byte)(26)))));
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpStart.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.dtpStart.Location = new System.Drawing.Point(942, 105);
            this.dtpStart.Margin = new System.Windows.Forms.Padding(10);
            this.dtpStart.MaxDate = new System.DateTime(2025, 11, 6, 0, 0, 0, 0);
            this.dtpStart.MinDate = new System.DateTime(2025, 11, 6, 0, 0, 0, 0);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(270, 41);
            this.dtpStart.TabIndex = 41;
            this.dtpStart.Value = new System.DateTime(2025, 11, 6, 0, 0, 0, 0);
            // 
            // lblDateClock
            // 
            this.lblDateClock.Font = new System.Drawing.Font("Inter", 15F);
            this.lblDateClock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.lblDateClock.Location = new System.Drawing.Point(1200, 43);
            this.lblDateClock.Name = "lblDateClock";
            this.lblDateClock.Size = new System.Drawing.Size(445, 39);
            this.lblDateClock.TabIndex = 8;
            this.lblDateClock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTimeClock
            // 
            this.lblTimeClock.Font = new System.Drawing.Font("Inter", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeClock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.lblTimeClock.Location = new System.Drawing.Point(1454, 11);
            this.lblTimeClock.Name = "lblTimeClock";
            this.lblTimeClock.Size = new System.Drawing.Size(191, 39);
            this.lblTimeClock.TabIndex = 7;
            this.lblTimeClock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Inter", 13F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.label3.Location = new System.Drawing.Point(499, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 22);
            this.label3.TabIndex = 1;
            this.label3.Text = "Search";
            // 
            // btnFilter
            // 
            this.btnFilter.Animated = true;
            this.btnFilter.BorderRadius = 15;
            this.btnFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilter.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnFilter.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnFilter.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnFilter.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnFilter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(156)))), ((int)(((byte)(219)))));
            this.btnFilter.Font = new System.Drawing.Font("Inter", 12F);
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(204)))), ((int)(((byte)(242)))));
            this.btnFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnFilter.Image")));
            this.btnFilter.ImageOffset = new System.Drawing.Point(-2, 0);
            this.btnFilter.ImageSize = new System.Drawing.Size(25, 25);
            this.btnFilter.Location = new System.Drawing.Point(1500, 97);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(142, 56);
            this.btnFilter.TabIndex = 2;
            this.btnFilter.Text = "Apply Filter";
            this.btnFilter.TextOffset = new System.Drawing.Point(-1, 0);
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Animated = true;
            this.txtSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.txtSearch.BorderRadius = 20;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.Font = new System.Drawing.Font("Nunito", 14F);
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.IconLeft = global::Woof_Gang_Sales___Inventory.Properties.Resources.search;
            this.txtSearch.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.txtSearch.Location = new System.Drawing.Point(494, 90);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 4, 10, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = " Search by Trans #, cashier name";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(440, 56);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextOffset = new System.Drawing.Point(10, 0);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Poppins", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.lblTitle.Location = new System.Drawing.Point(56, 47);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(186, 48);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Sale History";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvSales
            // 
            this.dgvSales.AllowUserToAddRows = false;
            this.dgvSales.AllowUserToDeleteRows = false;
            this.dgvSales.AllowUserToResizeColumns = false;
            this.dgvSales.AllowUserToResizeRows = false;
            this.dgvSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSales.BackgroundColor = System.Drawing.Color.White;
            this.dgvSales.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Inter", 11.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSales.Location = new System.Drawing.Point(11, 365);
            this.dgvSales.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSales.MultiSelect = false;
            this.dgvSales.Name = "dgvSales";
            this.dgvSales.ReadOnly = true;
            this.dgvSales.RowHeadersVisible = false;
            this.dgvSales.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvSales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSales.Size = new System.Drawing.Size(1652, 399);
            this.dgvSales.TabIndex = 6;
            // 
            // dgvDetails
            // 
            this.dgvDetails.AllowUserToAddRows = false;
            this.dgvDetails.AllowUserToDeleteRows = false;
            this.dgvDetails.AllowUserToResizeColumns = false;
            this.dgvDetails.AllowUserToResizeRows = false;
            this.dgvDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Inter", 11.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.Location = new System.Drawing.Point(10, 791);
            this.dgvDetails.Margin = new System.Windows.Forms.Padding(2);
            this.dgvDetails.MultiSelect = false;
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.ReadOnly = true;
            this.dgvDetails.RowHeadersVisible = false;
            this.dgvDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetails.Size = new System.Drawing.Size(1652, 229);
            this.dgvDetails.TabIndex = 5;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 20;
            this.guna2Elipse1.TargetControl = this.dgvSales;
            // 
            // guna2Elipse2
            // 
            this.guna2Elipse2.BorderRadius = 20;
            this.guna2Elipse2.TargetControl = this.dgvDetails;
            // 
            // totalRevenuePanel
            // 
            this.totalRevenuePanel.BackColor = System.Drawing.Color.White;
            this.totalRevenuePanel.BorderRadius = 20;
            this.totalRevenuePanel.Controls.Add(this.guna2PictureBox1);
            this.totalRevenuePanel.Controls.Add(this.label4);
            this.totalRevenuePanel.Controls.Add(this.lblTotalRevenue);
            this.totalRevenuePanel.Location = new System.Drawing.Point(79, 186);
            this.totalRevenuePanel.Margin = new System.Windows.Forms.Padding(3, 10, 100, 10);
            this.totalRevenuePanel.Name = "totalRevenuePanel";
            this.totalRevenuePanel.Size = new System.Drawing.Size(300, 150);
            this.totalRevenuePanel.TabIndex = 11;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(20, 43);
            this.guna2PictureBox1.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(70, 70);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 2;
            this.guna2PictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Inter", 13F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(19)))), ((int)(((byte)(26)))));
            this.label4.Location = new System.Drawing.Point(101, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 27);
            this.label4.TabIndex = 1;
            this.label4.Text = "Total Revenue";
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.Font = new System.Drawing.Font("Inter", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRevenue.Location = new System.Drawing.Point(100, 77);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(197, 36);
            this.lblTotalRevenue.TabIndex = 3;
            // 
            // guna2Elipse3
            // 
            this.guna2Elipse3.BorderRadius = 20;
            this.guna2Elipse3.TargetControl = this.totalRevenuePanel;
            // 
            // totalTransactionPanel
            // 
            this.totalTransactionPanel.BackColor = System.Drawing.Color.White;
            this.totalTransactionPanel.BorderRadius = 20;
            this.totalTransactionPanel.Controls.Add(this.guna2PictureBox2);
            this.totalTransactionPanel.Controls.Add(this.label1);
            this.totalTransactionPanel.Controls.Add(this.lblCount);
            this.totalTransactionPanel.Location = new System.Drawing.Point(482, 186);
            this.totalTransactionPanel.Margin = new System.Windows.Forms.Padding(3, 10, 100, 10);
            this.totalTransactionPanel.Name = "totalTransactionPanel";
            this.totalTransactionPanel.Size = new System.Drawing.Size(300, 150);
            this.totalTransactionPanel.TabIndex = 12;
            // 
            // guna2PictureBox2
            // 
            this.guna2PictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox2.Image")));
            this.guna2PictureBox2.ImageRotate = 0F;
            this.guna2PictureBox2.Location = new System.Drawing.Point(20, 43);
            this.guna2PictureBox2.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.guna2PictureBox2.Name = "guna2PictureBox2";
            this.guna2PictureBox2.Size = new System.Drawing.Size(70, 70);
            this.guna2PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox2.TabIndex = 2;
            this.guna2PictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Inter", 13F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(19)))), ((int)(((byte)(26)))));
            this.label1.Location = new System.Drawing.Point(101, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Total Transactions";
            // 
            // lblCount
            // 
            this.lblCount.Font = new System.Drawing.Font("Inter", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(100, 77);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(180, 36);
            this.lblCount.TabIndex = 3;
            // 
            // guna2Elipse4
            // 
            this.guna2Elipse4.BorderRadius = 20;
            this.guna2Elipse4.TargetControl = this.totalTransactionPanel;
            // 
            // avgSalePanel
            // 
            this.avgSalePanel.BackColor = System.Drawing.Color.White;
            this.avgSalePanel.BorderRadius = 20;
            this.avgSalePanel.Controls.Add(this.guna2PictureBox3);
            this.avgSalePanel.Controls.Add(this.label2);
            this.avgSalePanel.Controls.Add(this.lblAvg);
            this.avgSalePanel.Location = new System.Drawing.Point(885, 186);
            this.avgSalePanel.Margin = new System.Windows.Forms.Padding(3, 10, 100, 10);
            this.avgSalePanel.Name = "avgSalePanel";
            this.avgSalePanel.Size = new System.Drawing.Size(300, 150);
            this.avgSalePanel.TabIndex = 13;
            // 
            // guna2PictureBox3
            // 
            this.guna2PictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox3.Image")));
            this.guna2PictureBox3.ImageRotate = 0F;
            this.guna2PictureBox3.Location = new System.Drawing.Point(20, 43);
            this.guna2PictureBox3.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.guna2PictureBox3.Name = "guna2PictureBox3";
            this.guna2PictureBox3.Size = new System.Drawing.Size(70, 70);
            this.guna2PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox3.TabIndex = 2;
            this.guna2PictureBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Inter", 13F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(19)))), ((int)(((byte)(26)))));
            this.label2.Location = new System.Drawing.Point(101, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "Average Sale";
            // 
            // lblAvg
            // 
            this.lblAvg.Font = new System.Drawing.Font("Inter", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvg.Location = new System.Drawing.Point(100, 77);
            this.lblAvg.Name = "lblAvg";
            this.lblAvg.Size = new System.Drawing.Size(170, 36);
            this.lblAvg.TabIndex = 3;
            // 
            // highestSalePanel
            // 
            this.highestSalePanel.BackColor = System.Drawing.Color.White;
            this.highestSalePanel.BorderRadius = 20;
            this.highestSalePanel.Controls.Add(this.guna2PictureBox4);
            this.highestSalePanel.Controls.Add(this.label6);
            this.highestSalePanel.Controls.Add(this.lblHigh);
            this.highestSalePanel.Location = new System.Drawing.Point(1288, 186);
            this.highestSalePanel.Margin = new System.Windows.Forms.Padding(3, 10, 100, 10);
            this.highestSalePanel.Name = "highestSalePanel";
            this.highestSalePanel.Size = new System.Drawing.Size(300, 150);
            this.highestSalePanel.TabIndex = 14;
            // 
            // guna2PictureBox4
            // 
            this.guna2PictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox4.Image")));
            this.guna2PictureBox4.ImageRotate = 0F;
            this.guna2PictureBox4.Location = new System.Drawing.Point(20, 43);
            this.guna2PictureBox4.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.guna2PictureBox4.Name = "guna2PictureBox4";
            this.guna2PictureBox4.Size = new System.Drawing.Size(70, 70);
            this.guna2PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox4.TabIndex = 2;
            this.guna2PictureBox4.TabStop = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Inter", 13F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(19)))), ((int)(((byte)(26)))));
            this.label6.Location = new System.Drawing.Point(101, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 27);
            this.label6.TabIndex = 1;
            this.label6.Text = "Highest Sale";
            // 
            // lblHigh
            // 
            this.lblHigh.Font = new System.Drawing.Font("Inter", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHigh.Location = new System.Drawing.Point(100, 77);
            this.lblHigh.Name = "lblHigh";
            this.lblHigh.Size = new System.Drawing.Size(180, 36);
            this.lblHigh.TabIndex = 3;
            // 
            // guna2Elipse5
            // 
            this.guna2Elipse5.BorderRadius = 20;
            this.guna2Elipse5.TargetControl = this.avgSalePanel;
            // 
            // guna2Elipse6
            // 
            this.guna2Elipse6.BorderRadius = 20;
            this.guna2Elipse6.TargetControl = this.highestSalePanel;
            // 
            // FrmSalesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(41)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(1674, 1031);
            this.Controls.Add(this.highestSalePanel);
            this.Controls.Add(this.avgSalePanel);
            this.Controls.Add(this.totalTransactionPanel);
            this.Controls.Add(this.totalRevenuePanel);
            this.Controls.Add(this.dgvSales);
            this.Controls.Add(this.dgvDetails);
            this.Controls.Add(this.guna2Panel1);
            this.Font = new System.Drawing.Font("Inter", 11.25F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(19)))), ((int)(((byte)(26)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmSalesHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSaleHistory";
            this.Load += new System.EventHandler(this.FrmSaleHistory_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.totalRevenuePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.totalTransactionPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).EndInit();
            this.avgSalePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).EndInit();
            this.highestSalePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label lblDateClock;
        private System.Windows.Forms.Label lblTimeClock;
        private System.Windows.Forms.Label label3;
        public Guna.UI2.WinForms.Guna2Button btnFilter;
        public Guna.UI2.WinForms.Guna2TextBox txtSearch;
        public System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvSales;
        private System.Windows.Forms.DataGridView dgvDetails;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpStart;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpEnd;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotalRevenue;
        private Guna.UI2.WinForms.Guna2Panel totalRevenuePanel;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse3;
        private Guna.UI2.WinForms.Guna2Panel totalTransactionPanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCount;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse4;
        private Guna.UI2.WinForms.Guna2Panel avgSalePanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAvg;
        private Guna.UI2.WinForms.Guna2Panel highestSalePanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblHigh;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse5;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse6;
    }
}