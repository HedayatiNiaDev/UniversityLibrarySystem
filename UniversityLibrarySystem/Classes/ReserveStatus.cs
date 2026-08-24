using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes
{
    public static class ReserveStatus
    {
        public const int Cancel = 0;
        public const int TempReservation = 1;
        public const int Reservation = 2;
        public const int Fine = 3;
        public const int Delivered = 4;

        public static string badgeStatus(int? Reserve)
        {
            switch (Reserve)
            {
                case 0:
                    return @"<span class='badge bg-label-dark me-1'>حذف شده</span>";
                case 1:
                    return @"<span class='badge bg-label-primary me-1'>رزرو موقت</span>";
                case 2:
                    return @"<span class='badge bg-label-warning me-1'>امانت داده شده</span>";
                case 3:
                    return @"<span class='badge bg-label-danger me-1'>درحال جریمه</span>";
                case 4:
                    return @"<span class='badge bg-label-success me-1'>تحویل داده شده</span>";
                default:
                    return "";
            }

        }

        public static string ReserveStatusToText(int? Reserve)
        {
            switch (Reserve)
            {
                case 0:
                    return @"حذف شده";
                case 1:
                    return @"رزرو موقت";
                case 2:
                    return @"امانت داده شده";
                case 3:
                    return @"درحال جریمه";
                case 4:
                    return @"تحویل داده شده";
                default:
                    return "نامشخص";
            }

        }
    }
}