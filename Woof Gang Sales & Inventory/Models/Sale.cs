using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woof_Gang_Sales___Inventory.Models
{
    public class Sale
    {
        public int SaleID { get; set; }
        public DateTime SaleDate { get; set; }
        public TimeSpan SaleTime { get; set; }
        public int CashierID { get; set; }
        public int? DiscountID { get; set; } // Nullable
        public string? CustomerName { get; set; } // Nullable
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string? PaymentRef { get; set; } // Nullable
        public bool IsActive { get; set; } = true;

        // This is a helper property, not in the database table.
        // It's used to pass the list of items from the cart to the repository.
        public List<SalesDetail> Details { get; set; } = new List<SalesDetail>();

    }
}
