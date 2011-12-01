using System;
using System.Globalization;
using System.Windows.Data;
using SilverlightPersianDatePicker.DateHelper;

namespace SilverlightPersianDatePicker.Converters
{
    /// <summary>
    /// IValueConverter class for converting English numbers to Persian numbers in reports.
    /// </summary>
    /// <author>
    ///   <name>Vahid Nasiri</name>
    ///   <email>vahid_nasiri@yahoo.com</email>
    /// </author>    
    public class ToPersianNumberConverter : IValueConverter
    {       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? value : value.ToString().ToPersianNumbers();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? value : value.ToString().ToEnglishNumbers();
        }        
    }
}
