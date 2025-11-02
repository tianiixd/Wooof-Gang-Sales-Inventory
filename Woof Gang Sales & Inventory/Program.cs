using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Admin;
using Woof_Gang_Sales___Inventory.Forms.Admin;

namespace Woof_Gang_Sales___Inventory
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run( new frmLogin());
        }
    }
}
