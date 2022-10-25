using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Helpers
{
    public static class Errors
    {
        public static Dictionary<int, string> errors = new Dictionary<int, string>();

        static Errors()
        {
            errors.Add(200, "Sucsess");
            errors.Add(400, "Error");
            errors.Add(401, "Password do not match");
            errors.Add(402, "Wrong password try again");
        }
    }
}
