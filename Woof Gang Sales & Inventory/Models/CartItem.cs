using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woof_Gang_Sales___Inventory.Models
{
    public class CartItem
    {
        public int ProductID { get; set; }

        public string ProductBrand { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        // This is a "calculated property"
        public decimal Subtotal
        {
            get { return UnitPrice * Quantity; }
        }

    }
}
