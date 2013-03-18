using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WinWam6.Inspection.PCS;

namespace WinWam6
{



    [ValueConversion(typeof(string), typeof(Image))]
    public class InspectionTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string inspectionType = (string)value;

            switch (inspectionType)
            {
                case "P":
                    {
                        return new BitmapImage(new Uri("pack://application:,,,/WW6-WPF;component/Images/16/emblem-package.png"));
                    }
                case "D":
                    {
                        return new BitmapImage(new Uri("pack://application:,,,/WW6-WPF;component/Images/16/justice-balance-icon-16.png"));
                    }
                case "U":
                    {
                        return new BitmapImage(new Uri("pack://application:,,,/WW6-WPF;component/Images/16/barcode-icon-16.png"));
                    }
                case "Q":
                    {
                        return new BitmapImage(new Uri("pack://application:,,,/WW6-WPF;component/Images/16/kate-4.png"));
                    }
                case "R":
                    {
                        return new BitmapImage(new Uri("pack://application:,,,/WW6-WPF;component/Images/16/kate-4.png"));
                    }
                default:
                    {
                        return new BitmapImage(new Uri("pack://application:,,,/WW6-WPF;component/Images/16/emblem-brackets.png"));
                    }
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";

        }
    }

    public class PCSPackTypeToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            string checkValue = value.ToString();
            string targetValue = parameter.ToString();
            return checkValue.Equals(targetValue,
                     StringComparison.InvariantCultureIgnoreCase);
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return null;

            bool useValue = (bool)value;
            string targetValue = parameter.ToString();
            if (useValue)
                return Enum.Parse(targetType, targetValue);

            return null;
        }
    } 

    public class EnumMatchToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            string checkValue = value.ToString();
            string targetValue = parameter.ToString();
            return checkValue.Equals(targetValue,
                     StringComparison.InvariantCultureIgnoreCase);
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return null;

            bool useValue = (bool)value;
            string targetValue = parameter.ToString();
            if (useValue)
                return Enum.Parse(targetType, targetValue);

            return null;
        }
    } 
}
