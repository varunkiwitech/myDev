using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Kauffman.Utility
{
    public class Validator
    {

        public static bool IsEmailValid(string email)
        {
            Regex ValidEmailRegex = Util.CreateValidEmailRegex();
            bool isValid = ValidEmailRegex.IsMatch(email);
            return isValid;
        }
    }
}
