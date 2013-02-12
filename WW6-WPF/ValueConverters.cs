using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace WinWam6
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    class DateConvertor : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return ((DateTime)value).ToString("d");
            }
            else
            {
                return "";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime d;

            if (DateTime.TryParse(value.ToString(),out d))
            {
                return d;
            }
            else
            {
                return WWD.NullDate;
            }
        }
    }

}
