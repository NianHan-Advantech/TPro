using System;

namespace TPro.Common.PinYin
{
    public class PinYinHelper
    {
        public static PinYinHelper Instance
        { get { return new PinYinHelper(); } }

        public static string ConvertToPinYin(string simplifiedchinese)
        {
            return simplifiedchinese.ToPinYin();
        }
    }
}