using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woof_Gang_Sales___Inventory.Util
{
    public static class PasswordHelper
    {

        public static string HashPassword(string plainPassword)
        {

            return BCrypt.Net.BCrypt.HashPassword(plainPassword);

        }

        public static bool VerifyPassword(string plainPassword, string hashedPassword)
        {

            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);

        }

    }
}
