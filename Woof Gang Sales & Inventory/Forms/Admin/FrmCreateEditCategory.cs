using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmCreateEditCategory : Form
    {
        public bool IsEditMode { get; set; } = false;
        private int categoryID = 0;

        private CategoryRepository categoryRepo = new CategoryRepository();
        
        public FrmCreateEditCategory()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        private void FrmCreateEditCategory_Load(object sender, EventArgs e)
        {
            if (!IsEditMode)
            {
                toggleCategoryStatus.Checked = false;
                toggleCategoryStatus.Enabled = false;
                lbStatus.Text = "";
            }
            else
            {
                toggleCategoryStatus.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!verifyInput(txtCategoryName.Text)) return;

                // Build Category Object Galing sa Model which is the Category Class
                Category category = new Category
                {
                    CategoryID = categoryID,
                    CategoryName = txtCategoryName.Text.Trim(),
                    IsActive = toggleCategoryStatus.Checked,
                };

            
            bool success = false;

            if (category.CategoryID == 0)
            {
                success = categoryRepo.CreateCategory(category);
                if (success)
                {
                    ResetInput();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                success = categoryRepo.UpdateCategory(category);
                if (success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void EditCategory(Category category)
        {
            this.lblTitle.Text = "Edit Category";
            this.lblID.Text = category.CategoryID.ToString();
            this.txtCategoryName.Text = category.CategoryName ?? "";

            this.btnSave.Text = "Save";
            this.btnSave.Image = Properties.Resources.edit2;

            toggleCategoryStatus.Checked = category.IsActive;
            this.lbStatus.Text = category.IsActive ? "Activated" : "Deactivated";
            this.lbStatus.ForeColor = category.IsActive ? Color.Green : Color.Red;

            this.categoryID = category.CategoryID;
        }

        public bool verifyInput(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                DialogHelper.ShowCustomDialog("Missing Information",
                    "Please enter all the required fields.", "warning");
                return false;
            }
            return true;
        }

        private void ResetInput()
        {
            this.txtCategoryName.Text = string.Empty;
        }

        private void toggleCategoryStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleCategoryStatus.Checked)
            {
                lbStatus.Text = "Activated";
                lbStatus.ForeColor = Color.Green;
            }
            else
            {
                lbStatus.Text = "Deactivated";
                lbStatus.ForeColor = Color.Red;
            }
        }
    }
}
