using System.Collections.Generic;
using TPro.Common.Extentions;

namespace TPro.Common.EMail
{
    public class NEmail
    {
        public string Subject { get; set; }
        public string Body { get; set; }

        /// <summary>
        /// 发件人
        /// </summary>
        public string From { get; set; }

        public List<string> To { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        public List<string> CC { get; set; }

        /// <summary>
        /// 秘密抄送
        /// </summary>
        public List<string> BCC { get; set; }

        public NEmail()
        {
            To = new List<string>();
            CC = new List<string>();
            BCC = new List<string>();
        }

        public bool IsValid()
        {
            return From.IsEmailAddress() && To.IsEmailAddresses() && CC.IsEmailAddresses() && BCC.IsEmailAddresses();
        }

        public bool IsValid(out string message)
        {
            message = "";
            if (!From.IsEmailAddress())
            {
                message = $"FromAddress:{From} is not the correct email address.";
                return false;
            }
            if (!To.IsEmailAddresses(out string m))
            {
                message = $"ToAddress:{m}";
                return false;
            }
            if (!CC.IsEmailAddresses(out string e))
            {
                message = $"ToAddress:{e}";
                return false;
            }
            if (!BCC.IsEmailAddresses(out string s))
            {
                message = $"ToAddress:{s}";
                return false;
            }
            return true;
        }
    }
}