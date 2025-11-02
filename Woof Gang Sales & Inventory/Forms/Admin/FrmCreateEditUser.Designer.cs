namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    partial class FrmCreateEditUser
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.toggleUserStatus = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbRole = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtUsername = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtLName = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMName = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtFName = new Guna.UI2.WinForms.Guna2TextBox();
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
            this.btnChoose = new Guna.UI2.WinForms.Guna2Button();
            this.picProfileImage = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.label9 = new System.Windows.Forms.Label();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.guna2Separator2 = new Guna.UI2.WinForms.Guna2Separator();
            this.guna2Separator3 = new Guna.UI2.WinForms.Guna2Separator();
            this.guna2Separator4 = new Guna.UI2.WinForms.Guna2Separator();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProfileImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(277, 769);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 22);
            this.lblStatus.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Inter", 13F);
            this.label8.Location = new System.Drawing.Point(76, 757);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(154, 47);
            this.label8.TabIndex = 20;
            this.label8.Text = "Account Status:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggleUserStatus
            // 
            this.toggleUserStatus.Animated = true;
            this.toggleUserStatus.BackColor = System.Drawing.Color.Transparent;
            this.toggleUserStatus.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.toggleUserStatus.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.toggleUserStatus.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleUserStatus.CheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleUserStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toggleUserStatus.Location = new System.Drawing.Point(236, 771);
            this.toggleUserStatus.Name = "toggleUserStatus";
            this.toggleUserStatus.Size = new System.Drawing.Size(35, 20);
            this.toggleUserStatus.TabIndex = 19;
            this.toggleUserStatus.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.toggleUserStatus.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.toggleUserStatus.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleUserStatus.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleUserStatus.CheckedChanged += new System.EventHandler(this.toggleUserStatus_CheckedChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Animated = true;
            this.txtPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.txtPassword.BorderRadius = 10;
            this.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPassword.DefaultText = "";
            this.txtPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPassword.Location = new System.Drawing.Point(269, 552);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(5, 4, 4, 10);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PlaceholderText = "Enter Password";
            this.txtPassword.SelectedText = "";
            this.txtPassword.Size = new System.Drawing.Size(320, 41);
            this.txtPassword.TabIndex = 13;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Inter", 13F);
            this.label7.Location = new System.Drawing.Point(76, 552);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 41);
            this.label7.TabIndex = 16;
            this.label7.Text = "Password";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbRole
            // 
            this.cmbRole.BackColor = System.Drawing.Color.Transparent;
            this.cmbRole.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.cmbRole.BorderRadius = 10;
            this.cmbRole.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbRole.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbRole.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbRole.Font = new System.Drawing.Font("Nunito", 12F);
            this.cmbRole.ForeColor = System.Drawing.Color.Black;
            this.cmbRole.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbRole.ItemHeight = 41;
            this.cmbRole.Location = new System.Drawing.Point(269, 706);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(320, 47);
            this.cmbRole.TabIndex = 15;
            this.cmbRole.TextOffset = new System.Drawing.Point(3, 0);
            // 
            // txtUsername
            // 
            this.txtUsername.Animated = true;
            this.txtUsername.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.txtUsername.BorderRadius = 10;
            this.txtUsername.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUsername.DefaultText = "";
            this.txtUsername.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtUsername.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtUsername.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtUsername.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtUsername.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.ForeColor = System.Drawing.Color.Black;
            this.txtUsername.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtUsername.Location = new System.Drawing.Point(269, 482);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4, 4, 4, 10);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.PlaceholderText = "Enter Username";
            this.txtUsername.SelectedText = "";
            this.txtUsername.Size = new System.Drawing.Size(320, 41);
            this.txtUsername.TabIndex = 12;
            // 
            // txtLName
            // 
            this.txtLName.Animated = true;
            this.txtLName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.txtLName.BorderRadius = 10;
            this.txtLName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLName.DefaultText = "";
            this.txtLName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtLName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtLName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLName.Font = new System.Drawing.Font("Nunito", 12F);
            this.txtLName.ForeColor = System.Drawing.Color.Black;
            this.txtLName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLName.Location = new System.Drawing.Point(269, 358);
            this.txtLName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLName.Name = "txtLName";
            this.txtLName.PlaceholderText = "Enter LastName";
            this.txtLName.SelectedText = "";
            this.txtLName.Size = new System.Drawing.Size(320, 41);
            this.txtLName.TabIndex = 11;
            // 
            // txtMName
            // 
            this.txtMName.Animated = true;
            this.txtMName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.txtMName.BorderRadius = 10;
            this.txtMName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMName.DefaultText = "";
            this.txtMName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMName.Font = new System.Drawing.Font("Nunito", 12F);
            this.txtMName.ForeColor = System.Drawing.Color.Black;
            this.txtMName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMName.Location = new System.Drawing.Point(269, 307);
            this.txtMName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMName.Name = "txtMName";
            this.txtMName.PlaceholderText = "Enter MiddleName";
            this.txtMName.SelectedText = "";
            this.txtMName.Size = new System.Drawing.Size(320, 41);
            this.txtMName.TabIndex = 10;
            // 
            // txtFName
            // 
            this.txtFName.Animated = true;
            this.txtFName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.txtFName.BorderRadius = 10;
            this.txtFName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFName.DefaultText = "";
            this.txtFName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFName.Font = new System.Drawing.Font("Nunito", 12F);
            this.txtFName.ForeColor = System.Drawing.Color.Black;
            this.txtFName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFName.Location = new System.Drawing.Point(269, 256);
            this.txtFName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFName.Name = "txtFName";
            this.txtFName.PlaceholderText = "Enter FirstName";
            this.txtFName.SelectedText = "";
            this.txtFName.Size = new System.Drawing.Size(320, 41);
            this.txtFName.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Inter", 13F);
            this.label6.Location = new System.Drawing.Point(76, 706);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 47);
            this.label6.TabIndex = 8;
            this.label6.Text = "Role";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Inter", 13F);
            this.label5.Location = new System.Drawing.Point(76, 482);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 41);
            this.label5.TabIndex = 7;
            this.label5.Text = "Username";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Inter", 13F);
            this.label4.Location = new System.Drawing.Point(76, 358);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 41);
            this.label4.TabIndex = 6;
            this.label4.Text = "Last Name";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Inter", 13F);
            this.label3.Location = new System.Drawing.Point(76, 307);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 41);
            this.label3.TabIndex = 5;
            this.label3.Text = "Middle Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Inter", 13F);
            this.label2.Location = new System.Drawing.Point(76, 256);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 41);
            this.label2.TabIndex = 4;
            this.label2.Text = "First Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblID
            // 
            this.lblID.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(277, 153);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(244, 41);
            this.lblID.TabIndex = 3;
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Inter", 13F);
            this.label1.Location = new System.Drawing.Point(76, 152);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 41);
            this.label1.TabIndex = 2;
            this.label1.Text = "User ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.guna2Panel1.Controls.Add(this.lblTitle);
            this.guna2Panel1.Location = new System.Drawing.Point(0, -1);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1000, 120);
            this.guna2Panel1.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.lblTitle.Font = new System.Drawing.Font("Inter", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.lblTitle.Location = new System.Drawing.Point(2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(998, 120);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Create User";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 30;
            this.guna2Elipse1.TargetControl = this;
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
            this.btnChoose.Location = new System.Drawing.Point(728, 517);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.ShadowDecoration.BorderRadius = 8;
            this.btnChoose.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnChoose.Size = new System.Drawing.Size(180, 50);
            this.btnChoose.TabIndex = 23;
            this.btnChoose.Text = "Choose Photo";
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // picProfileImage
            // 
            this.picProfileImage.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.picProfileImage.ImageRotate = 0F;
            this.picProfileImage.Location = new System.Drawing.Point(719, 269);
            this.picProfileImage.Name = "picProfileImage";
            this.picProfileImage.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.picProfileImage.Size = new System.Drawing.Size(200, 200);
            this.picProfileImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProfileImage.TabIndex = 22;
            this.picProfileImage.TabStop = false;
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
            this.btnCancel.Location = new System.Drawing.Point(269, 823);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 51);
            this.btnCancel.TabIndex = 16;
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
            this.btnSave.Image = global::Woof_Gang_Sales___Inventory.Properties.Resources.add_user;
            this.btnSave.ImageOffset = new System.Drawing.Point(-4, 0);
            this.btnSave.ImageSize = new System.Drawing.Size(25, 25);
            this.btnSave.Location = new System.Drawing.Point(81, 823);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 51);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Create";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Inter", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(76, 210);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(213, 28);
            this.label9.TabIndex = 24;
            this.label9.Text = "Personal Information";
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.Location = new System.Drawing.Point(81, 404);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(508, 10);
            this.guna2Separator1.TabIndex = 25;
            // 
            // guna2Separator2
            // 
            this.guna2Separator2.Location = new System.Drawing.Point(81, 234);
            this.guna2Separator2.Name = "guna2Separator2";
            this.guna2Separator2.Size = new System.Drawing.Size(508, 10);
            this.guna2Separator2.TabIndex = 26;
            // 
            // guna2Separator3
            // 
            this.guna2Separator3.Location = new System.Drawing.Point(81, 462);
            this.guna2Separator3.Name = "guna2Separator3";
            this.guna2Separator3.Size = new System.Drawing.Size(508, 10);
            this.guna2Separator3.TabIndex = 27;
            // 
            // guna2Separator4
            // 
            this.guna2Separator4.Location = new System.Drawing.Point(81, 672);
            this.guna2Separator4.Name = "guna2Separator4";
            this.guna2Separator4.Size = new System.Drawing.Size(508, 10);
            this.guna2Separator4.TabIndex = 28;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Inter", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(76, 437);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(211, 28);
            this.label10.TabIndex = 29;
            this.label10.Text = "Account Credentials";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Inter", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(719, 239);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(200, 27);
            this.label11.TabIndex = 30;
            this.label11.Text = "Profile Photo";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Animated = true;
            this.txtConfirmPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.txtConfirmPassword.BorderRadius = 10;
            this.txtConfirmPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtConfirmPassword.DefaultText = "";
            this.txtConfirmPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtConfirmPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtConfirmPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtConfirmPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtConfirmPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmPassword.ForeColor = System.Drawing.Color.Black;
            this.txtConfirmPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtConfirmPassword.Location = new System.Drawing.Point(269, 621);
            this.txtConfirmPassword.Margin = new System.Windows.Forms.Padding(10, 4, 4, 10);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PlaceholderText = "Enter Password";
            this.txtConfirmPassword.SelectedText = "";
            this.txtConfirmPassword.Size = new System.Drawing.Size(320, 41);
            this.txtConfirmPassword.TabIndex = 14;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Inter", 13F);
            this.label12.Location = new System.Drawing.Point(76, 621);
            this.label12.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(174, 41);
            this.label12.TabIndex = 32;
            this.label12.Text = "Confirm Password";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.DimGray;
            this.label13.Location = new System.Drawing.Point(277, 526);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(312, 22);
            this.label13.TabIndex = 33;
            this.label13.Text = "(Min 4 chars. Letters, numbers, _ , - allowed)";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.DimGray;
            this.label14.Location = new System.Drawing.Point(277, 595);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(312, 22);
            this.label14.TabIndex = 34;
            this.label14.Text = "(Min 8 chars. At least one letter and one number)";
            // 
            // FrmCreateEditUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 900);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.guna2Separator4);
            this.Controls.Add(this.guna2Separator3);
            this.Controls.Add(this.guna2Separator2);
            this.Controls.Add(this.guna2Separator1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.picProfileImage);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.toggleUserStatus);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtLName);
            this.Controls.Add(this.txtMName);
            this.Controls.Add(this.txtFName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "FrmCreateEditUser";
            this.Text = "FrmUserAdd";
            this.Load += new System.EventHandler(this.FrmCreateEdit_Load);
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picProfileImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2TextBox txtFName;
        private Guna.UI2.WinForms.Guna2TextBox txtMName;
        private Guna.UI2.WinForms.Guna2TextBox txtLName;
        private Guna.UI2.WinForms.Guna2TextBox txtUsername;
        private Guna.UI2.WinForms.Guna2ComboBox cmbRole;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2TextBox txtPassword;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2ToggleSwitch toggleUserStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblStatus;
        private Guna.UI2.WinForms.Guna2CirclePictureBox picProfileImage;
        private Guna.UI2.WinForms.Guna2Button btnChoose;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.Label label10;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator4;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator3;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator2;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private Guna.UI2.WinForms.Guna2TextBox txtConfirmPassword;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
    }
}