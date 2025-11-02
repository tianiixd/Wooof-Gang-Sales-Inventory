using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woof_Gang_Sales___Inventory.Database
{
  public class DBConnection
    {
        private static readonly string connectionString = "Data Source=CHRISTIAN\\SQLEXPRESS06;Initial Catalog=WooofGangDB;Integrated Security=True;Trust Server Certificate=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

    }
}
