using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes
{
    public static class UserStatus
    {
        public const int NeedVerify = -1;
        public const int Deactive = 0;
        public const int Active = 1;
        public const int Fine = 2;
        public const int Penalty = -2;

        public static string UserStatusToText(int? input)
        {
            switch (input)
            {
                case NeedVerify:
                    return "تایید نشده";
                case Deactive:
                    return "غیرفعال";
                case Active:
                    return "فعال";
                case Fine:
                    return "درحال جریمه";
                case Penalty:
                    return "غیرفعال(جریمه)";
                default:
                    return "نامشخص";
            }
        }
    }
}