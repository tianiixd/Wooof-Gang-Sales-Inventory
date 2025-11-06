using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woof_Gang_Sales___Inventory.Models
{
    public class Discount
    {
        public int DiscountID { get; set; }
        public string DiscountName { get; set; } = string.Empty;

        // "Percentage" or "Fixed"
        public string DiscountType { get; set; } = string.Empty;

        // e.g., 20.00 (for 20%) or 50.00 (for ₱50)
        public decimal Value { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
