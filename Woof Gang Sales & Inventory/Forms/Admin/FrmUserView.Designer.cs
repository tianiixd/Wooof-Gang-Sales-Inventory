namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    partial class FrmUserView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserView));
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblDateClock = new System.Windows.Forms.Label();
            this.lblTimeClock = new System.Windows.Forms.Label();
            this.cmbFilterStatus = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbFilterRole = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.totalUsersPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotalUsers = new System.Windows.Forms.Label();
            this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.adminPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblAdmins = new System.Windows.Forms.Label();
            this.guna2Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.storeClerkPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2PictureBox3 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblStoreClerks = new System.Windows.Forms.Label();
            this.inactivePanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2PictureBox4 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblArchived = new System.Windows.Forms.Label();
            this.guna2Elipse4 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Elipse5 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.totalUsersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.adminPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).BeginInit();
            this.storeClerkPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).BeginInit();
            this.inactivePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::Woof_Gang_Sales___Inventory.Properties.Resources.edit;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 91;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Image = global::Woof_Gang_Sales___Inventory.Properties.Resources.delete;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Width = 92;
            // 
            // dgvUser
            // 
            this.dgvUser.AllowUserToAddRows = false;
            this.dgvUser.AllowUserToDeleteRows = false;
            this.dgvUser.AllowUserToResizeColumns = false;
            this.dgvUser.AllowUserToResizeRows = false;
            this.dgvUser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUser.BackgroundColor = System.Drawing.Color.White;
            this.dgvUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Inter", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUser.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUser.Location = new System.Drawing.Point(29, 352);
            this.dgvUser.Margin = new System.Windows.Forms.Padding(20);
            this.dgvUser.MultiSelect = false;
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.ReadOnly = true;
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(1616, 650);
            this.dgvUser.TabIndex = 2;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 20;
            this.guna2Elipse1.TargetControl = this.dgvUser;
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Inter", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.lblDate.Location = new System.Drawing.Point(1200, 43);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(395, 37);
            this.lblDate.TabIndex = 8;
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("Inter", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.lblTime.Location = new System.Drawing.Point(1454, 11);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(189, 37);
            this.lblTime.TabIndex = 7;
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(41)))), ((int)(((byte)(70)))));
            this.guna2Panel1.Controls.Add(this.lblDateClock);
            this.guna2Panel1.Controls.Add(this.lblTimeClock);
            this.guna2Panel1.Controls.Add(this.cmbFilterStatus);
            this.guna2Panel1.Controls.Add(this.cmbFilterRole);
            this.guna2Panel1.Controls.Add(this.label3);
            this.guna2Panel1.Controls.Add(this.btnAdd);
            this.guna2Panel1.Controls.Add(this.txtSearch);
            this.guna2Panel1.Controls.Add(this.lblTitle);
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1674, 156);
            this.guna2Panel1.TabIndex = 9;
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
            // cmbFilterStatus
            // 
            this.cmbFilterStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.cmbFilterStatus.Location = new System.Drawing.Point(1142, 104);
            this.cmbFilterStatus.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.cmbFilterStatus.Name = "cmbFilterStatus";
            this.cmbFilterStatus.Size = new System.Drawing.Size(168, 47);
            this.cmbFilterStatus.TabIndex = 6;
            this.cmbFilterStatus.TextOffset = new System.Drawing.Point(3, 0);
            this.cmbFilterStatus.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // cmbFilterRole
            // 
            this.cmbFilterRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFilterRole.BackColor = System.Drawing.Color.Transparent;
            this.cmbFilterRole.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.cmbFilterRole.BorderRadius = 8;
            this.cmbFilterRole.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbFilterRole.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFilterRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterRole.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbFilterRole.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbFilterRole.Font = new System.Drawing.Font("Nunito", 12F);
            this.cmbFilterRole.ForeColor = System.Drawing.Color.Black;
            this.cmbFilterRole.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbFilterRole.ItemHeight = 41;
            this.cmbFilterRole.ItemsAppearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterRole.ItemsAppearance.ForeColor = System.Drawing.Color.Black;
            this.cmbFilterRole.Location = new System.Drawing.Point(1330, 104);
            this.cmbFilterRole.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.cmbFilterRole.Name = "cmbFilterRole";
            this.cmbFilterRole.Size = new System.Drawing.Size(169, 47);
            this.cmbFilterRole.TabIndex = 5;
            this.cmbFilterRole.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Inter", 13F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.label3.Location = new System.Drawing.Point(699, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 22);
            this.label3.TabIndex = 1;
            this.label3.Text = "Search";
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
            this.btnAdd.ImageOffset = new System.Drawing.Point(-2, 0);
            this.btnAdd.Location = new System.Drawing.Point(1512, 95);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(142, 56);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "New User";
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
            this.txtSearch.Location = new System.Drawing.Point(682, 95);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 4, 10, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = " Search by name, username, role...";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(440, 56);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextOffset = new System.Drawing.Point(10, 0);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Poppins", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.lblTitle.Location = new System.Drawing.Point(56, 47);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(293, 48);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Users Management";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalUsersPanel
            // 
            this.totalUsersPanel.BackColor = System.Drawing.Color.White;
            this.totalUsersPanel.BorderRadius = 20;
            this.totalUsersPanel.Controls.Add(this.guna2PictureBox1);
            this.totalUsersPanel.Controls.Add(this.label4);
            this.totalUsersPanel.Controls.Add(this.lblTotalUsers);
            this.totalUsersPanel.Location = new System.Drawing.Point(143, 180);
            this.totalUsersPanel.Margin = new System.Windows.Forms.Padding(3, 10, 50, 10);
            this.totalUsersPanel.Name = "totalUsersPanel";
            this.totalUsersPanel.Size = new System.Drawing.Size(300, 150);
            this.totalUsersPanel.TabIndex = 10;
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
            this.label4.Location = new System.Drawing.Point(101, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 27);
            this.label4.TabIndex = 1;
            this.label4.Text = "Total Users";
            // 
            // lblTotalUsers
            // 
            this.lblTotalUsers.Font = new System.Drawing.Font("Inter", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalUsers.Location = new System.Drawing.Point(100, 77);
            this.lblTotalUsers.Name = "lblTotalUsers";
            this.lblTotalUsers.Size = new System.Drawing.Size(131, 36);
            this.lblTotalUsers.TabIndex = 3;
            // 
            // guna2Elipse2
            // 
            this.guna2Elipse2.BorderRadius = 20;
            this.guna2Elipse2.TargetControl = this.totalUsersPanel;
            // 
            // adminPanel
            // 
            this.adminPanel.BackColor = System.Drawing.Color.White;
            this.adminPanel.BorderRadius = 20;
            this.adminPanel.Controls.Add(this.guna2PictureBox2);
            this.adminPanel.Controls.Add(this.label5);
            this.adminPanel.Controls.Add(this.lblAdmins);
            this.adminPanel.Location = new System.Drawing.Point(496, 180);
            this.adminPanel.Margin = new System.Windows.Forms.Padding(3, 10, 50, 10);
            this.adminPanel.Name = "adminPanel";
            this.adminPanel.Size = new System.Drawing.Size(300, 150);
            this.adminPanel.TabIndex = 11;
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
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Inter", 13F);
            this.label5.Location = new System.Drawing.Point(101, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 27);
            this.label5.TabIndex = 1;
            this.label5.Text = "Administrators";
            // 
            // lblAdmins
            // 
            this.lblAdmins.Font = new System.Drawing.Font("Inter", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdmins.Location = new System.Drawing.Point(100, 77);
            this.lblAdmins.Name = "lblAdmins";
            this.lblAdmins.Size = new System.Drawing.Size(131, 36);
            this.lblAdmins.TabIndex = 3;
            // 
            // guna2Elipse3
            // 
            this.guna2Elipse3.BorderRadius = 20;
            this.guna2Elipse3.TargetControl = this.adminPanel;
            // 
            // storeClerkPanel
            // 
            this.storeClerkPanel.BackColor = System.Drawing.Color.White;
            this.storeClerkPanel.BorderRadius = 20;
            this.storeClerkPanel.Controls.Add(this.guna2PictureBox3);
            this.storeClerkPanel.Controls.Add(this.label6);
            this.storeClerkPanel.Controls.Add(this.lblStoreClerks);
            this.storeClerkPanel.Location = new System.Drawing.Point(849, 180);
            this.storeClerkPanel.Margin = new System.Windows.Forms.Padding(3, 10, 50, 10);
            this.storeClerkPanel.Name = "storeClerkPanel";
            this.storeClerkPanel.Size = new System.Drawing.Size(300, 150);
            this.storeClerkPanel.TabIndex = 12;
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
            this.label6.Location = new System.Drawing.Point(101, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 27);
            this.label6.TabIndex = 1;
            this.label6.Text = "Store Clerks";
            // 
            // lblStoreClerks
            // 
            this.lblStoreClerks.Font = new System.Drawing.Font("Inter", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoreClerks.Location = new System.Drawing.Point(100, 77);
            this.lblStoreClerks.Name = "lblStoreClerks";
            this.lblStoreClerks.Size = new System.Drawing.Size(131, 36);
            this.lblStoreClerks.TabIndex = 3;
            // 
            // inactivePanel
            // 
            this.inactivePanel.BackColor = System.Drawing.Color.White;
            this.inactivePanel.BorderRadius = 20;
            this.inactivePanel.Controls.Add(this.guna2PictureBox4);
            this.inactivePanel.Controls.Add(this.label8);
            this.inactivePanel.Controls.Add(this.lblArchived);
            this.inactivePanel.Location = new System.Drawing.Point(1207, 180);
            this.inactivePanel.Margin = new System.Windows.Forms.Padding(3, 10, 50, 10);
            this.inactivePanel.Name = "inactivePanel";
            this.inactivePanel.Size = new System.Drawing.Size(300, 150);
            this.inactivePanel.TabIndex = 13;
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
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Inter", 13F);
            this.label8.Location = new System.Drawing.Point(101, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(179, 27);
            this.label8.TabIndex = 1;
            this.label8.Text = "Archived";
            // 
            // lblArchived
            // 
            this.lblArchived.Font = new System.Drawing.Font("Inter", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArchived.Location = new System.Drawing.Point(100, 77);
            this.lblArchived.Name = "lblArchived";
            this.lblArchived.Size = new System.Drawing.Size(131, 36);
            this.lblArchived.TabIndex = 3;
            // 
            // guna2Elipse4
            // 
            this.guna2Elipse4.BorderRadius = 20;
            this.guna2Elipse4.TargetControl = this.storeClerkPanel;
            // 
            // guna2Elipse5
            // 
            this.guna2Elipse5.BorderRadius = 20;
            this.guna2Elipse5.TargetControl = this.inactivePanel;
            // 
            // FrmUserView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(41)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(1674, 1031);
            this.Controls.Add(this.inactivePanel);
            this.Controls.Add(this.storeClerkPanel);
            this.Controls.Add(this.adminPanel);
            this.Controls.Add(this.totalUsersPanel);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.dgvUser);
            this.Name = "FrmUserView";
            this.Text = "FrmUserView";
            this.Load += new System.EventHandler(this.FrmUserView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.totalUsersPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.adminPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).EndInit();
            this.storeClerkPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).EndInit();
            this.inactivePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridView dgvUser;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTime;
        public Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label lblDateClock;
        private System.Windows.Forms.Label lblTimeClock;
        public Guna.UI2.WinForms.Guna2ComboBox cmbFilterStatus;
        public Guna.UI2.WinForms.Guna2ComboBox cmbFilterRole;
        private System.Windows.Forms.Label label3;
        public Guna.UI2.WinForms.Guna2Button btnAdd;
        public Guna.UI2.WinForms.Guna2TextBox txtSearch;
        public System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel totalUsersPanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotalUsers;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private Guna.UI2.WinForms.Guna2Panel adminPanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblAdmins;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse3;
        private Guna.UI2.WinForms.Guna2Panel storeClerkPanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblStoreClerks;
        private Guna.UI2.WinForms.Guna2Panel inactivePanel;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblArchived;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse4;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse5;
    }
}