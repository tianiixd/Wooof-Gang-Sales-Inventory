using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woof_Gang_Sales___Inventory.Models
{
    public class PurchaseOrderDetail
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
    }
}
