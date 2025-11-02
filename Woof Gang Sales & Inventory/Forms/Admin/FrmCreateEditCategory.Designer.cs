namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    partial class FrmCreateEditCategory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCreateEditCategory));
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.toggleCategoryStatus = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.txtCategoryName = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.guna2Panel1.Controls.Add(this.lblTitle);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(641, 120);
            this.guna2Panel1.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(47)))));
            this.lblTitle.Font = new System.Drawing.Font("Inter", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(641, 120);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Add Category";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.btnCancel.Location = new System.Drawing.Point(261, 341);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 51);
            this.btnCancel.TabIndex = 70;
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
            this.btnSave.ImageOffset = new System.Drawing.Point(-4, 0);
            this.btnSave.ImageSize = new System.Drawing.Size(30, 30);
            this.btnSave.Location = new System.Drawing.Point(73, 341);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 51);
            this.btnSave.TabIndex = 69;
            this.btnSave.Text = "Add";
            this.btnSave.TextOffset = new System.Drawing.Point(-3, 0);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(279, 300);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 22);
            this.lblStatus.TabIndex = 61;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Inter", 13F);
            this.label8.Location = new System.Drawing.Point(71, 275);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(164, 47);
            this.label8.TabIndex = 60;
            this.label8.Text = "Category Status:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggleCategoryStatus
            // 
            this.toggleCategoryStatus.Animated = true;
            this.toggleCategoryStatus.BackColor = System.Drawing.Color.Transparent;
            this.toggleCategoryStatus.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.toggleCategoryStatus.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.toggleCategoryStatus.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleCategoryStatus.CheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleCategoryStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toggleCategoryStatus.Location = new System.Drawing.Point(237, 290);
            this.toggleCategoryStatus.Name = "toggleCategoryStatus";
            this.toggleCategoryStatus.Size = new System.Drawing.Size(35, 20);
            this.toggleCategoryStatus.TabIndex = 59;
            this.toggleCategoryStatus.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.toggleCategoryStatus.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.toggleCategoryStatus.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleCategoryStatus.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleCategoryStatus.CheckedChanged += new System.EventHandler(this.toggleCategoryStatus_CheckedChanged);
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Animated = true;
            this.txtCategoryName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.txtCategoryName.BorderRadius = 10;
            this.txtCategoryName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCategoryName.DefaultText = "";
            this.txtCategoryName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCategoryName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCategoryName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCategoryName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCategoryName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCategoryName.Font = new System.Drawing.Font("Nunito", 12F);
            this.txtCategoryName.ForeColor = System.Drawing.Color.Black;
            this.txtCategoryName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCategoryName.Location = new System.Drawing.Point(261, 220);
            this.txtCategoryName.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.PlaceholderText = "Enter Category Name";
            this.txtCategoryName.SelectedText = "";
            this.txtCategoryName.Size = new System.Drawing.Size(320, 41);
            this.txtCategoryName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Inter", 13F);
            this.label2.Location = new System.Drawing.Point(71, 220);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 41);
            this.label2.TabIndex = 75;
            this.label2.Text = "Category Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblID
            // 
            this.lblID.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(257, 159);
            this.lblID.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(244, 41);
            this.lblID.TabIndex = 74;
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Inter", 13F);
            this.label1.Location = new System.Drawing.Point(71, 159);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 41);
            this.label1.TabIndex = 73;
            this.label1.Text = "Category ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbStatus
            // 
            this.lbStatus.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(279, 280);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(244, 39);
            this.lbStatus.TabIndex = 77;
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 30;
            this.guna2Elipse1.TargetControl = this;
            // 
            // FrmCreateEditCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(641, 433);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.txtCategoryName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.toggleCategoryStatus);
            this.Controls.Add(this.guna2Panel1);
            this.Font = new System.Drawing.Font("Inter", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmCreateEditCategory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCreateEditCategory";
            this.Load += new System.EventHandler(this.FrmCreateEditCategory_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2ToggleSwitch toggleCategoryStatus;
        private Guna.UI2.WinForms.Guna2TextBox txtCategoryName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbStatus;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
    }
}