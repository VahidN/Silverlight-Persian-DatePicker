using System.Globalization;
using System;

namespace SilverlightPersianDatePicker.DateHelper
{
    /// <summary>
    /// Persian Date Helper class
    /// </summary>
    /// <author>
    ///   <name>Vahid Nasiri</name>
    ///   <email>vahid_nasiri@yahoo.com</email>
    /// </author>    
    public static class PDateHelper
    {
        #region Methods

        // Public Methods

        public static DateTime? DateTimeTryParse(this object data)
        {
            if (data == null)
                return null;

            if (data.GetType().Equals(typeof(DateTime)))
                return (DateTime)data;

            DateTime result;
            if (DateTime.TryParse((string)data, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return result;

            return null;
        }

        /// <summary>
        /// Finds 1st day of the given year and month.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="monthIndex"></param>
        /// <returns></returns>
        public static int Find1StDayOfMonth(int year, int monthIndex)
        {
            int outYear, outMonth, outDay, dayWeek = 1;
            HijriToGregorian(year, monthIndex, 1, out outYear, out outMonth, out outDay);

            var res = new DateTime(outYear, outMonth, outDay);

            switch (res.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    dayWeek = 0;
                    break;

                case DayOfWeek.Sunday:
                    dayWeek = 1;
                    break;

                case DayOfWeek.Monday:
                    dayWeek = 2;
                    break;

                case DayOfWeek.Tuesday:
                    dayWeek = 3;
                    break;

                case DayOfWeek.Wednesday:
                    dayWeek = 4;
                    break;

                case DayOfWeek.Thursday:
                    dayWeek = 5;
                    break;

                case DayOfWeek.Friday:
                    dayWeek = 6;
                    break;
            }

            return dayWeek;
        }

        /// <summary>
        /// Converts Gregorian date To Hijri date.
        /// </summary>
        /// <param name="inYear"></param>
        /// <param name="inMonth"></param>
        /// <param name="inDay"></param>
        /// <param name="outYear"></param>
        /// <param name="outMonth"></param>
        /// <param name="outDay"></param>
        /// <returns></returns>
        public static bool GregorianToHijri(int inYear, int inMonth, int inDay,
                                            out int outYear, out int outMonth, out int outDay)
        {
            try
            {
                var ym = inYear;
                var mm = inMonth;
                var dm = inDay;

                var sss = new PersianCalendar();
                outYear = sss.GetYear(new DateTime(ym, mm, dm, new GregorianCalendar()));
                outMonth = sss.GetMonth(new DateTime(ym, mm, dm, new GregorianCalendar()));
                outDay = sss.GetDayOfMonth(new DateTime(ym, mm, dm, new GregorianCalendar()));
                return true;
            }
            catch //invalid date
            {
                outYear = -1;
                outMonth = -1;
                outDay = -1;
                return false;
            }
        }

        /// <summary>
        /// Converts Hijri date To Gregorian date.
        /// </summary>
        /// <param name="inYear"></param>
        /// <param name="inMonth"></param>
        /// <param name="inDay"></param>
        /// <param name="outYear"></param>
        /// <param name="outMonth"></param>
        /// <param name="outDay"></param>
        /// <returns></returns>
        public static bool HijriToGregorian(
                    int inYear, int inMonth, int inDay,
                    out int outYear, out int outMonth, out int outDay)
        {
            try
            {
                var ys = inYear;
                var ms = inMonth;
                var ds = inDay;

                var sss = new GregorianCalendar();
                outYear = sss.GetYear(new DateTime(ys, ms, ds, new PersianCalendar()));
                outMonth = sss.GetMonth(new DateTime(ys, ms, ds, new PersianCalendar()));
                outDay = sss.GetDayOfMonth(new DateTime(ys, ms, ds, new PersianCalendar()));

                return true;
            }
            catch //invalid date
            {
                outYear = -1;
                outMonth = -1;
                outDay = -1;
                return false;
            }
        }

        /// <summary>
        /// Is a given year leap?
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static bool IsLeapYear(int year)
        {
            var r = year % 33;
            return (r == 1 || r == 5 || r == 9 || r == 13 || r == 17 || r == 22 || r == 26 || r == 30);
        }

        /// <summary>
        /// Is a given date valid?
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static bool IsValid(int year, int month, int day)
        {
            if (month < 1 || month > 12) return false;
            if (day < 1) return false;
            if (month < 7 && day > 31) return false;
            if (month >= 7 && day > 30) return false;
            return month != 12 || day <= 29 || IsLeapYear(year);
        }

        /// <summary>
        /// Extracts year/month/day parts of a given Persian date string.
        /// </summary>
        /// <param name="inData">Persian date</param>
        /// <returns></returns>
        public static Tuple<int, int, int> ExtractPersianDateParts(string inData)
        {
            if (string.IsNullOrWhiteSpace(inData))
            {
                throw new InvalidOperationException("لطفا تاريخي را وارد نمائيد.");
            }

            var parts = inData.Split('/');
            if (parts.Length != 3)
            {
                throw new InvalidOperationException("تاريخ شمسي وارد شده معتبر نيست.");
            }

            int year;
            if (!int.TryParse(parts[0], out year))
            {
                throw new InvalidOperationException("سال شمسي وارد شده معتبر نيست.");
            }

            int month;
            if (!int.TryParse(parts[1], out month))
            {
                throw new InvalidOperationException("ماه شمسي وارد شده معتبر نيست.");
            }

            int day;
            if (!int.TryParse(parts[2], out day))
            {
                throw new InvalidOperationException("روز شمسي وارد شده معتبر نيست.");
            }

            if (!IsValid(year, month, day))
            {
                throw new InvalidOperationException("تاريخ شمسي وارد شده معتبر نيست.");
            }

            return new Tuple<int, int, int>(year, month, day);
        }

        #endregion Methods
    }
}
