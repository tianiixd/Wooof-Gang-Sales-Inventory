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
using Woof_Gang_Sales___Inventory.Helpers;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmCategoryView : Form
    {
        private CategoryRepository categoryRepo = new CategoryRepository();
        private string[] categoryStatus = {"Active Categories", "Archived Categories", "All Categories"};
        public FrmCategoryView()
        {
            InitializeComponent();
            dgvCategory.CellFormatting += (s, e) =>
            {
                if (dgvCategory.Columns[e.ColumnIndex].Name == "Status")
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
            ReadCategory();
            cmbFilterStatus.SelectedIndexChanged += FilterChanged;
        }
        private void FrmCategoryView_Load(object sender, EventArgs e)
        {
            foreach (var category in categoryStatus)
            {
                cmbFilterStatus.Items.Add(category);
            }
            cmbFilterStatus.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmCreateEditCategory form = new FrmCreateEditCategory();
            form.IsEditMode = false;

            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadCategory();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (dgvCategory.SelectedRows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Selection", "Please select a category first.", "warning");
                return;
            }

            var value = this.dgvCategory.SelectedRows[0].Cells[0].Value.ToString();

            if (string.IsNullOrEmpty(value)) return;

            if (!int.TryParse(value, out int categoryID))
            {
                DialogHelper.ShowCustomDialog("Invalid Selection","The selected category ID is invalid.","error");
                return;
            }

            var category = categoryRepo.GetCategoryById(categoryID);

            if (category == null)
            {
                DialogHelper.ShowCustomDialog("Not Found","The selected category no longer exists.","error");
                ReadCategory();
                return;
            }

            

            FrmCreateEditCategory form = new FrmCreateEditCategory();
            form.IsEditMode = true;
            form.EditCategory(category);

            if (form.ShowDialog() == DialogResult.OK)
            {
                ReadCategory();

            }
  
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvCategory.SelectedRows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Selection", "Please select a category first.", "warning");
                return;
            }

            var value = this.dgvCategory.SelectedRows[0].Cells[0].Value.ToString();

            if (string.IsNullOrEmpty(value)) return;

            int categoryID = int.Parse(value);


            DialogResult result = DialogHelper.ShowConfirmDialog("Archive Category", "Are you sure you want to archive this category?","warning");

            if (result == DialogResult.No) return;

            bool success = categoryRepo.DeleteCategory(categoryID);
            if (success)
                ReadCategory();

        }


        private void ReadCategory()
        {
            string searchText = txtSearch.Text.Trim();
            string selectedStatus = cmbFilterStatus.SelectedItem?.ToString() ?? "All Categories";

            List<Category> categories;

            if (selectedStatus == "Active Categories")
            {
                categories = categoryRepo.GetCategories(searchText);
            }
            else if (selectedStatus == "Archived Categories")
            {
                categories = categoryRepo.GetAllInactiveCategories();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                   
                    string searchLower = searchText.ToLower();
                    categories = categories
                        .Where(c => c.CategoryName.ToLower().Contains(searchLower))
                        .ToList();
                }
            }
            else
            {
                var active = categoryRepo.GetCategories(searchText);
                var inactive = categoryRepo.GetAllInactiveCategories();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    string searchLower = searchText.ToLower();
                    inactive = inactive
                        .Where(c => c.CategoryName.ToLower().Contains(searchLower))
                        .ToList();
                }

                categories = active.Concat(inactive).ToList();
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("CategoryID");
            dt.Columns.Add("CategoryName");
            dt.Columns.Add("Status");

            foreach (var category in categories)
            {
                var row = dt.NewRow();
                row["CategoryID"] = category.CategoryID;
                row["CategoryName"] = category.CategoryName;
                row["Status"] = category.IsActive ? "Activated" : "Archived";
                dt.Rows.Add(row);  
            }

            dgvCategory.DataSource = dt;
            DataGridViewStyler.ApplyStyle(dgvCategory, "CategoryID");

            //dgvCategory.DefaultCellStyle.SelectionForeColor = dgvCategory.DefaultCellStyle.ForeColor;

            foreach (DataGridViewRow row in dgvCategory.Rows)
            {
                string status = row.Cells["Status"].Value?.ToString() ?? "";
                if (status == "Activated")
                {
                    row.Cells["Status"].Style.ForeColor = Color.Green;
                    row.Cells["Status"].Style.Font = new Font(dgvCategory.Font, FontStyle.Bold);
                }
                else if (status == "Archived")
                {
                    row.Cells["Status"].Style.ForeColor = Color.Red;
                    row.Cells["Status"].Style.Font = new Font(dgvCategory.Font, FontStyle.Bold);
                }
            }

        }

        private void FilterChanged(object sender, EventArgs e)
        {
            ReadCategory();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ReadCategory();
        }
    }
}
