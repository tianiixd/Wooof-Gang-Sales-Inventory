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
// Make sure to add these using statements to match your sample
using Woof_Gang_Sales___Inventory.Helpers;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmSupplierView : Form
    {
        private SupplierRepository supplierRepo = new SupplierRepository();
        // Changed to use the supplier status array
        private string[] supplierStatus = { "Active Suppliers", "Archived Suppliers", "All Suppliers" };
        public FrmSupplierView()
        {
            InitializeComponent();

            // Renamed dgvCategory to dgvSupplier (assuming this is your control's name)
            dgvSupplier.CellFormatting += (s, e) =>
            {
                // Renamed dgvCategory to dgvSupplier
                if (dgvSupplier.Columns[e.ColumnIndex].Name == "Status")
                {
                    string value = e.Value?.ToString() ?? "";

                    if (value == "Yes" || value.Equals("Activated", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.SelectionForeColor = Color.Green;
                        e.CellStyle.Font = new Font("Segoe UI", 10F);
                    }
                    else if (value == "No" || value.Equals("Archived", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.SelectionForeColor = Color.Red;
                        e.CellStyle.Font = new Font("Segoe UI", 10F);
                    }
                }
            };
            // Changed to call ReadSupplier()
            ReadSupplier();
            cmbFilterStatus.SelectedIndexChanged += FilterChanged;

            // Added this based on your sample
            txtSearch.TextChanged += txtSearch_TextChanged;
        }

        // This is the FrmSupplierView_Load event, based on your sample
        private void FrmSupplierView_Load(object sender, EventArgs e)
        {
            foreach (var status in supplierStatus)
            {
                cmbFilterStatus.Items.Add(status);
            }
            cmbFilterStatus.SelectedIndex = 0;
        }


        // Added btnAdd_Click based on your sample
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Assumes you have a FrmCreateEditSupplier form
            FrmCreateEditSupplier form = new FrmCreateEditSupplier();
            form.IsEditMode = false;

            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadSupplier();
            }
        }

        // Added btnEdit_Click based on your sample
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSupplier.SelectedRows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Selection", "Please select a supplier first.", "warning");
                return;
            }

            var value = this.dgvSupplier.SelectedRows[0].Cells[0].Value.ToString();

            if (string.IsNullOrEmpty(value)) return;

            if (!int.TryParse(value, out int supplierID))
            {
                DialogHelper.ShowCustomDialog("Invalid Selection", "The selected supplier ID is invalid.", "error");
                return;
            }

            // Use the Supplier repository
            var supplier = supplierRepo.GetSupplierById(supplierID);

            if (supplier == null)
            {
                DialogHelper.ShowCustomDialog("Not Found", "The selected supplier no longer exists.", "error");
                ReadSupplier();
                return;
            }

            // Assumes you have a FrmCreateEditSupplier form
            FrmCreateEditSupplier form = new FrmCreateEditSupplier();
            form.IsEditMode = true;
            // Assumes your edit form has this method
            form.EditSupplier(supplier);

            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadSupplier();
            }
        }

        // Added btnDelete_Click based on your sample
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSupplier.SelectedRows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Selection", "Please select a supplier first.", "warning");
                return;
            }

            var value = this.dgvSupplier.SelectedRows[0].Cells[0].Value.ToString();

            if (string.IsNullOrEmpty(value)) return;

            int supplierID = int.Parse(value);

            // Updated confirmation dialog text
            DialogResult result = DialogHelper.ShowConfirmDialog("Archive Supplier", "Are you sure you want to archive this supplier?", "warning");

            if (result == DialogResult.No) return;

            // Use the Supplier repository
            bool success = supplierRepo.DeleteSupplier(supplierID);
            if (success)
                ReadSupplier();
        }


        // This is the completed ReadSupplier method, based on your ReadCategory
        private void ReadSupplier()
        {
            string searchText = txtSearch.Text.Trim();
            string selectedStatus = cmbFilterStatus.SelectedItem?.ToString() ?? "All Suppliers";

            List<Supplier> suppliers;

            if (selectedStatus == "Active Suppliers")
            {
                // This method already filters by search in the DB
                suppliers = supplierRepo.GetSuppliers(searchText);
            }
            else if (selectedStatus == "Archived Suppliers")
            {
                // This method gets all inactive, so we filter in C# (LINQ)
                suppliers = supplierRepo.GetAllInactiveSuppliers();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    string searchLower = searchText.ToLower();
                    // Match the search logic from GetActiveSuppliers (Name, Contact, Email)
                    suppliers = suppliers
                        .Where(s => (s.SupplierName != null && s.SupplierName.ToLower().Contains(searchLower)) ||
                                    (s.ContactPerson != null && s.ContactPerson.ToLower().Contains(searchLower)) ||
                                    (s.Email != null && s.Email.ToLower().Contains(searchLower)))
                        .ToList();
                }
            }
            else // "All Suppliers"
            {
                // Active list is already filtered by search (from DB)
                var active = supplierRepo.GetSuppliers(searchText);

                // Inactive list is not, so we filter in C# (LINQ)
                var inactive = supplierRepo.GetAllInactiveSuppliers();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    string searchLower = searchText.ToLower();
                    inactive = inactive
                         .Where(s => (s.SupplierName != null && s.SupplierName.ToLower().Contains(searchLower)) ||
                                     (s.ContactPerson != null && s.ContactPerson.ToLower().Contains(searchLower)) ||
                                     (s.Email != null && s.Email.ToLower().Contains(searchLower)))
                        .ToList();
                }

                suppliers = active.Concat(inactive).ToList();
            }

            DataTable dt = new DataTable();
            // Add Supplier columns to the DataTable
            dt.Columns.Add("SupplierID");
            dt.Columns.Add("SupplierName");
            dt.Columns.Add("ContactPerson");
            dt.Columns.Add("PhoneNumber");
            dt.Columns.Add("Email");
            dt.Columns.Add("Address"); // Added Address
            dt.Columns.Add("Status");

            foreach (var supplier in suppliers)
            {
                var row = dt.NewRow();
                row["SupplierID"] = supplier.SupplierID;
                row["SupplierName"] = supplier.SupplierName;
                row["ContactPerson"] = supplier.ContactPerson;
                row["PhoneNumber"] = supplier.PhoneNumber;
                row["Email"] = supplier.Email;
                row["Address"] = supplier.Address;
                row["Status"] = supplier.IsActive ? "Activated" : "Archived";
                dt.Rows.Add(row);
            }

            dgvSupplier.DataSource = dt;
            // Assumes you have a DataGridViewStyler class
            DataGridViewStyler.ApplyStyle(dgvSupplier, "SupplierID");

            // This second loop for styling also matches your sample
            foreach (DataGridViewRow row in dgvSupplier.Rows)
            {
                string status = row.Cells["Status"].Value?.ToString() ?? "";
                if (status == "Activated")
                {
                    row.Cells["Status"].Style.ForeColor = Color.Green;
                    row.Cells["Status"].Style.Font = new Font(dgvSupplier.Font, FontStyle.Bold);
                }
                else if (status == "Archived")
                {
                    row.Cells["Status"].Style.ForeColor = Color.Red;
                    row.Cells["Status"].Style.Font = new Font(dgvSupplier.Font, FontStyle.Bold);
                }
            }

            if (selectedStatus == "Active Suppliers" || selectedStatus == "Archived Suppliers")
            {
                dgvSupplier.Columns["Status"].Visible = false;
            }
            else
            {
                dgvSupplier.Columns["Status"].Visible = true;
            }
        }

        // Event handler for the filter ComboBox
        private void FilterChanged(object sender, EventArgs e)
        {
            ReadSupplier();
        }

        // Event handler for the search TextBox
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ReadSupplier();
        }
    }
}
