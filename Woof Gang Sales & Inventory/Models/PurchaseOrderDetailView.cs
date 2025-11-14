using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woof_Gang_Sales___Inventory.Models
{
    public class PurchaseOrderDetailView
    {
        public int PODetailID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; } // From JOIN
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Subtotal { get; set; }

    }
}
