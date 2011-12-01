using System;
using System.Linq;
using System.Globalization;

namespace WpfPersianDatePicker.DateHelper
{
    public static class ResilientDate
    {
        #region Fields (1)

        static readonly string[] Separators = new[] { ".", "/", "-" };

        #endregion Fields

        #region Methods (9)

        // Public Methods 
        /// <summary>
        /// supported formats ۹۰/۸/۱۴ & ۱۳۹۰/۸/۱۴ & ۹۰-۸-۱۴ & ۱۳۹۰-۸-۱۴ & 8
        /// </summary>
        /// <param name="persianDate"></param>
        /// <returns></returns>
        public static string ToResilientPersianDate(this string persianDate)
        {
            if (string.IsNullOrWhiteSpace(persianDate)) return string.Empty;

            persianDate = persianDate.ToEnglishNumbers().Trim();

            var separator = Separators.FirstOrDefault(item => persianDate.Contains(item));
            if (separator == null) return string.Empty;

            var parts = persianDate.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3) return string.Empty;

            var pYear = getYear(parts[0]);
            if (!pYear.HasValue) return string.Empty;

            var pMonth = getMonth(parts[1]);
            if (!pMonth.HasValue) return string.Empty;

            var pDay = getDay(parts[2]);
            if (!pDay.HasValue) return string.Empty;

            return toString(pYear.Value, pMonth.Value, pDay.Value);
        }
        // Private Methods (7) 

        private static int? getDay(string part)
        {
            var day = part.toNumber();
            if (!day.Item1) return null;
            var pDay = day.Item2;
            if (pDay == 0 || pDay > 31) return null;
            return pDay;
        }

        private static int? getMonth(string part)
        {
            var month = part.toNumber();
            if (!month.Item1) return null;
            var pMonth = month.Item2;
            if (pMonth == 0 || pMonth > 12) return null;
            return pMonth;
        }

        private static int? getYear(string part)
        {
            var year = part.toNumber();
            if (!year.Item1) return null;
            var pYear = year.Item2;
            if (part.Length == 2) pYear += 1300;
            return pYear;
        }

        static Tuple<bool, int> toNumber(this string data)
        {
            int number;
            var result = false;
            if (int.TryParse(data, out number)) result = true;
            return new Tuple<bool, int>(result, number);
        }

        static string toString(int year, int month, int day)
        {
            return year + "/" + month + "/" + day;
        }

        #endregion Methods
    }
}
