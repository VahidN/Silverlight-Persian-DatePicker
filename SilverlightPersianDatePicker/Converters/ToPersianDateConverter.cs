using System;
using System.Globalization;
using System.Windows.Data;
using SilverlightPersianDatePicker.DateHelper;

namespace SilverlightPersianDatePicker.Converters
{
    /// <summary>
    /// IValueConverter class for converting Gregorian Date to Persian Date.
    /// </summary>
    /// <author>
    ///   <name>Vahid Nasiri</name>
    ///   <email>vahid_nasiri@yahoo.com</email>
    /// </author>    
    public class ToPersianDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            DateTime date;
            if (!DateTime.TryParse(value.ToString(), out date)) return null;
            int year, month, day;
            if (PDateHelper.GregorianToHijri(
                date.Year,
                date.Month,
                date.Day,
                out year, out month, out day))
            {
                return string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", year, month.ToString("00", CultureInfo.InvariantCulture), day.ToString("00", CultureInfo.InvariantCulture));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
