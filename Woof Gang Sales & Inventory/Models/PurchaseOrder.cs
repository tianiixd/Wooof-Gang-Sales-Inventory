using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woof_Gang_Sales___Inventory.Models
{
    public class PurchaseOrder
    {
        public int POID { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; } // From JOIN

        // ✅ --- FIX: Changed to 'int?' to allow nulls ---
        // This now matches your database schema
        public int? OrderedBy { get; set; }

        public DateTime PODate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string Status { get; set; }

        // ✅ --- FIX: Changed to 'decimal?' to allow nulls ---
        public decimal? TotalCost { get; set; }

        // ✅ --- FIX: Changed to 'string?' to allow nulls ---
        public string? Remarks { get; set; }

        public bool IsActive { get; set; } = true;
    }
}