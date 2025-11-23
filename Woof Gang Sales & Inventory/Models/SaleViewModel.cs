using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woof_Gang_Sales___Inventory.Models
{
    public class SaleViewModel
    {
        public int SaleID { get; set; }
        public DateTime SaleDate { get; set; }
        public TimeSpan SaleTime { get; set; } // To show "2:30 PM"
        public string CashierName { get; set; } // From Users table JOIN
        public string CustomerName { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; } // Calculated or from DB
    }
}
