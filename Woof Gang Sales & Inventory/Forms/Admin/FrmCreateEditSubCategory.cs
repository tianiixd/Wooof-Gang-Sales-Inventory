using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.Security;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;
// Make sure to add this using statement if it's not there
using Woof_Gang_Sales___Inventory.Helpers;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmCreateEditSubCategory : Form
    {
        public bool IsEditMode { get; set; } = false;
        private int subCategoryID = 0;

        // This is the fix: We use this variable to pass the ID
        private int categoryToSelect = 0;

        private readonly SubCategoryRepository subCategoryRepo = new SubCategoryRepository();
        private readonly CategoryRepository categoryRepo = new CategoryRepository();

        public FrmCreateEditSubCategory()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;

        }

        private void FrmCreateEditSubCategory_Load(object sender, EventArgs e)
        {
            // Step 1: ALWAYS load the dropdown first.
            LoadCategoryDropdown();

            if (!IsEditMode)
            {
                toggleSubCategoryStatus.Checked = false;
                toggleSubCategoryStatus.Enabled = false;
                lbStatus.Text = "";
            }
            else
            {
                toggleSubCategoryStatus.Enabled = true;

                // Step 2: NOW that the ComboBox is loaded,
                // set its value using the ID we saved.
                if (categoryToSelect > 0)
                {
                    cmbCategory.SelectedValue = categoryToSelect;
                }
            }
        }

        private void LoadCategoryDropdown()
        {
            // We must load ALL categories (active and inactive)
            // in case you are editing an item whose parent is archived.
            var activeCategories = categoryRepo.GetCategories("");
            var inactiveCategories = categoryRepo.GetAllInactiveCategories();

            var allCategories = activeCategories.Concat(inactiveCategories)
                                                .OrderBy(c => c.CategoryName)
                                                .ToList();

            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
            cmbCategory.DataSource = allCategories;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!VerifyInput()) return;

            SubCategory subCategory = new SubCategory
            {
                SubCategoryID = subCategoryID,
                CategoryID = (int)cmbCategory.SelectedValue,
                SubCategoryName = txtSubCategoryName.Text.Trim(),
                IsActive = toggleSubCategoryStatus.Checked
            };

            // This is needed for the "Duplicate" error message in the repo
            subCategory.CategoryName = cmbCategory.Text;

            bool success = false;

            if (subCategory.SubCategoryID == 0)
            {
                success = subCategoryRepo.CreateSubCategory(subCategory);
                if (success)
                {
                    ResetInput();
                    this.DialogResult = DialogResult.OK;
                    // this.Close(); // Closing is handled by DialogResult
                }
            }
            else
            {
                success = subCategoryRepo.UpdateSubCategory(subCategory);
                if (success)
                {
                    this.DialogResult = DialogResult.OK;
                    // this.Close(); // Closing is handled by DialogResult
                }
            }

            // If successful, the form will close because DialogResult is OK
            if (success)
            {
                this.Close();
            }
        }

        public void EditSubCategory(SubCategory subCategory)
        {
            this.lblTitle.Text = "Edit Subcategory";
            this.lblID.Text = subCategory.SubCategoryID.ToString();
            this.txtSubCategoryName.Text = subCategory.SubCategoryName ?? "";
            this.btnSave.Text = "Save";
            this.btnSave.Image = Properties.Resources.edit2;
            // --- THIS IS THE FIX ---
            // DO NOT load the ComboBox here.
            // DO NOT set the SelectedValue here.
            // Just save the ID so the _Load event can use it.
            this.categoryToSelect = subCategory.CategoryID;
            // --- END OF FIX ---

            toggleSubCategoryStatus.Checked = subCategory.IsActive;
            lbStatus.Text = subCategory.IsActive ? "Activated" : "Deactivated";
            lbStatus.ForeColor = subCategory.IsActive ? Color.Green : Color.Red;

            this.subCategoryID = subCategory.SubCategoryID;
        }

        private bool VerifyInput()
        {
            if (string.IsNullOrWhiteSpace(txtSubCategoryName.Text))
            {
                DialogHelper.ShowCustomDialog("Missing Information",
                    "Please enter a subcategory name.", "warning");
                return false;
            }

            if (cmbCategory.SelectedIndex < 0)
            {
                DialogHelper.ShowCustomDialog("Missing Information",
                    "Please select a category.", "warning");
                return false;
            }

            return true;
        }

        private void ResetInput()
        {
            this.txtSubCategoryName.Text = string.Empty;
            this.cmbCategory.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void toggleSubCategoryStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleSubCategoryStatus.Checked)
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

