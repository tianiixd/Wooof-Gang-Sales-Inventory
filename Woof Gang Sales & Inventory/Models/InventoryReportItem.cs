using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woof_Gang_Sales___Inventory.Models
{
    public class InventoryReportItem
    {
        public string ProductID { get; set; } // e.g. WG-12345
        public string ItemName { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }

        public int CurrentStock { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalAssetValue { get; set; } // Stock * Cost

        // The Special Analysis Columns
        public int SoldLast30Days { get; set; }
        public string StockMovement { get; set; } // "Fast", "Slow", "Non-Moving"
        public string Status { get; set; } // "Sufficient", "Low Stock", "Out of Stock"
    }
}
