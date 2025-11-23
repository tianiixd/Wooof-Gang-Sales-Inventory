namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    partial class FrmSalesReports
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSalesReports));
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.dtpEnd = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtpStart = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblDateClock = new System.Windows.Forms.Label();
            this.lblTimeClock = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.cmbReportType = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.revenuePanel = new Guna.UI2.WinForms.Guna2Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.profitPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalCash = new System.Windows.Forms.Label();
            this.costPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalDigital = new System.Windows.Forms.Label();
            this.marginPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalProfit = new System.Windows.Forms.Label();
            this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Elipse4 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Elipse5 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2PictureBox4 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2PictureBox3 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnExportPdf = new Guna.UI2.WinForms.Guna2Button();
            this.btnGenerate = new Guna.UI2.WinForms.Guna2Button();
            this.btnExportExcel = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.revenuePanel.SuspendLayout();
            this.profitPanel.SuspendLayout();
            this.costPanel.SuspendLayout();
            this.marginPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(41)))), ((int)(((byte)(70)))));
            this.guna2Panel1.Controls.Add(this.btnGenerate);
            this.guna2Panel1.Controls.Add(this.cmbReportType);
            this.guna2Panel1.Controls.Add(this.dtpEnd);
            this.guna2Panel1.Controls.Add(this.dtpStart);
            this.guna2Panel1.Controls.Add(this.lblDateClock);
            this.guna2Panel1.Controls.Add(this.lblTimeClock);
            this.guna2Panel1.Controls.Add(this.lblTitle);
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(10);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1674, 156);
            this.guna2Panel1.TabIndex = 11;
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
            this.dtpEnd.Location = new System.Drawing.Point(1225, 105);
            this.dtpEnd.Margin = new System.Windows.Forms.Padding(10);
            this.dtpEnd.MaxDate = new System.DateTime(2025, 11, 6, 0, 0, 0, 0);
            this.dtpEnd.MinDate = new System.DateTime(2025, 11, 6, 0, 0, 0, 0);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(270, 41);
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
            this.dtpStart.Location = new System.Drawing.Point(935, 105);
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
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Poppins", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.lblTitle.Location = new System.Drawing.Point(56, 47);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(194, 48);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Sales Report";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvReport
            // 
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.AllowUserToResizeColumns = false;
            this.dgvReport.AllowUserToResizeRows = false;
            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Inter", 11.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Location = new System.Drawing.Point(14, 344);
            this.dgvReport.Margin = new System.Windows.Forms.Padding(5);
            this.dgvReport.MultiSelect = false;
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(1646, 673);
            this.dgvReport.TabIndex = 12;
            // 
            // cmbReportType
            // 
            this.cmbReportType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbReportType.BackColor = System.Drawing.Color.Transparent;
            this.cmbReportType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.cmbReportType.BorderRadius = 8;
            this.cmbReportType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbReportType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportType.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbReportType.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbReportType.Font = new System.Drawing.Font("Nunito", 12F);
            this.cmbReportType.ForeColor = System.Drawing.Color.Black;
            this.cmbReportType.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbReportType.ItemHeight = 41;
            this.cmbReportType.ItemsAppearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReportType.ItemsAppearance.ForeColor = System.Drawing.Color.Black;
            this.cmbReportType.Location = new System.Drawing.Point(680, 99);
            this.cmbReportType.Margin = new System.Windows.Forms.Padding(10);
            this.cmbReportType.Name = "cmbReportType";
            this.cmbReportType.Size = new System.Drawing.Size(235, 47);
            this.cmbReportType.TabIndex = 43;
            this.cmbReportType.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 20;
            this.guna2Elipse1.TargetControl = this.dgvReport;
            // 
            // revenuePanel
            // 
            this.revenuePanel.BackColor = System.Drawing.Color.White;
            this.revenuePanel.BorderRadius = 20;
            this.revenuePanel.Controls.Add(this.guna2PictureBox1);
            this.revenuePanel.Controls.Add(this.label4);
            this.revenuePanel.Controls.Add(this.lblTotalRevenue);
            this.revenuePanel.Location = new System.Drawing.Point(81, 176);
            this.revenuePanel.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.revenuePanel.Name = "revenuePanel";
            this.revenuePanel.Size = new System.Drawing.Size(300, 150);
            this.revenuePanel.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Inter", 13F);
            this.label4.Location = new System.Drawing.Point(113, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 27);
            this.label4.TabIndex = 1;
            this.label4.Text = "Total Revenue";
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.Font = new System.Drawing.Font("Inter", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRevenue.Location = new System.Drawing.Point(112, 77);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(185, 36);
            this.lblTotalRevenue.TabIndex = 3;
            // 
            // profitPanel
            // 
            this.profitPanel.BackColor = System.Drawing.Color.White;
            this.profitPanel.BorderRadius = 20;
            this.profitPanel.Controls.Add(this.guna2PictureBox2);
            this.profitPanel.Controls.Add(this.label5);
            this.profitPanel.Controls.Add(this.lblTotalCash);
            this.profitPanel.Location = new System.Drawing.Point(421, 176);
            this.profitPanel.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.profitPanel.Name = "profitPanel";
            this.profitPanel.Size = new System.Drawing.Size(300, 150);
            this.profitPanel.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Inter", 13F);
            this.label5.Location = new System.Drawing.Point(113, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 27);
            this.label5.TabIndex = 1;
            this.label5.Text = "Total Cash";
            // 
            // lblTotalCash
            // 
            this.lblTotalCash.Font = new System.Drawing.Font("Inter", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCash.Location = new System.Drawing.Point(112, 77);
            this.lblTotalCash.Name = "lblTotalCash";
            this.lblTotalCash.Size = new System.Drawing.Size(185, 36);
            this.lblTotalCash.TabIndex = 3;
            // 
            // costPanel
            // 
            this.costPanel.BackColor = System.Drawing.Color.White;
            this.costPanel.BorderRadius = 20;
            this.costPanel.Controls.Add(this.guna2PictureBox3);
            this.costPanel.Controls.Add(this.label1);
            this.costPanel.Controls.Add(this.lblTotalDigital);
            this.costPanel.Location = new System.Drawing.Point(761, 176);
            this.costPanel.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.costPanel.Name = "costPanel";
            this.costPanel.Size = new System.Drawing.Size(300, 150);
            this.costPanel.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Inter", 13F);
            this.label1.Location = new System.Drawing.Point(113, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Total Digital";
            // 
            // lblTotalDigital
            // 
            this.lblTotalDigital.Font = new System.Drawing.Font("Inter", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDigital.Location = new System.Drawing.Point(113, 77);
            this.lblTotalDigital.Name = "lblTotalDigital";
            this.lblTotalDigital.Size = new System.Drawing.Size(184, 36);
            this.lblTotalDigital.TabIndex = 3;
            // 
            // marginPanel
            // 
            this.marginPanel.BackColor = System.Drawing.Color.White;
            this.marginPanel.BorderRadius = 20;
            this.marginPanel.Controls.Add(this.guna2PictureBox4);
            this.marginPanel.Controls.Add(this.label3);
            this.marginPanel.Controls.Add(this.lblTotalProfit);
            this.marginPanel.Location = new System.Drawing.Point(1101, 176);
            this.marginPanel.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.marginPanel.Name = "marginPanel";
            this.marginPanel.Size = new System.Drawing.Size(300, 150);
            this.marginPanel.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Inter", 13F);
            this.label3.Location = new System.Drawing.Point(113, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 27);
            this.label3.TabIndex = 1;
            this.label3.Text = "Total Profit";
            // 
            // lblTotalProfit
            // 
            this.lblTotalProfit.Font = new System.Drawing.Font("Inter", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProfit.Location = new System.Drawing.Point(113, 77);
            this.lblTotalProfit.Name = "lblTotalProfit";
            this.lblTotalProfit.Size = new System.Drawing.Size(163, 36);
            this.lblTotalProfit.TabIndex = 3;
            // 
            // guna2Elipse2
            // 
            this.guna2Elipse2.BorderRadius = 20;
            this.guna2Elipse2.TargetControl = this.revenuePanel;
            // 
            // guna2Elipse3
            // 
            this.guna2Elipse3.BorderRadius = 20;
            this.guna2Elipse3.TargetControl = this.profitPanel;
            // 
            // guna2Elipse4
            // 
            this.guna2Elipse4.BorderRadius = 20;
            this.guna2Elipse4.TargetControl = this.costPanel;
            // 
            // guna2Elipse5
            // 
            this.guna2Elipse5.BorderRadius = 20;
            this.guna2Elipse5.TargetControl = this.marginPanel;
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
            // btnExportPdf
            // 
            this.btnExportPdf.Animated = true;
            this.btnExportPdf.BorderRadius = 15;
            this.btnExportPdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportPdf.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExportPdf.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExportPdf.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExportPdf.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExportPdf.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(156)))), ((int)(((byte)(219)))));
            this.btnExportPdf.Font = new System.Drawing.Font("Inter", 12F);
            this.btnExportPdf.ForeColor = System.Drawing.Color.White;
            this.btnExportPdf.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(204)))), ((int)(((byte)(242)))));
            this.btnExportPdf.Image = ((System.Drawing.Image)(resources.GetObject("btnExportPdf.Image")));
            this.btnExportPdf.ImageOffset = new System.Drawing.Point(-2, 0);
            this.btnExportPdf.ImageSize = new System.Drawing.Size(30, 30);
            this.btnExportPdf.Location = new System.Drawing.Point(1461, 194);
            this.btnExportPdf.Margin = new System.Windows.Forms.Padding(10);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(184, 56);
            this.btnExportPdf.TabIndex = 13;
            this.btnExportPdf.Text = "Export To Pdf";
            this.btnExportPdf.TextOffset = new System.Drawing.Point(-1, 0);
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Animated = true;
            this.btnGenerate.BorderRadius = 15;
            this.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerate.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGenerate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGenerate.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGenerate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGenerate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(156)))), ((int)(((byte)(219)))));
            this.btnGenerate.Font = new System.Drawing.Font("Inter", 12F);
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(204)))), ((int)(((byte)(242)))));
            this.btnGenerate.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerate.Image")));
            this.btnGenerate.ImageOffset = new System.Drawing.Point(-2, 0);
            this.btnGenerate.ImageSize = new System.Drawing.Size(30, 30);
            this.btnGenerate.Location = new System.Drawing.Point(1515, 90);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(10);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(140, 56);
            this.btnGenerate.TabIndex = 44;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.TextOffset = new System.Drawing.Point(-1, 0);
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Animated = true;
            this.btnExportExcel.BorderRadius = 15;
            this.btnExportExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportExcel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExportExcel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExportExcel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExportExcel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExportExcel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(156)))), ((int)(((byte)(219)))));
            this.btnExportExcel.Font = new System.Drawing.Font("Inter", 12F);
            this.btnExportExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportExcel.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(204)))), ((int)(((byte)(242)))));
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.ImageOffset = new System.Drawing.Point(-2, 0);
            this.btnExportExcel.ImageSize = new System.Drawing.Size(30, 30);
            this.btnExportExcel.Location = new System.Drawing.Point(1461, 270);
            this.btnExportExcel.Margin = new System.Windows.Forms.Padding(10);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(184, 56);
            this.btnExportExcel.TabIndex = 2;
            this.btnExportExcel.Text = "Export To Excel";
            this.btnExportExcel.TextOffset = new System.Drawing.Point(-1, 0);
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // FrmSalesReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(41)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(1674, 1031);
            this.Controls.Add(this.marginPanel);
            this.Controls.Add(this.costPanel);
            this.Controls.Add(this.profitPanel);
            this.Controls.Add(this.revenuePanel);
            this.Controls.Add(this.btnExportPdf);
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.btnExportExcel);
            this.Font = new System.Drawing.Font("Inter", 11.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "FrmSalesReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSalesReports";
            this.Load += new System.EventHandler(this.FrmSalesReports_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.revenuePanel.ResumeLayout(false);
            this.profitPanel.ResumeLayout(false);
            this.costPanel.ResumeLayout(false);
            this.marginPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpEnd;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblDateClock;
        private System.Windows.Forms.Label lblTimeClock;
        public Guna.UI2.WinForms.Guna2Button btnExportExcel;
        public System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvReport;
        public Guna.UI2.WinForms.Guna2ComboBox cmbReportType;
        public Guna.UI2.WinForms.Guna2Button btnExportPdf;
        public Guna.UI2.WinForms.Guna2Button btnGenerate;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel revenuePanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotalRevenue;
        private Guna.UI2.WinForms.Guna2Panel profitPanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalCash;
        private Guna.UI2.WinForms.Guna2Panel costPanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalDigital;
        private Guna.UI2.WinForms.Guna2Panel marginPanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalProfit;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse3;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse4;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse5;
    }
}