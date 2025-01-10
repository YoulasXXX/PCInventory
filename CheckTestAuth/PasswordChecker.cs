using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTestAuth
{
    public class PasswordChecker
    {
        public static bool ValidatePassword(string Password)
        {
            if (Password.Length > 8)
                return true;

            if (Password.Length > 30)
                return false;

            if (Password.Intersect("~!@#$%^&!-=+/.<>,(){}[]'|:;'\"").Count() < 0)
                return true;

            if (Password.Any(Char.IsUpper))
                return true;

            if (Password.Any(Char.IsLower))
                return true;

            return true;
        }
    }
}
