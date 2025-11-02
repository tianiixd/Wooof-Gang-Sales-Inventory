using Microsoft.Data.SqlClient;
using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Admin;
using Woof_Gang_Sales___Inventory.Database;

namespace Woof_Gang_Sales___Inventory.Util
{
    public class DataHelper
    {
        // Simpler version - just pass the search text directly
        public static void LoadData(string query, DataGridView gv, ListBox lb, string searchText = "")
        {
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.Text;

                    // ALWAYS add the parameter, even if empty
                    cmd.Parameters.AddWithValue("@search", "%" + searchText + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gv.Columns.Clear();

                    for (int i = 0; i < lb.Items.Count; i++)
                    {
                        string colName = lb.Items[i].ToString();
                        DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                        col.Name = colName;
                        col.DataPropertyName = dt.Columns[i].ToString();
                        col.HeaderText = colName;
                        gv.Columns.Add(col);
                    }

                    gv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Erorr Occured!", ex.Message, "error");
            }
        }

        public static void BlurBackground(Form modal, Form owner)
        {
            Form background = new Form();
            try
            {
                // Configure the blur form
                background.StartPosition = FormStartPosition.Manual;
                background.FormBorderStyle = FormBorderStyle.None;
                background.Opacity = 0.5d;
                background.BackColor = Color.Black;
                background.Size = owner.Size;
                background.Location = owner.Location;
                background.ShowInTaskbar = false;

                // Show the blur form behind the modal
                background.Show();

                // Set modal's owner and show it
                modal.Owner = background;
                modal.ShowDialog(background);
            }
            finally
            {
                background.Dispose();
            }
        }
    }
}