using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woof_Gang_Sales___Inventory.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? SKU { get; set; }
        public string? Brand { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal? Weight { get; set; }
        public string? Unit { get; set; }
        public int? SupplierID { get; set; }
        public int? SubCategoryID { get; set; }
        public decimal SellingPrice { get; set; }
        public int Quantity { get; set; }
        public int? ReorderLevel { get; set; }
        public DateTime? LastSoldDate { get; set; }
        public int TotalSold { get; set; }
        public string? ImagePath { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // --- Display Properties (for JOINs) ---
        // These are not in the database table but will be
        // populated by the repository for display in the DataGridView.
        public string SupplierName { get; set; } = string.Empty;
        public string SubCategoryName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int CategoryID { get; set; } // Useful for cascading
    }
}
