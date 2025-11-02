using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Helpers;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmSubCategoryView : Form
    {
        private readonly SubCategoryRepository subCategoryRepo = new SubCategoryRepository();
        private string[] subCategoryStatus = { "Active Subcategories", "Archived Subcategories", "All Subcategories" };

        public FrmSubCategoryView()
        {
            InitializeComponent();

            // ✅ Apply same visual style logic as FrmCategoryView
            dgvSubCategory.CellFormatting += (s, e) =>
            {
                if (dgvSubCategory.Columns[e.ColumnIndex].Name == "Status")
                {
                    string value = e.Value?.ToString() ?? "";

                    if (value.Equals("Activated", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.SelectionForeColor = Color.Green;
                        e.CellStyle.Font = new Font("Segoe UI", 10F);
                    }
                    else if (value.Equals("Archived", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.SelectionForeColor = Color.Red;
                        e.CellStyle.Font = new Font("Segoe UI", 10F);
                    }
                }
            };

            ReadSubCategories();
            cmbFilterStatus.SelectedIndexChanged += FilterChanged;
        }

        private void FrmSubCategoryView_Load(object sender, EventArgs e)
        {
            foreach (var status in subCategoryStatus)
                cmbFilterStatus.Items.Add(status);

            cmbFilterStatus.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmCreateEditSubCategory form = new FrmCreateEditSubCategory();
            form.IsEditMode = false;

            if (form.ShowDialog() == DialogResult.OK)
                ReadSubCategories();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSubCategory.SelectedRows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Selection", "Please select a subcategory first.", "warning");
                return;
            }

            var value = dgvSubCategory.SelectedRows[0].Cells["SubCategoryID"].Value?.ToString();
            if (string.IsNullOrEmpty(value)) return;

            if (!int.TryParse(value, out int subCategoryID))
            {
                DialogHelper.ShowCustomDialog("Invalid Selection", "The selected subcategory ID is invalid.", "error");
                return;
            }

            var subCategory = subCategoryRepo.GetSubCategoryById(subCategoryID);
            if (subCategory == null)
            {
                DialogHelper.ShowCustomDialog("Not Found", "The selected subcategory no longer exists.", "error");
                ReadSubCategories();
                return;
            }

            FrmCreateEditSubCategory form = new FrmCreateEditSubCategory
            {
                IsEditMode = true
            };
            form.EditSubCategory(subCategory);

            if (form.ShowDialog() == DialogResult.OK)
                ReadSubCategories();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSubCategory.SelectedRows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Selection", "Please select a subcategory first.", "warning");
                return;
            }

            var value = dgvSubCategory.SelectedRows[0].Cells["SubCategoryID"].Value?.ToString();
            if (string.IsNullOrEmpty(value)) return;

            int subCategoryID = int.Parse(value);

            DialogResult result = DialogHelper.ShowConfirmDialog("Archive Subcategory", "Are you sure you want to archive this subcategory?", "warning");
            if (result == DialogResult.No) return;

            bool success = subCategoryRepo.DeleteSubCategory(subCategoryID);
            if (success)
                ReadSubCategories();
        }

        private void ReadSubCategories()
        {
            string searchText = txtSearch.Text.Trim();
            string selectedStatus = cmbFilterStatus.SelectedItem?.ToString() ?? "All Subcategories";

            List<SubCategory> subCategories;

            if (selectedStatus == "Active Subcategories")
            {
                subCategories = subCategoryRepo.GetSubCategories(searchText);
            }
            else if (selectedStatus == "Archived Subcategories")
            {
                subCategories = subCategoryRepo.GetAllInactiveSubCategories();

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    string searchLower = searchText.ToLower();
                    subCategories = subCategories
                        .Where(sc => sc.SubCategoryName.ToLower().Contains(searchLower)
                                  || sc.CategoryName.ToLower().Contains(searchLower))
                        .ToList();
                }
            }
            else
            {
                var active = subCategoryRepo.GetSubCategories(searchText);
                var inactive = subCategoryRepo.GetAllInactiveSubCategories();

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    string searchLower = searchText.ToLower();
                    inactive = inactive
                        .Where(sc => sc.SubCategoryName.ToLower().Contains(searchLower)
                                  || sc.CategoryName.ToLower().Contains(searchLower))
                        .ToList();
                }

                subCategories = active.Concat(inactive).ToList();
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("SubCategoryID");
            dt.Columns.Add("CategoryID");
            dt.Columns.Add("SubCategoryName");
            dt.Columns.Add("CategoryName");
            dt.Columns.Add("Status");

            foreach (var sc in subCategories)
            {
                var row = dt.NewRow();
                row["SubCategoryID"] = sc.SubCategoryID;
                row["CategoryID"] = sc.CategoryID;
                row["SubCategoryName"] = sc.SubCategoryName;
                row["CategoryName"] = sc.CategoryName;
                row["Status"] = sc.IsActive ? "Activated" : "Archived";
                dt.Rows.Add(row);
            }
            
            dgvSubCategory.DataSource = dt;
            dgvSubCategory.Columns["CategoryID"].Visible = false;
            DataGridViewStyler.ApplyStyle(dgvSubCategory, "SubCategoryID");

            foreach (DataGridViewRow row in dgvSubCategory.Rows)
            {
                string status = row.Cells["Status"].Value?.ToString() ?? "";
                if (status == "Activated")
                {
                    row.Cells["Status"].Style.ForeColor = Color.Green;
                    row.Cells["Status"].Style.Font = new Font(dgvSubCategory.Font, FontStyle.Bold);
                }
                else if (status == "Archived")
                {
                    row.Cells["Status"].Style.ForeColor = Color.Red;
                    row.Cells["Status"].Style.Font = new Font(dgvSubCategory.Font, FontStyle.Bold);
                }
            }
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            ReadSubCategories();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ReadSubCategories();
        }
    }
}
