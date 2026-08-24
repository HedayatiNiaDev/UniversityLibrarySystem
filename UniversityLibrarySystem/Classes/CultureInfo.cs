using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Classes.CultureInfo
{
    /*example
            #region CultureInfo
            var cultureInfo = new Classes.CultureInfo.Persian();
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            #endregion
     */
    public class Persian : System.Globalization.CultureInfo
    {
        private readonly Calendar calendar;
        private readonly Calendar[] optionals;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cultureName">fa-IR</param>
        /// <param name="useUserOverride">true</param>
        public Persian() : this("fa-IR", true) { }

        public Persian(string cultureName, bool useUserOverride) : base(cultureName, useUserOverride)
        {
            //Temporary Value for cal.
            calendar = base.OptionalCalendars[0];

            //populating new list of optional calendars.
            var optionalCalendars = new List<Calendar>();
            optionalCalendars.AddRange(base.OptionalCalendars);
            optionalCalendars.Insert(0, new PersianCalendar());


            Type formatType = typeof(DateTimeFormatInfo);
            Type calendarType = typeof(Calendar);


            PropertyInfo idProperty = calendarType.GetProperty("ID", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo optionalCalendarfield = formatType.GetField("optionalCalendars",
                                                                  BindingFlags.Instance | BindingFlags.NonPublic);

            //populating new list of optional calendar ids
            var newOptionalCalendarIDs = new Int32[optionalCalendars.Count];
            for (int i = 0; i < newOptionalCalendarIDs.Length; i++)
                newOptionalCalendarIDs[i] = (Int32)idProperty.GetValue(optionalCalendars[i], null);

            optionalCalendarfield.SetValue(DateTimeFormat, newOptionalCalendarIDs);

            optionals = optionalCalendars.ToArray();
            calendar = optionals[0];
            DateTimeFormat.Calendar = optionals[0];

            DateTimeFormat.MonthNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
            DateTimeFormat.MonthGenitiveNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
            DateTimeFormat.AbbreviatedMonthNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
            DateTimeFormat.AbbreviatedMonthGenitiveNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };


            DateTimeFormat.AbbreviatedDayNames = new string[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
            DateTimeFormat.ShortestDayNames = new string[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
            DateTimeFormat.DayNames = new string[] { "یکشنبه", "دوشنبه", "ﺳﻪشنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };

            DateTimeFormat.AMDesignator = "ق.ظ";//AM
            DateTimeFormat.PMDesignator = "ب.ظ";//PM


            DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            DateTimeFormat.LongDatePattern = "yyyy/MM/dd";

            DateTimeFormat.SetAllDateTimePatterns(new[] { "yyyy/MM/dd" }, 'd');
            DateTimeFormat.SetAllDateTimePatterns(new[] { "dddd, dd MMMM yyyy" }, 'D');
            DateTimeFormat.SetAllDateTimePatterns(new[] { "yyyy MMMM" }, 'y');
            DateTimeFormat.SetAllDateTimePatterns(new[] { "yyyy MMMM" }, 'Y');


        }

        public override Calendar Calendar => calendar;

        public override Calendar[] OptionalCalendars => optionals;
    }
}