using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woof_Gang_Sales___Inventory.Models
{
    public class SalesDetail
    {
        public int DetailID { get; set; }
        public int SaleID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public bool IsActive { get; set; } = true;

        // Note: Your schema has UnitPrice as a *computed* column,
        // so we only need to save ProductID, Quantity, and Subtotal.
        // The database will calculate the UnitPrice for us.
    }
}
