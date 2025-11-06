using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woof_Gang_Sales___Inventory.Models;

namespace Woof_Gang_Sales___Inventory.Util
{
    public static class SessionManager
    {
        public static User? CurrentUser { get; set; }
    }
}
