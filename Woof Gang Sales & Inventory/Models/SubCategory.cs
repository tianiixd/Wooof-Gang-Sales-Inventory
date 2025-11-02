using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Woof_Gang_Sales___Inventory.Models
{

    public class SubCategory
    {
        public int SubCategoryID { get; set; }
        public int CategoryID { get; set; }
        public string SubCategoryName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        // Optional for display
        public string CategoryName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

