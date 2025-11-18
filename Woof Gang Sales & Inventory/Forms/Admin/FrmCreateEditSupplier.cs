using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
// Added for Regex validation
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;
// Make sure to add this using statement
using Woof_Gang_Sales___Inventory.Helpers;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    // Assumes your form is named FrmCreateEditSupplier
    public partial class FrmCreateEditSupplier : Form
    {
        public bool IsEditMode { get; set; } = false;
        private int supplierID = 0;

        // This is the repository we will use
        private SupplierRepository supplierRepo = new SupplierRepository();

        public FrmCreateEditSupplier()
        {
            InitializeComponent();
            // Set default dialog result
            this.DialogResult = DialogResult.Cancel;
        }

        private void FrmCreateEditSupplier_Load(object sender, EventArgs e)
        {
            if (!IsEditMode)
            {
                // In Create Mode
                toggleSupplierStatus.Checked = false;
                toggleSupplierStatus.Enabled = false;
                lbStatus.Text = "";
            }
            else
            {
                // In Edit Mode
                toggleSupplierStatus.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Use the updated verifyInput method
            if (!verifyInput()) return;

            // Build Supplier Object from the form's controls
            Supplier supplier = new Supplier
            {
                SupplierID = supplierID,
                SupplierName = txtSupplierName.Text.Trim(),
                ContactPerson = txtContactPerson.Text.Trim(), // Added this
                PhoneNumber = txtPhoneNumber.Text.Trim(),     // Added this
                Email = txtEmail.Text.Trim(),                 // Added this
                Address = txtAddress.Text.Trim(),             // Added this
                IsActive = toggleSupplierStatus.Checked,
            };

            bool success = false;

            if (supplier.SupplierID == 0) // Or !IsEditMode
            {
                // CREATE MODE
                success = supplierRepo.CreateSupplier(supplier);
                if (success)
                {
                    ResetInput();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                // EDIT MODE
                success = supplierRepo.UpdateSupplier(supplier);
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

        /// <summary>
        /// Public method called by the View form to populate data for editing.
        /// </summary>
        public void EditSupplier(Supplier supplier)
        {
            this.lblTitle.Text = "Edit Supplier";
            this.lblID.Text = supplier.SupplierID.ToString();

            

            // Populate all supplier fields
            this.txtSupplierName.Text = supplier.SupplierName ?? "";
            this.txtContactPerson.Text = supplier.ContactPerson ?? "";
            this.txtPhoneNumber.Text = supplier.PhoneNumber ?? "";
            this.txtEmail.Text = supplier.Email ?? "";
            this.txtAddress.Text = supplier.Address ?? "";

            this.btnSave.Text = "Save";
            this.btnSave.Image = Properties.Resources.edit3;
            // Assumes you have an 'edit' icon in your Properties.Resources
            // this.btnSave.Image = Properties.Resources.edit2; 

            toggleSupplierStatus.Checked = supplier.IsActive;
            this.lbStatus.Text = supplier.IsActive ? "Activated" : "Deactivated";
            this.lbStatus.ForeColor = supplier.IsActive ? Color.Green : Color.Red;

            // Store the ID for the save click event
            this.supplierID = supplier.SupplierID;
        }

        /// <summary>
        /// Validates required inputs.
        /// </summary>
        public bool verifyInput()
        {
            // 1. Check required field (Supplier Name)
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                DialogHelper.ShowCustomDialog("Missing Information",
                    "Please enter all the required fields. (Supplier Name)", "warning");
                txtSupplierName.Focus();
                return false;
            }

            // 2. Check Email format (if not empty)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                // A common, simple regex for email validation
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(txtEmail.Text, emailPattern))
                {
                    DialogHelper.ShowCustomDialog("Invalid Format",
                        "Please enter a valid email address.", "warning");
                    txtEmail.Focus();
                    return false;
                }
            }

            // 3. Check Phone format (if not empty)
            if (!string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                // A flexible regex that allows digits, spaces, +, -, and ()
                // and requires at least 7 digits.
                string phonePattern = @"^[\d\+\-\(\) ]{7,20}$";
                if (!Regex.IsMatch(txtPhoneNumber.Text, phonePattern))
                {
                    DialogHelper.ShowCustomDialog("Invalid Format",
                        "Please enter a valid phone number. (e.g., +639171234567, 0917-123-4567)", "warning");
                    txtPhoneNumber.Focus();
                    return false;
                }
            }

            // All checks passed
            return true;
        }

        /// <summary>
        /// Clears all input fields.
        /// </summary>
        private void ResetInput()
        {
            this.txtSupplierName.Text = string.Empty;
            this.txtContactPerson.Text = string.Empty;
            this.txtPhoneNumber.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.txtAddress.Text = string.Empty;
        }

        // Event handler for the status toggle
        private void toggleSupplierStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleSupplierStatus.Checked)
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

