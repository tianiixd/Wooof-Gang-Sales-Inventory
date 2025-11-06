using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woof_Gang_Sales___Inventory.Models
{
    public class StockTransaction
    {
        public int StockTransID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public int StockBefore { get; set; }
        public int StockAfter { get; set; }
        public int ChangeQty { get; set; } // e.g., -2 for a sale
        public string TransactionType { get; set; } = string.Empty; // e.g., "Sale", "Adjustment"
        public int? ReferenceID { get; set; } // e.g., the SaleID
        public string? Remarks { get; set; }
        public DateTime TransDate { get; set; }

    }
}
