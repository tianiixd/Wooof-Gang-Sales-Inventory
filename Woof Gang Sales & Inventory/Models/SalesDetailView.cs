using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woof_Gang_Sales___Inventory.Models
{
    public class SalesDetailView
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } // From Products table JOIN
        public string Brand { get; set; }       // From Products table JOIN
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
}
