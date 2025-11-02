using System;
using System.Drawing;
using System.Windows.Forms;

namespace Woof_Gang_Sales___Inventory.Helpers
{
    public static class DataGridViewStyler
    {
        public static void ApplyStyle(DataGridView dgv, string centerColumnName = "")
        {
            // Basic setup
            dgv.EnableHeadersVisualStyles = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = Color.White;

            // Grid lines — soft gray
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgv.GridColor = Color.FromArgb(220, 220, 220); // elegant gray

            // Column Header Style
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251); // soft white-blue
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(107, 114, 128); // muted gray
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersHeight = 48; // taller header
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Header selection color override
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 230, 230); // gray on select
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(100, 100, 100);

            // Cell Style
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(0,0,0);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.RowTemplate.Height = 35;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240); // light gray highlight
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.DefaultCellStyle.Padding = new Padding(10, 6, 9, 6);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.False;


            foreach (DataGridViewColumn column in dgv.Columns) 
            { 
                column.SortMode = DataGridViewColumnSortMode.NotSortable; 
            
            }

            dgv.DataBindingComplete += (s, e) => { foreach (DataGridViewColumn col in dgv.Columns) { 
            if (col.Name.Equals(centerColumnName, StringComparison.OrdinalIgnoreCase)) 
                 col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; 
                    else col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft; } 
            };


            // Alternating rows
            
            

            // Row height
            dgv.RowTemplate.Height = 42;

            // Column-specific alignment
            if (!string.IsNullOrEmpty(centerColumnName) && dgv.Columns.Contains(centerColumnName))
            {
                dgv.Columns[centerColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[centerColumnName].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Optional: truncate long text visually
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                col.DefaultCellStyle.Padding = new Padding(12, 6, 12, 6);
            }
        }
    }
}