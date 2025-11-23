using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woof_Gang_Sales___Inventory.Models
{
    public class SalesReportItem
    {
        public string Period { get; set; } // e.g., "2025-11-23" or "November 2025"
        public int TransactionCount { get; set; }

        public decimal CashSales { get; set; }
        public decimal DigitalSales { get; set; } // GCash + Maya

        public decimal Revenue { get; set; }     // Total Sales
        public decimal CostOfGoods { get; set; } // Total Cost (From Suppliers)
        public decimal GrossProfit { get; set; } // Revenue - Cost
    }
}
