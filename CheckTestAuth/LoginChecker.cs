using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTestAuth
{
    public class LoginChecker
    {
        public static bool ValidateLogin(string login)
        {
            if (login.Length < 4 || login.Length > 15)
                return false;

            if (login.Intersect("~!@#$%^&*-=+/.<>,(){}[]`|:;'\"").Count() > 0)
                return false;

            return true;
        }
    }
}
