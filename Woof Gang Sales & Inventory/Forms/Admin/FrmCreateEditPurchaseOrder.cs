using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmCreateEditPurchaseOrder : Form
    {
        public bool IsEditMode { get; set; } = false;

        public FrmCreateEditPurchaseOrder()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        
    }
}
