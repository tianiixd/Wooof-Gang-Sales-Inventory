using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Woof_Gang_Sales___Inventory.Database
{
  public class DBConnection
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                return conn;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 59 || ex.Number == 18456)
                {
                    MessageBox.Show("Login failed. Please check your username and password.",
                                    "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Connection error: " + ex.Message,
                                    "Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return null;
            }
        }


    }
}
