using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TPro.Common.Extentions
{
    public static class StringExtention
    {
        public static bool IsUrlStr(this string str)
        {
            return true;
        }

        public static bool IsEmailAddress(this string str)
        {
            return new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$").IsMatch(str);
        }

        public static bool IsEmailAddresses(this List<string> strs)
        {
            return strs.All(e => e.IsEmailAddress());
        }
        public static bool IsEmailAddresses(this List<string> strs,out string message)
        {
            message = "";
            foreach (string str in strs)
            {
                if (!str.IsEmailAddress())
                {
                    message = $"{str} is not the correct email address.";
                    return false;
                }
            }
            return true;
        }
    }
}