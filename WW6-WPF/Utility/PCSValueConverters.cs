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
    [ValueConversion(typeof(PCSDetail.PCSResults), typeof(Image))]
    public class PCSResultsToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((PCSDetail.PCSResults)value)
            {
                case PCSDetail.PCSResults.Pass:
                    {
                        return new BitmapImage(new Uri("pack://application:,,,/WW6-WPF;component/Images/16/emblem-default.png"));
                    }
                case PCSDetail.PCSResults.Fail:
                case PCSDetail.PCSResults.PkgFail:
                    {
                        return new BitmapImage(new Uri("pack://application:,,,/WW6-WPF;component/Images/16/emblem-unreadable-2.png"));
                    }
                case PCSDetail.PCSResults.Gray:
                case PCSDetail.PCSResults.SingleComm:
                case PCSDetail.PCSResults.Audit:
                    {
                        return new BitmapImage(new Uri("pack://application:,,,/WW6-WPF;component/Images/16/emblem-brackets.png"));
                    }
                default:
                    {
                        return new BitmapImage(new Uri("pack://application:,,,/WW6-WPF;component/Images/16/emblem-important-2.png"));
                    }
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;

        }
    }

    [ValueConversion(typeof(PCSDetail.PCSResults), typeof(String))]
    public class PCSResultsToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            switch ((PCSDetail.PCSResults)value)
            {
                case PCSDetail.PCSResults.Pass:
                    {
                        return "Green";
                    }
                case PCSDetail.PCSResults.Fail:
                case PCSDetail.PCSResults.PkgFail:
                    {
                        return "Red";
                    }
                case PCSDetail.PCSResults.Gray:
                case PCSDetail.PCSResults.SingleComm:
                case PCSDetail.PCSResults.Audit:
                    {
                        return "Yellow";
                    }
                default:
                    {
                        return "Gray";
                    }
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;

        }
    }

    [ValueConversion(typeof(PCSTest.PCSTareType), typeof(String))]
    public class TareStateToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                switch ((PCSTest.PCSTareType)value)
                {
                    case PCSTest.PCSTareType.None:
                        {
                            return "white";
                        }
                    case PCSTest.PCSTareType.Initial:
                        {
                            return "#C0C0FF";
                        }
                    case PCSTest.PCSTareType.Final:
                        {
                            return "#FFFFC0";
                        }
                    default:
                        {
                            return "White";
                        }
                }
            }
            return "white";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(PCSTest.PCSTareType), typeof(Image))]
    public class TareStateToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                switch ((PCSTest.PCSTareType)value)
                {
                    case PCSTest.PCSTareType.None:
                        {
                            return null;
                        }
                    case PCSTest.PCSTareType.Initial:
                        {
                            return
                                new BitmapImage(
                                    new Uri("pack://application:,,,/WW6-WPF;component/Images/16/emblem-package.png"));
                        }
                    case PCSTest.PCSTareType.Final:
                        {
                            return
                                new BitmapImage(
                                    new Uri("pack://application:,,,/WW6-WPF;component/Images/16/emblem-important-2.png"));
                        }
                    default:
                        {
                            return null;
                        }
                }
            }
            return "white";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(PCSTest.PCSTareType), typeof(string))]
    public class TareStateToToolTip : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                switch ((PCSTest.PCSTareType)value)
                {
                    case PCSTest.PCSTareType.None:
                        {
                            return null;
                        }
                    case PCSTest.PCSTareType.Initial:
                        {
                            return "Initial Tare Sample";
                        }
                    case PCSTest.PCSTareType.Final:
                        {
                            return "Adjusted Tare Sample";
                        }
                    default:
                        {
                            return null;
                        }
                }
            }
            return "white";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    [ValueConversion(typeof(PCSDetail.PCSResults), typeof(String))]
    public class PCSResultsToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((PCSDetail.PCSResults)value)
            {
                case PCSDetail.PCSResults.Pass:
                    {
                        return "Pass";
                    }
                case PCSDetail.PCSResults.Fail:
                    {
                        return "Fail";
                    }
                case PCSDetail.PCSResults.PkgFail:
                    {
                        return "Pkg Fail";
                    }
                case PCSDetail.PCSResults.Gray:
                    {
                        return "Pkg Fail";
                    }
                case PCSDetail.PCSResults.SingleComm:
                    {
                        return "Single Comm";
                    }
                case PCSDetail.PCSResults.Audit:
                    {
                        return "Audit";
                    }
                default:
                    {
                        return "None";
                    }
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;

        }
    }

}
