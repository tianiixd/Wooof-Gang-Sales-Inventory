namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    partial class FrmSupplierView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSupplierView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.cmbFilterStatus = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvSupplier = new System.Windows.Forms.DataGridView();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.totalSuppliersPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalSuppliers = new System.Windows.Forms.Label();
            this.totalUnusedPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblUnusedSuppliers = new System.Windows.Forms.Label();
            this.totalTopSuppliersPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2PictureBox3 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTopSupplier = new System.Windows.Forms.Label();
            this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Elipse4 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupplier)).BeginInit();
            this.totalSuppliersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.totalUnusedPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).BeginInit();
            this.totalTopSuppliersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(41)))), ((int)(((byte)(70)))));
            this.guna2Panel2.Controls.Add(this.cmbFilterStatus);
            this.guna2Panel2.Controls.Add(this.label1);
            this.guna2Panel2.Controls.Add(this.btnAdd);
            this.guna2Panel2.Controls.Add(this.txtSearch);
            this.guna2Panel2.Controls.Add(this.label2);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel2.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(1674, 156);
            this.guna2Panel2.TabIndex = 4;
            // 
            // cmbFilterStatus
            // 
            this.cmbFilterStatus.BackColor = System.Drawing.Color.Transparent;
            this.cmbFilterStatus.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.cmbFilterStatus.BorderRadius = 8;
            this.cmbFilterStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbFilterStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterStatus.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbFilterStatus.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbFilterStatus.Font = new System.Drawing.Font("Nunito", 12F);
            this.cmbFilterStatus.ForeColor = System.Drawing.Color.Black;
            this.cmbFilterStatus.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbFilterStatus.ItemHeight = 41;
            this.cmbFilterStatus.ItemsAppearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterStatus.ItemsAppearance.ForeColor = System.Drawing.Color.Black;
            this.cmbFilterStatus.Location = new System.Drawing.Point(1241, 104);
            this.cmbFilterStatus.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.cmbFilterStatus.Name = "cmbFilterStatus";
            this.cmbFilterStatus.Size = new System.Drawing.Size(230, 47);
            this.cmbFilterStatus.TabIndex = 6;
            this.cmbFilterStatus.TextOffset = new System.Drawing.Point(3, 0);
            this.cmbFilterStatus.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Inter", 13F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.label1.Location = new System.Drawing.Point(797, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search";
            // 
            // btnAdd
            // 
            this.btnAdd.Animated = true;
            this.btnAdd.BorderRadius = 15;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAdd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(156)))), ((int)(((byte)(219)))));
            this.btnAdd.Font = new System.Drawing.Font("Inter", 12F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(204)))), ((int)(((byte)(242)))));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageOffset = new System.Drawing.Point(-1, 0);
            this.btnAdd.Location = new System.Drawing.Point(1484, 95);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(170, 56);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = " New Supplier";
            this.btnAdd.TextOffset = new System.Drawing.Point(-1, 0);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.txtSearch.Location = new System.Drawing.Point(781, 95);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 4, 10, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = " Search by contact person, supplier name";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(440, 56);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextOffset = new System.Drawing.Point(10, 0);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Poppins", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.label2.Location = new System.Drawing.Point(56, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(331, 48);
            this.label2.TabIndex = 0;
            this.label2.Text = "Supplier Management";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvSupplier
            // 
            this.dgvSupplier.AllowUserToAddRows = false;
            this.dgvSupplier.AllowUserToDeleteRows = false;
            this.dgvSupplier.AllowUserToResizeColumns = false;
            this.dgvSupplier.AllowUserToResizeRows = false;
            this.dgvSupplier.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSupplier.BackgroundColor = System.Drawing.Color.White;
            this.dgvSupplier.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Inter", 11.25F);
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSupplier.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dgvSupplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSupplier.Location = new System.Drawing.Point(29, 352);
            this.dgvSupplier.Margin = new System.Windows.Forms.Padding(20);
            this.dgvSupplier.MultiSelect = false;
            this.dgvSupplier.Name = "dgvSupplier";
            this.dgvSupplier.ReadOnly = true;
            this.dgvSupplier.RowHeadersVisible = false;
            this.dgvSupplier.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvSupplier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSupplier.Size = new System.Drawing.Size(1616, 650);
            this.dgvSupplier.TabIndex = 5;
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Inter", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.lblDate.Location = new System.Drawing.Point(1200, 43);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(445, 39);
            this.lblDate.TabIndex = 8;
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("Inter", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.lblTime.Location = new System.Drawing.Point(1454, 11);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(191, 39);
            this.lblTime.TabIndex = 7;
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 20;
            this.guna2Elipse1.TargetControl = this.dgvSupplier;
            // 
            // totalSuppliersPanel
            // 
            this.totalSuppliersPanel.BackColor = System.Drawing.Color.White;
            this.totalSuppliersPanel.BorderRadius = 20;
            this.totalSuppliersPanel.Controls.Add(this.guna2PictureBox1);
            this.totalSuppliersPanel.Controls.Add(this.label3);
            this.totalSuppliersPanel.Controls.Add(this.lblTotalSuppliers);
            this.totalSuppliersPanel.Location = new System.Drawing.Point(213, 180);
            this.totalSuppliersPanel.Margin = new System.Windows.Forms.Padding(3, 10, 100, 10);
            this.totalSuppliersPanel.Name = "totalSuppliersPanel";
            this.totalSuppliersPanel.Size = new System.Drawing.Size(350, 150);
            this.totalSuppliersPanel.TabIndex = 9;
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
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Inter", 13F);
            this.label3.Location = new System.Drawing.Point(113, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 27);
            this.label3.TabIndex = 1;
            this.label3.Text = "Total Suppliers";
            // 
            // lblTotalSuppliers
            // 
            this.lblTotalSuppliers.Font = new System.Drawing.Font("Inter", 15F);
            this.lblTotalSuppliers.Location = new System.Drawing.Point(112, 77);
            this.lblTotalSuppliers.Name = "lblTotalSuppliers";
            this.lblTotalSuppliers.Size = new System.Drawing.Size(131, 36);
            this.lblTotalSuppliers.TabIndex = 3;
            // 
            // totalUnusedPanel
            // 
            this.totalUnusedPanel.BackColor = System.Drawing.Color.White;
            this.totalUnusedPanel.BorderRadius = 20;
            this.totalUnusedPanel.Controls.Add(this.guna2PictureBox2);
            this.totalUnusedPanel.Controls.Add(this.label4);
            this.totalUnusedPanel.Controls.Add(this.lblUnusedSuppliers);
            this.totalUnusedPanel.Location = new System.Drawing.Point(666, 180);
            this.totalUnusedPanel.Margin = new System.Windows.Forms.Padding(3, 10, 100, 10);
            this.totalUnusedPanel.Name = "totalUnusedPanel";
            this.totalUnusedPanel.Size = new System.Drawing.Size(350, 150);
            this.totalUnusedPanel.TabIndex = 10;
            // 
            // guna2PictureBox2
            // 
            this.guna2PictureBox2.Image = global::Woof_Gang_Sales___Inventory.Properties.Resources.warning;
            this.guna2PictureBox2.ImageRotate = 0F;
            this.guna2PictureBox2.Location = new System.Drawing.Point(20, 43);
            this.guna2PictureBox2.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.guna2PictureBox2.Name = "guna2PictureBox2";
            this.guna2PictureBox2.Size = new System.Drawing.Size(70, 70);
            this.guna2PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox2.TabIndex = 2;
            this.guna2PictureBox2.TabStop = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Inter", 13F);
            this.label4.Location = new System.Drawing.Point(122, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 27);
            this.label4.TabIndex = 1;
            this.label4.Text = "Unused / Idle";
            // 
            // lblUnusedSuppliers
            // 
            this.lblUnusedSuppliers.Font = new System.Drawing.Font("Inter", 15F);
            this.lblUnusedSuppliers.Location = new System.Drawing.Point(121, 77);
            this.lblUnusedSuppliers.Name = "lblUnusedSuppliers";
            this.lblUnusedSuppliers.Size = new System.Drawing.Size(131, 36);
            this.lblUnusedSuppliers.TabIndex = 3;
            // 
            // totalTopSuppliersPanel
            // 
            this.totalTopSuppliersPanel.BackColor = System.Drawing.Color.White;
            this.totalTopSuppliersPanel.BorderRadius = 20;
            this.totalTopSuppliersPanel.Controls.Add(this.guna2PictureBox3);
            this.totalTopSuppliersPanel.Controls.Add(this.label6);
            this.totalTopSuppliersPanel.Controls.Add(this.lblTopSupplier);
            this.totalTopSuppliersPanel.Location = new System.Drawing.Point(1119, 180);
            this.totalTopSuppliersPanel.Margin = new System.Windows.Forms.Padding(3, 10, 100, 10);
            this.totalTopSuppliersPanel.Name = "totalTopSuppliersPanel";
            this.totalTopSuppliersPanel.Size = new System.Drawing.Size(350, 150);
            this.totalTopSuppliersPanel.TabIndex = 11;
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
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Inter", 13F);
            this.label6.Location = new System.Drawing.Point(110, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 27);
            this.label6.TabIndex = 1;
            this.label6.Text = "Top Supplier";
            // 
            // lblTopSupplier
            // 
            this.lblTopSupplier.Font = new System.Drawing.Font("Inter", 15F);
            this.lblTopSupplier.Location = new System.Drawing.Point(114, 77);
            this.lblTopSupplier.Name = "lblTopSupplier";
            this.lblTopSupplier.Size = new System.Drawing.Size(218, 36);
            this.lblTopSupplier.TabIndex = 3;
            // 
            // guna2Elipse2
            // 
            this.guna2Elipse2.BorderRadius = 20;
            this.guna2Elipse2.TargetControl = this.totalSuppliersPanel;
            // 
            // guna2Elipse3
            // 
            this.guna2Elipse3.BorderRadius = 20;
            this.guna2Elipse3.TargetControl = this.totalUnusedPanel;
            // 
            // guna2Elipse4
            // 
            this.guna2Elipse4.BorderRadius = 20;
            this.guna2Elipse4.TargetControl = this.totalTopSuppliersPanel;
            // 
            // FrmSupplierView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(41)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(1674, 1031);
            this.Controls.Add(this.totalTopSuppliersPanel);
            this.Controls.Add(this.totalUnusedPanel);
            this.Controls.Add(this.totalSuppliersPanel);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.dgvSupplier);
            this.Controls.Add(this.guna2Panel2);
            this.Font = new System.Drawing.Font("Inter", 11.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "FrmSupplierView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSupplierView";
            this.Load += new System.EventHandler(this.FrmSupplierView_Load);
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupplier)).EndInit();
            this.totalSuppliersPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.totalUnusedPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).EndInit();
            this.totalTopSuppliersPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        public Guna.UI2.WinForms.Guna2ComboBox cmbFilterStatus;
        private System.Windows.Forms.Label label1;
        public Guna.UI2.WinForms.Guna2Button btnAdd;
        public Guna.UI2.WinForms.Guna2TextBox txtSearch;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvSupplier;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTime;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel totalSuppliersPanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalSuppliers;
        private Guna.UI2.WinForms.Guna2Panel totalUnusedPanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblUnusedSuppliers;
        private Guna.UI2.WinForms.Guna2Panel totalTopSuppliersPanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTopSupplier;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse3;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse4;
    }
}